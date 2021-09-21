using UnityEngine;

public class Cell : MonoBehaviour
{
    public GroundType Type;
}

public enum GroundType 
{
    Place, Road
}