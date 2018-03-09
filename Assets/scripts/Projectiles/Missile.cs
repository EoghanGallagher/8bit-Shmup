using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : BaseCharacter
{
	[SerializeField]
	private float speed;
	
	[SerializeField]
	private Vector2 direction , tempDirection;

	private Rigidbody2D rigidbody2d;


	[SerializeField]
	private SpriteRenderer spriteRenderer;
	// Use this for initialization

	[SerializeField]
	private Sprite falling,  onGround;

	void Awake()
	{
		
	}

	void OnEnable()
	{
		rigidbody2d = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		
		spriteRenderer.sprite = falling;
		direction = new Vector2( 0.5f , -1.0f );
	}

	void Start () 
	{
		
		tempDirection = direction;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		rigidbody2d.velocity = direction * speed;
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if( other.tag == "Ground" )
		{
			spriteRenderer.sprite = onGround;
			
			direction = Vector2.right;
			speed = 5;
			
		}

		if( other.tag == "Enemy" )
		{
			IDestroyable iD = other.GetComponent<IDestroyable>();
			iD.Destroy(); 

			direction = tempDirection;
			
			Destroy();
		}

		if( other.tag == "Terrain" )
		{
			Destroy();
		}


	}

	public override void Destroy()
	{
		gameObject.SetActive( false );
	}
}
