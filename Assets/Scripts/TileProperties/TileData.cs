using BuildingTools;
public class TileData
{
   public ushort ID;
   public TileType type;
}

public interface HasTile
{
   TileData GetTile();

}