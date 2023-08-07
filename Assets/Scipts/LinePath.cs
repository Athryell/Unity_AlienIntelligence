using UnityEngine;

public class LinePath : MonoBehaviour
{
    public AIPathCustom playerAIPath;
    public LineRenderer lineRenderer;
    public bool isLineDrawn = false;

    void Start()
    {
        if (GameManager.instance.player != null)
        {
            playerAIPath = GameManager.instance.player.GetComponent<AIPathCustom>();
        }
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void ResetLine()
    {
        if (playerAIPath.hasPath)
        {
            lineRenderer.positionCount = playerAIPath.GetPathLength();

            for (int i = 0; i < playerAIPath.GetPathLength(); i++)
            {
                lineRenderer.SetPosition(i, playerAIPath.GetPathPosition(i));
            }

            isLineDrawn = true;
        }
        else
        {
            Debug.LogError("no path", gameObject);
        }
    }
}
