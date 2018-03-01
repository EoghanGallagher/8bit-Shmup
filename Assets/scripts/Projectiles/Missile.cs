using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour , IDestroyable
{
	[SerializeField]
	private float speed;
	
	[SerializeField]
	private Vector2 direction , tempDirection;

	private Rigidbody2D rigidbody2D;


	[SerializeField]
	private SpriteRenderer spriteRenderer;
	// Use this for initialization

	[SerializeField]
	private Sprite falling,  onGround;



	void Start () 
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();

		tempDirection = direction;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		rigidbody2D.velocity = direction * speed;
	}

	void OnBecameInvisible() 
	{
        Debug.Log( "Missile Im fading....." );
		Destroy();
    }

	void OnTriggerEnter2D(Collider2D other) 
	{
		Debug.Log( "Missile " + other.name );
		
		if( other.tag == "Ground" )
		{
			spriteRenderer.sprite = onGround;
			
			direction = Vector2.right;
			
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
			spriteRenderer.sprite = falling;
			direction = tempDirection;
			
			Destroy();
		}


	}

	public void Destroy()
	{
		spriteRenderer.sprite = falling;
		direction = new Vector2( 0.5f , -1.0f );
		gameObject.SetActive( false );
	}
}
