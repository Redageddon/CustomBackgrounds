using CustomBackgrounds.Settings;

namespace CustomBackgrounds.Managers;

public class SkyboxManager
{
    private SkyboxBehavior? skyboxBehavior;
    private readonly PluginConfig pluginConfig;
    private readonly AssetBundle assetBundle;
    private readonly Material skyboxMaterial;

    public SkyboxManager(AssetBundle assetBundle, PluginConfig pluginConfig)
    {
        this.assetBundle = assetBundle;
        this.pluginConfig = pluginConfig;
        this.skyboxMaterial = assetBundle.LoadAllAssets<Material>()[0];
        assetBundle.Unload(false);
    }

    public void EnableSkybox(bool value)
    {
        if (value)
        {
            this.skyboxBehavior = new GameObject(nameof(SkyboxBehavior)).AddComponent<SkyboxBehavior>();
            this.skyboxBehavior.skyboxObject.GetComponent<Renderer>().material = this.skyboxMaterial; // may have to swap load assets into this method if it doesn't work
            UnityEngine.Object.DontDestroyOnLoad(this.skyboxBehavior);
        }
        else
        {
            UnityEngine.Object.Destroy(this.skyboxBehavior);
        }
    }

    public void UpdateRotation(int value)
    {
        if (this.skyboxBehavior != null)
        {
            this.skyboxBehavior.skyboxObject.transform.rotation = Quaternion.Euler(0, value - 90, 180);
        }
    }

    public void UpdateTexture(Texture2D? texture)
    {
        if (texture != null)
        {
            this.skyboxMaterial.SetTexture("_Tex", texture);
        }
    }
}