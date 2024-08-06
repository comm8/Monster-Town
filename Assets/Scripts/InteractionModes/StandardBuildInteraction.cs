using BuildingTools;
using UnityEngine;
public class StandardBuildInteraction : InteractionMode
{
    [SerializeField] SelectionScheme PlaceScheme;
    [SerializeField] SelectionScheme InteractScheme;

    [SerializeField] SelectionScheme CantAffordScheme;

    BuildingType hologramType = BuildingType.None;
    public override void OnPressEnd(BuildingProperties tile, BuildingType selected)
    {
        //Do nothing
    }
    public override void OnPress(BuildingProperties tile, BuildingType selected)
    {
        if (tile.buildingType == BuildingType.None && Inventory.TryChargeCost(selected, true))
        {
            PlaceTile(tile, selected);
            tile.UpdateModel();
            tile.UpdateMonsterEmployment();
            CheckScheme(tile, selected);
            //gameManager.SetInteractionMode(gameManager.unselectedInteraction);
        }
        else
        {
            tile.GetComponentInChildren<TileAnimator>().playUpdateAnimation();
        }

    }
    public override void OnPressStart(BuildingProperties tile, BuildingType selected)
    {
        if (tile.buildingType != BuildingType.None && tile.buildingType != BuildingType.Road && !tile.locked)
        {
            gameManager.RefreshUnitSelectionPanel(tile);
        }
    }

    public override void OnTileEnter(BuildingProperties tile, BuildingType selected)
    {
        CheckScheme(tile, selected);
    }

    public override void OnModeEnter(BuildingProperties tile, BuildingType selected)
    {
        gameManager.selectionHologram.SetActive(true);
        CheckScheme(tile,selected);
    }

    public override void OnModeExit(BuildingProperties tile, BuildingType selected)
    {
        gameManager.selectionHologram.SetActive(false);
    }


    void CheckScheme(BuildingProperties tile, BuildingType desired)
    {
        if (tile.buildingType == BuildingType.None)
        {
            gameManager.selectionHologram.SetActive(true);
            if (hologramType != desired)
            {
                Destroy(gameManager.selectionHologram);
                gameManager.selectionHologram = Instantiate(gameManager.buildings.GetBuilding(desired).Model, gameManager.selectionTweened, false);
            }
            hologramType = desired;


            if (tile.buildingType == BuildingType.None && Inventory.TryChargeCost(desired, false))
            {
                gameManager.SetSelectionScheme(PlaceScheme);
            }
            else
            {
                gameManager.SetSelectionScheme(CantAffordScheme);
            }


        }
        else
        {
            gameManager.selectionHologram.SetActive(false);
            gameManager.SetSelectionScheme(InteractScheme);
        }
    }

    public override void OnTileExit(BuildingProperties tile, BuildingType selected)
    {
        
    }

}
