using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TMP_Text numberLeftTiles;
    public GameObject noMoreTilesText;
    public GameObject askForExtraTiles;
    public bool useTips = false;
    public GameObject tipsText;
    public int clickToRevealTips = 3;
    private int clickOnWrongTiles = 0;

    public TMP_Text currentLevelText;
    public TMP_Text tutorialText;

    private LanguageManager lm;

    private void Awake()
    {
        if (instance != null) return;
        instance = this;
    }

    private void Start()
    {
        lm = LanguageManager.Instance;

        if (SceneManager.GetActiveScene().buildIndex == 0) return;

        lm.SetupGameText();

        noMoreTilesText.SetActive(false);

        if(tipsText != null)
        {
            tipsText.SetActive(false);
        }

        currentLevelText.text = (SceneManager.GetActiveScene().buildIndex - 1).ToString("00");

        if(tutorialText != null)
        {
            tutorialText.text = lm.GetTutorialText(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void ShowTipsPanel()
    {
        if (!useTips) return;

        clickOnWrongTiles++;

        if (clickOnWrongTiles >= clickToRevealTips)
        {
            tipsText.GetComponent<TMP_Text>().text = lm.GetHintText(SceneManager.GetActiveScene().buildIndex);

            tipsText.SetActive(true);
        }
    }

    public void UpdateTileCountText(int numOfTiles)
    {
        if (numOfTiles <= 0)
        {
            noMoreTilesText.SetActive(true);
        } else
        {
            noMoreTilesText.SetActive(false);
        }

        numberLeftTiles.text = numOfTiles.ToString();
    }
}
