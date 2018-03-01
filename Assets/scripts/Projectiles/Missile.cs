using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour 
{
	[SerializeField]
	private float speed;
	
	[SerializeField]
	private Vector2 direction;

	private Rigidbody2D rigidbody2D;


	[SerializeField]
	private SpriteRenderer spriteRenderer;
	// Use this for initialization

	[SerializeField]
	private Sprite onGround;

	void Start () 
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		rigidbody2D.velocity = direction * speed;
	}

	void OnBecameInvisible() 
	{
        Debug.Log( "Missile Im fading....." );
		gameObject.SetActive( false );
    }

	void OnTriggerEnter2D(Collider2D other) 
	{
		Debug.Log( "Missile " + other.name );
		
		if( other.tag == "Ground" )
		{
			spriteRenderer.sprite = onGround;
			direction = Vector2.right;
			speed = speed +2;
		}

		if( other.tag == "Enemy" )
		{
			IDestroyable iD = other.GetComponent<IDestroyable>();
			iD.Destroy();  
				
			gameObject.SetActive( false ); 
		}


		if( other.tag == "Terrain" )
		{
			gameObject.SetActive( false ); 
		}
	}
}
