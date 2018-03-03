using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherSpawn : BaseCharacter
{
	[SerializeField]
	private bool isFlipped;
	
	[SerializeField]
	private float speed;

	[SerializeField]
	private Rigidbody2D rigidBody2D;

	[SerializeField]
	private Transform playerShip;

	private Transform _transform;

	private Vector2 movementDirection;

	private bool gotTarget = false , isOkToMove = false;

	private Vector2 targetPosition;

	// Use this for initialization
	IEnumerator Start () 
	{
		rigidBody2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		playerShip = GameObject.FindGameObjectWithTag( "Player" ).transform;

		_transform = transform;

		movementDirection = _transform.up;
		yield return new WaitForSeconds( 0.25f );
		movementDirection = Vector2.left;
		isOkToMove = true;

	}


	void FixedUpdate()
	{
		if( isOkToMove )
			Move();
		
		rigidBody2D.velocity = movementDirection * speed;
	}

	public void Move()
	{
		if( !gotTarget )
		{
			targetPosition.y = playerShip.position.y;
			gotTarget = true;
		}
		

		if( !isFlipped )
		{
			if( _transform.position.y < targetPosition.y  )
			{
				movementDirection = _transform.up;
			}
			else
			{
				movementDirection = Vector2.left;
			}	
		}
		else
		{
			if( _transform.position.y > targetPosition.y )
			{
				movementDirection = _transform.up;
			}
			else
			{
				movementDirection = Vector2.left;
			}
		}
	
	}

	public override void Destroy()
	{
		movementDirection = Vector2.zero;
		
		animator.SetTrigger( "death" );

		base.Destroy();
	}

	public void SetFlipped( bool b )
	{
		isFlipped = b;
	}
	
	
	
}
