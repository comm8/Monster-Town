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


  public void Setup(MonsterStats stats, int id)
  {
    monsterName.text = stats.name;
    monsterIcon.sprite = stats.icon;
    species.text = stats.type.ToString();
    MonsterID = id;


    if(GameManager.instance.monsters[id].tile != null)
    {
          employementStatus.text = "Employed at " + GameManager.instance.monsters[id].tile.buildingType.ToString();
    }
    else
    {
          employementStatus.text = "Not Currently Employed";
    }

  }

  public void SetMonster()
  {
    UnitSelectionMenu.instance.EmployMonster(MonsterID);
    Setup(GameManager.instance.monsters[MonsterID], MonsterID);
  }

}
