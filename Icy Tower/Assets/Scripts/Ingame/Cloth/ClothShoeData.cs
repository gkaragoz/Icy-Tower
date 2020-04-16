using UnityEngine;

[System.Serializable]
public class ClothShoeData {

    [SerializeField]
    private ClothShoeTypes _clothShoeType;
    [SerializeField]
    private Vector2 _clothColor;

    public ClothShoeTypes ClothShoeType { get => _clothShoeType; set => _clothShoeType = value; }
    public Vector2 ClothColor { get => _clothColor; set => _clothColor = value; }
}
