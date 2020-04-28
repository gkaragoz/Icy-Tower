using UnityEngine;

public class Move : MonoBehaviour {

    public float speed = 5f;

    private void Update() {
        Camera.main.transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

}
