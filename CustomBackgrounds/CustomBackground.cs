namespace CustomBackgrounds;

public class CustomBackground : IDisposable
{
    public CustomBackground(string name)
    {
        this.Name = name;

        if (name != "Default")
        {
            this.Texture = Utilities.LoadTextureRaw(File.ReadAllBytes(Path.Combine(Plugin.BackgroundsDirectory, name)));
        }
    }

    public string Name { get; }

    public Texture2D? Texture { get; }

    public void Dispose()
    {
        this.ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~CustomBackground()
    {
        this.ReleaseUnmanagedResources();
    }

    private void ReleaseUnmanagedResources() => UnityEngine.Object.Destroy(this.Texture);
}