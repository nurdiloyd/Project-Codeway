using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Game Config")]
public class GameConfigData : ScriptableObject 
{
    // Grid
    public Array2D Ground;
    public float GridMargin;

    public Cell[] GroundCells;
    public Tower Tower;

    public Monster Monster;
}