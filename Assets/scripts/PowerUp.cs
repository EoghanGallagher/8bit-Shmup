using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour 
{

	
	 void OnTriggerEnter2D(Collider2D other) 
	 {
        
		if( other.gameObject.name == "PlayerShip" )
		{
			EventManager.TriggerEvent( "IncrementPowerUpCount", 0 );
			gameObject.SetActive( false );
		}
	
	 }

	
}
