using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField]
	private Transform _target;
	[SerializeField]
	private float _offset = 0f;

	void LateUpdate() {
		if (_target.position.y + _offset > transform.position.y) {

			Vector3 newPos = new Vector3(transform.position.x, _target.position.y + _offset, transform.position.z);
			transform.position = Vector3.Lerp(transform.position , newPos,10f);
		}
	}
}
