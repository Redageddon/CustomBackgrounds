namespace CustomBackgrounds.Settings;

public class PluginConfig
{
    public bool Enabled { get; set; } = true;

    public string? SelectedBackground { get; set; } = "Default";

    public bool HideMenuGround { get; set; }

    public bool HideMenuNotes { get; set; } = true;

    public bool HideMenuPileOfNotes { get; set; } = true;

    public bool HideGameEnvironment { get; set; } = true;

    public bool HidePlatform { get; set; }

    public bool HideGameLighting { get; set; } = true;

    public bool HideRings { get; set; } = true;

    public int RotationOffset { get; set; }
}