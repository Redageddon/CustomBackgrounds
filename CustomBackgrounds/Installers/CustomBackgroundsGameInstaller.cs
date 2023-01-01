using CustomBackgrounds.Managers;
using CustomBackgrounds.Settings;

namespace CustomBackgrounds.Installers;

internal class CustomBackgroundsGameInstaller : Installer
{
    private readonly BackgroundAssetLoader backgroundAssetLoader;
    private readonly PluginConfig config;

    private CustomBackgroundsGameInstaller(PluginConfig config, BackgroundAssetLoader backgroundAssetLoader)
    {
        this.config = config;
        this.backgroundAssetLoader = backgroundAssetLoader;
    }

    public override void InstallBindings()
    {
        if (this.config.Enabled && this.backgroundAssetLoader.SelectedBackgroundIndex != 0)
        {
            this.Container.BindInstance(this.config).AsSingle();
        }
    }
}