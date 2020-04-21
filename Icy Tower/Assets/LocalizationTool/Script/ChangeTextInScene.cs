using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LocalizationSystem;

public class ChangeTextInScene : MonoBehaviour
{
   public void ChangeLanguageToTr()
    {
        Language lang = Language.Turkish;
        language = lang;

        TextLocaliserUI[] allTexts = GameObject.FindObjectsOfType<TextLocaliserUI>();
        foreach (var text in allTexts)
        {
            text.ChangeText();
        }
    }


    public void ChangeLanguageToEng()
    {
        Language lang = Language.English;
        language = lang;

        TextLocaliserUI[] allTexts = GameObject.FindObjectsOfType<TextLocaliserUI>();
        foreach (var text in allTexts)
        {
            text.ChangeText();
        }
    }

    public void ChangeLanguageToFr()
    {
        Language lang = Language.French;
        language = lang;

        TextLocaliserUI[] allTexts = GameObject.FindObjectsOfType<TextLocaliserUI>();
        foreach (var text in allTexts)
        {
            text.ChangeText();
        }
    }

}
