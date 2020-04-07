using UnityEngine;

public class MeshUVChanger : MonoBehaviour {

    [Header("Initializations")]
    [SerializeField]
    private MeshRenderer _meshRenderer = null;
    [SerializeField]
    private bool _inverseX = false;
    [SerializeField]
    private bool _inverseY = false;
    [SerializeField]
    private int _row = 0;
    [SerializeField]
    private int _column = 0;
    [SerializeField]
    private UVTextureMap _textureMap;

    [SerializeField]
    private int _rowInput = 0;
    [SerializeField]
    private int _columnInput = 0;


    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private Texture _baseMapTexture = null;
    [SerializeField]
    [Utils.ReadOnly]
    private float _baseMapWidth = 0;
    [SerializeField]
    [Utils.ReadOnly]
    private float _baseMapHeigth = 0;
    [SerializeField]
    [Utils.ReadOnly]
    private float _tileWidth = 0;
    [SerializeField]
    [Utils.ReadOnly]
    private float _tileHeight = 0;
    [SerializeField]
    [Utils.ReadOnly]
    private Vector2 _selectedUVCoordinate = Vector2.zero;

    private void Awake() {
        _baseMapTexture = _meshRenderer.material.GetTexture("_BaseMap");
        _baseMapWidth = _baseMapTexture.width;
        _baseMapHeigth = _baseMapTexture.height;

        _tileWidth = _baseMapWidth / _column;
        _tileHeight = _baseMapHeigth / _row;
    }

    private void Update() {
        float uvCoordX = (_columnInput * _tileWidth) / _baseMapWidth;
        float uvCoordY = (_rowInput * _tileHeight) / _baseMapHeigth;

        if (_inverseX) {
            uvCoordX *= -1;
        }
        if (_inverseY) {
            uvCoordY *= -1;
        }

        _selectedUVCoordinate = new Vector2(uvCoordX, uvCoordY);

        SetOffset(_selectedUVCoordinate);
    }

    public void SetOffset(Vector2 offset) {
        _meshRenderer.material.SetTextureOffset("_BaseMap", offset);
    }

}

[System.Serializable]
public class UVTextureMap {
    
    [SerializeField]
    [Utils.ReadOnly]
    public int ID;
    [SerializeField]
    [Utils.ReadOnly]
    public Vector2 coordinate;

    public UVTextureMap(int id, float posX, float posY) {
        this.ID = id;
        this.coordinate = CalculateCoordinate(posX, posY);
    }

    private Vector2 CalculateCoordinate(float posX, float posY) {
        return Vector2.zero;
    }

}