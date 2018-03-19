using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCannonControl : MonoBehaviour 
{
	[SerializeField]
	private float deathCount , maxNumLasers;

	private void OnEnable()
	{
		EventManager.StartListening( "UpdateDeathCount" , UpdateDeathCount );
	}

	private void OnDisable()
	{
		EventManager.StopListening( "UpdateDeathCount" , UpdateDeathCount );
	}
	
	void Update()
	{
		if ( Input.GetKeyDown( KeyCode.F ) )
		{
			EventManager.TriggerEvent( "FireLaser", 0 );
		}
	}

	private void OnTriggerEnter2D( Collider2D collider )
	{
		if( collider.tag == "Player" )
			StartCoroutine( FireLaser() );
	}

	IEnumerator FireLaser()
	{
		while(true)
		{
			EventManager.TriggerEvent( "FireLaser" , 0 );
			yield return new WaitForSeconds( 2.5f );

			if( deathCount == maxNumLasers )
			{
				break;
			}

		}

		yield break;
	}


	private void UpdateDeathCount( int x )
	{
		deathCount ++;
	}
	
	
}
