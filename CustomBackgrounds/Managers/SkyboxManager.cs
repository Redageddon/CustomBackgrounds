using System.Threading.Tasks;
using CustomBackgrounds.Helpers;
using CustomBackgrounds.Settings;
using UnityEngine.SceneManagement;

namespace CustomBackgrounds.Managers;

public class SkyboxManager : IInitializable, IDisposable
{
    private readonly Texture2D defaultTexture = new(0, 0);
    private readonly BackgroundAssetLoader backgroundAssetLoader;
    private readonly PluginConfig pluginConfig;
    private Material skyboxMaterial = null!;
    private GameObject? skyboxObject;

    internal SkyboxManager(PluginConfig pluginConfig, BackgroundAssetLoader backgroundAssetLoader)
    {
        this.pluginConfig = pluginConfig;
        this.backgroundAssetLoader = backgroundAssetLoader;
    }

    public async void Initialize()
    {
        this.CreateSkyboxObject();
        this.EnableSkybox(this.pluginConfig.Enabled);
        this.UpdateRotation(this.pluginConfig.RotationOffset);
        this.UpdateSize(this.pluginConfig.SkyboxSize);
        await this.UpdateTexture(this.backgroundAssetLoader.SelectedBackgroundIndex);

        SceneManager.sceneLoaded += this.SceneManagerOnSceneLoaded;     // read the method comments.
        SceneManager.sceneUnloaded += this.SceneManagerOnSceneUnloaded; // read the method comments.
    }

    public void Dispose()
    {
        if (this.skyboxObject != null)
        {
            UnityEngine.Object.Destroy(this.skyboxObject);

            Logger.Log.Debug("Disposed Skybox");
        }
    }

    public void EnableSkybox(bool value)
    {
        if (this.skyboxObject != null)
        {
            this.skyboxObject.SetActive(value);

            Logger.Log.Debug($"Skybox is enabled: {value}");
        }
    }

    public void UpdateRotation(int degrees)
    {
        if (this.skyboxObject != null)
        {
            this.skyboxObject.transform.rotation = Quaternion.Euler(0, degrees - 90, 180);

            Logger.Log.Debug($"Updated Rotation: {degrees}");
        }
    }

    public void UpdateSize(int size)
    {
        if (this.skyboxObject != null)
        {
            int scale = -(int)Math.Exp(size / 14.959d);
            this.skyboxObject.transform.localScale = Vector3.one * scale;

            Logger.Log.Debug($"Updated Skybox size: {scale}");
        }
    }

    public async Task UpdateTexture(int index)
    {
        // Fixes flash-bang problem
        if (index == 0)
        {
            this.skyboxMaterial.SetTexture("_Tex", this.defaultTexture);

            return;
        }

        if (this.skyboxObject != null)
        {
            CustomBackground? customBackground = this.backgroundAssetLoader.CustomBackgroundObjects?[index];
            this.skyboxMaterial.SetTexture("_Tex", await customBackground?.GetTextureAsync()!);

            Logger.Log.Debug($"Updated Texture: {customBackground.Name}");
        }
    }

    private void CreateSkyboxObject()
    {
        if (this.skyboxObject == null)
        {
            this.skyboxObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            this.skyboxMaterial = this.skyboxObject.GetComponent<Renderer>().material = AssetBundleHelpers.GetMaterialFromAssetBundle();
            this.skyboxObject.transform.position = Vector3.zero;
            this.skyboxObject.layer = 13;
            this.skyboxObject.name = "_SkyBGObject";
            UnityEngine.Object.DontDestroyOnLoad(this.skyboxObject);

            Logger.Log.Debug("Created Skybox");
        }
    }

    // If anyone knows how I can remove the scene load/unloading but keep the menu/game enabled functionality, please tell me.
    private void SceneManagerOnSceneLoaded(Scene scene, LoadSceneMode _)
    {
        if (!this.pluginConfig.Enabled)
        {
            return;
        }

        switch (scene.name)
        {
            case "MainMenu": this.EnableSkybox(this.pluginConfig.MenuEnabled);

                break;
            case "GameCore": this.EnableSkybox(this.pluginConfig.GameEnabled);

                break;
        }
    }

    // If anyone knows how I can remove the scene load/unloading but keep the menu/game enabled functionality, please tell me.
    private void SceneManagerOnSceneUnloaded(Scene scene)
    {
        if (!this.pluginConfig.Enabled)
        {
            return;
        }

        if (scene.name == "GameCore")
        {
            this.EnableSkybox(this.pluginConfig.MenuEnabled);
        }
    }
}