using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour {

    [SerializeField]
    private Transform _cameraTransform = null;
    [SerializeField]
    private Vector2 _parallaxEffectMultiplier = Vector2.zero;

    private float _textureUnitSizeY;
    private float _offsetPositionY;
    private Vector3 _lastCameraPosition;
    private Vector3 _deltaMovement;

    void Start() {
        _lastCameraPosition = _cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        _textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void LateUpdate() {
        _deltaMovement = _cameraTransform.position - _lastCameraPosition;
        transform.position += new Vector3(_deltaMovement.x * _parallaxEffectMultiplier.x, _deltaMovement.y * _parallaxEffectMultiplier.y, transform.position.z);
        _lastCameraPosition = _cameraTransform.position;

        if (Mathf.Abs(transform.position.y - _cameraTransform.position.y) > _textureUnitSizeY) {
            _offsetPositionY = (_cameraTransform.position.y - transform.position.y) % _textureUnitSizeY;
            transform.position = new Vector3(transform.position.x, _cameraTransform.position.y + _offsetPositionY);
        }
    }
}
