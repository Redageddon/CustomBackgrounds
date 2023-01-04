using CustomBackgrounds.Settings;

namespace CustomBackgrounds.Managers;

public class MenuEnvironmentManager : IInitializable
{
    private readonly PluginConfig pluginConfig;
    private GameObject? ground;
    private GameObject? notes;
    private GameObject? pileOfNotes;

    internal MenuEnvironmentManager(PluginConfig pluginConfig)
    {
        this.pluginConfig = pluginConfig;
    }

    public void Initialize()
    {
        this.ground = GameObject.Find("MenuEnvironmentManager/DefaultMenuEnvironment/BasicMenuGround");
        this.notes = GameObject.Find("MenuEnvironmentManager/DefaultMenuEnvironment/Notes");
        this.pileOfNotes = GameObject.Find("MenuEnvironmentManager/DefaultMenuEnvironment/PileOfNotes");

        this.HideAll(this.pluginConfig.Enabled);
    }

    public void HideGround(bool shouldHide) => this.ground?.SetActive(!shouldHide);

    public void HideNotes(bool shouldHide) => this.notes?.SetActive(!shouldHide);

    public void HidePileOfNotes(bool shouldHide) => this.pileOfNotes?.SetActive(!shouldHide);

    // This is so it doesn't get locked while editing the settings
    internal void HideAll(bool hideAll)
    {
        if (hideAll)
        {
            this.HideGround(this.pluginConfig.HideMenuGround);
            this.HideNotes(this.pluginConfig.HideMenuNotes);
            this.HidePileOfNotes(this.pluginConfig.HideMenuPileOfNotes);
        }
        else
        {
            this.HideGround(false);
            this.HideNotes(false);
            this.HidePileOfNotes(false);
        }
    }
}