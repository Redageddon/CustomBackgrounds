using CustomBackgrounds.Settings.UI.Controllers;

namespace CustomBackgrounds.Settings.UI;

public class BackgroundsFlowCoordinator : FlowCoordinator
{
    [Inject] private MainFlowCoordinator mainFlowCoordinator = null!;

    [Inject] private LeftScreenViewController leftScreenViewController = null!;

    [Inject] private MainScreenViewController mainScreenViewController = null!;

    [Inject] private RightScreenViewController rightScreenViewController = null!;

    protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
    {
        if (firstActivation)
        {
            this.SetTitle("Custom Backgrounds");
            this.showBackButton = true;
        }

        this.ProvideInitialViewControllers(this.mainScreenViewController, this.leftScreenViewController, this.rightScreenViewController);
    }

    protected override void BackButtonWasPressed(ViewController _) => this.mainFlowCoordinator.DismissFlowCoordinator(this);
}