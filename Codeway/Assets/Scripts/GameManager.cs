using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public Camera GameCamera;               // Main Camera
    public GameConfigData GameConfig;       // Game Config
    public GameBoard GameBoard;             // Game Board
    public ProductionMenu ProductionMenu;   // Production Menu
    public InformationMenu InformationMenu; // Information Menu
    public SelectionController SelectionManager; // Selection Controller

    private void Awake() {
        GameBoard.InitGameBoard(this);                  // Initing the Game Board
        ProductionMenu.InitProductionMenu(this);        // Initing the Production Menu
        SelectionManager.InitSelectionManager(this);    // Initing the Selection Manager
        InformationMenu.InitInformationMenu(this);      // Initing the Information Menu
    }
}
