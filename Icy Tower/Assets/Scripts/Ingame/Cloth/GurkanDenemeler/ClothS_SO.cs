using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Character Clothes", menuName = "Scriptable Objects/Clothes")]
public class ClothS_SO : ScriptableObject
{
   
    [SerializeField]
    private Material _material ;

    [SerializeField]
    private ColorNameOfset[] colorOfsets;

    public Material ObjectMaterial
    {
        get { return _material; }
        set { _material = value; }
    }

    public ColorNameOfset[] ColorOfsets
    {
        get { return colorOfsets; }
        set { colorOfsets = value; }
    }

}
