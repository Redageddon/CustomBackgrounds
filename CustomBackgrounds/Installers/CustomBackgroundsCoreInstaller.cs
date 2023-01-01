using CustomBackgrounds.Managers;

namespace CustomBackgrounds.Installers;

internal class CustomBackgroundsCoreInstaller : Installer
{
    public override void InstallBindings() => this.Container.BindInterfacesAndSelfTo<BackgroundAssetLoader>().AsSingle();
}