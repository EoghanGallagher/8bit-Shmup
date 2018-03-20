using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCannon : BaseCharacter 
{
	[SerializeField]
	private WeaponSystem muzzle;
	
	private void OnEnable()
	{
		EventManager.StartListening( "FireLaser", FireLaser ); //Listen for Fire laser event
	}

	private void OnDisable()
	{
		EventManager.StopListening( "FireLaser", FireLaser );//Stop listening for fire laser event
	}


	private void Start()
	{
		ScoreValue = 1000; 
	}

	
	//Fire Laser Bolt
	private void FireLaser( int x )
	{
		Debug.Log( "Firing Laser Bolt" );
		muzzle.LaserBolt();

	}

	private void OnTriggerEnter2D( Collider2D other )
	{
		/*if( other.name == "Bullet" || other.name == "Laser" )
		{
			//Let laser cannon control know that i am dead
			EventManager.TriggerEvent( "UpdateDeathCount" , 0 );
		}*/
	}

	public override void Destroy()
	{
		EventManager.TriggerEvent( "Score" , ScoreValue );
			
		//Trigger death animation
		animator.SetTrigger( "death" );
			
		//Use parents destroy method
		base.Destroy();

	}

}
