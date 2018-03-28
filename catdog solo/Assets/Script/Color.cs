using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color : MonoBehaviour {
	private float i = 0.0f;
	// Use this for initialization
	void Start () {
		transform.GetComponent<Renderer>().material.color = UnityEngine.Color.white;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnCollisionEnter(Collision other)
	{
		if (i == 0.0f)
		{			
			if (other.transform.tag == "Plane") // 태그는 임시로 설정함.
			{
				float or = other.transform.GetComponent<Renderer>().material.color.r;
				float og = other.transform.GetComponent<Renderer>().material.color.g;
				float ob = other.transform.GetComponent<Renderer>().material.color.b;

				ColorLerp(or, og, ob); // 색상 혼합 시작 ~ 완료

				is_Black(); // 색상 변환 이후 검은색이 되었는지 확인
							//transform.GetComponent<Renderer>().material.color = new Vector4(other.transform.GetComponent<Renderer>().material.color.r, other.transform.GetComponent<Renderer>().material.color.g, other.transform.GetComponent<Renderer>().material.color.b, 1.0f);
				other.transform.GetComponent<Renderer>().material.color = UnityEngine.Color.yellow; //부딪힌 물체 색상 하얀색으로
				i = 1.0f;
			}
			
		}
	}
	private void OnCollisionExit(Collision collision)
	{
		i = 0.0f;
	}

	void ColorLerp(float or, float og, float ob)
	{
		float r = transform.GetComponent<Renderer>().material.color.r;
		float g = transform.GetComponent<Renderer>().material.color.g;
		float b = transform.GetComponent<Renderer>().material.color.b;

		float k = RtoK(r, g, b);
		float ok = RtoK(or, og, ob);

		float c = RtoC(k, r);
		float oc = RtoC(ok, or);

		float m = RtoM(k, g);
		float om = RtoM(ok, og);

		float y = RtoY(k, b);
		float oy = RtoY(ok, ob);

		float nk = k+ok - k*ok; //감산혼합 공식을 몰라서 임의로 대충 때려박음
		float nc = c+oc - c*oc;
		float nm = m+om - m*om;
		float ny = y+oy - y*oy;
		
		transform.GetComponent<Renderer>().material.color = new UnityEngine.Color(CtoR(nk, nc), CtoG(nk, nm), CtoB(nk, ny), 1.0f); // 색상 변환 완료
	}

	float RtoK(float r, float g, float b) // K값
	{
		float tmp = 0.0f;
		if (r >= g && r >= b)
			tmp = r;
		else if (g >= b && g >= r)
			tmp = g;
		else
			tmp = b;
		
		return 1 - tmp;
	}

	float RtoC(float k, float r) {
		return (1 - k - r) / (1 - k);
	}

	float RtoM(float k, float g)
	{
		return (1 - k - g) / (1 - k);
	}

	float RtoY(float k, float b)
	{
		
		return (1 - k - b) / (1 - k);
	}


	float CtoR(float nk, float nc)
	{
		return (1 - nc) * (1 - nk);
	}

	float CtoG(float nk, float nm)
	{
		return (1 - nm) * (1 - nk);
	}

	float CtoB(float nk, float ny)
	{
		return (1 - ny) * (1 - nk);
	}

	void is_Black() //검은색인지 확인
	{
		float r = transform.GetComponent<Renderer>().material.color.r;
		float g = transform.GetComponent<Renderer>().material.color.g;
		float b = transform.GetComponent<Renderer>().material.color.b;

		Debug.Log(r+" " + g + " " + b);
		if ((r + g + b / 3) == 0)
		{
			Destroy(this.gameObject);
		}
		if(transform.GetComponent<Renderer>().material.color == UnityEngine.Color.black)
		{
			Destroy(this.gameObject);
		}
	}
}
