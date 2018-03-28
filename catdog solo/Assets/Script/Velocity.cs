using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity : MonoBehaviour {
	Rigidbody rigid;
	private Vector3 target_position;
	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("a"))
		{
			transform.Rotate(new Vector3(0.0f, 15.0f, 0.0f), Space.Self);
			//rigid.velocity = Vector3.zero;
			rigid.velocity = GetVelocity(transform.position, target_position + new Vector3(-1.0f, 0.0f, 1.0f), 0.0f);
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if(other.transform.tag == "Plane")
		{
			rigid.velocity = Vector3.zero;
			target_position = transform.position + new Vector3(1.9f, 0.0f, 0.0f);
			rigid.velocity = GetVelocity(transform.position, transform.position + new Vector3(1.9f, 0.0f, 0.0f), 75f);
			
		}
	}


	Vector3 GetVelocity(Vector3 currentPos, Vector3 targetPos, float initialAngle)
	{
		float gravity = Physics.gravity.magnitude;
		float angle = initialAngle * Mathf.Deg2Rad;

		Vector3 planarTarget = new Vector3(targetPos.x, 0, targetPos.z);
		Vector3 planarPosition = new Vector3(currentPos.x, 0, currentPos.z);

		float distance = Vector3.Distance(planarTarget, planarPosition);
		float yOffset = currentPos.y - targetPos.y;

		float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

		Vector3 velocity = new Vector3(0f, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

		float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPosition) * (targetPos.x > currentPos.x ? 1 : -1);
		Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;
		finalVelocity = transform.InverseTransformVector(finalVelocity); // 자신의 각도 참조

		return finalVelocity;
	}

}
