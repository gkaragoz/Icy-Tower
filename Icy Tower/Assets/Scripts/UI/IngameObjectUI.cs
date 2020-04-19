using UnityEngine;
using UnityEngine.Events;

public class IngameObjectUI : MonoBehaviour {

    public UnityEvent unityEvent;

    public void OnClick() {
        unityEvent.Invoke();
    }

}
