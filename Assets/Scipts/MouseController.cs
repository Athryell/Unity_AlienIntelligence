using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using UnityEngine.SceneManagement;

public class MouseController : MonoBehaviour
{
    public Tilemap tilemapObstaclesPlaced;
    public Tilemap tilemapHighlight;
    public TileBase tileToPlace;
    public TileBase tileHighlight;
    public TileBase tileHighlightOccupiedSpace;

    public Tilemap[] tilemapsToCheckForPlacingTile;

    private Camera cam;
    private Vector3Int previousTileHovered = new();
    private Vector3Int tileHovered;
    private Vector3Int currentTile;

    private LinePath linePath;
    private bool isObstacleSet = false;

    public float timeShowingTile = 0.2f;
    [SerializeField] private float shakeIntensity = 1f;
    [SerializeField] private float shakeTime = 0.1f;
    
    private void Start()
    {
        cam = Camera.main;
        linePath = GameManager.instance.GetComponent<LinePath>();

        if (SceneManager.GetActiveScene().buildIndex == 0)
            return;

        currentTile = new Vector3Int(100, 100, 100);

        Invoke(nameof(SetFirstSearch), 0.5f);
    }

    void SetFirstSearch()
    {
        AstarPath.active.Scan();
        GameManager.instance.aiPathCustom.SearchPath();

        Invoke(nameof(SetFirstLine), 0.5f);
    }

    void SetFirstLine()
    {
        linePath.ResetLine();
    }

    void Update()
    {
        tileHovered = GetTileFromMousePosition();

        if (tileHovered.y <= -5) return;

        if (!tileHovered.Equals(previousTileHovered))
        {
            tilemapHighlight.SetTile(tileHovered, tileHighlight);

            tilemapHighlight.SetTile(previousTileHovered, null);
            previousTileHovered = tileHovered;
        }

        if (Input.GetMouseButtonDown(0))
        {
            PlaceObstacle();
            currentTile = tileHovered;
        }

        if (Input.GetMouseButton(0))
        {
            KeepPlacingOrRemoving();
        }

        if (Input.GetMouseButtonUp(0))
        {
            currentTile = new Vector3Int(100, 100, 100);
            linePath.ResetLine();
        }
    }

    private void FixedUpdate()
    {
        if (isObstacleSet)
        {
            AstarPath.active.Scan();
            GameManager.instance.aiPathCustom.SearchPath();

            // TODO: Check if no path are available;

            isObstacleSet = false;
        }
    }

    private void PlaceObstacle()
    {
        if (tilemapObstaclesPlaced.HasTile(tileHovered))
        {
            RemoveTile();
        }
        else
        {
            if(GameManager.instance.remainingTiles <= 0) //If we run out of tiles
            {
                // TODO: Highlight UI zero tiles available
                return;
            }

            PlaceTile();
            UIManager.instance.ShowTipsPanel();
        }
    }


    IEnumerator SetForbiddenPlaceTile(Vector3Int tileHov)
    {
        AudioManager.Instance.Play("ErrorPlaceTile");

        CinemachineShake.Instance.ShakeCamera(shakeIntensity, shakeTime);

        tilemapHighlight.SetTile(tileHov, tileHighlightOccupiedSpace); // TODO: Too quick?

        yield return new WaitForSeconds(timeShowingTile);

        tilemapHighlight.SetTile(tileHov, null);
    }

    private void PlaceTile()
    {
        foreach (var tm in tilemapsToCheckForPlacingTile)
        {
            if (tm.HasTile(tileHovered))
            {
                StartCoroutine(nameof(SetForbiddenPlaceTile), tileHovered);

                UIManager.instance.ShowTipsPanel();
                return;
            }
        }

        AudioManager.Instance.Play("PlaceTile");

        tilemapObstaclesPlaced.SetTile(tileHovered, tileToPlace);
        isObstacleSet = true;

        GameManager.instance.remainingTiles--;
        UIManager.instance.UpdateTileCountText(GameManager.instance.remainingTiles);
    }

    private void RemoveTile()
    {
        AudioManager.Instance.Play("RemoveTile");

        tilemapObstaclesPlaced.SetTile(tileHovered, null);
        isObstacleSet = true;

        GameManager.instance.remainingTiles++;
        UIManager.instance.UpdateTileCountText(GameManager.instance.remainingTiles);
    }

    private void KeepPlacingOrRemoving()
    {
        if (currentTile == tileHovered) return;

        if (tilemapObstaclesPlaced.HasTile(tileHovered))
        {
            RemoveTile();
            currentTile = tileHovered;
        } else
        {
            if (GameManager.instance.remainingTiles <= 0) return;

            PlaceTile();
            currentTile = tileHovered;
        }
    }

    private Vector3Int GetTileFromMousePosition()
    {
        Vector3 mousePosWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosFromMousePos = tilemapObstaclesPlaced.WorldToCell(new Vector3(mousePosWorld.x, mousePosWorld.y, tilemapObstaclesPlaced.transform.position.z));

        return cellPosFromMousePos;
    }
}
