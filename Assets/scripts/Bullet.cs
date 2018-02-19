using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	[SerializeField]
	private float speed = 30.0f;
	// Use this for initialization

	private Rigidbody2D rigidbody2D;
	void Start () 
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		rigidbody2D.velocity = Vector2.right * speed;
	}

	void OnBecameInvisible() 
	{
        Debug.Log( "Im fading....." );
		gameObject.SetActive( false );
    }

	void OnTriggerEnter2D(Collider2D other) 
	{
        
		if( other.gameObject.tag == "Enemy" )
		{
				Debug.Log( "Enemy Hit" );
				IDestroyable iD = other.GetComponent<IDestroyable>();
				iD.Destroy();
				gameObject.SetActive( false );
		}
		else if( other.tag == "Terrain" )
		{
				Debug.Log( "I hit terrain ...." );
				gameObject.SetActive( false );
				
		}

		
    
	}
}
