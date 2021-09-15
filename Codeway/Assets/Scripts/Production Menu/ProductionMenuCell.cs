using UnityEngine;
using UnityEngine.UI;

public class ProductionMenuCell : MonoBehaviour
{
    public GameConfigData GameConfig;   // Game Config

    [SerializeField]
    protected Sprite defaultSprite; // Default Sprite
    [SerializeField]
    protected Image image;          // Sprite of building
    [SerializeField]
    protected Text text;            // Text on cell

    private ProductionMenu _productionMenu;   
    private int _index;             // Index of building


    // Indexing cells
    public void CellIndexing(int index, ProductionMenu productionMenu) {
        _productionMenu = productionMenu;

        // If there is a building on given index
        int buildingCount = GameConfig.Buildings.Length;
        if (index < buildingCount){
            _index = index;
            BuildingData building = GameConfig.Buildings[_index];
            text.text = building.BuildingName;
            image.sprite = building.BuildingIcon;
        }
        else {
            _index = buildingCount;
            text.text = "";
            image.sprite = defaultSprite;
        }
    }

    // Creates Building Template
    public void GenerateBuildingTemplate(){
        if (text.text != "")
            _productionMenu.CreateBuildingTemplate(_index);
    }
}
