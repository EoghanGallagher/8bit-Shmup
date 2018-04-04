using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInput : MonoBehaviour 
{

	[SerializeField] private bool isAxisRaw;
	public float Horizontal { get; private set; }
	public float Vertical { get; private set; }
	
	
	// Update is called once per frame
	void Update () 
	{
		
		//Cheat Keys 
		if (Input.GetKeyDown( KeyCode.M ) )
		{
			EventManager.TriggerEvent( "Missile" , 0 );
		}

		if (Input.GetKeyDown( KeyCode.O ) )
		{
			EventManager.TriggerEvent( "Option1" , 0 );
		}

		if (Input.GetKeyDown( KeyCode.I ) )
		{
			EventManager.TriggerEvent( "Option2" , 0 );
		}

		if( Input.GetKeyDown( KeyCode.C ) )
		{
			EventManager.TriggerEvent( "ActivatePowerUp" , 0 );
		}

		if( Input.GetKeyDown( KeyCode.L ) )
		{
			EventManager.TriggerEvent( "Laser" , 1 );
		}

		if( Input.GetKeyDown( KeyCode.D ) )
		{
			EventManager.TriggerEvent( "Double" , 1 );
		}

		
		if( isAxisRaw )
		{
			Horizontal = Input.GetAxisRaw( "Horizontal" );
			Vertical = Input.GetAxisRaw( "Vertical" );
		}
		else
		{
			Horizontal = Input.GetAxis( "Horizontal" );
			Vertical = Input.GetAxis( "Vertical" );
		}

		if( Input.GetButton( "Jump" ) )
		{
			EventManager.TriggerEvent( "Fire", 0 );
		}

	}
}
