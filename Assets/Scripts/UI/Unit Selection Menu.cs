using UnityEngine;
using BuildingTools;
using TMPro;
using UnityEngine.UI;
using System.Collections;
public class UnitSelectionMenu : MonoBehaviour
{

    [SerializeField] GameObject UnitPanel, UnitList;
    public TileProperties currentTile;

    public MonsterStats CurrentlyEmployedMonster;

    [SerializeField] TMP_Text monsterName, production;
    [SerializeField] Image monsterIcon;


    void Awake()
    {
        if (CurrentlyEmployedMonster == null)
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

    public void EmployMonster(int id)
    {
       if(CurrentlyEmployedMonster != null )
       {
        CurrentlyEmployedMonster.tile = null;
       } 

        CurrentlyEmployedMonster = GameManager.instance.monsters[id];
        CurrentlyEmployedMonster.tile = currentTile;

        if (CurrentlyEmployedMonster == null)
        {
            SetupBuildingPanelEmpty();
        }
        else
        {
            SetupBuildingPanel();
        }
    }

    void SetupBuildingPanelEmpty()
    {
        monsterName.text = "No monster employed";
        production.text = "";
    }

    void SetupMonsterList()
    {
        for(int i = 0; i < GameManager.instance.monsters.Count; i++)
        {
            var Panel = Instantiate(UnitPanel, UnitList.transform);
            Panel.GetComponent<UnitPanel>().Setup(GameManager.instance.monsters[i], i);
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
