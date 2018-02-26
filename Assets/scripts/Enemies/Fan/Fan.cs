﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour , IDestroyable 
{

	// Use this for initialization
	private Rigidbody2D rigidbody2d;

	[SerializeField]
	private float speed = 2.0f;

	private int selection;

	Vector2 movementDirection;


	[SerializeField]
	private Transform player;

	private Animator animator;


	IEnumerator Start()
	{
		
		animator = GetComponent<Animator>();

		player = GameObject.FindGameObjectWithTag( "Player" ).transform;
		
		rigidbody2d = GetComponent< Rigidbody2D >();
	
		movementDirection = new Vector2( -2.0f , 0 ) * speed;

		yield return new WaitForSeconds( 1.5f );
	
		movementDirection = new Vector2( 2.0f , player.position.y ) * speed;

		yield return new WaitForSeconds( 0.5f );
		
		movementDirection = new Vector2( 2.0f , 0 ) * speed;
		
	}

	void FixedUpdate()
	{
		Move( movementDirection );


	}

	void Move( Vector2 movementDirection )
	{
		rigidbody2d.velocity = movementDirection;
	}

	public void Destroy()
	{
		Debug.Log( "Destroying myself ...." );

		movementDirection = Vector2.zero;

		animator.SetTrigger( "death" );

		Invoke( "DisableSelf" , 0.5f );
		
	}

	public void DisableSelf()
	{
		gameObject.SetActive( false );
	}

	void OnBecameInvisible() 
	{
        Debug.Log( "Fan Im fading....." );
		gameObject.SetActive( false );
    }



	
	
}
