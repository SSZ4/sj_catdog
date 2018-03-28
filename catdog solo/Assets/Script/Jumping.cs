using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour {
	public float up = 50.0f;
	public float right = 10.0f;
	private Rigidbody rigid;
	
	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody>();		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnCollisionEnter(Collision other)
	{
		
		
		if (other.transform.tag == "Plane")
		{
			//rigid.velocity = new Vector3(0.0f, 0.0f, 0.0f); //힘 초기화
			rigid.AddForce(Vector3.up * up);
			rigid.AddForce(transform.right * right);

			transform.Rotate(transform.up * 270.0f * Time.deltaTime);
			//rigid.AddForce(Vector3.forward * 6.0f); //addforce로 위와 앞방향으로 힘을 준다면 -> 방향전환후에도 그 방향이 아닌 계속 축을 기준으로 앞방향으로 가는것 같음
													 // 앞으로 가는데 오른쪽 혹은 왼쪽으로 힘을 주게되면 원래 가게되는 거리보다 조금 더 이동하지않나
		}
		
	
	}
}
