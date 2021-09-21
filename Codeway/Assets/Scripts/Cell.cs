using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector2Int Index;


    public void Init(Vector2Int index) 
    {
        Index = index;
    }
}