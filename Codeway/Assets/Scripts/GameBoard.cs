using UnityEngine;

public class GameBoard : MonoBehaviour 
{
    public Cell[,] Board;      // Content of each cell

    public int Height 
    {
        get 
        {
            return Board.GetLength(0);
        }
    }
    public int Width 
    {
        get 
        {
            return Board.GetLength(1);
        }
    }

    [HideInInspector] public float Margin;  // Margin between cells

      
    // Inits the Game Board
    public void Init(GameManager manager) 
    {
        int[,] grid = manager.GameConfig.Grid.GetCells();
     
        // Creating Board system
        Board = new Cell[grid.GetLength(0), grid.GetLength(1)];
        Margin = manager.GameConfig.GridMargin;

        // Filling the board with cell
        Vector2 pos = new Vector2((1 - Width) / 2f, (Height - 1) / 2f) * Margin;
        for (var i = 0; i < Height; i++) 
        {
            for (var j = 0; j < Width; j++) 
            {
                if (grid[i, j] < 4) 
                {
                    Cell cell = null;
                    cell = manager.GameConfig.Cells[grid[i, j]];
                    cell = Instantiate(cell, pos, Quaternion.identity, transform);
                    cell?.Init(new Vector2Int(i, j));
                    Board[i, j] = cell;
                }

                pos.x += Margin;
            }

            pos.x = (1 - Width) / 2f * Margin;
            pos.y -= Margin;
        }
    } 
}
