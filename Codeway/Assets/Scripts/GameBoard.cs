using UnityEngine;
using System.Collections.Generic;

public class GameBoard : MonoBehaviour 
{
    private GameManager _gm;    // Game Manager
    
    private Cell[,] _ground;    // Content of each cell
    private int _height { get { return _ground.GetLength(0); } }
    private int _width { get { return _ground.GetLength(1); } }

    private List<Vector2Int> _buildAreaIndexes = new List<Vector2Int>();
    private Transform _towersParent;

    private List<Vector2Int> _path = new List<Vector2Int>();


    // Inits the Game Board
    public void Init(GameManager manager)
    {
        _gm = manager;
     
        // Creating Board system
        int[,] grid = _gm.GameConfig.Ground.GetCells();
        _ground = new Cell[grid.GetLength(0), grid.GetLength(1)];
        
        Transform groundParent = new GameObject("Ground").transform;
        groundParent.parent = transform;

        // Filling the board with cell
        Vector2Int pathStartIndex = Vector2Int.zero;
        float margin = _gm.GameConfig.GridMargin;   // Margin between cells
        Vector2 pos = new Vector2((1 - _width) / 2f, (_height - 1) / 2f) * margin;
        for (var i = 0; i < _height; i++) 
        {
            for (var j = 0; j < _width; j++) 
            {
                int groundType = grid[i, j];
                if (groundType < 4) 
                {
                    Cell cell = null;
                    cell = _gm.GameConfig.GroundCells[groundType];
                    cell = Instantiate(cell, pos, Quaternion.identity, groundParent);
                    _ground[i, j] = cell;

                    if (groundType == 1)
                    {
                        pathStartIndex = new Vector2Int(i, j);
                    }
                }

                pos.x += margin;
            }

            pos.x = (1 - _width) / 2f * margin;
            pos.y -= margin;
        }

        CreatePath(pathStartIndex);
        CheckBuildableAreas();
    }

    private void CreatePath(Vector2Int pathStartIndex) 
    {
        Vector2Int index = pathStartIndex;
        
        bool end = false;
        while (!end)
        {
            bool isRoad = false;
            for (int x = 0; x < 4; x++)
            {
                Vector2Int newIndex = index + FourNeighbor(x);
                if (newIndex.x < 0 || newIndex.y < 0 || newIndex.x >= _height || newIndex.y >= _width)
                {
                    break;
                }

                Cell neigh = _ground[newIndex.x, newIndex.y];
                if (neigh != null && neigh.Type == GroundType.Road && !_path.Contains(newIndex))
                {
                    _path.Add(index);
                    Instantiate(_gm.GameConfig.Tower, neigh.transform.position, Quaternion.identity);
                    index = newIndex;
                    isRoad = true;
                    break;
                }
            }

            if (!isRoad)
            {
                end = true;
            }
        }
    }

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
                        _buildAreaIndexes.Add(new Vector2Int(i, j));
                    }
                }
            }
        }
    }

    public void SpawnTower() 
    {
        if (_towersParent == null)
        {
            _towersParent = new GameObject("Towers").transform;
            _towersParent.parent = transform;
        }

        if (_buildAreaIndexes.Count > 0)
        {
            int buildAt = Random.Range(0, _buildAreaIndexes.Count);
            Vector2Int index =_buildAreaIndexes[buildAt];
            _buildAreaIndexes.RemoveAt(buildAt);
            
            Vector2 pos = _ground[index.x, index.y].transform.position;
            Instantiate(_gm.GameConfig.Tower, pos, Quaternion.identity, _towersParent);
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
}
