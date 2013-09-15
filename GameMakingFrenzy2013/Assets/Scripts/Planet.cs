using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {
	
	Vector3 rotSpeed;
	
	// Use this for initialization
	void Start () 
	{
		SetRotateSpeed();
	}
	
	void SetRotateSpeed()
	{
		rotSpeed = new Vector3(Random.Range(1f,3f),Random.Range(1f,3f),Random.Range(1f,3f));
		rotSpeed*=Time.deltaTime;
		rotSpeed*= (Random.Range(0,2)*2)-1;
	}
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(rotSpeed);
	}
}
