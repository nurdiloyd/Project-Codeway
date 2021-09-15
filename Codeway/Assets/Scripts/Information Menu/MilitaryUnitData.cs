using UnityEngine;

[CreateAssetMenu(fileName = "Soldier", menuName= "Military Unit")]
public class MilitaryUnitData : ScriptableObject
{
    public Sprite UnitIcon;    
    public Color UnitColor = Color.white;
}