namespace CustomBackgrounds.Settings;

public class PluginConfig
{
    public bool Enabled { get; set; } = true;

    public bool MenuEnabled { get; set; } = true;

    public bool GameEnabled { get; set; } = true;

    public string? SelectedBackground { get; set; } = "Default";

    public bool HideMenuGround { get; set; }

    public bool HideMenuNotes { get; set; }

    public bool HideMenuPileOfNotes { get; set; }

    public bool HideGameEnvironment { get; set; }

    public bool HidePlatform { get; set; }

    public bool HideGameLighting { get; set; }

    public bool HideRings { get; set; }

    public bool HideTrackMirror { get; set; }

    public int RotationOffset { get; set; }

    public int SkyboxSize { get; set; } = 100;
}