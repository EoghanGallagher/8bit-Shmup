using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherSpawn : MonoBehaviour , IDestroyable
{
	[SerializeField]
	private float speed;

	[SerializeField]
	private Rigidbody2D rigidBody2D;

	[SerializeField]
	private Transform playerShip;

	private Transform _transform;

	private Vector2 movementDirection;

	private Animator animator;

	private bool gotTarget = false;


	private Vector2 targetPosition;

	// Use this for initialization
	void Start () 
	{
		rigidBody2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		playerShip = GameObject.FindGameObjectWithTag( "Player" ).transform;

		_transform = transform;

	}


	void FixedUpdate()
	{
		Move();
	}

	public void Move()
	{
		if( !gotTarget )
		{
			targetPosition.y = playerShip.position.y;
			gotTarget = true;
		}
		
		
		if( _transform.position.y < targetPosition.y || _transform.position.y < 0.025f )
		{
			movementDirection = new Vector2( 0 , 1.0f );
		}
		else
		{
			movementDirection = new Vector2( -1.0f , 0.0f );
		}

		rigidBody2D.velocity = movementDirection * speed;
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
