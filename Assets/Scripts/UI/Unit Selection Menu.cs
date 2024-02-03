using UnityEngine;
public class UnitSelectionMenu : MonoBehaviour
{

    [SerializeField] GameObject UnitPanel, UnitList;
    
    
    void Awake()
    {

        foreach (var monster in GameManager.instance.monsters)
        {
           var Panel = Instantiate(UnitPanel, UnitList.transform);
           Panel.GetComponent<UnitPanel>().setup(monster);
        }
       Destroy(UnitPanel);

        //fetch Unit list from gamemanager
        //create menu elements

    }

    void Update()
    {
        //on click off or minimize delete
        //on select unit set unit to building
        //if building occupied then set other unit building to free
    }

    public void CloseMenu()
    {
        Destroy(this.gameObject);
    }
}
