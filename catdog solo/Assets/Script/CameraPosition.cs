using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour {
	Transform SpherePosition;
	float offsetX = -5.0f;
	float offsetY = 1.0f;
	float offsetZ = 0.0f;
	// Use this for initialization
	void Start () {
		SpherePosition = GameObject.FindGameObjectWithTag("Mover").transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = SpherePosition.position + (new Vector3(offsetX, offsetY, offsetZ));
	}
}
