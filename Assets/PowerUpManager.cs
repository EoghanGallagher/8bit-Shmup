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
}



public class PowerUpManager : MonoBehaviour 
{

	public List<PowerUpSprite> powerUps;

	[SerializeField]
	private static int powerUpCount;

	
	private void OnEnable()
	{
		EventManager.StartListening( "IncrementPowerUpCount" , IncrementPowerUpCount );
		EventManager.StartListening( "ActivatePowerUp" , ActivatePowerUp );
		EventManager.StartListening( "ResetPowerUpCount" , ResetPowerUpCount );
	}

	private void OnDisable()
	{
		EventManager.StopListening( "IncrementPowerUpCount" , IncrementPowerUpCount );
		EventManager.StopListening( "ActivatePowerUp" , ActivatePowerUp );
		EventManager.StopListening( "ResetPowerUpCount" , ResetPowerUpCount );
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
	
	
	private void SetActivePowerUpSprite( )
	{
		
		SetPowerUpToInactive();
		
		if( powerUpCount < 7 )
			powerUps[ powerUpCount -1 ].spriteRenderer.sprite = powerUps[ powerUpCount - 1 ].activeSprite;
		else
		{
			powerUpCount = 0;
			//powerUps[ powerUpCount -1 ].spriteRenderer.sprite = powerUps[ powerUpCount - 1 ].activeSprite;
		}

	}

	private void ActivatePowerUp( int x )
	{
		Debug.Log( "Activating PowerUp" );

		switch( powerUpCount )
		{
			case 1:

				Debug.Log( "Activating SpeedUp" );
				EventManager.TriggerEvent( "SpeedUp" , 1 );
				SetPowerUpToInactive();
				

			break;

			case 2:

				Debug.Log( "Activating Missile" );
				EventManager.TriggerEvent( "Missile" , 0 );
				SetPowerUpToInactive();
				

			break;

			case 3:

				Debug.Log( "Activating Double" );
				EventManager.TriggerEvent( "Double" , 0 );
				SetPowerUpToInactive();
				

			break;

			case 4:
				
				Debug.Log( "Activating Lazer" );
				EventManager.TriggerEvent( "Lazer" , 0 );
				SetPowerUpToInactive();
			break;

			case 5:

				Debug.Log( "Activating Option" );
				EventManager.TriggerEvent( "Option1" , 0 );
				SetPowerUpToInactive();
			break;

			case 6:
			
				Debug.Log( "Activating Special" );
				EventManager.TriggerEvent( "Special" , 0 );
				SetPowerUpToInactive();	
			break;
		}
	}
	

}
