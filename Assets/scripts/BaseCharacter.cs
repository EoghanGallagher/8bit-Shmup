using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour , IDestroyable
{

	public Animator animator;
	

	public virtual void Destroy()
	{
	  Debug.Log( "Base Class Destroy" );
	  Invoke( "DisableSelf" , 0.5f );
	}

	public void DisableSelf()
	{
		gameObject.SetActive( false );
	}

	void OnBecameInvisible() 
	{
    	gameObject.SetActive( false );
    }
}
