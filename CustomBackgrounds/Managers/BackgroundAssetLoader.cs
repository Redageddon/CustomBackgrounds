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

    public void Initialize()
    {
        if (this.CustomBackgroundObjects == null)
        {
            this.CustomBackgroundObjects = this.GetCustomBackgrounds();
            this.SelectedBackgroundIndex = this.GetConfigIndex();
        }
    }

    public void Dispose()
    {
        Logger.Log.Debug("Beginning background disposing.");

        if (this.CustomBackgroundObjects != null && this.CustomBackgroundObjects.Count != 0)
        {
            foreach (CustomBackground? customBackground in this.CustomBackgroundObjects)
            {
                string? name = customBackground?.Name;
                customBackground?.Dispose();
                Logger.Log.Debug($"Disposed {name}.");
            }
        }
        else
        {
            Logger.Log.Debug("No backgrounds to dispose.");
        }

        this.SelectedBackgroundIndex = 0;
        this.CustomBackgroundObjects = null;
        Logger.Log.Debug("Finished background disposing.");
    }

    internal void Reload()
    {
        Logger.Log.Debug("Reloading the BackgroundAssetLoader");
        this.Dispose();
        this.Initialize();
    }

    private List<CustomBackground?> GetCustomBackgrounds()
    {
        Logger.Log.Debug("Beginning background loading.");

        List<CustomBackground?> customBackgrounds = new()
        {
            new CustomBackground("Default"),
        };

        Logger.Log.Debug("Successfully loaded background: Default.");

        string[] paths = Directory.GetFiles(Plugin.BackgroundsDirectory);

        foreach (string path in paths)
        {
            string name = Path.GetFileName(path);
            string extension = Path.GetExtension(path);

            if (extension is ".png" or ".jpeg" or ".jpg" or ".gif")
            {
                try
                {
                    CustomBackground customBackground = new(name);
                    customBackgrounds.Add(customBackground);
                    Logger.Log.Debug($"Successfully loaded background: {name}.");
                }
                catch (Exception ex) // Texture may fail to load correctly
                {
                    Logger.Log.Warn($"Failed to Load Custom Background with path '{path}'.");
                    Logger.Log.Warn(ex);
                }
            }
        }

        Logger.Log.Debug("Finished background loading.");

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