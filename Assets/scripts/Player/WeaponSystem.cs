using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour 
{
	[SerializeField]
	private float myTime = 0.0F;
	[Space (5)]
	[SerializeField]
	private float missileDelta = 1.0f;
	[SerializeField]
	private float missileNextFire  = 1.0f;

	// Use this for initialization
	public void Bullet()
	{
		StartCoroutine( "LoadBullet" );
	}

	private bool isLoadBulletExecuting = false;


	//Bullets
	private IEnumerator LoadBullet( )
	{
		if( isLoadBulletExecuting )
			yield break;

		isLoadBulletExecuting = true;
		
		yield return new WaitForSeconds( 0.2f );
		PoolManager.instance.SpawnFromPool( "Bullet" , transform.position, transform.rotation );

		isLoadBulletExecuting = false;
	}


	//Missiles
	public void Missile()
	{
		StartCoroutine( "LoadMissile" );
	}


	private bool isLoadMissileExecuting = false;
	private IEnumerator LoadMissile()
	{

		if( isLoadMissileExecuting  )
			yield break;

		isLoadMissileExecuting = true;

		yield return new WaitForSeconds( 1.0f );
		PoolManager.instance.SpawnFromPool( "Missile" , transform.position, transform.rotation );
		//yield return new WaitForSeconds( 0.25f );
		//PoolManager.instance.SpawnFromPool( "Missile" , transform.position, transform.rotation );

		isLoadMissileExecuting = false;

	}


	//Lasers

	public void Laser()
	{
		
	}

}
