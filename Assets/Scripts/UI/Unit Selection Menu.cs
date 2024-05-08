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
    public List<UnitPanel> panels;


    private void Awake()
    {
        instance = this;
        panels = new List<UnitPanel>();
    }

    public void UpdateBuildingPanel()
    {
        foreach (UnitPanel panel in panels)
        {
            panel.UpdateProduction(currentTile.buildingType);
        }
        if (currentTile.monsterID == 0) { UpdateBuildingPanelEmpty(); currentTile.UpdateMonsterEmployment(); return; }
        monsterName.text = CurrentlyEmployedMonster.name + " (" + CurrentlyEmployedMonster.type.ToString() + ")";
        monsterIcon.sprite = CurrentlyEmployedMonster.icon;
        production.text = "Producing " + GameManager.instance.buildings.GetBuilding((int)currentTile.buildingType).production[(int)CurrentlyEmployedMonster.type].ToString();


    }
    void UpdateBuildingPanelEmpty()
    {
        //monsterIcon.sprite = 
        monsterName.text = "No monster employed";
        production.text = "";
    }

    public void EmployMonster(int id)
    {
        GameManager.instance.SetMonsterEmploymentStatus(GameManager.instance.monsters[id], currentTile.ID);
    }


    public void AddMonster(int id)
    {
        var Panel = Instantiate(UnitPanel, UnitList.transform);
        Panel.GetComponent<UnitPanel>().Setup(GameManager.instance.monsters[id]);
        panels.Add(Panel.GetComponent<UnitPanel>());
        panels[^1].UpdateProduction(currentTile.buildingType);
        LayoutRebuilder.MarkLayoutForRebuild(transform as RectTransform);

    }

    public void RemoveMonster(int id)
    {
        //-1 because unit 0 is no unit
        Destroy(panels[id - 1].gameObject);
    }
    public void CloseMenu()
    {
        LeanTween.scaleY(gameObject, 0.0f, 0.2f).setEase(LeanTweenType.easeOutBounce).setOnComplete(MyComplete);
    }

    void MyComplete()
    {
        gameObject.SetActive(false);
    }
    public void OpenMenu(TileProperties tile)
    {

        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
            transform.localPosition = new Vector2(0, 0);
        }


        transform.localScale = new Vector3(1, 0);
        LeanTween.scaleY(gameObject, 1.0f, 0.2f).setEase(LeanTweenType.easeOutElastic);

        UpdateBuildingPanel();
        StartCoroutine(SetScrollToTop());
    }


    IEnumerator SetScrollToTop()
    {
        yield return new WaitForEndOfFrame();
        GetComponentInChildren<ScrollRect>().verticalNormalizedPosition = 1.0f;
    }
}
