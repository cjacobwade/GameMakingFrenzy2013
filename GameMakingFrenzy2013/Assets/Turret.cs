using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {
	
	public Vector3 missileOffset;
	Vector3 lookPoint;
	public GameObject rotQuad, missile;
	GameObject currentTurret;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
			

		
	}
	
	void TouchControls()
	{
		Ray worldMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Input.touchCount == 1 && Input.GetTouch(0).phase != TouchPhase.Ended)
		{
			if(Physics.Raycast(worldMouse,out hit))
			{
				if(hit.transform.gameObject.CompareTag("Turret"))
				{
					currentTurret = hit.transform.gameObject;
					rotQuad.SetActive(true);
				}
			}
		}
		else
		{
			if(currentTurret)
			{
				Instantiate(missile,missileOffset,Quaternion.identity);
				currentTurret = null;
				rotQuad.SetActive(false);
			}
		}
	}
	
	void TurretControls()
	{
		//TurretControls
			//if currentturret != null
				//look at lookpoint
				//draw arc (ugh)
		if(currentTurret)
			TurretLook();
			TurretArc();
	}
	
	void TurretLook()
	{
		Ray worldMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(worldMouse,out hit))
		{
			print (hit.point);
			lookPoint = hit.point;
			transform.LookAt(lookPoint, transform.forward);
		
			//When referring to local rotation, you have to use localeulerangles
			transform.localRotation = Quaternion.Euler(0,transform.localEulerAngles.y,90);
		}
	}
	
	void TurretArc()
	{
			
	}
	
	void OnGUI()
	{
		GUI.TextArea(new Rect(0,Screen.height/8+3*Screen.height/14,Screen.width/6,Screen.height/14),Input.mousePosition.ToString());	
	}
}
