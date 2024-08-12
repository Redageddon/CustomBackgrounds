using CustomBackgrounds.Managers;

namespace CustomBackgrounds.Settings.UI.Controllers;

public class MainScreenViewController : BSMLResourceViewController
{
    public override string ResourceName => "CustomBackgrounds.Settings.UI.Views.MainScreenMenu.bsml";

    [Inject] private PluginConfig pluginConfig = null!;

    [Inject] private BackgroundAssetLoader backgroundAssetLoader = null!;

    [Inject] private SkyboxManager skyboxManager = null!;

    [UIComponent("background-list")] private CustomListTableData customListTableData = null!;

    private readonly Dictionary<string, CustomListTableData.CustomCellInfo> map = new();

    [UIAction("reloadBackgrounds")]
    public async Task ReloadBackgrounds()
    {
        this.backgroundAssetLoader.Reload();
        await this.SetupList();
        await this.Select(this.customListTableData.tableView, this.backgroundAssetLoader.SelectedBackgroundIndex);
    }

    [UIAction("backgroundSelect")]
    public async Task Select(TableView _, int row)
    {
        this.backgroundAssetLoader.SelectedBackgroundIndex = row;
        this.pluginConfig.SelectedBackground = this.backgroundAssetLoader.CustomBackgroundObjects?[row]?.Name;

        await this.skyboxManager.UpdateTexture(row);
    }

    [UIAction("#post-parse")]
    public async Task SetupList()
    {
        this.customListTableData.data.Clear();

        List<CustomBackground>? backgrounds = this.backgroundAssetLoader.CustomBackgroundObjects!;

        foreach (CustomBackground backgroundObject in backgrounds)
        {
            this.AddNewCell(backgroundObject);
        }

        this.customListTableData.tableView.ReloadData();
        int selectedBackgroundIndex = this.backgroundAssetLoader.SelectedBackgroundIndex;
        this.customListTableData.tableView.ScrollToCellWithIdx(selectedBackgroundIndex, 0, false);
        this.customListTableData.tableView.SelectCellWithIdx(selectedBackgroundIndex);

        foreach (CustomBackground? backgroundObject in backgrounds)
        {
            await this.UpdateCellIcon(backgroundObject);
        }
    }

    private void AddNewCell(CustomBackground backgroundObject)
    {
        if (!this.map.ContainsKey(backgroundObject.Name))
        {
            CustomListTableData.CustomCellInfo customCell = new(backgroundObject.Name, "Background Image");
            this.customListTableData.data.Add(customCell);
            this.map[backgroundObject.Name] = customCell;
        }
    }

    private async Task UpdateCellIcon(CustomBackground backgroundObject)
    {
        Rect rect = new(0.0f, 0.0f, 300f, 100f);
        Vector2 pivot = new(0.5f, 0.5f);

        Texture2D? texture = await backgroundObject.GetTextureAsync();
        Sprite? icon = Sprite.Create(texture, rect, pivot);
        this.map[backgroundObject.Name].icon = icon;
    }
}