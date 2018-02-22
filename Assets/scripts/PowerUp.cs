using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour 
{

	
	 void OnTriggerEnter2D(Collider2D other) 
	 {
        
		if( other.gameObject.name == "PlayerShip" )
		{
			Debug.Log( "Hit by Player Ship" );
			gameObject.SetActive( false );
		}
	
	 }

	
}
