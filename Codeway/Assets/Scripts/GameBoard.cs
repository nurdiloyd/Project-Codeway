using UnityEngine;
using System.Collections.Generic;

public class GameBoard : MonoBehaviour 
{    
    private Cell[,] _ground;    // Content of each cell
    private int _height { get { return _ground.GetLength(0); } }
    private int _width { get { return _ground.GetLength(1); } }

    private List<Vector2> _buildAreas = new List<Vector2>();
    private List<Vector2> _path = new List<Vector2>();
    private Vector2Int _pathStartIndex;


    public void Init()
    {
        // Creating Board system
        SpawnCells(GameManager.Instance.GameConfig.Ground.GetCells());
        CreatePath();
        CheckBuildableAreas();
    }

    // Spawns Ground cells to show map
    private void SpawnCells(int[,] grid) 
    {
        Transform groundParent = new GameObject("Ground").transform;
        groundParent.parent = transform;

        // Filling the board with cell
        _ground = new Cell[grid.GetLength(0), grid.GetLength(1)];
        float margin = GameManager.Instance.GameConfig.GridMargin;   // Margin between cells
        Vector2 pos = new Vector2((1 - _width) / 2f, (_height - 1) / 2f) * margin;
        for (var i = 0; i < _height; i++) 
        {
            for (var j = 0; j < _width; j++) 
            {
                int groundType = grid[i, j];
                if (groundType < 4) 
                {
                    Cell cell = null;
                    cell = GameManager.Instance.GameConfig.GroundCells[groundType];
                    cell = Instantiate(cell, pos, Quaternion.identity, groundParent);
                    _ground[i, j] = cell;

                    if (groundType == 1)
                    {
                        _pathStartIndex = new Vector2Int(i, j);
                    }
                }

                pos.x += margin;
            }

            pos.x = (1 - _width) / 2f * margin;
            pos.y -= margin;
        }
    }

    // Lists the path points positions
    private void CreatePath() 
    {
        Vector2Int index = _pathStartIndex;
        
        bool isRoad = true;
        while (isRoad)
        {
            Vector2 pos = _ground[index.x, index.y].transform.position;
            _path.Add(pos);

            isRoad = false;
            for (int x = 0; x < 4; x++)
            {
                Vector2Int neighIndex = index + FourNeighbor(x);
                if (neighIndex.x < 0 || neighIndex.y < 0 || neighIndex.x >= _height || neighIndex.y >= _width)
                {
                    continue;
                }

                Cell neigh = _ground[neighIndex.x, neighIndex.y];
                if (neigh != null && neigh.Type == GroundType.Road && !_path.Contains(neigh.transform.position))
                {
                    index = neighIndex;
                    isRoad = true;
                    break;
                }
            }
        }
    }

    // Lists the buildable points positions
    private void CheckBuildableAreas() 
    {
        for (int i = 1; i < _height - 1; i++) 
        {
            for (int j = 1; j < _width - 1; j++)
            {
                bool buildable = false;
                Cell center = _ground[i, j];
                if (center != null && center.Type == GroundType.Place)
                {
                    for (int x = -1; x < 2; x++)
                    {
                        for (int y = -1; y < 2; y++)
                        {
                            Cell neigh = _ground[i + x, j + y];
                            if (neigh != null && neigh.Type == GroundType.Road)
                            {
                                buildable = true;
                                break;
                            }
                        }
                    }
                    
                    if (buildable)
                    {
                        _buildAreas.Add(center.transform.position);
                    }
                }
            }
        }
    }

    private Vector2Int FourNeighbor(int neigh) {
        Vector2Int side = Vector2Int.zero;

        switch (neigh)
        {
            case 0:
                side.x = -1;
                break;
            case 1:
                side.x = 1;
                break;
            case 2:
                side.y = -1;
                break;
            case 3:
                side.y = 1;
                break;
        }
        
        return side;
    }

    public List<Vector2> GetPath() 
    {
        return _path;
    }

    public List<Vector2> GetBuildAreas() 
    {
        return _buildAreas;
    }
}
