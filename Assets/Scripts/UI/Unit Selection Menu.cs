using UnityEngine;
using BuildingTools;
using TMPro;
using UnityEngine.UI;
using System.Collections;
public class UnitSelectionMenu : MonoBehaviour
{

    [SerializeField] GameObject UnitPanel, UnitList;

    public MonsterStats CurrentlyEmployedMonster;

    [SerializeField] TMP_Text monsterName, production;
    [SerializeField] Image monsterIcon;


    void Awake()
    {
        if(CurrentlyEmployedMonster == null)
        {
            SetupBuildingPanelEmpty();
        }
        else
        {
        SetupBuildingPanel();
        }

        SetupMonsterList(); 


       Destroy(UnitPanel);

    StartCoroutine(SetScrollToTop());

    }

    void SetupBuildingPanel()
    {
        monsterName.text = CurrentlyEmployedMonster.name + " (" + CurrentlyEmployedMonster.type.ToString() + ")";
        production.text = "0";
    }

    void SetupBuildingPanelEmpty()
    {
        monsterName.text = "No monster employed";
        production.text = "";
    }

    void SetupMonsterList()
    {
        foreach (var monster in GameManager.instance.monsters)
        {
            var Panel = Instantiate(UnitPanel, UnitList.transform);
            Panel.GetComponent<UnitPanel>().Setup(monster);
        }

    }


    public void CloseMenu()
    {
        Destroy(this.gameObject);
    }


    IEnumerator SetScrollToTop()
{
    yield return new WaitForEndOfFrame();
    GetComponentInChildren<ScrollRect>().verticalNormalizedPosition = 1.0f;
}
}
