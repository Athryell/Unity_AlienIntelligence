using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;

    public bool isNewLevel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }
}
