using UnityEngine;

public static class Consts
{
    public const int defaultGridSize = 3;
}

[System.Serializable]
[CreateAssetMenu(fileName = "Matrix", menuName = "Matrix")]
public class Array2D : ScriptableObject
{
    [SerializeField]
    public Vector2Int GridSize = Vector2Int.one * Consts.defaultGridSize;

    [SerializeField]
    CellRowInt[] cells = new CellRowInt[Consts.defaultGridSize];
    protected CellRowInt GetCellRow(int idx) {
        return cells[idx];
    }

    public int[,] GetCells() {
        int[,] ret = new int[GridSize.y, GridSize.x];

        for (int i = 0; i < GridSize.y; i++) {
            for (int j = 0; j < GridSize.x; j++) {
                ret[i, j] = GetCellRow(i)[j];
            }
        }

        return ret;
    }
}

[System.Serializable]
public class CellRowInt
{
    [SerializeField]
    private int[] row = new int[Consts.defaultGridSize];


    public int this[int i]
    {
        get { return row[i]; }
    }
}