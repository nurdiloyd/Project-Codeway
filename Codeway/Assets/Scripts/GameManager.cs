using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public GameConfigData GameConfig;   // Game Config
    public GameBoard GameBoard;         // Game Board
    public MonstersController MonstersController;


    private void Awake() {
        GameBoard.Init(this);           // Initing the Game Board
        MonstersController.Init(this);
    }

    public void SpawnNewTower() {
        GameBoard.SpawnTower();
    }
}
