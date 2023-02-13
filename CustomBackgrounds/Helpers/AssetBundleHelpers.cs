namespace CustomBackgrounds.Helpers;

public static class AssetBundleHelpers
{
    private const string EmbeddedCustomBackgroundsAssetBundlePath = "CustomBackgrounds.Resources.SkyboxMaterialAssetBundle";
    private const string MaterialAssetName = "SkyboxMaterial";
    private static readonly Material Material;

    static AssetBundleHelpers()
    {
        AssetBundle assetBundle = GetAssetBundle();
        Material = assetBundle.LoadAsset<Material>(MaterialAssetName);
    }

    private static AssetBundle GetAssetBundle()
    {
        Assembly customBackgroundsAssembly = Assembly.GetAssembly(typeof(Plugin));
        Stream assetBundleStream = customBackgroundsAssembly.GetManifestResourceStream(EmbeddedCustomBackgroundsAssetBundlePath)!;
        byte[] data = new byte[assetBundleStream.Length];
        int _ = assetBundleStream.Read(data, 0, (int)assetBundleStream.Length);

        return AssetBundle.LoadFromMemory(data);
    }

    internal static Material GetMaterialFromAssetBundle() => Material;
}