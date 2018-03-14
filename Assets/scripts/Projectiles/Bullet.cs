using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BaseCharacter
{
	
	private enum State { Alive, Dead }

	private Transform _transform;

	private State state;
	
	[SerializeField]
	private float speed;
	// Use this for initialization

	private Rigidbody2D rigidbody2d;


	private Vector2 movementDirection;

	[SerializeField]
	private bool isAngledBullet = false;
	
	void Awake()
	{
		_transform = transform;
	}
	
	void Start () 
	{
		rigidbody2d = GetComponent<Rigidbody2D>();

		if( isAngledBullet )
		{
		
			movementDirection = new Vector2( 1.0f , 1.0f );
			
		}
		else
		{
			movementDirection = Vector2.right;
		}
		
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		rigidbody2d.velocity = movementDirection * speed;
	}


	void OnTriggerEnter2D(Collider2D other) 
	{
		if( other.gameObject.tag == "Enemy"  )
		{
				
			IDestroyable iD = other.GetComponent<IDestroyable>();
			iD.Destroy();  
				
			Destroy();

		}
		else if( other.tag == "Terrain" || other.tag == "Core" )
		{
			Destroy();	
		}
	
	}

	private void RotateBullet()
	{
		Debug.Log( "Rotating Bullet..." );
		_transform.Rotate( 0 , 0, 45.0f );
	}

	public override void Destroy()
	{
		gameObject.SetActive( false );
	}
}
