using UnityEngine;
using TMPro;

public class GUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _killCounter;  


    // Sends request to TowersController to Spawn new tower by the button on GUI
    public void SpawnNewTower() 
    {
        GameManager.Instance.TowersController.SpawnTower();
    }

    // Sets the kill text on GUI
    public void SetKillCounter(int count)
    {
        _killCounter.text = "KILL " + count;
    }
}
