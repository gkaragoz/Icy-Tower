using UnityEngine;

[System.Serializable]
public class ClothUpData {

    [SerializeField]
    private ClothUpTypes _clothUpType;
    [SerializeField]
    private Vector2 _clothColor;

    public ClothUpTypes ClothUpType { get => _clothUpType; set => _clothUpType = value; }
    public Vector2 ClothColor { get => _clothColor; set => _clothColor = value; }
}

