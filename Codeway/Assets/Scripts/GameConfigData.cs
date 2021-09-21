using UnityEngine;


[CreateAssetMenu(fileName = "GameConfig", menuName = "Game Config")]
public class GameConfigData : ScriptableObject 
{
    // Grid
    public Array2D Grid;
    public float GridMargin;

    public Cell[] Cells;
}