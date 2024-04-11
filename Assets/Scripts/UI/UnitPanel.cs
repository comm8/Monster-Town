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
  }

  public void SetMonster()
  {
    UnitSelectionMenu.instance.EmployMonster(MonsterID);
  }

}
