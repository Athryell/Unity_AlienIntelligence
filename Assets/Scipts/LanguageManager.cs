using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;

    public bool isItalian = false;

    private TMP_Text noMoreTilesText;
    private TMP_Text askForExtraTiles;
    private TMP_Text stringTextForTilesLeft;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public void SetupGameText()
    {

        noMoreTilesText = UIManager.instance.noMoreTilesText.GetComponent<TMP_Text>();
        askForExtraTiles = UIManager.instance.askForExtraTiles.GetComponent<TMP_Text>();
        stringTextForTilesLeft = noMoreTilesText.GetComponentInParent<TMP_Text>();

        if (isItalian)
        {
            stringTextForTilesLeft.text = "TILES";
            noMoreTilesText.text = "TOGLINE\nUN PO'!";

            askForExtraTiles.text = "DAMMI UNA MANO!\n+1 TILE\n(-1 punteggio)";
        }
    }

    public void ChooseItalianLanguage()
    {
        isItalian = true;
    }


    public string GetTutorialText(int level)
    {
        return GetTranslatedText(level, true);
    }

    public string GetHintText(int level)
    {
        return GetTranslatedText(level, false);
    }



    private string GetTranslatedText(int level, bool isTutorial)
    {
        string tutorialText = "";
        string hintText = "";

        if (level == 1)
        {
            if (isItalian)
            {
                tutorialText = "TILLY L'ALIENO SI E' PERSO MA E' MOLTO INTELLIGENTE:\n\n" +
                               "RIESCE SEMPRE A RITROVARE I SUOI AMICI...";
                hintText = "PER QUESTA VOLTA PUOI PREMERE \"GO!\" E GUARDARE TILLY SEGUIRE IL PERCORSO :) ";
            }
            else
            {
                tutorialText = "TILLY THE ALIEN IS LOST BUT QUITE CLEVER:\n\n" +
                               "HE ALWAYS FINDS THE BEST ROUTE TO HIS FRIEND'S CAMP...";
                hintText = "FOR TODAY YOU CAN JUST PRESS \"GO!\" AND SEE TILLY FOLLOWING THE ROUTE :)";
            }
        }
        else if (level == 2)
        {
            if (isItalian)
            {
                tutorialText = "MA TILLY E' INGENUO E SI SCORDA DI STARE ALLA LARGA DAGLI UMANI\n\n" +
                               "PIAZZA DELLE MONTAGNE PER MODIFICARE LA STRADA DI TILLY.";
                hintText = "";
            }
            else
            {
                tutorialText = "BUT TILLY IS NAIVE AND ALWAYS FORGETS TO STAY AWAY FROM HUMANS!\n\n" +
                               "CHANGE TILLY'S SHORTEST PATH BY PLACING SOME MORE MOUNTAINS.";
                hintText = "";
            }
        }
        else if (level == 3)
        {
            if (isItalian)
            {
                tutorialText = "";
                hintText = "A VOLTE AVRAI RISORSE LIMITATE: LE TESSERE RIMANENTI SONO NELL'ANGOLO IN ALTO A SINISTRA :)";
            }
            else
            {
                tutorialText = "";
                hintText = "SOMETIMES YOU HAVE LIMITED RESOURCES: YOUR REMAINING TILES ARE IN THE TOP LEFT CORNER :)";
            }
        }
        else if (level == 5)
        {
            if (isItalian)
            {
                tutorialText = "DOVREMMO... ANZI dobbiamo FAR VISITA AGLI AMICI DI TILLY!";
                hintText = "";
            }
            else
            {
                tutorialText = "WE NEED.. I MEAN WE MUST VISIT TILLY'S FRIENDS TOO!";
                hintText = "";
            }
        } else if(level == 6)
        {
            if (isItalian)
            {
                tutorialText = "SI', LORO SONO VICINI AGLI UMANI MA SANNO COME PRENDERSI CURA DI SE STESSI. (EHM... TRAMA PIU' PROFONDA NEL DLC...)";
                hintText = "";
            }
            else
            {
                tutorialText = "YES, THEY ARE CLOSE TO HUMANS BUT THEY KNOW HOW TO TAKE CARE OF THEMSELVES. (EHM... DEEPER STORY IN THE DLC...)";
                hintText = "";
            }
        }

        if (isTutorial)
        {
            return tutorialText;
        }
        else if (!isTutorial) {
            return hintText;
        }
        else
        {
            return "";
        }
    }
}
