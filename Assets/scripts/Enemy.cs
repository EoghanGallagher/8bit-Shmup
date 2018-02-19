using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour , IDestroyable
{

	// Use this for initialization
	void Start () 
	{
		
	}

	public void Destroy()
	{
		Debug.Log( "Hit By Bullet ...im Dead.." );
		Destroy( gameObject );
	}
		
}
