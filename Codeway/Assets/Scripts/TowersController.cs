using System.Collections.Generic;
using UnityEngine;

public class TowersController : MonoBehaviour
{
    private List<Vector2> _buildAreas;


    public void SpawnTower() 
    {
        if (_buildAreas == null)
        {
            _buildAreas = GameManager.Instance.GameBoard.GetBuildAreas();            
        }
        
        if (_buildAreas.Count > 0)
        {
            int buildAt = Random.Range(0, _buildAreas.Count);
            Vector2 pos =_buildAreas[buildAt];
            _buildAreas.RemoveAt(buildAt);
            
            Tower tower = Instantiate(GameManager.Instance.GameConfig.Tower, pos, Quaternion.identity, transform);
        }
    }
}
