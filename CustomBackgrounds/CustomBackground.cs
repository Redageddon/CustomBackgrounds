using System.Threading.Tasks;

namespace CustomBackgrounds;

public class CustomBackground : IDisposable
{
    private readonly Task<byte[]> task;

    private Texture2D? texture;

    public CustomBackground(string name)
    {
        this.Name = name;

        if (name != "Default")
        {
            this.task = new Task<byte[]>(this.GetTextureData);
            this.task.Start();
        }
    }

    public string Name { get; }

    public Texture2D? Texture
    {
        get
        {
            if (this.Name != "Default" && !this.texture)
            {
                this.texture = Utilities.LoadTextureRaw(this.task.Result);
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

    private byte[] GetTextureData()
    {
        string textureFullPath = Path.Combine(Plugin.BackgroundsDirectory, this.Name);

        return File.ReadAllBytes(textureFullPath);
    }
}