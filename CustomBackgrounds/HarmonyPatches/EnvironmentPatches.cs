namespace CustomBackgrounds.HarmonyPatches;

// TODO: Patches for environment
public class EnvironmentPatches
{
    private readonly Harmony instance = new(nameof(CustomBackgrounds));

    public bool IsPatched { get; private set; }

    internal void ApplyHarmonyPatches()
    {
        if (!this.IsPatched)
        {
            this.instance.PatchAll(Assembly.GetExecutingAssembly());
            this.IsPatched = true;
        }
    }

    internal void RemoveHarmonyPatches()
    {
        if (this.IsPatched)
        {
            this.instance.UnpatchSelf();
            this.IsPatched = false;
        }
    }
}