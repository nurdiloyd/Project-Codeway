using System.Collections.Generic;
using UnityEngine;

public class MilitaryUnit : MonoBehaviour
{
    public CircleCollider2D Collider;       // Unit Collider
    public SpriteRenderer Sprite;           // Unit Sprite
    public Color SelectedColor;             // When unit is selecte
    public AStarPathfinding AStar;          // Path finder 

    private GameManager _manager;               // Game Manager
    private MilitaryUnitData _militaryUnitData; // Unit information

	[SerializeField] 
    private float _speed = 1f;              // speed of the unit
	private Vector2 _targetPos;             // target given 


    public void InitMilitaryUnit(MilitaryUnitData militaryUnitData, GameManager manager) {
        enabled = false;
        _manager = manager;
        // Adding this as a subscribers to SelectionManager
        _manager.SelectionManager.SelectUnit += SelectUnitSubsc;
        _manager.SelectionManager.SelectUnits += SelectUnitsSubsc; 
        _manager.SelectionManager.Deselect += DeselectMe;

        // Setting military unit information
        _militaryUnitData = militaryUnitData;
        name = _militaryUnitData.name;
        Sprite.color = _militaryUnitData.UnitColor;
    }

	// Update is called once per frame
	void Update() {
        // If there is a order given
        Movement();
	}


    // Moves the unit
	private void Movement() {
		if (AStar.Path != null && AStar.Path.Count > 0) {
			if (_targetPos == new Vector2Int(1000, 0) || transform.position == (Vector3) _targetPos)
				_targetPos = AStar.Path.Pop();
		}
        else /*if(AStar.Path.Count == 0)*/{
            enabled = false;
        }

        // moving the soldier towards to target
		if (_targetPos != new Vector2Int(1000, 0)) 
			transform.position = Vector2.MoveTowards(transform.position, _targetPos, Time.deltaTime * _speed);      

	}


    // Selection Listener
    private void SelectUnitSubsc(RaycastHit2D hit, List<MilitaryUnit> selectedUnits) {
        if (hit.collider == Collider)
            SelectMe(selectedUnits);
    }

    // Selection Listener
    private void SelectUnitsSubsc(Vector2 min, Vector2 max, List<MilitaryUnit> selectedUnits) {
        Vector3 screenPos = _manager.GameCamera.WorldToScreenPoint(transform.position);
        if (screenPos.x > min.x && screenPos.x < max.x) {
            if (screenPos.y > min.y && screenPos.y < max.y )
                SelectMe(selectedUnits);
        }
    }

    // Selection
    private void SelectMe(List<MilitaryUnit> selectedUnits) {
        _manager.SelectionManager.AStarOrder += StartAStar;
        Sprite.color = SelectedColor;    
        selectedUnits.Add(this);
    }

    // Deselection event Listener
    private void DeselectMe() {
        _manager.SelectionManager.AStarOrder -= StartAStar;
        Sprite.color = _militaryUnitData.UnitColor;
    }

    // AStar Listener
    private void StartAStar(Vector2Int targetPos) {
        enabled = true;
        _targetPos = new Vector2Int(1000, 0);        // null value
        AStar.Current = null;
		AStar.StartPos = new Vector2Int((int)transform.position.x, (int)transform.position.y);
		AStar.GoalPos = targetPos;
        AStar.Path = null;
        AStar.PathFinding(_manager.GameBoard);
    }
}
