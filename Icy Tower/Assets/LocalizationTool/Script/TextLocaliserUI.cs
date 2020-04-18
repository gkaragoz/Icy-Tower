using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextLocaliserUI : MonoBehaviour
{
   TextMeshProUGUI textField;
  public LocalisedString localisedString;

    // Start is called before the first frame update
    void Start()
    {
        textField = GetComponent<TextMeshProUGUI>();
        textField.text = localisedString.value;
    }

  
}
