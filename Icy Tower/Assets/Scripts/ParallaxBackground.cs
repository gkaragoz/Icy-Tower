using UnityEngine;

public class ParallaxBackground : MonoBehaviour {

    [SerializeField]
    private Vector2 _parallaxEffectMultiplier = Vector2.zero;

    private float _textureUnitSizeY;
    private float _offsetPositionY;
    private Vector3 _lastCameraPosition;
    private Vector3 _deltaMovement;

    private Camera _camera;

    private void Awake() {
        _camera = Camera.main;
    }

    private void Start() {
        _lastCameraPosition = _camera.transform.position;

        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        _textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
    }

    private void LateUpdate() {
        _deltaMovement = _camera.transform.position - _lastCameraPosition;
        transform.position += new Vector3(_deltaMovement.x * _parallaxEffectMultiplier.x, _deltaMovement.y * _parallaxEffectMultiplier.y, transform.position.z);
        _lastCameraPosition = _camera.transform.position;

        if (Mathf.Abs(transform.position.y - _camera.transform.position.y) > _textureUnitSizeY) {
            _offsetPositionY = (_camera.transform.position.y - transform.position.y) % _textureUnitSizeY;
            transform.position = new Vector3(transform.position.x, _camera.transform.position.y + _offsetPositionY);
        }
    }
}
