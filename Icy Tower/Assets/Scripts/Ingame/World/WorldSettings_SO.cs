using UnityEngine;

[CreateAssetMenu(fileName = "World Settings", menuName = "Scriptable Objects/World Settings")]
public class WorldSettings_SO : ScriptableObject {

    [SerializeField]
    private Transform _leftMapTransform = null;
    [SerializeField]
    private Transform _rightMapTransform = null;
    [SerializeField]
    private float _wallHeight= 0;

    public Transform MapLeftBorder { 
        get { 
            return _leftMapTransform; 
        } 
        set {
            _leftMapTransform = value;
        }
    }

    public Transform MapRightBorder { 
        get { 
            return _rightMapTransform; 
        }
        set {
            _rightMapTransform = value;
        }
    }

    public float WallHeight {
        get {
            return _wallHeight;
        }
        set {
            _wallHeight = value;
        }
    }

}
