using UnityEngine;
using BuildingTools;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class UnitSelectionMenu : MonoBehaviour
{

    [SerializeField] GameObject UnitPanel, UnitList;
    public TileProperties currentTile;
    public MonsterStats CurrentlyEmployedMonster;

    [SerializeField] TMP_Text monsterName, production;
    [SerializeField] Image monsterIcon;
    [SerializeField] List<UnitPanel> panels;

    void UpdateBuildingPanel()
    {
        if (currentTile.monsterID == 0) { UpdateBuildingPanelEmpty(); return; }
        monsterName.text = CurrentlyEmployedMonster.name + " (" + CurrentlyEmployedMonster.type.ToString() + ")";
        production.text = "0";
    }

    public void EmployMonster(int id)
    {
        CurrentlyEmployedMonster.tile = null;
        CurrentlyEmployedMonster = GameManager.instance.monsters[id];
        CurrentlyEmployedMonster.tile = currentTile;

        UpdateBuildingPanel();
    }

    void UpdateBuildingPanelEmpty()
    {
        monsterName.text = "No monster employed";
        production.text = "";
    }

    public void AddMonster(int id)
    {
        var Panel = Instantiate(UnitPanel, UnitList.transform);
        Panel.GetComponent<UnitPanel>().Setup(GameManager.instance.monsters[id], id);
    }

    public void RemoveMonster(int id)
    {
        //-1 because unit 0 is no unit
        Destroy(panels[id - 1].gameObject);
    }
    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
        StartCoroutine(SetScrollToTop());
    }


    IEnumerator SetScrollToTop()
    {
        yield return new WaitForEndOfFrame();
        GetComponentInChildren<ScrollRect>().verticalNormalizedPosition = 1.0f;
    }
}
