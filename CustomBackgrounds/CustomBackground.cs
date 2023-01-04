namespace CustomBackgrounds;

public class CustomBackground : IDisposable
{
    public CustomBackground(string name)
    {
        this.Name = name;

        if (name != "Default")
        {
            string textureFullPath = Path.Combine(Plugin.BackgroundsDirectory, name);
            byte[] textureData = File.ReadAllBytes(textureFullPath);
            this.Texture = Utilities.LoadTextureRaw(textureData);
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