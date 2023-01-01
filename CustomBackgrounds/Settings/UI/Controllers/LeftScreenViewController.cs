namespace CustomBackgrounds.Settings.UI.Controllers;

public class LeftScreenViewController : BSMLResourceViewController
{
    [Inject] private PluginConfig pluginConfig = null!;

    [UIComponent("custom-backgrounds")] private ToggleSetting enableBackground = null!;

    [UIComponent("menu-previews")] private ToggleSetting previewToggle = null!;

    [UIComponent("rotation-offset")] private SliderSetting offsetSlider = null!;

    public override string ResourceName => "CustomBackgrounds.Settings.UI.Views.LeftScreenMenu.bsml";

    [UIValue("enabled")]
    public bool Enabled
    {
        get => this.pluginConfig.Enabled;
        set
        {
            this.pluginConfig.Enabled = value;
            this.NotifyPropertyChanged();
        }
    }

    [UIValue("enablePreviews")]
    public bool EnablePreviews
    {
        get => this.pluginConfig.EnablePreviews;
        set
        {
            this.pluginConfig.EnablePreviews = value;
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
            this.NotifyPropertyChanged();
        }
    }
}