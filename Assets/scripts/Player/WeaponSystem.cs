using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour 
{
	[SerializeField]
	private float missileDelay  = 1.0f , 
	bulletDelay = 0.25f ,
	laserDelay = 0.2f,
	enemyBulletDelay = 2.0f,
	midBossMissileDelay = 0f,
	laserBoltDelay = 1.5f;

	private bool isLoadProjectileExecuting = false;
	private bool isAngledBullet = false;


	// Use this for initialization
	public void Bullet()
	{
		isAngledBullet = false;
		StartCoroutine( LoadProjectile( "Bullet" , bulletDelay, 2 ) );
		
	}

	public void AngledBullet()
	{
		isAngledBullet = false;
		StartCoroutine( LoadProjectile( "AngledBullet" , bulletDelay ,2 ) );

	}

	public void Missile()
	{
		StartCoroutine( LoadProjectile( "Missile" , missileDelay , 2 ) );
	}

	public void Laser()
	{
		StartCoroutine( LoadProjectile( "Laser" , laserDelay, 4 ) );
	}

	public void EnemyBullet()
	{
		StartCoroutine( LoadProjectile( "EnemyBullet" , enemyBulletDelay, 2 ) );
	}

	public void MidBossMissile()
	{
		StartCoroutine( LoadProjectile( "MidBossMissile" , midBossMissileDelay,2 ) );
	}

	public void LaserBolt()
	{
		StartCoroutine( LoadProjectile( "LaserBolt" , laserBoltDelay,2 ) );
	}
	

	//Bullets
	private IEnumerator LoadProjectile( string objName, float delay , int soundIndex )
	{
		if( isLoadProjectileExecuting )
			yield break;

		isLoadProjectileExecuting = true;
		
		yield return new WaitForSeconds( delay );
		
		//Play projectile sound effect
		EventManager.TriggerEvent( "PlaySound" , soundIndex ); //Bullet Sound
		//Spawn projectile from poolmanager
		PoolManager.instance.SpawnFromPool( objName , transform.position, transform.rotation );

		isLoadProjectileExecuting = false;

		yield break;
	}



}
