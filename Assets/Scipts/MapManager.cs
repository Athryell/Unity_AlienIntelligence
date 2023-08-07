using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    public Vector2Int boardSize = new(20, 10);

    [SerializeField] private List<TileData> tileDatas;

    public Dictionary<TileBase, TileData> dataFromTiles;

    private GridGraph graph;
    private List<Vector3> nodeList = new List<Vector3>();
    private LineRenderer lineRenderer;

    public List<GraphNode> connections = new List<GraphNode>();
    public Seeker seeker;


    public RectInt Bounds
    {
        get
        {
            return new RectInt(Vector2Int.zero, boardSize);
        }
    }

    private void Awake()
    {
        instance = this;

        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach(var tileData in tileDatas)
        {
            foreach (var tile in tileData.tile)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }
    }

    private void Start()
    {
        graph = AstarPath.active.data.gridGraph;
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetLinePositions()
    {
        lineRenderer.positionCount = 0;

        graph.GetNodes(node => {
            nodeList.Add((Vector3)node.position);
            node.GetConnections(connections.Add);
        });

        lineRenderer.positionCount = connections.Count;
        for (int i = 0; i < connections.Count; i++)
        {
            lineRenderer.SetPosition(i, (Vector3)connections[i].position);
        }
    }
}
