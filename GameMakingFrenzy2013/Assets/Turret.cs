using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	Vector3 turretTarget;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Ray worldMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(worldMouse,out hit))
		{
			if(hit.transform.gameObject.CompareTag("TurretPlane"))
			{
				print (hit.point);
				turretTarget = hit.point;
				transform.LookAt(turretTarget, transform.forward);
				
				//When referring to local rotation, you have to use localeulerangles
				transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x,transform.localEulerAngles.y,0);
			}
		}
		
	}
}
