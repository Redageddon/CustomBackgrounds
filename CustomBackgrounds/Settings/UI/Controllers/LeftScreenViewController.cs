using CustomBackgrounds.Managers;

namespace CustomBackgrounds.Settings.UI.Controllers;

public class LeftScreenViewController : BSMLResourceViewController
{
    [Inject] private PluginConfig pluginConfig = null!;

    [Inject] private SkyboxManager skyboxManager = null!;

    [Inject] private Managers.MenuEnvironmentManager menuEnvironmentManager = null!;

    [UIComponent("custom-backgrounds")] private ToggleSetting enableBackground = null!;

    [UIComponent("menu-enabled")] private ToggleSetting enableMenuSkybox = null!;

    [UIComponent("game-enabled")] private ToggleSetting enableGameSkybox = null!;

    [UIComponent("rotation-offset")] private SliderSetting offsetSlider = null!;

    public override string ResourceName => "CustomBackgrounds.Settings.UI.Views.LeftScreenMenu.bsml";

    [UIValue("enabled")]
    public bool Enabled
    {
        get => this.pluginConfig.Enabled;
        set
        {
            this.pluginConfig.Enabled = value;
            this.skyboxManager.EnableSkybox(value && this.pluginConfig.MenuEnabled);
            this.menuEnvironmentManager.HideAll(value);
            this.NotifyPropertyChanged();
        }
    }

    [UIValue("menuEnabled")]
    public bool MenuEnabled
    {
        get => this.pluginConfig.MenuEnabled;
        set
        {
            this.pluginConfig.MenuEnabled = value;
            this.skyboxManager.EnableSkybox(this.pluginConfig.Enabled && value);
            this.NotifyPropertyChanged();
        }
    }

    [UIValue("gameEnabled")]
    public bool GameEnabled
    {
        get => this.pluginConfig.GameEnabled;
        set
        {
            this.pluginConfig.GameEnabled = value;
            this.NotifyPropertyChanged();
        }
    }

    [UIValue("rotation")]
    public int Rotation
    {
        get => this.pluginConfig.RotationOffset;
        set
        {
            this.pluginConfig.RotationOffset = value;
            this.skyboxManager.UpdateRotation(value);
            this.NotifyPropertyChanged();
        }
    }
}