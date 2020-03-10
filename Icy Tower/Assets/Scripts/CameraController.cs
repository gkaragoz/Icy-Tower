using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class CameraController : MonoBehaviour {
    [SerializeField]
    private Transform _target = null;
    [SerializeField]
    private float _offset = 0f;
    [SerializeField]
    private float _cameraSpeed = 0f;
    [SerializeField]
    private float _smoothSpeed = 0.0125f;

    public Camera mainCam;


    void LateUpdate() {
       
        //Debug.Log(mainCam.WorldToScreenPoint(_target.transform.position).x.Map(0, 1080,0,100));
        if (_target.position.y + _offset > transform.position.y) {

            Vector3 newPos = new Vector3(transform.position.x, _target.position.y + _offset, transform.position.z);
            Vector3 smoothedPos = Vector3.Lerp(transform.position, newPos, _smoothSpeed);
            transform.position = smoothedPos;
        } else {
            transform.Translate(Vector3.up * Time.deltaTime * _cameraSpeed);

        }
    }

   

}
