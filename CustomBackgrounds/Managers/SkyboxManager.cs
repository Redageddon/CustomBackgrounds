using CustomBackgrounds.Helpers;
using CustomBackgrounds.Settings;

namespace CustomBackgrounds.Managers;

public class SkyboxManager : IInitializable, IDisposable
{
    private readonly BackgroundAssetLoader backgroundAssetLoader;
    private readonly PluginConfig pluginConfig;
    private Material? skyboxMaterial;
    private GameObject? skyboxObject;

    internal SkyboxManager(PluginConfig pluginConfig, BackgroundAssetLoader backgroundAssetLoader)
    {
        this.pluginConfig = pluginConfig;
        this.backgroundAssetLoader = backgroundAssetLoader;
    }

    public void Initialize()
    {
        this.EnableSkybox(this.pluginConfig.Enabled);
        this.UpdateRotation(this.pluginConfig.RotationOffset);
        this.UpdateTexture(this.backgroundAssetLoader.SelectedBackgroundIndex);
    }

    public void Dispose()
    {
        this.EnableSkybox(false);
    }

    public void EnableSkybox(bool value)
    {
        if (value)
        {
            if (this.skyboxObject == null)
            {
                this.skyboxObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                this.skyboxMaterial = this.skyboxObject.GetComponent<Renderer>().material = AssetBundleHelpers.GetMaterialFromAssetBundle();
                this.skyboxObject.transform.position = Vector3.zero;
                this.skyboxObject.layer = 13;
                this.skyboxObject.name = "_SkyBGObject";
                this.skyboxObject.transform.localScale = Vector3.one * -800;
                this.UpdateRotation(this.pluginConfig.RotationOffset);
                UnityEngine.Object.DontDestroyOnLoad(this.skyboxObject);

                Logger.Log.Info("Enabled Skybox");
            }
        }
        else
        {
            if (this.skyboxObject != null)
            {
                UnityEngine.Object.Destroy(this.skyboxObject);

                Logger.Log.Info("Disabled Skybox");
            }
        }
    }

    public void UpdateRotation(int value)
    {
        if (this.skyboxObject != null)
        {
            this.skyboxObject.transform.rotation = Quaternion.Euler(0, value - 90, 180);
            Logger.Log.Info("Updated Rotation");
        }
    }

    public void UpdateTexture(int row)
    {
        if (this.skyboxObject != null && this.skyboxMaterial != null)
        {
            this.skyboxMaterial.SetTexture("_Tex", this.backgroundAssetLoader.CustomBackgroundObjects?[row]?.Texture);
            Logger.Log.Info("Updated Texture");
        }
    }
}