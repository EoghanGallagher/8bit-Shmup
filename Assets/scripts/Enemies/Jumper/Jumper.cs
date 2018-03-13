using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour 
{

	public float jumpTime = 0.5f;

	private float jumpVector;

	private float jumpPower;

	public float nextJump;

	Rigidbody2D rB2D;
	// Use this for initialization

	private Vector2 direction;
	void Start () 
	{

		rB2D = GetComponent<Rigidbody2D>();
	

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		Jump();
	}

	void Jump()
	{

	 	rB2D.AddForce( new Vector2( -1.0f, 1.0f ) );
	
	}
}
