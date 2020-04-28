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

    public void SetOffset(int row, int column) {
        float uvCoordX = (column * _tileWidth) / _baseMapWidth;
        float uvCoordY = (row * _tileHeight) / _baseMapHeigth;

        if (_inverseX) {
            uvCoordX *= -1;
        }
        if (_inverseY) {
            uvCoordY *= -1;
        }

        _selectedUVCoordinate = new Vector2(uvCoordX, uvCoordY);
        _meshRenderer.material.SetTextureOffset("_BaseMap", _selectedUVCoordinate);
    }

}