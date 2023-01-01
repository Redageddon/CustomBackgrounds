namespace CustomBackgrounds.Settings;

public class PluginConfig
{
    public bool Enabled { get; set; } = true;

    public bool EnablePreviews { get; set; } = true;

    public string? SelectedBackground { get; set; } = "Default";

    public bool HideMenuEnvironment { get; set; } = true;

    public bool HideGameEnvironment { get; set; } = true;

    public bool HideRings { get; set; } = true;

    public int RotationOffset { get; set; }
}