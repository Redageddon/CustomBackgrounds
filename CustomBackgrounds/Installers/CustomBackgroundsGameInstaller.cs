using CustomBackgrounds.Settings;

namespace CustomBackgrounds.Installers;

internal class CustomBackgroundsGameInstaller : Installer
{
    private readonly PluginConfig config;

    private CustomBackgroundsGameInstaller(PluginConfig config)
    {
        this.config = config;
    }

    public override void InstallBindings()
    {
        if (this.config.Enabled)
        {
            this.Container.BindInstance(this.config).AsSingle();
        }
    }
}