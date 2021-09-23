using UnityEngine;
using TMPro;

public class GUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _killCounter;


    public void SpawnNewTower() 
    {
        GameManager.Instance.TowersController.SpawnTower();
    }

    public void SetKillCounter(int count)
    {
        _killCounter.text = "KILL " + count;
    }
}
