using UnityEngine;
using BuildingTools;
using UnityEngine.Rendering.Universal;
public class DeleteInteraction : InteractionMode
{

    [SerializeField] Material bulldozerPostProcessing;
    [SerializeField] ScriptableRendererFeature rendererFeature;

    [SerializeField] SelectionScheme validDelete;
    [SerializeField] SelectionScheme invalidDelete;

    public override void OnPressEnd(BuildingData tile, BuildingType selected)
    {
        //do nothing
    }
    public override void OnPress(BuildingData tile, BuildingType selected)
    {
        if (tile.buildingType != BuildingType.None && !tile.locked)
        {
            if (tile.buildingType == BuildingType.Road)
            {
                RemoveRoad();
            }

            gameManager.monsters[tile.monsterID].tile = null;
            tile.monsterID = 0;
            LeanTween.moveY(tile.model, 3, 0.4f).setEase(LeanTweenType.punch);
            LeanTween.scale(tile.model, new Vector3(0.2f, 0, 0.2f), 0.4f).setEase(LeanTweenType.easeOutBounce).setOnComplete(delayedPlace).setOnCompleteParam(tile as BuildingData);

        }
    }

    void delayedPlace(object tile)
    {
        BuildingData tiletemp = tile as BuildingData;
        tiletemp.resetscale();
        PlaceTile(tiletemp, BuildingType.None);
    }
    public override void OnPressStart(BuildingData tile, BuildingType selected)
    {
        //do nothing
    }

    public void RemoveRoad()
    {
        var adjacentTiles = BuildingUtils.GetAdjacentTiles(gameManager.SelectionGridPos);



        for (int i = 0; i < 4; i++)
        {
            var tile = adjacentTiles[i];
            if (tile.z < 0 || tile.z > gameManager.gridDimensions.z || tile.x < 0 || tile.x > gameManager.gridDimensions.x)
            {
                continue;
            }
            if (gameManager.tiles[BuildingUtils.CoordsToSlotID(tile, gameManager.gridDimensions)].TryGetComponent(out RoadProperties road))
            {
                if (i == 0)
                {
                    road.table.right = false;
                }
                else if (i == 1)
                {
                    road.table.left = false;
                }
                else if (i == 2)
                {
                    road.table.up = false;
                }
                else if (i == 3)
                {
                    road.table.down = false;
                }

                UpdatetileRoadMaterial(road, road.table);

                road.GetComponentInChildren<TileAnimator>().playUpdateAnimation();


            }
        }
    }

    public override void OnTileEnter(BuildingData tile, BuildingType selected)
    {
        CheckTileSelection(tile);
    }

    public override void OnTileExit(BuildingData tile, BuildingType selected)
    {
        if (checkValidTile(tile))
        {
            tile.SetDeletePreview(false);
        }
    }

    public override void OnModeEnter(BuildingData tile, BuildingType selected)
    {
        CheckTileSelection(tile);
        LeanTween.value(gameObject, updateBulldozerBorderSize, 0, 0.04f, 0.4f).setEase(LeanTweenType.easeOutBounce);
    }

    public override void OnModeExit(BuildingData tile, BuildingType selected)
    {
        if (checkValidTile(tile))
        {
            tile.SetDeletePreview(false);
        }
        LeanTween.value(gameObject, updateBulldozerBorderSize, 0.04f, 0, 0.4f).setEase(LeanTweenType.easeOutBounce);
    }

    void updateBulldozerBorderSize(float val)
    {
        bulldozerPostProcessing.SetFloat("_Border_Thickness", val);
    }

    bool checkValidTile(BuildingData tile)
    {
        return tile.buildingType != BuildingType.Road && tile.buildingType != BuildingType.None;
    }

    void CheckTileSelection(BuildingData tile)
    {
        if (tile == null) { return; }
        if (checkValidTile(tile))
        {
            gameManager.SetSelectionScheme(validDelete);
            tile.SetDeletePreview(true);
        }
        else
        {
            gameManager.SetSelectionScheme(invalidDelete);
        }
    }

}
