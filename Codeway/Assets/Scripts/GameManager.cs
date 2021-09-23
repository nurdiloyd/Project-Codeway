using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance;     /// It is Singleton class, You reach from anywhere with "GameManager.Instance"

    public GameConfigData GameConfig;   // Game Config
    public GameBoard GameBoard;         // Game Board
    public TowersController TowersController;
    public MonstersController MonstersController;
    public GUIController GUIController;

    private int _killCounter;


    private void Awake() 
    {
        Instance = this;

        GameBoard.Init();           // Initing the Game Board
        MonstersController.Init();
        GUIController.SetKillCounter(_killCounter);
    }

    public void GameOver()
    {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void IncrementKill() 
    {
        _killCounter += 1;
        GUIController.SetKillCounter(_killCounter);
    }
}
