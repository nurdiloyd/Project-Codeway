using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance;     /// It is Singleton class, You reach from anywhere with "GameManager.Instance"

    public GameConfigData GameConfig;       // Game Config
    public GameBoard GameBoard;             // Game Board
    public TowersController TowersController;
    public MonstersController MonstersController;
    public GUIController GUIController;
    public AudioManager AudioManager;

    private int _killCounter;   // Holds count of killed Monsters


    private void Awake() 
    {
        Instance = this;    

        GameBoard.Init();
        MonstersController.Init();
        GUIController.SetKillCounter(_killCounter);
    }

    // Calling when a monster arrived to the end
    public void GameOver()
    {
        StartCoroutine(LoadMainScene(2));
    }

    public void StopTheGame()
    {
        StartCoroutine(LoadMainScene(0));
    }

    private IEnumerator LoadMainScene(float time)
    {
        yield return new WaitForSeconds(time);

        // Loading main scene
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    
    // Increments the kill count
    public void IncrementKill() 
    {
        _killCounter += 1;
        GUIController.SetKillCounter(_killCounter);
    }
}
