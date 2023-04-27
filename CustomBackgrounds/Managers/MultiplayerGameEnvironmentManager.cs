using System.Linq;
using CustomBackgrounds.Settings;

namespace CustomBackgrounds.Managers;

public class MultiplayerGameEnvironmentManager : IInitializable
{
    private readonly PluginConfig pluginConfig;

    private GameObject?[] multiplayerEnvironment = Array.Empty<GameObject>();
    private GameObject?[] multiplayerPlatform = Array.Empty<GameObject>();
    private GameObject?[] multiplayerLighting = Array.Empty<GameObject>();

    internal MultiplayerGameEnvironmentManager(PluginConfig pluginConfig)
    {
        this.pluginConfig = pluginConfig;
    }

    public void Initialize()
    {
        Transform activePlayerController = FindUnityObjectsHelper.GetAllGameObjectsInLoadedScenes()
                                                                 .First(gameObject => gameObject.name is "MultiplayerLocalActivePlayerController(Clone)" or "MultiplayerDuelLocalActivePlayerController(Clone)").transform;

        this.multiplayerEnvironment = new[]
        {
            activePlayerController.Find("IsActiveObjects/Construction/ConstructionL").gameObject,
            activePlayerController.Find("IsActiveObjects/Construction/ConstructionR").gameObject,
            activePlayerController.Find("IsActiveObjects/Lasers").gameObject,
            activePlayerController.Find("IsActiveObjects/BigSmokePS").gameObject,
            activePlayerController.Find("IsActiveObjects/DustPS").gameObject,
        };

        this.multiplayerPlatform = new[]
        {
            activePlayerController.Find("IsActiveObjects/Construction/PlayersPlace").gameObject,
            activePlayerController.Find("IsActiveObjects/PlatformEnd").gameObject,
        };

        this.multiplayerLighting = new[]
        {
            activePlayerController.Find("IsActiveObjects/DirectionalLights").gameObject,
        };

        this.HideGameEnvironment(this.pluginConfig.HideGameEnvironment);
        this.HidePlatform(this.pluginConfig.HidePlatform);
        this.HideGameLighting(this.pluginConfig.HideGameLighting);
    }

    public void HideGameEnvironment(bool shouldHide)
    {
        foreach (GameObject? gameObject in this.multiplayerEnvironment)
        {
            gameObject?.SetActive(!shouldHide);
        }
    }

    public void HidePlatform(bool shouldHide)
    {
        foreach (GameObject? gameObject in this.multiplayerPlatform)
        {
            gameObject?.SetActive(!shouldHide);
        }
    }

    public void HideGameLighting(bool shouldHide)
    {
        foreach (GameObject? gameObject in this.multiplayerLighting)
        {
            gameObject?.SetActive(!shouldHide);
        }
    }
}