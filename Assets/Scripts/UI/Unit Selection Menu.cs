using UnityEngine;
using BuildingTools;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class UnitSelectionMenu : MonoBehaviour
{
    [HideInInspector] public static UnitSelectionMenu instance;
    [SerializeField] GameObject UnitPanel, UnitList;
    public TileProperties currentTile;
    public MonsterStats CurrentlyEmployedMonster;
    [SerializeField] TMP_Text monsterName, production;
    [SerializeField] Image monsterIcon;
    [SerializeField] List<UnitPanel> panels;


    private void Awake()
    {
        instance = this;
    }

    void UpdateBuildingPanel()
    {
        if (currentTile.monsterID == 0) { UpdateBuildingPanelEmpty(); currentTile.UpdateMonsterEmployment(); return; }
        monsterName.text = CurrentlyEmployedMonster.name + " (" + CurrentlyEmployedMonster.type.ToString() + ")";
        monsterIcon.sprite =  CurrentlyEmployedMonster.icon;
        production.text = GameManager.instance.buildings.GetBuilding((int)currentTile.buildingType).production[(int)CurrentlyEmployedMonster.type].ToString();

    }

    public void EmployMonster(int id)
    {

        if (!ReferenceEquals(CurrentlyEmployedMonster, null))
        {
            CurrentlyEmployedMonster.tile = null;
        }
        CurrentlyEmployedMonster = GameManager.instance.monsters[id];
        CurrentlyEmployedMonster.tile = currentTile;
        currentTile.monsterID = (ushort)id;
        currentTile.UpdateMonsterEmployment();

        UpdateBuildingPanel();
    }

    void UpdateBuildingPanelEmpty()
    {
        //monsterIcon.sprite = 
        monsterName.text = "No monster employed";
        production.text = "";
    }

    public void AddMonster(int id)
    {
        var Panel = Instantiate(UnitPanel, UnitList.transform);
        Panel.GetComponent<UnitPanel>().Setup(GameManager.instance.monsters[id], id);
        //ADD PANELS TO LIST DINGUS
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

    public void OpenMenu(TileProperties tile)
    {
        gameObject.SetActive(true);
        UpdateBuildingPanel();
        StartCoroutine(SetScrollToTop());
    }


    IEnumerator SetScrollToTop()
    {
        yield return new WaitForEndOfFrame();
        GetComponentInChildren<ScrollRect>().verticalNormalizedPosition = 1.0f;
    }
}
