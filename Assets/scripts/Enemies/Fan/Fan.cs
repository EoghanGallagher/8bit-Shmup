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
	private Transform player , _transform;

	private Collider2D collider2d;
	

	float tempY;


	void OnEnable()
	{
		
		StopCoroutine( "FanMovementPattern" );
		StartCoroutine( "FanMovementPattern" );

		collider2d = GetComponent<Collider2D>();

		collider2d.enabled = true;
		isHit = false;
	}

	void Start()
	{
		_transform = transform;

		ScoreValue = 100;
		animator = GetComponent<Animator>();

		player = GameObject.FindGameObjectWithTag( "Player" ).transform;
		
		rigidbody2d = GetComponent< Rigidbody2D >();
	
		StartCoroutine( "FanMovementPattern" );
	
	}

	IEnumerator FanMovementPattern()
	{
			
		movementDirection = new Vector2( -2.0f , 0 ) * speed;

		yield return new WaitForSeconds( 1.5f );

		if( _transform.position.y > player.position.y )
		{
			tempY = -2.0f;
		}
		
		if( _transform.position.y < player.position.y )
		{
			tempY = +2.0f;
		}

		if( Mathf.Round ( _transform.position.y)  ==  Mathf.Round( player.position.y ) )
		{
			tempY = 0;
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

		while( Mathf.Round ( _transform.position.y) != Mathf.Round( player.position.y ) )
		{
				yield return new WaitForSeconds( 0.02f );


		}
		
		movementDirection = new Vector2( 2.0f , 0 ) * speed;

		
		yield break;

	}

	void FixedUpdate()
	{
		Move( movementDirection );
	}

	void Move( Vector2 movementDirection )
	{
		rigidbody2d.velocity = movementDirection;
	}


	private bool isHit; 
	public override void Destroy()
	{
		
		if( !isHit )
		{
			isHit = true;
			collider2d.enabled = false;

			EventManager.TriggerEvent( "Score" , ScoreValue );
			
			movementDirection = Vector2.zero;
			animator.SetTrigger( "death" );
			
			base.Destroy();

		}
		
	}

	
}
