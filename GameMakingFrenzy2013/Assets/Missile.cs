using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

	public Transform planetPrefab;
	public float moveSpeed;
	public GameObject explosion;
	
	// Use this for initialization
	void Start () 
	{
		StartCoroutine("Lifetime");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(moveSpeed > 0)
			moveSpeed -= 7*Time.deltaTime;
		transform.RotateAround(planetPrefab.transform.position, -transform.right, moveSpeed*Time.deltaTime);
	}
	
	void Detonate()
	{
		//Instantiate explosion particle
		GameObject explosionInstance = Instantiate(explosion,transform.position,transform.rotation) as GameObject;
		GameObject planet = GameObject.Find("Planet");
		explosionInstance.transform.parent = planet.transform;

		//Destroy this
		Destroy(gameObject);
	}
	
	IEnumerator Lifetime()
	{
		yield return new WaitForSeconds(3f);
		Detonate();
	}
	
	void OnCollisionEnter(Collision col)
	{
		//Destroy other
		Destroy (col.transform.gameObject);
		Detonate();
	}
}
