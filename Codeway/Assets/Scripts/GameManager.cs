using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public GameConfigData GameConfig;       // Game Config
    public GameBoard GameBoard;             // Game Board


    private void Awake() {
        GameBoard.Init(this);      // Initing the Game Board
    }
}
