using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidBossMissiles : MonoBehaviour {

	private Rigidbody2D r2d;

	[SerializeField]
	private float speed;
	
	// Use this for initialization
	void Start () 
	{
		r2d = GetComponent<Rigidbody2D>();

		if( r2d  == null)
			Debug.Log( "Rigidbody2D is missing ...." );
	}
		
	private void FixedUpdate()
	{
		r2d.velocity = Vector2.left * speed;
	}
	
}
