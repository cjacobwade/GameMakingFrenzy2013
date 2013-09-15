using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {
	
	public Vector3 missileOffset;
	Vector3 lookPoint;
	public GameObject rotQuad, missile, planet;
	GameObject currentTurret;
	float sections= 5;
	LineRenderer aimArc;
	string message = "-";
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		TouchControls();
		TurretControls();
	}
	
	void TouchControls()
	{
		Ray worldMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
//		if(Input.GetMouseButtonDown(0))
//		{
			if(Physics.Raycast(worldMouse,out hit))
			{
				if(hit.transform.gameObject.CompareTag("Turret"))
				{
					message = "hit turret";
					currentTurret = hit.transform.parent.gameObject;
					aimArc = currentTurret.GetComponent<LineRenderer>();
					aimArc.enabled = true;
					rotQuad.SetActive(true);
				}
			}
		}
		if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
		{
//		if(Input.GetMouseButtonUp(0))
//		{
			if(currentTurret)
			{
				message = "transform = null";
				GameObject missileInstance = Instantiate(missile,currentTurret.transform.position,currentTurret.transform.rotation*Quaternion.Euler(0,-180,-90)) as GameObject;
				missileInstance.transform.parent = planet.transform;
				aimArc.enabled = false;
				currentTurret = null;
				rotQuad.SetActive(false);
			}
		}
	}
	
	void TurretControls()
	{
		if(currentTurret)
		{
			TurretLook();
			TurretArc();
		}
	}
	
	void TurretLook()
	{
		Ray worldMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(worldMouse,out hit))
		{
			print (hit.point);
			lookPoint = hit.point;
			currentTurret.transform.LookAt(lookPoint, currentTurret.transform.forward);
		
			//When referring to local rotation, you have to use localeulerangles
			currentTurret.transform.localRotation = Quaternion.Euler(0,currentTurret.transform.localEulerAngles.y,90);
		}
	}
	
	void TurretArc()
	{
		Vector3 arcStart, arcDirection;
		float t;
		arcStart = currentTurret.transform.position;
		aimArc.enabled = true;
		for(int i=0;i<sections;i++)
		{
			t = i/(sections-1); //percentage through the list of points
			
			//Set line to be straight out of the turret
			arcDirection = currentTurret.transform.position - currentTurret.transform.forward*10000;
			
			//Set length of arc to be reasonable
			arcDirection = arcDirection/5000;
			
			aimArc.SetPosition(i,arcStart + arcDirection*t);
		}
	}
	
	void OnGUI()
	{
//		GUI.TextArea(new Rect(0,Screen.height/8+4*Screen.height/14,Screen.width/6,Screen.height/14),Input.mousePosition.ToString());
//		GUI.TextArea(new Rect(0,Screen.height/8+5*Screen.height/14,Screen.width/6,Screen.height/14),currentTurret.ToString());
//		GUI.TextArea(new Rect(0,Screen.height/8+6*Screen.height/14,Screen.width/6,Screen.height/14),message.ToString());
	}
}
