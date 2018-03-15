using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour , IDestroyable
{
	public int ScoreValue { get; set; }

	[SerializeField]
	public int SpawnerId { get; set; }

	public Animator animator;

	public virtual void Destroy()
	{
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
