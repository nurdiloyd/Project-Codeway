using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName= "Building")]
public class BuildingData : ScriptableObject
{
    public string BuildingName;
    public Sprite BuildingImage;    // Image on info menu
    public Sprite BuildingIcon;     // Icon on scroll var
    public Color BuildingColor = Color.white;
    
    public bool CanProductUnit = false; // True, if can product unit
    public MilitaryUnitData[] ProductionUnits;  // Productable units

    // Dimensions of the building
    public int Rows = 1;    // Height
    public int Cols = 1;    // Width

    public Vector2 dimensions 
    {
        get { return new Vector2(Cols, Rows); }
    }

}
