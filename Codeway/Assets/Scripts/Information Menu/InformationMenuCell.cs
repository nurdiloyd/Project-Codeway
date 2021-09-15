using UnityEngine.UI;
using UnityEngine;

public class InformationMenuCell : MonoBehaviour
{
    public Image UnitIcon;  // Icon of the unit
    public Text Amount;

    private int _unitIndex;
    private InformationMenu _informationMenu;

    // Sets the Cell by icon of military unit
    public void SetUnitCell(int unitIndex, Sprite unitIcon, int amount, InformationMenu informationMenu) {
        _informationMenu = informationMenu;
        _unitIndex = unitIndex;
        UnitIcon.sprite = unitIcon;
        Amount.text = amount.ToString() + "x ";
    }

    // Send Spawn Request when pressed the unit button
    public void SpawnRequest() {
        _informationMenu.SendSpawnRequest(_unitIndex);
    }
}
