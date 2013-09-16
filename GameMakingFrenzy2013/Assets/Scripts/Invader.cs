using UnityEngine;
using System.Collections;

public class Invader : MonoBehaviour {
	
	public float moveSpeed;
	SpawnControl spawner;
	//Used for the nearest target whether a landig zone or turret
	GameObject nearest, planet;
	
	public enum states
	{
		flying,
		seeking,
		attacking
	}
	
	public states invaderState = states.flying;
	
	// Use this for initialization
	void Start () 
	{
		planet = GameObject.Find("Planet");
		spawner	= GameObject.Find("SpawnControl").GetComponent<SpawnControl>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch(invaderState)
		{
			case states.attacking:
			{
				if(nearest) 
					Attack();
				else 
					FindTurret();
				break;
			}
			case states.seeking:
			{
				if(nearest) 
					Seek();
				else 
					FindTurret();
				break;
			}	
			case states.flying:
			{
				if(nearest) 
					Landing();
				else 
					FindLanding();
				break;
			}
		}
	}
	
	void FindLanding()
	{
		GameObject[] landingZones = GameObject.FindGameObjectsWithTag("LandingZone");
		float nearestDist = Mathf.Infinity;
		
		foreach(GameObject landingZone in landingZones)
		{
			float dist = (transform.position - landingZone.transform.position).sqrMagnitude;
			if(dist < nearestDist)
			{
				nearestDist = dist;
				nearest = landingZone;
			}
		}
	}
	
	void Landing()
	{
		//instantiate destruction particle
		transform.LookAt(nearest.transform);
		rigidbody.AddForce(0,0,0);
		transform.Translate(transform.forward*moveSpeed*Time.deltaTime);
	}
	
	void FindTurret()
	{
		GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
		float nearestDist = Mathf.Infinity;
		
		foreach(GameObject turret in turrets)
		{
			float dist = (transform.position - turret.transform.position).sqrMagnitude;
			if(dist < nearestDist)
			{
				nearestDist = dist;
				nearest = turret;
			}
		}
	}
	
	void Seek()
	{
		Vector3 lookPoint = nearest.transform.position;
		transform.LookAt(lookPoint, transform.up);
		transform.RotateAround(planet.transform.position, transform.up, moveSpeed*5*Time.deltaTime);
	}
	
	void Attack()
	{
		
	}
	
	void OnCollisionEnter(Collision col)
	{
		if(col.transform.CompareTag("LandingZone")&&invaderState==states.flying)
		{
			invaderState = states.seeking;
			transform.position = col.transform.position;
			transform.rotation = col.transform.rotation;
			transform.parent = planet.transform;
			nearest = null;
		}
		
		if(col.transform.CompareTag("Turret")&&invaderState==states.seeking)
		{	
			invaderState = states.attacking;
		}
	}
	
	void OnDestroy()
	{
		//instantiate destruction particle
		spawner.minEnemies++;
	}
}
