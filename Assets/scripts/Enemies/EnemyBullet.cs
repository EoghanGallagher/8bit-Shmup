using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

	private Rigidbody2D r2D;

	private Transform player ,_transform;
	private Vector2 movementDirection;
	
	[SerializeField]
	private float speed = 0;
	
	// Use this for initialization
	void Start () 
	{
		r2D = GetComponent<Rigidbody2D>();

		//Find Player
		player = GameObject.FindGameObjectWithTag( "Player" ).transform;

		_transform = transform;

		//Get Players last position
		if( player != null )
			movementDirection = ( player.position - _transform.position );
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//Move towards players last known position
		r2D.velocity = movementDirection * speed; 
	}
}
