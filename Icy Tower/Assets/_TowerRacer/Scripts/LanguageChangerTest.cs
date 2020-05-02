using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LocalizationSystem;

public class LanguageChangerTest : MonoBehaviour
{
    public Language somethin;

    private void Start()
    {
        language = somethin;
    }
}
