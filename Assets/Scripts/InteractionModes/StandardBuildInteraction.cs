using BuildingTools;
using UnityEngine;
public class StandardBuildInteraction : InteractionMode
{
    [SerializeField] SelectionScheme PlaceScheme;
    [SerializeField] SelectionScheme InteractScheme;

    BuildingType hologramType = BuildingType.None;
    public override void OnPressEnd(TileProperties tile, BuildingType selected)
    {
        //Do nothing
    }
    public override void OnPress(TileProperties tile, BuildingType selected)
    {
        if (tile.buildingType == BuildingType.None && Inventory.TryChargeCost(selected))
        {
            PlaceTile(tile, selected);
            tile.UpdateModel();
            tile.UpdateMonsterEmployment();
            CheckScheme(tile, selected);
        }
        else
        {
            tile.GetComponentInChildren<TileAnimator>().playUpdateAnimation();
        }

    }
    public override void OnPressStart(TileProperties tile, BuildingType selected)
    {
        if (tile.buildingType != BuildingType.None && tile.buildingType != BuildingType.Road && !tile.locked)
        {
            gameManager.RefreshUnitSelectionPanel(tile);
        }
    }

    public override void OnTileEnter(TileProperties tile, BuildingType selected)
    {
        CheckScheme(tile, selected);
    }

    public override void OnModeEnter(TileProperties tile, BuildingType selected)
    {
        gameManager.selectionHologram.SetActive(true);
        gameManager.SetSelectionScheme(PlaceScheme);
    }

    public override void OnModeExit(TileProperties tile, BuildingType selected)
    {
        gameManager.selectionHologram.SetActive(false);
    }


    void CheckScheme(TileProperties tile, BuildingType desired)
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
            gameManager.SetSelectionScheme(PlaceScheme);

        }
        else
        {
            gameManager.selectionHologram.SetActive(false);
            gameManager.SetSelectionScheme(InteractScheme);
        }
    }
}
