namespace CustomBackgrounds.Settings.UI.Controllers;

public class RightScreenViewController : BSMLResourceViewController
{
    [Inject] private readonly PluginConfig pluginConfig = null!;
    [Inject] private readonly Managers.MenuEnvironmentManager menuEnvironmentManager = null!;

    public override string ResourceName => "CustomBackgrounds.Settings.UI.Views.RightScreenMenu.bsml";

    [UIValue(nameof(MenuGroundHidden))]
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

    [UIValue(nameof(MenuNotesHidden))]
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

    [UIValue(nameof(MenuPileOfNotesHidden))]
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

    [UIValue(nameof(GameEnvironmentHidden))]
    public bool GameEnvironmentHidden
    {
        get => this.pluginConfig.HideGameEnvironment;
        set
        {
            this.pluginConfig.HideGameEnvironment = value;
            this.NotifyPropertyChanged();
        }
    }

    [UIValue(nameof(GamePlatformHidden))]
    public bool GamePlatformHidden
    {
        get => this.pluginConfig.HidePlatform;
        set
        {
            this.pluginConfig.HidePlatform = value;
            this.NotifyPropertyChanged();
        }
    }

    [UIValue(nameof(GameLightingHidden))]
    public bool GameLightingHidden
    {
        get => this.pluginConfig.HideGameLighting;
        set
        {
            this.pluginConfig.HideGameLighting = value;
            this.NotifyPropertyChanged();
        }
    }

    [UIValue(nameof(TrackRingsHidden))]
    public bool TrackRingsHidden
    {
        get => this.pluginConfig.HideRings;
        set
        {
            this.pluginConfig.HideRings = value;
            this.NotifyPropertyChanged();
        }
    }

    [UIValue(nameof(TrackMirrorHidden))]
    public bool TrackMirrorHidden
    {
        get => this.pluginConfig.HideTrackMirror;
        set
        {
            this.pluginConfig.HideTrackMirror = value;
            this.NotifyPropertyChanged();
        }
    }
}