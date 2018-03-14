using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreShield : MonoBehaviour 
{

	[SerializeField]
	private int health = 200;
	void Start()
	{

	}

	[SerializeField]
	private int x = 1;
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
			EventManager.TriggerEvent( "Deactivate", x );
			health = 200;
			x++;

			if( x > 5 )
			{
				EventManager.TriggerEvent( "CoreShieldDeath" , 1 );
				gameObject.SetActive( false );
			}
		}
	}
	
}
