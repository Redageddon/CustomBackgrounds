using CustomBackgrounds.Managers;
using CustomBackgrounds.Settings;

namespace CustomBackgrounds.Installers;

public class CustomBackgroundsMultiplayerGameInstaller : Installer
{
    private readonly PluginConfig config;

    private CustomBackgroundsMultiplayerGameInstaller(PluginConfig config)
    {
        this.config = config;
    }

    public override void InstallBindings()
    {
        if (this.config.Enabled)
        {
            this.Container.BindInterfacesAndSelfTo<MultiplayerGameEnvironmentManager>().AsSingle();
        }
    }
}