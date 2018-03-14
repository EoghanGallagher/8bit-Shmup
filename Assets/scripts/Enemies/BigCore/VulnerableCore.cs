using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulnerableCore : MonoBehaviour {

	[SerializeField]
	private int health = 300;
	// Use this for initialization

	void OnTriggerEnter2D( Collider2D other )
	{
		if( other.tag == "Bullet" )
		{
			health = health - 20;
		}
		else if( other.tag == "Laser" )
		{
			health = health - 40;
		}

		if( health <= 0 )
		{
			EventManager.TriggerEvent( "CoreDeath" , 0 );
			gameObject.SetActive( false );
		}
	}
	
}
