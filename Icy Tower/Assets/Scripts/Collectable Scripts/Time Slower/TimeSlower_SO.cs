using UnityEngine;

[CreateAssetMenu(fileName = "TimeSlower Stats", menuName = "Scriptable Objects/TimeSlower Stats")]
public class TimeSlower_SO : ScriptableObject{

    [SerializeField]
    private string _name = "Time Slower";

    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private float _duration;

    [SerializeField]
    private float _scrollSpeed;

    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    public GameObject Prefab {
        get { return _prefab; }
        set { _prefab = value; }
    }

    public float Duration {
        get { return _duration; }
        set { _duration = value; }
    }

    public float ScrollSpeed {
        get { return _scrollSpeed; }
        set { _scrollSpeed = value; }
    }
}
