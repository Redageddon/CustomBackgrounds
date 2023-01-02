using CustomBackgrounds.Managers;

namespace CustomBackgrounds.Settings.UI.Controllers;

public class MainScreenViewController : BSMLResourceViewController
{
    [Inject] private PluginConfig pluginConfig = null!;

    [Inject] private BackgroundAssetLoader backgroundAssetLoader = null!;

    [UIComponent("background-list")] public CustomListTableData customListTableData = null!;

    public override string ResourceName => "CustomBackgrounds.Settings.UI.Views.MainScreenMenu.bsml";

    [UIAction("reloadBackgrounds")]
    public void ReloadBackgrounds()
    {
        this.backgroundAssetLoader.Reload();
        this.SetupList();
        this.Select(this.customListTableData.tableView, this.backgroundAssetLoader.SelectedBackgroundIndex);
    }

    [UIAction("backgroundSelect")]
    public void Select(TableView _, int row)
    {
        this.backgroundAssetLoader.SelectedBackgroundIndex = row;
        this.pluginConfig.SelectedBackground = this.backgroundAssetLoader.CustomBackgroundObjects?[row]?.Name;

        Texture2D? texture = this.backgroundAssetLoader.CustomBackgroundObjects?[row]?.Texture;
        this.backgroundAssetLoader.SkyboxManager.UpdateTexture(texture); //todo: this
    }

    [UIAction("#post-parse")]
    public void SetupList()
    {
        this.customListTableData.data.Clear();

        foreach (CustomBackground? backgroundObject in this.backgroundAssetLoader.CustomBackgroundObjects!)
        {
            this.customListTableData.data.Add(new CustomListTableData.CustomCellInfo(backgroundObject?.Name, "Background Image", Sprite.Create(backgroundObject?.Texture, new Rect(0.0f, 0.0f, 300f, 100f), new Vector2(0.5f, 0.5f))));
        }

        this.customListTableData.tableView.ReloadData();
        int selectedBackgroundIndex = this.backgroundAssetLoader.SelectedBackgroundIndex;
        this.customListTableData.tableView.ScrollToCellWithIdx(selectedBackgroundIndex, 0, false);
        this.customListTableData.tableView.SelectCellWithIdx(selectedBackgroundIndex);
    }
}