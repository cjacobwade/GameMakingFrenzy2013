using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {
	
	Quaternion gyroRot;
	Transform buttonHeld;
	
	// Update is called once per frame
	void Update () 
	{
		UIControl();
	}
	
	void UIControl()
	{
		Ray touchPos = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Input.GetMouseButtonDown(0))
			
			if(Physics.Raycast(touchPos,out hit))
			{
				if(hit.transform.CompareTag("Button"))
				{
					buttonHeld = hit.transform;
					hit.transform.SendMessage("Click",SendMessageOptions.DontRequireReceiver);
				}
			}
		if(Input.GetMouseButtonUp(0) && buttonHeld)
		{
			buttonHeld.SendMessage("Release", SendMessageOptions.DontRequireReceiver);
			buttonHeld = null;
		}
	}
}
