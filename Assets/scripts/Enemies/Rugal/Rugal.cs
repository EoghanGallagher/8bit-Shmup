﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rugal : MonoBehaviour , IDestroyable
{

	private Transform target;
	
	[SerializeField]
	private float speed;

	private Vector2 direction;
	private Vector2 temp;


	private Rigidbody2D rigidbody2d;

	[SerializeField]
	private SpriteRenderer spriteRenderer;

	[SerializeField]
	private Sprite up;

	[SerializeField]
	private Sprite forward;

	[SerializeField]
	private Sprite down;

	void Start()
	{
		
		rigidbody2d = GetComponent<Rigidbody2D>();
		target = GameObject.FindGameObjectWithTag( "Player" ).transform;

		spriteRenderer = GetComponent< SpriteRenderer >();


	}

	void FixedUpdate()
	{	
		Move();		 
	}


	private void  Move()
	{
		temp = transform.position;
		
		
		
		if( target.position.x < transform.position.x )
		{
			direction = ( target.position - transform.position ).normalized;
			RotateRugal(); 
		}
		else if( target.position.x > transform.position.x )
		{
			Debug.Log( "Position X is Greater....." );
			spriteRenderer.sprite = forward;
			direction = Vector2.left;
		}
		
		rigidbody2d.velocity = direction * speed;
	}

	private void RotateRugal()
	{
		if( target.position.y < ( transform.position.y - 1 ) )
		{
			
			spriteRenderer.sprite = down;

		}
		else if( target.position.y > ( transform.position.y + 1 )  )
		{
			spriteRenderer.sprite = up;
		}
		else
		{
			spriteRenderer.sprite = forward;
		}
	}

	public void Destroy()
	{

	}
}