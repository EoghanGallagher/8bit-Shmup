using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour 
{


	[SerializeField] private GameObject bulletMuzzle;
	[SerializeField] private GameObject missileMuzzle;
	[SerializeField] private GameObject laserMuzzle;
	[SerializeField] private GameObject angledMuzzle;

	void OnEnable()
	{	
		EventManager.StartListening( "Bullet" , ToggleBullet );
		EventManager.StartListening( "Missile" , ToggleMissile );
		EventManager.StartListening( "Laser" , ToggleLaser );
		EventManager.StartListening( "Double" , ToggleDouble );

		//check what weapon is active on the player ship .
		
	}

	void OnDisable()
	{
		EventManager.StopListening( "Bullet" , ToggleBullet );
		EventManager.StopListening( "Missile" , ToggleMissile );
		EventManager.StopListening( "Laser" , ToggleLaser );
		EventManager.StopListening( "Double" , ToggleDouble );
	}


	void ToggleBullet( int x )
	{
		ResetWeapons();
		
		if( !bulletMuzzle.activeSelf )
		{
			bulletMuzzle.SetActive( true );
		}
		else
		{
			bulletMuzzle.SetActive( false );
		}

	}

	void ToggleMissile( int x )
	{
		
		if( !missileMuzzle.activeSelf )
		{
			missileMuzzle.SetActive( true );
		}
		else
		{
			missileMuzzle.SetActive( false );
		}

	}

	void ToggleLaser( int x )
	{
		ResetWeapons();

		if( !laserMuzzle.activeSelf )
		{
			laserMuzzle.SetActive( true );
			EventManager.TriggerEvent( "DoubleStatus" , 0 );
		}
		else
		{
			laserMuzzle.SetActive( false );
		}
	}

	public void ToggleDouble( int x )
	{
		
		if( x == 0 )
		{
			angledMuzzle.SetActive( false);	
				
		}
		else
		{
			angledMuzzle.SetActive( true );		
		}
			
	}


	void ResetWeapons()
	{
		bulletMuzzle.SetActive( false );
		laserMuzzle.SetActive( false );
		angledMuzzle.SetActive( false );
	}	
}
