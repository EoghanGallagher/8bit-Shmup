using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : BaseCharacter
{

	

	
	// Use this for initialization
	private Rigidbody2D rigidbody2d;

	[SerializeField]
	private float speed = 2.0f;

	private int selection;

	Vector2 movementDirection;


	[SerializeField]
	private Transform player;

	float tempY;


	IEnumerator Start()
	{
		
		ScoreValue = 100;
		animator = GetComponent<Animator>();

		player = GameObject.FindGameObjectWithTag( "Player" ).transform;
		
		rigidbody2d = GetComponent< Rigidbody2D >();
	
		movementDirection = new Vector2( -2.0f , 0 ) * speed;

		yield return new WaitForSeconds( 1.5f );

		if( player.position.y < transform.position.y )
		{
			tempY = -2.0f;
		}
		else
		{
			tempY = 2.0f;
		}

		if( transform.position.y >= 2.3  && player.position.y > transform.position.y )
		{
			tempY = 0;
		} 
		
		if( transform.position.y <= -2 && player.position.y < transform.position.y )
		{
			tempY = 0;
		}

		movementDirection = new Vector2( 2.0f , tempY ) * speed;

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

	public override void Destroy()
	{
		Debug.Log( "Triggering Eventmanager ....." );
		EventManager.TriggerEvent( "Score" , ScoreValue );
		
		movementDirection = Vector2.zero;
		animator.SetTrigger( "death" );
		
		base.Destroy();
		
	}

	
}
