using UnityEngine;

[CreateAssetMenu(fileName = "StartingFloor Stats", menuName = "Scriptable Objects/Collectables/StartingFloor Stats")]
public class StartingFloorStats_SO : ScriptableObject {

    [SerializeField]
    private string _name = "Starting Floor";

    [SerializeField]
    private int _level = 0;

    [SerializeField]
    private int _floorMultiplier = 10;

    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    public int Level {
        get { return _level; }
        set { _level = value; }
    }

    public int FloorMultiplier {
        get { return _floorMultiplier; }
        set { _floorMultiplier = value; }
    }
}
    

