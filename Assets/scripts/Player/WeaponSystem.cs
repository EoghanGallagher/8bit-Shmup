using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour 
{
	[SerializeField]
	private float missileDelay  = 1.0f , 
	bulletDelay = 0.25f ,
	laserDelay = 0.2f;

	private bool isLoadProjectileExecuting = false;
	private bool isAngledBullet = false;


	// Use this for initialization
	public void Bullet()
	{
		isAngledBullet = false;
		StartCoroutine( LoadProjectile( "Bullet" , bulletDelay ) );
		
	}

	public void AngledBullet()
	{
		isAngledBullet = false;
		StartCoroutine( LoadProjectile( "AngledBullet" , bulletDelay ) );

	}

	public void Missile()
	{
		StartCoroutine( LoadProjectile( "Missile" , missileDelay ) );
	}

	public void Laser()
	{
		StartCoroutine( LoadProjectile( "Laser" , laserDelay ) );
	}

	//Bullets
	private IEnumerator LoadProjectile( string objName, float delay )
	{
		if( isLoadProjectileExecuting )
			yield break;

		isLoadProjectileExecuting = true;
		
		yield return new WaitForSeconds( delay );
		
		
		
		PoolManager.instance.SpawnFromPool( objName , transform.position, transform.rotation );

		isLoadProjectileExecuting = false;


		yield break;
	}



}
