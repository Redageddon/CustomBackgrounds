using CustomBackgrounds.Settings;

namespace CustomBackgrounds.Managers;

public class BackgroundAssetLoader : IInitializable, IDisposable
{
    private readonly PluginConfig pluginConfig;

    internal BackgroundAssetLoader(PluginConfig pluginConfig)
    {
        this.pluginConfig = pluginConfig;
    }

    public int SelectedBackgroundIndex { get; set; }

    public List<CustomBackground?>? CustomBackgroundObjects { get; private set; }

    public void Dispose()
    {
        Logger.Log.Info("Beginning background disposing.");

        if (this.CustomBackgroundObjects == null || this.CustomBackgroundObjects.Count == 0)
        {
            Logger.Log.Info("No backgrounds to dispose, finished background disposing.");

            return;
        }

        for (int i = 0; i < this.CustomBackgroundObjects.Count; i++)
        {
            string? name = this.CustomBackgroundObjects[i]?.Name;
            this.CustomBackgroundObjects[i]?.Dispose();
            this.CustomBackgroundObjects[i] = null;
            Logger.Log.Info($"Disposed {name}.");
        }

        this.SelectedBackgroundIndex = 0;
        this.CustomBackgroundObjects = null;
        Logger.Log.Info("Finished background disposing.");
    }

    public void Initialize()
    {
        if (this.CustomBackgroundObjects == null)
        {
            this.CustomBackgroundObjects = this.GetCustomBackgrounds();
            this.SelectedBackgroundIndex = this.GetConfigIndex();
        }
    }

    internal void Reload()
    {
        Logger.Log.Info("Reloading the BackgroundAssetLoader");
        this.Dispose();
        this.Initialize();
    }

    private List<CustomBackground?> GetCustomBackgrounds()
    {
        Logger.Log.Info("Beginning background loading.");

        List<CustomBackground?> customBackgrounds = new()
        {
            new CustomBackground("Default"),
        };

        Logger.Log.Info("Successfully loaded background: Default.");

        foreach (string path in Directory.GetFiles(Plugin.BackgroundsDirectory))
        {
            string name = Path.GetFileName(path);
            string extension = Path.GetExtension(path);

            if (extension is ".png" or ".jpeg" or ".jpg" or ".gif")
            {
                try
                {
                    CustomBackground customBackground = new(name);
                    customBackgrounds.Add(customBackground);
                    Logger.Log.Info($"Successfully loaded background: {name}.");
                }
                catch (Exception ex)
                {
                    Logger.Log.Warn($"Failed to Load Custom Background with path '{path}'.");
                    Logger.Log.Warn(ex);
                }
            }
        }

        Logger.Log.Info("Finished background loading.");

        return customBackgrounds;
    }

    private int GetConfigIndex()
    {
        if (!string.IsNullOrEmpty(this.pluginConfig.SelectedBackground))
        {
            int numberOfNotes = this.CustomBackgroundObjects?.Count ?? 0;

            for (int i = 0; i < numberOfNotes; i++)
            {
                if (this.CustomBackgroundObjects?[i]?.Name == this.pluginConfig.SelectedBackground)
                {
                    return i;
                }
            }
        }

        return 0;
    }
}