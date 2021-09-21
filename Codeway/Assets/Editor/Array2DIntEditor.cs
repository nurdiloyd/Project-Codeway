using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Array2D))]
public class Array2DIntEditor : Array2DEditor
{
    protected override int CellWidth { get { return 18; } }
    protected override int CellHeight { get { return 18; } }


    protected override void SetValue(SerializedProperty cell, int i, int j) {
        int[,] previousCells = (target as Array2D).GetCells();

        cell.intValue = default(int);

        if ((i < gridSize.vector2IntValue.y && j < gridSize.vector2IntValue.x))
        {
            if (previousCells[i, j] < 10)
            {           
                cell.intValue = previousCells[i, j];                
            }
            else
            {            
                cell.intValue = 0;
            }
        }

        if (!gridSizeChanged && previousCells[i, j] < 10)
        {
            cell.intValue = previousCells[i, j];
        }
        else if ((i == 0 || j == 0 || i == newGridSize.y - 1 || j == newGridSize.x - 1))
        {
            cell.intValue = 99;
        }
    }
}
