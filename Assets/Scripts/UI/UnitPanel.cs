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


    public void Setup(MonsterStats stats)
    {
        monsterName.text = stats.name;
        monsterIcon.sprite = stats.icon;
        species.text = stats.type.ToString();
    }

}
