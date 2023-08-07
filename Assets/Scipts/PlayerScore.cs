using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScore : MonoBehaviour
{
    public static PlayerScore Instance;

    public int HelpRequests { get; private set; }

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

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            HelpRequests = 0;
        }
    }

    public void AddHelpRequest()
    {
        HelpRequests++;
    }
}
