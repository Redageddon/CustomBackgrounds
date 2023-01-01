using CustomBackgrounds.Settings;

namespace CustomBackgrounds.Managers;

public class BackgroundAssetLoader : IInitializable, IDisposable
{
    private readonly PluginConfig pluginConfig;

    internal BackgroundAssetLoader(PluginConfig pluginConfig)
    {
        this.pluginConfig = pluginConfig;
        this.AssetBundle = AssetBundle.LoadFromFile(Plugin.BackgroundShaderAssetBundlePath);
    }

    public AssetBundle AssetBundle { get; }

    public int SelectedBackgroundIndex { get; set; }

    public List<CustomBackground?>? CustomBackgroundObjects { get; private set; }

    public void Dispose()
    {
        Logger.Log.Debug("Beginning background disposing.");

        if (this.CustomBackgroundObjects == null || this.CustomBackgroundObjects.Count == 0)
        {
            Logger.Log.Debug("No backgrounds to dispose, finished background disposing.");

            return;
        }

        for (int i = 0; i < this.CustomBackgroundObjects.Count; i++)
        {
            this.CustomBackgroundObjects[i]?.Dispose();
            this.CustomBackgroundObjects[i] = null;
            Logger.Log.Debug($"Disposed {this.CustomBackgroundObjects[i]?.FileName}.");
        }

        this.SelectedBackgroundIndex = 0;
        this.CustomBackgroundObjects = null;
        Logger.Log.Debug("Finished background disposing.");
    }

    public void Initialize()
    {
        if (this.CustomBackgroundObjects == null)
        {
            this.CustomBackgroundObjects = this.GetCustomBackgrounds();
            this.SelectedBackgroundIndex = this.GetConfigIndex();
        }
    }

    private int GetConfigIndex()
    {
        if (!string.IsNullOrEmpty(this.pluginConfig.SelectedBackground))
        {
            int numberOfNotes = this.CustomBackgroundObjects?.Count ?? 0;

            for (int i = 0; i < numberOfNotes; i++)
            {
                if (this.CustomBackgroundObjects?[i]?.FileName == this.pluginConfig.SelectedBackground)
                {
                    return i;
                }
            }
        }

        return 0;
    }

    private List<CustomBackground?> GetCustomBackgrounds()
    {
        Logger.Log.Debug("Beginning background loading.");

        List<CustomBackground?> customBackgrounds = new()
        {
            new CustomBackground("Default"),
        };

        foreach (string file in Directory.GetFiles(Plugin.BackgroundsDirectory))
        {
            string extension = Path.GetExtension(file);

            if (extension is ".png" or ".jpeg" or ".jpg" or ".gif")
            {
                try
                {
                    CustomBackground customBackground = new(file);
                    customBackgrounds.Add(customBackground);
                    Logger.Log.Debug($"Successfully loaded background: {file}.");
                }
                catch (Exception ex)
                {
                    Logger.Log.Warn($"Failed to Load Custom Background with path '{file}'.");
                    Logger.Log.Warn(ex);
                }
            }
        }

        Logger.Log.Debug("Finished background loading.");

        return customBackgrounds;
    }

    internal void Reload()
    {
        Logger.Log.Debug("Reloading the BackgroundAssetLoader");
        this.Dispose();
        this.Initialize();
    }
}