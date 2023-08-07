using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Tile", menuName = "Scriptable Objects/Tile")]
public class TileData : ScriptableObject
{
    public enum TileType
    {
        start,
        end,
        staticObstacle,
        placedObstacle,
        enemy,
        collectable
    }

    public TileBase[] tile;
    public TileType tileType;

    public bool isObstacle = false;
}
