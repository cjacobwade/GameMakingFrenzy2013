using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	
	public Transform planet;
	public Vector3 moveSpeed;
	Vector3 screenMouse, initTouch, currentTouch, velocity = Vector3.zero;
	public float rotSpeed;
	bool playerRot = false;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		TouchInput();
	}
	
	void TouchInput()
	{
		//THIS IS JUNK
		
		//Need to take average of both fingers, otherwise missing the planet with either finger won't be responsive
		Ray worldMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
		//Ray worldMouse = Camera.main.ScreenPointToRay((Input.GetTouch(0).position + Input.GetTouch(1).position)/2);
		screenMouse = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0);
		RaycastHit hit;
		if(Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			if(Physics.Raycast(worldMouse,out hit))
			{
				if(hit.transform.gameObject.CompareTag("Planet"))
				{
					playerRot = true;
					initTouch = screenMouse;
				}
			}
		}
		
		else if(Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			if(playerRot)
			{
				currentTouch = screenMouse;
				moveSpeed = currentTouch - initTouch;
				moveSpeed.y*=-1;
				moveSpeed*=Time.deltaTime*rotSpeed;
			}
		}
		
		else if(Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Stationary)
		{
			if(playerRot)
				//Slow down rot speed to zero
				moveSpeed = Vector3.SmoothDamp(moveSpeed,Vector3.zero,ref velocity,1.5f); 
		}
		
		//stop rotation if touch has ended
		else if(Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Ended) playerRot = false; 
		
		//Movespeed is zero when not touched
		else moveSpeed = Vector3.SmoothDamp(moveSpeed,Vector3.zero,ref velocity,.7f); 
		
		moveSpeed = Vector3.ClampMagnitude(moveSpeed,75f);
		MoveCamera();
	}
	
	void MoveCamera()
	{
		transform.RotateAround(planet.transform.position, transform.right, 1f*Time.deltaTime + moveSpeed.y*Time.deltaTime);
		transform.RotateAround(planet.transform.position, transform.up,  1f*Time.deltaTime + moveSpeed.x*Time.deltaTime);
	}
	
	void OnGUI()
	{
//		GUI.TextArea(new Rect(0,Screen.height/8,Screen.width/6,Screen.height/14),initTouch.ToString());
//		GUI.TextArea(new Rect(0,Screen.height/8+Screen.height/14,Screen.width/6,Screen.height/14),currentTouch.ToString());
//		GUI.TextArea(new Rect(0,Screen.height/8+2*Screen.height/14,Screen.width/6,Screen.height/14),Input.touchCount.ToString());
//		GUI.TextArea(new Rect(0,Screen.height/8+3*Screen.height/14,Screen.width/6,Screen.height/14),moveSpeed.ToString());
		//rotSpeed = GUI.VerticalSlider(new Rect(Screen.width/8,Screen.height/8+4*Screen.height/14,Screen.width/6,Screen.height/2),rotSpeed,15,1);
		//GUI.TextArea(new Rect(0,Screen.height/8+4*Screen.height/14,Screen.width/6,Screen.height/14),mousePos.ToString());
	}
}
