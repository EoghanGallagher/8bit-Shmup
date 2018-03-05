using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BaseCharacter
{
	[SerializeField]
	private float speed;
	// Use this for initialization

	private Rigidbody2D rigidbody2d;
	void Start () 
	{
		rigidbody2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		rigidbody2d.velocity = Vector2.right * speed;
	}


	void OnTriggerEnter2D(Collider2D other) 
	{
		
		if( other.gameObject.tag == "Enemy" )
		{
			other.enabled = false;	
			IDestroyable iD = other.GetComponent<IDestroyable>();
			iD.Destroy();  
				
			Destroy();

		}
		else if( other.tag == "Terrain" )
		{
				Debug.Log( "I hit terrain ...." );
				Destroy();
				
		}

		
    
	}

	public override void Destroy()
	{
		gameObject.SetActive( false );
	}
}
