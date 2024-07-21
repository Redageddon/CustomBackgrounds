using CustomBackgrounds.Settings;
using System.Linq;

namespace CustomBackgrounds.Managers;

public class GameEnvironmentManager : IInitializable
{
    private readonly PluginConfig pluginConfig;
    private Renderer[]? environmentRenderers;
    private Renderer[]? platformRenderers;
    private Renderer[]? coreLightingRenderers;
    private Renderer[]? bloomLightingRenderers;
    private TrackLaneRingsManager[]? trackRings;
    private Renderer[]? trackMirror;

    internal GameEnvironmentManager(PluginConfig pluginConfig)
    {
        this.pluginConfig = pluginConfig;
    }

    public void Initialize()
    {
        this.environmentRenderers = GameObject.Find("Environment")?.GetComponentsInChildren<Renderer>();
        this.platformRenderers = GameObject.Find("Environment/PlayersPlace")?.GetComponentsInChildren<Renderer>();
        this.coreLightingRenderers = GameObject.Find("Environment/CoreLighting")?.GetComponentsInChildren<Renderer>();
        this.bloomLightingRenderers = GameObject.Find("BloomSkyboxQuad")?.GetComponentsInChildren<Renderer>();
        this.trackRings = UnityEngine.Object.FindObjectsOfType<TrackLaneRingsManager>();
        this.trackMirror = GameObject.Find("Environment/TrackMirror")?.GetComponentsInChildren<Renderer>();

        this.HideGameEnvironment(this.pluginConfig.HideGameEnvironment);
        this.HidePlatform(this.pluginConfig.HidePlatform);
        this.HideGameLighting(this.pluginConfig.HideGameLighting);
        this.HideRings(this.pluginConfig.HideRings);
        this.HideTrackMirror(this.pluginConfig.HideTrackMirror);
    }

    public void HideGameEnvironment(bool shouldHide)
    {
        if (this.environmentRenderers == null)
        {
            return;
        }

        foreach (Renderer renderer in this.environmentRenderers)
        {
            renderer.enabled = !shouldHide;
        }
    }

    public void HidePlatform(bool shouldHide)
    {
        if (this.platformRenderers == null)
        {
            return;
        }

        foreach (Renderer renderer in this.platformRenderers)
        {
            renderer.enabled = !shouldHide;
        }
    }

    public void HideGameLighting(bool shouldHide)
    {
        if (this.coreLightingRenderers != null)
        {
            foreach (Renderer renderer in this.coreLightingRenderers)
            {
                renderer.enabled = !shouldHide;
            }
        }

        if (this.bloomLightingRenderers != null)
        {
            foreach (Renderer renderer in this.bloomLightingRenderers)
            {
                renderer.enabled = !shouldHide;
            }
        }

        if (this.environmentRenderers != null)
        {
            foreach (Renderer renderer in this.environmentRenderers)
            {
                if (renderer.GetComponent<LightManager>() == null && (renderer.name.Contains("bloom") || renderer.name.Contains("light") || renderer.name.Contains("glow")))
                {
                    renderer.forceRenderingOff = shouldHide;
                }
            }
        }
    }

    public void HideRings(bool shouldHide)
    {
        if (this.trackRings == null)
        {
            return;
        }

        foreach (TrackLaneRingsManager trackLaneRingsManager in this.trackRings)
        {
            trackLaneRingsManager.gameObject.SetActive(!shouldHide);
        }
    }

    public void HideTrackMirror(bool shouldHide)
    {
        if (this.trackMirror == null)
        {
            return;
        }

        foreach (Renderer renderer in this.trackMirror)
        {
            renderer.enabled = !shouldHide;
        }
    }
}