using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour {

	// Use this for initialization
	public void LoadBullet()
	{
		PoolManager.instance.SpawnFromPool( "Bullet" , transform.position, transform.rotation );
	}
}
