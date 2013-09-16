using UnityEngine;
using System.Collections;

public class ScoreControl : MonoBehaviour {

	GUIText score;
	int currentScore = 0;
	
	// Use this for initialization
	void Start () 
	{
		score = GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		score.text = "Score: " + currentScore;
	}
	
	void AddScore(int score)
	{
		currentScore += score;
	}
}
