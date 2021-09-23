using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Game Config")]
public class GameConfigData : ScriptableObject 
{
    public Array2D Ground;      // Ground matrix
    public Cell[] GroundCells;  // Cell Types
    public float GridMargin;    // Margin btw cells

    public Tower Tower;     // Tower Prefab
    public Monster Monster; // Monster Prefab
}