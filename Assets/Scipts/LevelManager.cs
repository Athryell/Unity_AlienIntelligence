using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public float transitionTimeDefault = 1f;
    public float transitionTimeFast = 0.3f;

    private Animator transitionAnim;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        transitionAnim = GameObject.FindGameObjectWithTag("Transition").GetComponent<Animator>();

        if (TransitionManager.Instance.isNewLevel && SceneManager.GetActiveScene().buildIndex != 0)
        {
            transitionAnim.SetTrigger("isNewLevel");
        }
    }

    public void ResetLevel()
    {
        TransitionManager.Instance.isNewLevel = false;
        transitionAnim.SetBool("isLevelCompleted", false);

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex, transitionTimeFast));
    }

    public void LoadNextLevel()
    {
        TransitionManager.Instance.isNewLevel = true;
        transitionAnim.SetBool("isLevelCompleted", true);

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1, transitionTimeDefault));
    }

    public void RestartGame()
    {
        TransitionManager.Instance.isNewLevel = true;
        transitionAnim.SetBool("isLevelCompleted", true);

        StartCoroutine(LoadLevel(1, transitionTimeDefault));
    }

    public void QuitGame()
    {
        Debug.Log("Game quitted!");
        Application.Quit();
    }

    private IEnumerator LoadLevel(int levelIndex, float transitionTime)
    {
        transitionAnim.SetTrigger("StartTransition");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);

    }
}
