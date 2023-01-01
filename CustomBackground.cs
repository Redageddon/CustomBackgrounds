namespace CustomBackgrounds;

public class CustomBackground : IDisposable
{
    public CustomBackground(string fileName)
    {
        this.FileName = fileName;

        if (fileName != "Default")
        {
            this.Texture = Utilities.LoadTextureRaw(File.ReadAllBytes(Path.Combine(Plugin.BackgroundsDirectory, fileName)));
        }
    }

    public string FileName { get; }

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