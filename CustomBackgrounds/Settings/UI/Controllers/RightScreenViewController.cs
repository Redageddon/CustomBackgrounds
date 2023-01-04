namespace CustomBackgrounds.Settings.UI.Controllers;

public class RightScreenViewController : BSMLResourceViewController
{
    [Inject] private PluginConfig pluginConfig = null!;

    [Inject] private Managers.MenuEnvironmentManager menuEnvironmentManager = null!;

    [UIComponent("menu-ground")] private ToggleSetting menuGroundToggle = null!;

    [UIComponent("menu-notes")] private ToggleSetting menuNotesToggle = null!;

    [UIComponent("menu-pile-of-notes")] private ToggleSetting menuPileOfNotesToggle = null!;

    [UIComponent("game-environment")] private ToggleSetting gameEnvironmentToggle = null!;

    [UIComponent("game-platform")] private ToggleSetting gamePlatformToggle = null!;

    [UIComponent("track-rings")] private ToggleSetting trackRingsToggle = null!;

    public override string ResourceName => "CustomBackgrounds.Settings.UI.Views.RightScreenMenu.bsml";

    [UIValue("menuGround")]
    public bool MenuGroundHidden
    {
        get => this.pluginConfig.HideMenuGround;
        set
        {
            this.pluginConfig.HideMenuGround = value;

            if (this.pluginConfig.Enabled)
            {
                this.menuEnvironmentManager.HideGround(value);
            }

            this.NotifyPropertyChanged();
        }
    }

    [UIValue("menuNotes")]
    public bool MenuNotesHidden
    {
        get => this.pluginConfig.HideMenuNotes;
        set
        {
            this.pluginConfig.HideMenuNotes = value;

            if (this.pluginConfig.Enabled)
            {
                this.menuEnvironmentManager.HideNotes(value);
            }

            this.NotifyPropertyChanged();
        }
    }

    [UIValue("menuPileOfNotes")]
    public bool MenuPileOfNotesHidden
    {
        get => this.pluginConfig.HideMenuPileOfNotes;
        set
        {
            this.pluginConfig.HideMenuPileOfNotes = value;

            if (this.pluginConfig.Enabled)
            {
                this.menuEnvironmentManager.HidePileOfNotes(value);
            }

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

    [UIValue("gamePlatformHidden")]
    public bool GamePlatformHidden
    {
        get => this.pluginConfig.HidePlatform;
        set
        {
            this.pluginConfig.HidePlatform = value;
            this.NotifyPropertyChanged();
        }
    }

    [UIValue("gameLightingHidden")]
    public bool GameLightingHidden
    {
        get => this.pluginConfig.HideGameLighting;
        set
        {
            this.pluginConfig.HideGameLighting = value;
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