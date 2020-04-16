using UnityEngine;

[System.Serializable]
public class ClothDownData {

    [SerializeField]
    private ClothDownTypes _clothDownType;
    [SerializeField]
    private Vector2 _clothColor;

    public ClothDownTypes ClothDownType { get => _clothDownType; set => _clothDownType = value; }
    public Vector2 ClothColor { get => _clothColor; set => _clothColor = value; }
}
