using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		FindObjectOfType<Fan>().deathEvent += OnEnemyDeath;
	}
	
	public void OnEnemyDeath( int score )
	{
		Debug.Log( "Enemy Died it is worth : " + score  );
	}
}
