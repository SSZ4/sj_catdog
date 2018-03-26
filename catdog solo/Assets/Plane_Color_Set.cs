using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane_Color_Set : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().material.color = UnityEngine.Color.blue;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
