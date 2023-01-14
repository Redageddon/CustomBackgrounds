using CustomBackgrounds.Managers;

namespace CustomBackgrounds.Settings.UI.Controllers;

public class LeftScreenViewController : BSMLResourceViewController
{
    [Inject] private readonly PluginConfig pluginConfig = null!;
    [Inject] private readonly SkyboxManager skyboxManager = null!;
    [Inject] private readonly Managers.MenuEnvironmentManager menuEnvironmentManager = null!;

    public override string ResourceName => "CustomBackgrounds.Settings.UI.Views.LeftScreenMenu.bsml";

    [UIValue(nameof(Enabled))]
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

    [UIValue(nameof(MenuEnabled))]
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

    [UIValue(nameof(GameEnabled))]
    public bool GameEnabled
    {
        get => this.pluginConfig.GameEnabled;
        set
        {
            this.pluginConfig.GameEnabled = value;
            this.NotifyPropertyChanged();
        }
    }

    [UIValue(nameof(Rotation))]
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