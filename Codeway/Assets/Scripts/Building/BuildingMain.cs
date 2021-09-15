using UnityEngine;

public class BuildingMain : MonoBehaviour
{
    public int BuildingIndex;

    protected GameManager _manager;         // Game Manager
    protected BuildingData _buildingData;   // Building information on Matrix form 


    public void CreateBuilding(int buildingIndex, GameManager manager) {
        _manager = manager;
        BuildingIndex = buildingIndex;
        _buildingData = manager.GameConfig.Buildings[BuildingIndex];
        // Setting name of objet as building name
        name = _buildingData.name;
        Created();
    }

    protected virtual void Created(){}
}
