using UnityEngine;

public class TimedKiller : MonoBehaviour
{
    [SerializeField] protected float killAfter = 1;
    
    
    private void Start() {
        Destroy(gameObject, killAfter);
    }
}