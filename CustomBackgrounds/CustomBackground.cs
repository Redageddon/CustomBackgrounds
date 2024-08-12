namespace CustomBackgrounds;

public class CustomBackground : IDisposable
{
    private Texture2D? texture;
    private readonly string path;

    public CustomBackground(string name)
    {
        this.Name = name;
        this.path = Path.Combine(Plugin.BackgroundsDirectory, this.Name);
    }

    public string Name { get; }

    public async Task<Texture2D?> GetTextureAsync()
    {
        if (this.Name != "Default" && !this.texture)
        {
            this.texture = await Utilities.LoadImageAsync(this.path);
        }

        return this.texture;
    }

    public void Dispose()
    {
        this.ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~CustomBackground()
    {
        this.ReleaseUnmanagedResources();
    }

    private void ReleaseUnmanagedResources() => UnityEngine.Object.Destroy(this.texture);
}