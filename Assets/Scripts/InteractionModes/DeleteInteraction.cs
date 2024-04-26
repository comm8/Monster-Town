using UnityEngine;
using BuildingTools;
using UnityEngine.Rendering.Universal;
public class DeleteInteraction : InteractionMode
{

    [SerializeField] Material bulldozerPostProcessing;
    [SerializeField] ScriptableRendererFeature rendererFeature;

    public override void OnPressEnd(TileProperties tile, BuildingType selected)
    {
        //do nothing
    }
    public override void OnPress(TileProperties tile, BuildingType selected)
    {
        if (tile.buildingType != BuildingType.None)
        {
            if (tile.buildingType == BuildingType.Road)
            {
                RemoveRoad();
            }
            PlaceTile(tile, BuildingType.None);
            gameManager.monsters[tile.monsterID].tile = null;
            tile.monsterID = 0;

        }
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
    }

    public override void OnModeEnter(TileProperties tile, BuildingType selected)
    {
        rendererFeature.SetActive(true);
        LeanTween.value(gameObject, updateBulldozerBorderSize, 0, 0.04f, 0.4f).setEase(LeanTweenType.easeOutBounce);
    }

    public override void OnModeExit(TileProperties tile, BuildingType selected)
    {
        LeanTween.value(gameObject, updateBulldozerBorderSize, 0.04f, 0, 0.4f).setEase(LeanTweenType.easeOutBounce).setOnComplete(disableRenderFeature);
    }



    void updateBulldozerBorderSize(float val)
    {
        bulldozerPostProcessing.SetFloat("_Border_Thickness", val);
    }

    void disableRenderFeature()
    {
        rendererFeature.SetActive(false);
    }

}
