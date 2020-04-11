using UnityEngine;

[CreateAssetMenu(fileName = "World Settings", menuName = "Scriptable Objects/World Settings")]
public class WorldSettings_SO : ScriptableObject {

    [SerializeField]
    private Transform _leftMapTransform = null;
    [SerializeField]
    private Transform _rightMapTransform = null;
    [SerializeField]
    private Transform _leftMapBorderTransform = null;
    [SerializeField]
    private Transform _rightMapBorderTransform = null;
    [SerializeField]

    public Transform MapLeftSpawnTransform { 
        get { 
            return _leftMapTransform; 
        } 
        set {
            _leftMapTransform = value;
        }
    }

    public Transform MapRightSpawnTransform { 
        get { 
            return _rightMapTransform; 
        }
        set {
            _rightMapTransform = value;
        }
    }

    public Transform MapLeftBorderTransform {
        get {
            return _leftMapBorderTransform;
        }
        set {
            _leftMapBorderTransform = value;
        }
    }

    public Transform MapRightBorderTransform {
        get {
            return _rightMapBorderTransform;
        }
        set {
            _rightMapBorderTransform = value;
        }
    }

}
