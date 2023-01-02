using CustomBackgrounds.Installers;
using CustomBackgrounds.Settings;

namespace CustomBackgrounds;

[Plugin(RuntimeOptions.DynamicInit)]
public class Plugin
{
    public static readonly string BackgroundsDirectory = Path.Combine(Environment.CurrentDirectory, "CustomBackgrounds");

    [Init]
    public Plugin(IPA.Logging.Logger logger, IPA.Config.Config config, Zenjector zenjector)
    {
        Logger.Log = logger;

        PluginConfig pluginConfig = config.Generated<PluginConfig>();
        zenjector.Install(Location.App, container => container.BindInstance(pluginConfig).AsSingle());
        zenjector.Install<CustomBackgroundsCoreInstaller>(Location.App);
        zenjector.Install<CustomBackgroundsMenuInstaller>(Location.Menu);
        zenjector.Install<CustomBackgroundsGameInstaller>(Location.Player);
    }
}