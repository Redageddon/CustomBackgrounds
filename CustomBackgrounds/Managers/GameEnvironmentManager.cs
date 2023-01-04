using CustomBackgrounds.Settings;

namespace CustomBackgrounds.Managers;

public class GameEnvironmentManager : IInitializable
{
    private readonly PluginConfig pluginConfig;
    private Renderer[] environmentRenderers = null!;
    private Renderer[] platformRenderers = null!;
    private Renderer[] lightingRenderers = null!;
    private TrackLaneRingsManager[] trackRings = null!;

    internal GameEnvironmentManager(PluginConfig pluginConfig)
    {
        this.pluginConfig = pluginConfig;
    }

    public void Initialize()
    {
        this.environmentRenderers = GameObject.Find("Environment").GetComponentsInChildren<Renderer>();
        this.platformRenderers = GameObject.Find("Environment/PlayersPlace").GetComponentsInChildren<Renderer>();
        this.lightingRenderers = GameObject.Find("Environment/CoreLighting").GetComponentsInChildren<Renderer>();
        this.trackRings = UnityEngine.Object.FindObjectsOfType<TrackLaneRingsManager>();

        this.HideGameEnvironment(this.pluginConfig.HideGameEnvironment);
        this.HidePlatform(this.pluginConfig.HidePlatform);
        this.HideGameLighting(this.pluginConfig.HideGameLighting);
        this.HideRings(this.pluginConfig.HideRings);
    }

    public void HideGameEnvironment(bool shouldHide)
    {
        foreach (Renderer renderer in this.environmentRenderers)
        {
            renderer.enabled = !shouldHide;
        }
    }

    public void HidePlatform(bool shouldHide)
    {
        foreach (Renderer renderer in this.platformRenderers)
        {
            renderer.enabled = !shouldHide;
        }
    }

    public void HideGameLighting(bool shouldHide)
    {
        foreach (Renderer renderer in this.lightingRenderers)
        {
            renderer.enabled = !shouldHide;
        }

        foreach (Renderer renderer in this.environmentRenderers)
        {
            if (renderer.GetComponent<LightManager>() == null && (renderer.name.Contains("bloom") || renderer.name.Contains("light")))
            {
                renderer.forceRenderingOff = shouldHide;
            }
        }
    }

    public void HideRings(bool shouldHide)
    {
        foreach (TrackLaneRingsManager trackLaneRingsManager in this.trackRings)
        {
            trackLaneRingsManager.gameObject.SetActive(!shouldHide);
        }
    }
}