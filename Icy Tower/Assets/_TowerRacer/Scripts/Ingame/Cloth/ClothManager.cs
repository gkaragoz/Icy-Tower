using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothManager:MonoBehaviour
{
   public ClothS_SO clothData;
   public Renderer _myRenderer;


    public void SetColor(int value)
    {
        _myRenderer.material = clothData.ObjectMaterial;
        _myRenderer.material.SetVector("OFFSETREF", clothData.ColorOfsets[value].colorOfset);
    }

}


