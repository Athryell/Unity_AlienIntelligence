using UnityEngine;

public class CollectiblesManager : MonoBehaviour
{
    public static CollectiblesManager Instance { get; private set; }
    

    private void Awake()
    {
        Instance = this;
    }

}
