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



public class PowerUpManager : MonoBehaviour {

	
	public List<PowerUpSprite> powerUps;

	[SerializeField]
	private static int powerUpCount;

	
	private void OnEnable()
	{
		EventManager.StartListening( "IncrementPowerUpCount" , IncrementPowerUpCount );
		EventManager.StartListening( "ActivatePowerUp" , ActivatePowerUp );
	}

	private void OnDisable()
	{
		EventManager.StopListening( "IncrementPowerUpCount" , IncrementPowerUpCount );
		EventManager.StopListening( "ActivatePowerUp" , ActivatePowerUp );
	}
	

	private void IncrementPowerUpCount(int x)
	{
		powerUpCount ++;

		Debug.Log( "PoweUpCount ... " + powerUpCount );

		SetActivePowerUpSprite();
	}


	private void SetActivePowerUpSprite( )
	{
		foreach( PowerUpSprite p in powerUps )
		{
			p.spriteRenderer.sprite = p.inactiveSprite;
		}

		
		if( powerUpCount < 7 )
			powerUps[ powerUpCount -1 ].spriteRenderer.sprite = powerUps[ powerUpCount - 1 ].activeSprite;
		else
		{
			powerUpCount = 1;
			powerUps[ powerUpCount -1 ].spriteRenderer.sprite = powerUps[ powerUpCount - 1 ].activeSprite;
		}

	}

	private void ActivatePowerUp( int x )
	{
		Debug.Log( "Activating PowerUp" );

		switch( powerUpCount )
		{
			case 1:

				Debug.Log( "Activating SpeedUp" );

			break;

			case 2:

				Debug.Log( "Activating Missile" );

			break;

			case 3:

				Debug.Log( "Activating Double" );

			break;

			case 4:
				
				Debug.Log( "Activating Lazer" );
			
			break;

			case 5:

				Debug.Log( "Activating Option" );

			break;

			case 6:
			
				Debug.Log( "Activating Special" );
			
			break;
		}
	}
	

}
