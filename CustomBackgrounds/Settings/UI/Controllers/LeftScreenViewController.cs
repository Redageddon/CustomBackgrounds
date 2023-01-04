using CustomBackgrounds.Managers;

namespace CustomBackgrounds.Settings.UI.Controllers;

public class LeftScreenViewController : BSMLResourceViewController
{
    [Inject] private PluginConfig pluginConfig = null!;

    [Inject] private SkyboxManager skyboxManager = null!;

    [UIComponent("custom-backgrounds")] private ToggleSetting enableBackground = null!;

    [UIComponent("rotation-offset")] private SliderSetting offsetSlider = null!;

    public override string ResourceName => "CustomBackgrounds.Settings.UI.Views.LeftScreenMenu.bsml";

    [UIValue("enabled")]
    public bool Enabled
    {
        get => this.pluginConfig.Enabled;
        set
        {
            this.pluginConfig.Enabled = value;
            this.skyboxManager.EnableSkybox(value);
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