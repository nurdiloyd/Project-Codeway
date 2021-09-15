using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Game Config")]
public class GameConfigData : ScriptableObject 
{
        // Grid dimensions
        public int MapGridWidth;
        public int MapGridHeight;

        public Cell Cell;           // Cell Prefab for buildingTemplate
        public Pool Pool;           // Pool of Production Menu

        // Buildings
        public GameObject BuildingTemplate;     // Template to place selected building
        public GameObject BuildingSolid;        // Building that will be placed
        public BuildingData[] Buildings;        // All distinct buildings on the game

        public MilitaryUnit MilitaryUnit;       // Military unit object

        public SpriteRenderer Marker;   // Target marker
    }
