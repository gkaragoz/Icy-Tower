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
       // _myRenderer.material.SetTextureOffset("_Vector2_B985E7DD", clothData.ColorOfsets[value].colorOfset);
        Debug.Log("My Color is : "+ clothData.ColorOfsets[value].colorName);
    }

}


