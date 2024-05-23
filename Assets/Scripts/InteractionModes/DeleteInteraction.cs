using UnityEngine;
using BuildingTools;
using UnityEngine.Rendering.Universal;
public class DeleteInteraction : InteractionMode
{

    [SerializeField] Material bulldozerPostProcessing;
    [SerializeField] ScriptableRendererFeature rendererFeature;

    [SerializeField] SelectionScheme scheme;

    TileProperties lasttile;

    public override void OnPressEnd(TileProperties tile, BuildingType selected)
    {
        //do nothing
    }
    public override void OnPress(TileProperties tile, BuildingType selected)
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
            LeanTween.scale(tile.model, new Vector3(0.2f, 0, 0.2f), 0.4f).setEase(LeanTweenType.easeOutBounce).setOnComplete(delayedPlace).setOnCompleteParam(tile as TileProperties);

        }
    }

    void delayedPlace(object tile)
    {
        TileProperties tiletemp = tile as TileProperties;
        tiletemp.resetscale();
        PlaceTile(tiletemp, BuildingType.None);
    }
    public override void OnPressStart(TileProperties tile, BuildingType selected)
    {
        //do nothing
    }

    public void RemoveRoad()
    {
        var adjacentTiles = BuildingUtils.GetAdjacentTiles(gameManager.SelectionGridPos);



        for (int i = 0; i < 4; i++)
        {
            var tile = adjacentTiles[i];
            if (tile.y < 0 || tile.y > gameManager.gridSize || tile.x < 0 || tile.x > gameManager.gridSize)
            {
                continue;
            }
            if (gameManager.tileProperties[BuildingUtils.CoordsToSlotID(tile, gameManager.gridSize)].TryGetComponent(out RoadProperties road))
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

    public override void OnTileEnter(TileProperties tile, BuildingType selected)
    {
          if (lasttile != null && lasttile.buildingType != BuildingType.Road && lasttile.buildingType != BuildingType.None )
        {
            lasttile.SetDeletePreview(false);
        }
          if (tile != null && tile.buildingType != BuildingType.Road && tile.buildingType != BuildingType.None)
        {
            tile.SetDeletePreview(true);
            lasttile = tile;
        }
    }

    public override void OnModeEnter(TileProperties tile, BuildingType selected)
    {
        if (tile != null && tile.buildingType != BuildingType.Road && tile.buildingType != BuildingType.None)
        {
            tile.SetDeletePreview(true);
            lasttile = tile;
        }

        LeanTween.value(gameObject, updateBulldozerBorderSize, 0, 0.04f, 0.4f).setEase(LeanTweenType.easeOutBounce);
        gameManager.SetSelectionScheme(scheme);
    }

    public override void OnModeExit(TileProperties tile, BuildingType selected)
    {
        if (lasttile != null && lasttile.buildingType != BuildingType.Road && lasttile.buildingType != BuildingType.None)
        {
            lasttile.SetDeletePreview(false);
        }
        LeanTween.value(gameObject, updateBulldozerBorderSize, 0.04f, 0, 0.4f).setEase(LeanTweenType.easeOutBounce);
    }

    void updateBulldozerBorderSize(float val)
    {
        bulldozerPostProcessing.SetFloat("_Border_Thickness", val);
    }
}
