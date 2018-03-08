using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PowerUpSprite
{
	public string name;
	public Sprite activeSprite;
	public Sprite inactiveSprite;
	public SpriteRenderer spriteRenderer;
	public int activeCount;
	public int maxActiveCount;
}



public class PowerUpManager : MonoBehaviour 
{

	public List<PowerUpSprite> powerUps;

	public Sprite defaultSprite;

	[SerializeField]
	private static int powerUpCount;

	
	private void OnEnable()
	{
		EventManager.StartListening( "IncrementPowerUpCount" , IncrementPowerUpCount );
		EventManager.StartListening( "ActivatePowerUp" , ActivatePowerUp );
		EventManager.StartListening( "ResetPowerUpCount" , ResetPowerUpCount );
		EventManager.StartListening( "DoubleStatus" , DoubleStatus  );
	}

	private void OnDisable()
	{
		EventManager.StopListening( "IncrementPowerUpCount" , IncrementPowerUpCount );
		EventManager.StopListening( "ActivatePowerUp" , ActivatePowerUp );
		EventManager.StopListening( "ResetPowerUpCount" , ResetPowerUpCount );
		EventManager.StopListening( "DoubleStatus" , DoubleStatus  );
	}
	

	private void IncrementPowerUpCount(int x)
	{
		powerUpCount ++;

		Debug.Log( "PoweUpCount ... " + powerUpCount );

		SetActivePowerUpSprite();
	}

	private void ResetPowerUpCount( int x )
	{
		powerUpCount = 0;
	}


	private void SetPowerUpToInactive()
	{
		foreach( PowerUpSprite p in powerUps )
		{
			p.spriteRenderer.sprite = p.inactiveSprite;
		}
	}
	

	private static int speedUpCount;
	private static int missileCount;
	
	
	/* 
		LOOP THROUGH POWER UP MENU
		CHECK IF EACH ITEM HAS A POWERUP ACTIVE
		ALSO CHECK IF NUM OF THAT TYPE OF POWERUP IS MAXED
		SET THE CURRENTLY SELECTED POWERUP SPRITE TO ACTIVE
		IF MAXED REPLACE SPRITE WITH DEFAULT( BLANK ) ACTIVE SPRITE
	 */

	private void SetActivePowerUpSprite( )
	{
		
		Sprite sprite = null;

		PowerUpSprite currentPowerUp = powerUps[ powerUpCount - 1 ];

		
		//RESET POWER UP METER ITEMS TO DEFAULT
		SetPowerUpToInactive();


		if( currentPowerUp.activeCount < currentPowerUp.maxActiveCount )
		{
			sprite = currentPowerUp.activeSprite;
			
		}
		else
		{ 
			sprite = defaultSprite;
		}

		powerUps[ powerUpCount -1 ].spriteRenderer.sprite = sprite;

		if( powerUpCount == powerUps.Count )
		{
			powerUpCount = 0;
		}
		
	
	}

	/* 
		Activate the slected powerup 
	*/
	private void ActivatePowerUp( int x )
	{

		if( powerUpCount == 0 )
		{
			return;
		}

		PowerUpSprite currentPowerUp = powerUps[ powerUpCount - 1 ];

		switch( powerUpCount )
		{
			case 1:

				//Activate SpeedUp
				Debug.Log( "Activating SpeedUp" );
				
				if( currentPowerUp.activeCount < currentPowerUp.maxActiveCount )
				{
					EventManager.TriggerEvent( "SpeedUp" , 1 );	
					ResetPowerUpCount( 0 );
					SetPowerUpToInactive();
				}
						
			break;

			case 2:

				//Activate Missile
				Debug.Log( "Activating Missile" );
				
				if( currentPowerUp.activeCount < currentPowerUp.maxActiveCount )
				{
					EventManager.TriggerEvent( "Missile" , 0 );
					ResetPowerUpCount( 0 );	
					SetPowerUpToInactive();				
				}
				else
				{
					Debug.Log( "Missile Maxed" );
				}
				
			break;

			case 3:

				//Activate Double
				Debug.Log( "Activating Double" );


				if( currentPowerUp.activeCount < currentPowerUp.maxActiveCount )
				{
					if( currentPowerUp.activeCount  == 0 )
					{
						Debug.Log( "Activating Muzzle for Double...." );	
						EventManager.TriggerEvent( "Double" , 0 );
					}
					else if( currentPowerUp.activeCount == 1 )
					{
						Debug.Log( "Activating Angled Muzzle for Double...." );
						EventManager.TriggerEvent( "Double" , 1 );	
					}

					SetPowerUpToInactive();	
					ResetPowerUpCount( 0 );		
				}
				else
				{
					Debug.Log( "Double is maxed out ..." );
				}


			break;

			case 4:
				
				//Activate Lazer
				Debug.Log( "Activating Lazer" );

				//if lazer is not active then bullet must be
				//so disable bullet
				if( currentPowerUp.activeCount < currentPowerUp.maxActiveCount )
				{
					EventManager.TriggerEvent( "Double" , 0 );
					EventManager.TriggerEvent( "Lazer" , 1 );

					ResetPowerUpCount( 0 );	
					SetPowerUpToInactive();			
				}
				else
				{
					Debug.Log( "Laser is maxed out" );
				}


				
			
			break;

			case 5:

				//Activate Option. 
				//Max Option count is 2
				Debug.Log( "Activating Option" );
				
				if( currentPowerUp.activeCount == 0 )
					EventManager.TriggerEvent( "Option1" , 0 );
				
				if( currentPowerUp.activeCount == 1 )
					EventManager.TriggerEvent( "Option2" , 0 );

				ResetPowerUpCount( 0 );	
				SetPowerUpToInactive();			

			break;

			case 6:
			
				//Activate Special
				Debug.Log( "Activating Special" );
				
				if( currentPowerUp.activeCount < currentPowerUp.maxActiveCount )
				{
					EventManager.TriggerEvent( "Special" , 0 );	
				}
			
			break;

			default:

				Debug.Log( "No Power Up Selected" );
			
			break;

		}

		currentPowerUp.activeCount ++;
	}


	private static int doubleCount = 0;
	private void DoubleStatus( int status )
	{
		string name = "Double";
		
		foreach( PowerUpSprite p in powerUps )
		{
			if( p.name == name )
			{
				Debug.Log( "Resetting  " + p.name + " Count" );
				p.activeCount = 0;
			}
		}
	}


	private void DisableAllPowerups()
	{
		foreach( PowerUpSprite p in powerUps )
		{
			p.activeCount = 0;
		}

		EventManager.TriggerEvent( "Double" , 0 );
	}


	

}
