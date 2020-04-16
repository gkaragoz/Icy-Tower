using UnityEngine;

[System.Serializable]
public class ClothAccesoryData {

    [SerializeField]
    private ClothAccesoryTypes _clothAccesoryTypes;
    [SerializeField]
    private Vector2 _clothColor;

    public ClothAccesoryTypes ClothAccesoryTypes { get => _clothAccesoryTypes; set => _clothAccesoryTypes = value; }
    public Vector2 ClothColor { get => _clothColor; set => _clothColor = value; }
}
