namespace CustomBackgrounds;

public class CustomBackground : IDisposable
{
    private readonly byte[] textureData = Array.Empty<byte>();

    private Texture2D? texture;

    public CustomBackground(string name)
    {
        this.Name = name;

        if (name != "Default")
        {
            this.textureData = GetTextureData(name);;
        }
    }

    public string Name { get; }

    public Texture2D? Texture
    {
        get
        {
            if (!this.texture)
            {
                this.texture = Utilities.LoadTextureRaw(this.textureData);
            }

            return this.texture;
        }
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

    private void ReleaseUnmanagedResources() => UnityEngine.Object.Destroy(this.Texture);

    private static byte[] GetTextureData(string name)
    {
        string textureFullPath = Path.Combine(Plugin.BackgroundsDirectory, name);

        return File.ReadAllBytes(textureFullPath);
    }
}