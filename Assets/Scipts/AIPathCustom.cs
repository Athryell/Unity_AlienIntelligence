using UnityEngine;
using Pathfinding;

public class AIPathCustom : AIPath
{
    public int GetPathLength()
    {
        return path.vectorPath.Count;
    }

    public Vector3 GetPathPosition(int theIndex)
    {
        return path.vectorPath[theIndex];
    }
}
