using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dee001 : BaseCharacter 
{

	[SerializeField]
	private bool isFlippedHorizontally = false , isFlippedVertically = false;

	private Transform playerShip;

	private Transform _transform;
	// Use this for initialization

	private Animator anim;

	private float m_Angle;
	void Start () 
	{
		playerShip = GameObject.FindGameObjectWithTag( "Player" ).transform;
		_transform = transform;

		anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () 
	{

		FlipHorizontally();
		
		Aim();
	
	}

	private void FlipHorizontally()
	{
		if( playerShip.position.x < transform.position.x )
		{
		
				if( !isFlippedHorizontally )
				{
					
					if( isFlippedVertically )
						transform.localRotation = Quaternion.Euler(180, 180, 0);
					else
						transform.localRotation = Quaternion.Euler(0, 180, 0);

					isFlippedHorizontally = true;
					Debug.Log( "Dee 01 is facing left" );
				}
			
		}
		else
		{	

			if( isFlippedHorizontally )
			{
				if( isFlippedVertically )
					transform.localRotation = Quaternion.Euler(180, 0, 0);
				else
					transform.localRotation = Quaternion.Euler(0, 0, 0);	

				isFlippedHorizontally = false;
				Debug.Log( "Dee 01 is facing right" );
			}
		}
	}


	//Aim dee-01 turret 
	//Get current angle between ship and Dee-01
	//Pass result to animator
	private void Aim()
	{
		 
		 
		 m_Angle = Vector2.Angle( playerShip.position , _transform.position );

		 Debug.Log( m_Angle );

		 anim.SetFloat( "angle" , m_Angle );
	
	}
}
