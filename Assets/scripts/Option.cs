﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour 
{

	[SerializeField]
	private float speed;	
	private Transform target;

	[SerializeField]
	private Vector3 offSet;

	private Vector2 desiredPosition;

	// Use this for initialization

	[SerializeField]
	private float horiz , vert;

	[SerializeField]
	private float offSetAmt;


	public GameObject muzzle;
	private WeaponSystem weaponSystem;

	void OnEnable()
	{
		EventManager.StartListening( "Fire" , Fire );
	}

	void OnDisable()
	{
		EventManager.StopListening( "Fire" , Fire );
	}


	void Start () 
	{
		
		target = GameObject.FindGameObjectWithTag( "Player" ).transform;
		weaponSystem = muzzle.GetComponent<WeaponSystem>();

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		
		Follow( target );		

	}

	private void Follow( Transform target )
	{
		horiz = Input.GetAxisRaw( "Horizontal" );
		vert = Input.GetAxisRaw( "Vertical" );
		
		if( vert > 0 )
			offSet.y = ( offSetAmt * -1 );
		else if( vert < 0 )
			offSet.y = offSetAmt;
		else if( vert == 0 && horiz == 1 )
		{
			offSet.x = ( offSetAmt ) * -1; 
			offSet.y = 0;
		}
		else if( vert == 0 && horiz == -1 )
		{
			offSet.x = ( offSetAmt );
			offSet.y = 0;	
		}
		
		if( horiz > 0 )
			offSet.x = ( offSetAmt * -1 );
		else if( horiz < 0 )
			offSet.x = offSetAmt;
		else if( vert == 1 && horiz == 0 )
		{
			offSet.y = ( offSetAmt ) * -1; 
			offSet.x = 0;
		}
		else if( vert == -1 && horiz == 0 )
		{
			offSet.y = ( offSetAmt );
			offSet.x = 0;	
		}

		desiredPosition = target.position + offSet;		
		
		transform.position = Vector2.MoveTowards( transform.position , desiredPosition, speed * Time.fixedDeltaTime );
	}

	public void Fire( int weaponType )
	{
		
		weaponSystem.Bullet();

	}
}
