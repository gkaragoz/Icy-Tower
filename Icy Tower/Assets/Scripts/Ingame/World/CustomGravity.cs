using UnityEngine;

public class CustomGravity : MonoBehaviour {

    public static float globalGravity = -9.81f;

    Rigidbody m_rb;

    void OnEnable() {
        m_rb = GetComponent<Rigidbody>();
        m_rb.useGravity = false;
    }

    void FixedUpdate() {
        Vector3 gravity = globalGravity * GameManager.instance.GetGravityScale() * Vector3.up;
        m_rb.AddForce(gravity, ForceMode.Acceleration);
    }
}
