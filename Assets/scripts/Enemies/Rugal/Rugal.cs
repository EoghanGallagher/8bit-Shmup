using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rugal : BaseCharacter , IDestroyable
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

	private Collider2D collider2d;

	void OnEnable()
	{
		isHit = false;
		collider2d = GetComponent<Collider2D>();

		if( !collider2d.enabled )
		{
			collider2d.enabled = true;
		}
	}

	public void Start()
	{
		ScoreValue = 100;
		rigidbody2d = GetComponent<Rigidbody2D>();
		target = GameObject.FindGameObjectWithTag( "Player" ).transform;
		animator.GetComponent<Animator>();
		spriteRenderer = GetComponent< SpriteRenderer >();
	}


	void FixedUpdate()
	{	
		Move();		 
	}

	private void Move()
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
		
		
		if( target.position.y < ( transform.position.y + 0.25f ) && target.position.y > ( transform.position.y - 0.25f ) && Vector2.Distance( transform.position , target.position ) < 3.0f  )
		{
			spriteRenderer.sprite = forward;
			direction = Vector2.left * speed * 2;
		
		}
		
		rigidbody2d.velocity = direction * speed;
	}

	private void RotateRugal()
	{
		if( target.position.y < ( transform.position.y - 1 ) )
		{
			
		
			animator.SetBool( "Down" , true );

		}
		else if( target.position.y > ( transform.position.y + 1 )  )
		{
			animator.SetBool( "Down" , false );
			animator.SetBool( "Up" , true );
			
		}
		else
		{
			animator.SetBool( "Up" , false);
			animator.SetBool( "Down" , false);
			
	
		}
	}

	private bool isHit = false;
	public override void Destroy()
	{
		if( !isHit )
		{
			isHit = true;
			collider2d.enabled = false;
			EventManager.TriggerEvent( "Score" , ScoreValue );
			animator.SetTrigger( "death" );
			base.Destroy();
		}
	}

}
