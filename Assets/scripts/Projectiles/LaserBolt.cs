using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBolt : BaseCharacter
{

	private Rigidbody2D r2d;
	// Use this for initialization
	private Vector2 movementDirection;

	[SerializeField]
	private float speed;

	void Start () 
	{
		r2d = GetComponent<Rigidbody2D>();
		
		if(r2d == null)
			Debug.Log( "Rigidbody 2D not found...." );

		movementDirection = Vector2.left;


	}
	
	void FixedUpdate () 
	{
		r2d.velocity = movementDirection * speed;
	}

}
