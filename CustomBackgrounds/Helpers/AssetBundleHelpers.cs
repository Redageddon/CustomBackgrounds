using System.Reflection;

namespace CustomBackgrounds.Helpers;

public static class AssetBundleHelpers
{
    private static readonly AssetBundle AssetBundle;

    static AssetBundleHelpers()
    {
        Stream customBgShader = Assembly.GetCallingAssembly().GetManifestResourceStream("CustomBackgrounds.Resources.CustomBG")!;
        byte[] data = new byte[customBgShader.Length];
        int _ = customBgShader.Read(data, 0, (int)customBgShader.Length);

        AssetBundle = AssetBundle.LoadFromMemory(data);
    }

    internal static Material? GetMaterialFromAssetBundle() => AssetBundle.LoadAsset<Material>("_CustomBGMat");
}