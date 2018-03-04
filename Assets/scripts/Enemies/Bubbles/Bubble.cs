using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour , IDestroyable
{

	public void Destroy()
	{
		gameObject.SetActive( false );
	}


	public void OnTriggerEnter2D( Collider2D other )
	{
		if( other.tag == "Player" )
		{
			Debug.Log( "Bubble Kill Player..." );
			Destroy();
		}
	}
	
}
