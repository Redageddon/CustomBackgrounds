using CustomBackgrounds.Managers;
using CustomBackgrounds.Settings.UI;
using CustomBackgrounds.Settings.UI.Controllers;

namespace CustomBackgrounds.Installers;

public class CustomBackgroundsMenuInstaller : Installer
{
    public override void InstallBindings()
    {
        this.Container.Bind<LeftScreenViewController>().FromNewComponentAsViewController().AsSingle();
        this.Container.Bind<MainScreenViewController>().FromNewComponentAsViewController().AsSingle();
        this.Container.Bind<RightScreenViewController>().FromNewComponentAsViewController().AsSingle();
        this.Container.Bind<BackgroundsFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle();
        this.Container.BindInterfacesTo<MenuButtonManager>().AsSingle();
        this.Container.BindInterfacesAndSelfTo<CustomBackgrounds.Managers.MenuEnvironmentManager>().AsSingle();
    }
}