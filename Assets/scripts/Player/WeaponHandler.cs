using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour 
{


	[SerializeField] private GameObject bulletMuzzle;
	[SerializeField] private GameObject missileMuzzle;
	[SerializeField] private GameObject laserMuzzle;
	[SerializeField] private GameObject angledMuzzle;

	public string ActiveWeapon { get; private set; }



	void OnEnable()
	{	
		EventManager.StartListening( "Bullet" , ToggleBullet );
		EventManager.StartListening( "Missile" , ToggleMissile );
		EventManager.StartListening( "Laser" , ToggleLaser );
		EventManager.StartListening( "Double" , ToggleDouble );

		
		//Shitty Fix ! find a better way
		//check what weapon is active on the player ship .
		Debug.Log( gameObject.name );

		if( gameObject.name == "Option"  )
		{
			if( PlayerPrefs.HasKey( "activeWeapon" ) )
			{
				if( PlayerPrefs.GetString( "activeWeapon" ) == "Laser" )
				{
					Debug.Log( "Match" );
					bulletMuzzle.SetActive( false );
					laserMuzzle.SetActive( true );
				}
				else
				{
					bulletMuzzle.SetActive( true );
					laserMuzzle.SetActive( false );
				}

			}
				
		}
	
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
			RecordActiveWeapon( "Bullet" );
			
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

			RecordActiveWeapon( "Laser" );
			
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

	void RecordActiveWeapon(string weapon)
	{
		if( gameObject.name == "PlayerShip" )
		{
			Debug.Log( "Ready to set players prefs...." );
			PlayerPrefs.SetString( "activeWeapon", weapon );
		}
	}

}
