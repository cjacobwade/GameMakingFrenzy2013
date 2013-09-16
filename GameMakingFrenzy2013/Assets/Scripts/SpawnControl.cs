using UnityEngine;
using System.Collections;

public class SpawnControl : MonoBehaviour {
	
	public GameObject invader;
	public int minEnemies;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		int enemiesAlive = GameObject.FindGameObjectsWithTag("Invader").Length;
		if(enemiesAlive < minEnemies && enemiesAlive < 12)
			SpawnInvader();
	}
	
	void SpawnInvader()
	{
		Vector3 pointOnSphere = Random.onUnitSphere*25;
		Instantiate(invader,pointOnSphere,Quaternion.identity);
	}
}
