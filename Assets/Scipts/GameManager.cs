using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject aStar;
    [SerializeField] private Tilemap tilemapBase;
    [SerializeField] private Transform playerPrefab;
    [SerializeField] private Transform destinationPrefab;
    [SerializeField] private GameObject particleConfetti;
    private Tilemap collectiblesTilemap;

    private Transform destination;
    public Transform player;
    public AIPathCustom aiPathCustom;

    [SerializeField] private int maxAmountTileToUse;
    public int remainingTiles;

    public bool sceneHasCollectibles = false;
    private int numberOfCollectibles;
    public int collectiblesGathered = 0;

    private float minDistanceToReach = 0.3f;

    private bool isLevelEnded = false;

    private void Awake()
    {
        instance = this;

        collectiblesTilemap = GameObject.FindWithTag("Collectibles").GetComponent<Tilemap>();
    }

    private void Start()
    {
        isLevelEnded = false;

        SetupDestination();
        SetupPlayer();
        aiPathCustom = player.GetComponent<AIPathCustom>();
        aiPathCustom.canMove = false;

        numberOfCollectibles = GetAllTilesPosition(collectiblesTilemap, TileData.TileType.collectable).Count;

        player.GetComponent<AIDestinationSetter>().target = destination;

        if (UIManager.instance == null) return;

        remainingTiles = maxAmountTileToUse;
        UIManager.instance.UpdateTileCountText(remainingTiles);
    }

    private void Update()
    {
        if(Vector2.Distance(player.position, destination.position) <= minDistanceToReach && !isLevelEnded)
        {
            if (collectiblesGathered < numberOfCollectibles)
            {
                LevelManager.Instance.ResetLevel();
            }
            else
            {
                LevelCompleted();
            }

            isLevelEnded = true;
        }
    }

    private void LevelCompleted()
    {
        GameObject particle = Instantiate(particleConfetti, destination.position, Quaternion.identity);

        AudioManager.Instance.Play("LevelComplete");

        Destroy(particle, 1f);

        LevelManager.Instance.LoadNextLevel();
    }

    private void SetupDestination()
    {
        destination = Instantiate(destinationPrefab);
        destination.transform.position = GetTilePosition(tilemapBase, TileData.TileType.end);
    }

    private void SetupPlayer()
    {
        player = Instantiate(playerPrefab);
        player.transform.position = GetTilePosition(tilemapBase, TileData.TileType.start);
        player.GetComponent<AIDestinationSetter>().target = null;
    }

    private Vector3 GetTilePosition(Tilemap tilemap, TileData.TileType tiletype)
    {
        foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(position);
            if (tile != null)
            {
                if (MapManager.instance.dataFromTiles[tile].tileType == tiletype)
                {
                    return tilemap.CellToWorld(position);
                }
            }
        }

        Debug.LogError("Tile position not found! Type: " + tiletype + " Tilemap: " + tilemap);
        return Vector3.zero;
    }

    private List<Vector3> GetAllTilesPosition(Tilemap tilemap, TileData.TileType tiletype)
    {
        List<Vector3> temp = new();

        foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(position);

            if (tile != null)
            {
                if (MapManager.instance.dataFromTiles[tile].tileType == tiletype)
                {
                    temp.Add(tilemap.CellToWorld(position));
                }
            }
        }

        return temp;
    }

    public void StartMovement()
    {
        aiPathCustom.canMove = true;
    }

    public void AddOneTileToMaxTotal()
    {
        remainingTiles++;
        PlayerScore.Instance.AddHelpRequest();
        UIManager.instance.UpdateTileCountText(remainingTiles);
    }
}
