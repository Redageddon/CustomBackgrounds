namespace CustomBackgrounds.Settings.UI.Controllers;

public class RightScreenViewController : BSMLResourceViewController
{
    [Inject] private PluginConfig pluginConfig = null!;

    [UIComponent("menu-environment")] private ToggleSetting menuEnvironmentToggle = null!;

    [UIComponent("game-environment")] private ToggleSetting gameEnvironmentToggle = null!;

    [UIComponent("track-rings")] private ToggleSetting trackRingsToggle = null!;

    public override string ResourceName => "CustomBackgrounds.Settings.UI.Views.RightScreenMenu.bsml";

    [UIValue("menuEnvironmentHidden")]
    public bool MenuEnvironmentHidden
    {
        get => this.pluginConfig.HideMenuEnvironment;
        set
        {
            this.pluginConfig.HideMenuEnvironment = value;
            this.NotifyPropertyChanged();
        }
    }

    [UIValue("gameEnvironmentHidden")]
    public bool GameEnvironmentHidden
    {
        get => this.pluginConfig.HideGameEnvironment;
        set
        {
            this.pluginConfig.HideGameEnvironment = value;
            this.NotifyPropertyChanged();
        }
    }

    [UIValue("trackRingsHidden")]
    public bool TrackRingsHidden
    {
        get => this.pluginConfig.HideRings;
        set
        {
            this.pluginConfig.HideRings = value;
            this.NotifyPropertyChanged();
        }
    }
}