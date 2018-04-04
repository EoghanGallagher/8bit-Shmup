using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

	[SerializeField] private string projectileType;	
	[SerializeField] private float fireDelay;
	[SerializeField] private int soundIndex;

	private bool isLoadProjectileExecuting = false;
	void OnEnable()
	{
		EventManager.StartListening( "Fire" , Fire );
	}

	void OnDisable()
	{
		EventManager.StopListening( "Fire" , Fire );
	}

	// Use this for initialization
	void Start () {
		
	}
	
	void Fire( int x )
	{
		StartCoroutine( LoadProjectile( projectileType , fireDelay , soundIndex ) );
	}

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
