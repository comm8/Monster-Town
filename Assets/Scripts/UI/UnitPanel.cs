using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BuildingTools;
public class UnitPanel : MonoBehaviour
{
    public TMP_Text monsterName, production, employementStatus, species;
    public Image monsterIcon;
    public int MonsterID;
    public MonsterType monsterType;

    public void Setup(MonsterStats stats)
    {
        monsterName.text = stats.name;
        monsterIcon.sprite = stats.icon;
        species.text = stats.type.ToString();
        MonsterID = stats.ID;
        monsterType = stats.type;


        if (GameManager.instance.monsters[MonsterID].tile != null)
        {
            employementStatus.text = "Employed at " + GameManager.instance.monsters[MonsterID].tile.buildingType.ToString();
        }
        else
        {
            employementStatus.text = "Not Currently Employed";
        }

    }

    public void ButtonClickSetMonster()
    {
        UnitSelectionMenu.instance.EmployMonster(MonsterID);
        Setup(GameManager.instance.monsters[MonsterID]);
    }

    public void UpdateProduction(BuildingType type)
    {
        production.text = "Potential prod: " + GameManager.instance.buildings.GetBuilding((int)type).production[(int)monsterType].ToString();
    }
}
