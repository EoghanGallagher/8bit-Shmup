using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : BaseCharacter
{

	// Use this for initialization
	private Rigidbody2D rigidbody2d;

	[SerializeField]
	private float speed = 6.0f;

	[SerializeField]
	private int spawnerId;

	[SerializeField]
	private float squadLeftAliveCount = 4; //How many in the squad are alive


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

		EventManager.StartListening( "UpdateSquadCount" , UpdateSquadCount );

		squadLeftAliveCount = 4;

	
	}

	void OnDisable()
	{
		EventManager.StopListening( "UpdateSquadCount" , UpdateSquadCount );
	}

	void Start()
	{
		_transform = transform;

		spawnerId = SpawnerId;

		ScoreValue = 100;
		animator = GetComponent<Animator>();

		player = GameObject.FindGameObjectWithTag( "Player" ).transform;
		
		rigidbody2d = GetComponent< Rigidbody2D >();
	
		StartCoroutine( "FanMovementPattern" );

		
	
	}

	IEnumerator FanMovementPattern()
	{
			
		movementDirection = new Vector2( -2.0f , 0 ).normalized * speed;

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

		movementDirection = new Vector2( 2.0f , tempY ).normalized * speed;

		while( Mathf.Round ( _transform.position.y) != Mathf.Round( player.position.y ) )
		{
				yield return new WaitForSeconds( 0.02f );


		}
		
		movementDirection = new Vector2( 2.0f , 0 ).normalized * speed;

		
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


	//Assign Squad id
	//Received from Spawner
	

	//Decrement the squadLeftAliveCount
	private void UpdateSquadCount( int id )
	{

		//Check if message came from member of squad
		if( SpawnerId != id )
		{
			return;
		}

		// if I'm the last in my squad 
		//When I die I will drop a powerup
	
		if( squadLeftAliveCount == 0 )
		{
			//Spawn the power up using pool manager
			PoolManager.instance.SpawnFromPool( "OrangePowerUp" , transform.position, transform.rotation );
	
		}

		//if I'm not the last in my squad decrease the squad count
		squadLeftAliveCount --;
	}

	private bool isHit; 
	public override void Destroy()
	{
		
		if( !isHit )
		{
			isHit = true;
			collider2d.enabled = false;

			//Let other enemies in squad know this unit is dead
			//Each enemy will update their squad count
			EventManager.TriggerEvent( "UpdateSquadCount" , SpawnerId );
			
			//Stop listening for incoming messages from squad
			//This prevents this enemy from updating its squad count again before its destruction cycle is completed
			EventManager.StopListening( "UpdateSquadCount" , UpdateSquadCount );
			squadLeftAliveCount = 5;
			
			//Update score with this enemies score value
			EventManager.TriggerEvent( "Score" , ScoreValue );
			
			movementDirection = Vector2.zero;
			
			//Trigger death animation
			animator.SetTrigger( "death" );
			
			//Use parents destroy method
			base.Destroy();

		}
		
	}

	
}
