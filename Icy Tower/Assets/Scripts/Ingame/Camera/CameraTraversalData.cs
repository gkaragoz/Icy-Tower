using UnityEngine;

[System.Serializable]
public class CameraTraversalData {
    public string name;
    public Vector3 position;
    public Vector3 rotation;
    public float positionTimeWeight;
    public float rotationTimeWeight;
    public LeanTweenType easeType;

    [SerializeField]
    [Utils.ReadOnly]
    private float _positionFlyTime = 0f;
    [SerializeField]
    [Utils.ReadOnly]
    private float _rotationFlyTime = 0f;

    public void SetPositionFlyTime(float time) {
        _positionFlyTime = time;

        if (_positionFlyTime == 0) {
            _positionFlyTime = 0.01f;
        }
    }
    public void SetRotationFlyTime(float time) {
        _rotationFlyTime = time;
        
        if (_rotationFlyTime == 0) {
            _rotationFlyTime = 0.01f;
        }
    }
}