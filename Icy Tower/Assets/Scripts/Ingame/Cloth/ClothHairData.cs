using UnityEngine;

[System.Serializable]
public class ClothHairData {

    [SerializeField]
    private ClothHairTypes _clothHairType;
    [SerializeField]
    private Vector2 _clothColor = Vector2.zero;

    public ClothHairTypes ClothHairType { get => _clothHairType; set => _clothHairType = value; }

    public Vector2 ClothColor { get => _clothColor; set => _clothColor = value; }
}
