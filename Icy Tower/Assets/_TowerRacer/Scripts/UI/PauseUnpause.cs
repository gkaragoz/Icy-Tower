using UnityEngine;

public class PauseUnpause : MonoBehaviour {
    
    public void Pause() {
        Time.timeScale = 0;
    }

    public void Unpuase() {
        Time.timeScale = 1;
    }

}
