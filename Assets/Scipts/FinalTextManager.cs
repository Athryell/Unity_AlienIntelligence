using UnityEngine;
using TMPro;

public class FinalTextManager : MonoBehaviour
{
    public TMP_Text winText;
    public TMP_Text winSubText;
    public TMP_Text playerScore;
    public TMP_Text retryBtnText;
    public TMP_Text quitBtnText;
    public TMP_Text creditsText;
    public TMP_Text youtubeText;
    public TMP_Text infoText;
    public TMP_Text letmeknowText;

    void Start()
    {
        if(LanguageManager.Instance != null)
        {
            SetEndScreenLanguage(LanguageManager.Instance.isItalian);
        }
    }

    private void SetEndScreenLanguage(bool isItalian )
    {
        GetPlayerGrade(isItalian);

        if (isItalian)
        {
            winText.text = "Grande!";
            winSubText.text = "Grazie per aver giocato!";
            playerScore.text = "Valutazione: " + GetPlayerGrade(isItalian) +
                               "\nHai usato " + PlayerScore.Instance.HelpRequests + " aiuti.";
            retryBtnText.text = "Fammi fare un altro giro";
            quitBtnText.text = "Ho un sacco di cose da fare\n" +
                               "e devo provare altri giochi della jam..\n" +
                               "Però grazie! E' stato superdivertente!\n" +
                               "Alla prossima! [ESCI]";
            creditsText.text = "Fantastici asset esagonali\nda Kenney: www.kenney.nl\n\n(Mi ha salvato la vita!)";
            youtubeText.text = "Fammi un saluto su youtube:\nHAPPY DUNGEON\n Un piccolo e amichevole canale italiano,\ncon la passione per il Game Design :)";
            letmeknowText.text = " Se hai risolto alcuni puzzle senza usare tutti i pezzi,\nFammelo sapere!";
            infoText.text = "Questa è stata la mia seconda Jam. E anche questa volta è stato super divertente!\n\n" +
                            "Ho avuto poco più di 2 giorni per lavorare su questo gioco ma mi sono detto \"basta scuse\", quindi ho fatto quello che ho potuto e da ora in avanti sarò più costante!\n\n" +
                            "Ho imparato un sacco di cose nuove: ho usato e manipolato una tilemap e ho imparato come funziona l'algoritmo di pathfinding A*.\n\n" +
                            "Alla prossima Game Jam!";
        }
        else
        {
            playerScore.text = "Grade: " + GetPlayerGrade(isItalian) +
                               "\nYou used " + PlayerScore.Instance.HelpRequests + " helps.";
        }
    }

    private string GetPlayerGrade(bool isItalian)
    {

       switch (PlayerScore.Instance.HelpRequests)
       {
            case 0:
                if (isItalian) return "Ottimo";
                return "Awesome";
            case <= 2:
                if (isItalian) return "Molto bene";
                return "Very good";
            case <= 4:
                if (isItalian) return "Buono";
                return "Good";
            case <= 10:
                if (isItalian) return "Non male";
                return "Not bad";
            case > 10:
                if (isItalian) return "Ehm...";
                return "Ehm...";
       }
    }
}
