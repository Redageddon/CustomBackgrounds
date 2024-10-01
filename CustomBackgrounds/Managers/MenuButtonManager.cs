﻿using CustomBackgrounds.Settings.UI;

namespace CustomBackgrounds.Managers;

public class MenuButtonManager : IInitializable, IDisposable
{
    private readonly BackgroundsFlowCoordinator backgroundsFlowCoordinator;
    private readonly MainFlowCoordinator mainFlowCoordinator;
    private readonly MenuButton menuButton;

    public MenuButtonManager(MainFlowCoordinator mainFlowCoordinator, BackgroundsFlowCoordinator backgroundsFlowCoordinator)
    {
        this.mainFlowCoordinator = mainFlowCoordinator;
        this.backgroundsFlowCoordinator = backgroundsFlowCoordinator;
        this.menuButton = new MenuButton("Custom Backgrounds", "Change The skybox background!", this.ShowBackgroundFlow);
    }

    public void Initialize() => MenuButtons.Instance.RegisterButton(this.menuButton);

    public void Dispose() => MenuButtons.Instance.UnregisterButton(this.menuButton);

    private void ShowBackgroundFlow() => this.mainFlowCoordinator.PresentFlowCoordinator(this.backgroundsFlowCoordinator);
}