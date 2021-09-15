using System.Collections.Generic;
using UnityEngine;

public class BuildingSolid : BuildingMain 
{
    public SpriteRenderer Sprite;   // Image of the building
    public BoxCollider2D Collider;  // Collidr of th building
    public Color SelectedColor;     // When the building selected

    private Transform _spawnPoint;  // Spaen Point

    // When Firstly created
    protected override void Created() {
        transform.parent = _manager.GameBoard.transform.GetChild(1);

        // Adding Listeners
        _manager.SelectionManager.SelectBuilding += SelectBuildingSubsc;
        _manager.SelectionManager.Deselect += DeselectMe;

        AdjustCollider();   // Adjusting Collider
        DeselectMe();       // Coloring the cells with color of selected building
        
        // Creating spawnPoint
        if (_buildingData.CanProductUnit)
            CreateSpawnPoint();
    }

    // Adjusts the collider
    private void AdjustCollider(){
        float width = _buildingData.Cols;
        float height = _buildingData.Rows;
        Vector2 size = new Vector2(width, height);
        
        Collider.size = size;
        Sprite.transform.localScale = size;
    }

    // Created spawnPoint on available space
    private void CreateSpawnPoint() {
        Vector3 pos = transform.position + new Vector3(_buildingData.Cols/2, _buildingData.Rows/2);
        
        // Spawn position of unit, when created
        pos.x += 2;
        _spawnPoint = new GameObject().transform;
        _spawnPoint.parent = transform;  
        _spawnPoint.position = pos;  
        _spawnPoint.name = "Spawn Point";
    }

    // Selection event listener
    private void SelectBuildingSubsc(RaycastHit2D hit, List<BuildingSolid> selectedBuilding) {
        if (hit.collider == Collider) {
            // Adding spawn listener
            _manager.InformationMenu.SpawnUnit += SpawnUnit;    
            // Coloring the cells with selection color
            Sprite.color = SelectedColor;

            selectedBuilding.Add(this);
        }
    }

    // Deselection event Listener
    private void DeselectMe() {
        // Rempveing spawn listener
        _manager.InformationMenu.SpawnUnit -= SpawnUnit;    
        // Coloring the cells with color of building
        Sprite.color = _buildingData.BuildingColor;
    }

    // Spawns new unit on spawnPoint
    private void SpawnUnit(int unitIndex, Transform parent) {
        // Positioning on an interval
        Vector3 spawnPosition = _spawnPoint.position;
        spawnPosition.x += Random.Range(-.5f, .5f);
        spawnPosition.y += Random.Range(-.5f, .5f);

        // Creating the unit
        MilitaryUnit unit = Instantiate(_manager.GameConfig.MilitaryUnit, spawnPosition, Quaternion.identity, parent);
        unit.InitMilitaryUnit(_buildingData.ProductionUnits[unitIndex], _manager);
    }
}
