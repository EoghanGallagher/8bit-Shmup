using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ducker : BaseCharacter
{
	[SerializeField]
	private float speed , distance;
	private bool isOkToMove;
	private bool isMoving = false;

	private Vector2 movementDirection;

	private Transform player;
	private Rigidbody2D r2d;

	private Transform _transform;

	private WeaponSystem weaponSystem;
	
	[SerializeField]
	private GameObject muzzle;
	private void OnEnable()
	{
		Setup();

		isOkToMove = true;
	}


	// Use this for initialization
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		r2d.velocity = movementDirection * speed;

		if( _transform.position.x < ( player.position.x - distance ) )
		{
			if( !isMoving )
			{
				animator.SetBool( "aim", false );
				
				if( _transform.localScale.x == -1 )
					Flip();
				
				Fire();
				
				StartCoroutine( "Movement" );
			}
		}
	
	}

	private void Setup()
	{
		
		ScoreValue = 200;
		//Cache Transform
		_transform = transform;
		
		//Get attached rigidbody
		r2d = GetComponent<Rigidbody2D>();

		if( r2d == null )
			Debug.Log( "RigidBody not found ..." );
		
		//Get attached animator
		animator = GetComponent<Animator>();

		if( animator == null )
			Debug.Log( "Animator not found ..." );

		//Get access to wepaonsystem 
		weaponSystem = muzzle.GetComponent<WeaponSystem>();

		if( weaponSystem == null )
			Debug.Log( "Weapon System not found ..." );
		
		//Find Player in scene
		player = GameObject.FindGameObjectWithTag( "Player" ).transform;

		if( player == null )
			Debug.Log( "Player ship not found ..." );
	}

	private IEnumerator Movement()
	{
		

		isMoving = true;
		movementDirection = Vector2.right;
		animator.SetBool( "walk" , true );

		while( _transform.position.x < player.position.x + 2 )
		{
			yield return null;
		}

		movementDirection = Vector2.zero;
		Flip();
		animator.SetBool( "walk" , false );
		animator.SetBool( "aim" , true );

		
		isMoving = false;

		yield break;
	}

	 private  void Flip()
     {
         Vector2 theScale = _transform.localScale;
         theScale.x *= -1;
         _transform.localScale = theScale;
     }

	 private void Fire()
	 {
		 weaponSystem.EnemyBullet();
	 }


	public override void Destroy()
	{
		EventManager.TriggerEvent( "Score" , ScoreValue );
		animator.SetTrigger( "death" );
		base.Destroy();
	}
}
