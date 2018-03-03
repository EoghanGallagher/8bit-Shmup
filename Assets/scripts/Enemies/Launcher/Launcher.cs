using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour , IDestroyable
{

	private Animator animator;
	// Use this for initialization

	[SerializeField]
	private int spawnCount; 

	[SerializeField]
	private int spawnLimit;


	private SpriteRenderer spriteRenderer;

	[SerializeField]
	private Sprite launcherEmpty ; 
	void Start () 
	{
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	public void Destroy()
	{
		animator.SetTrigger( "death" );
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

	public void CheckLauncherCount()
	{
		spawnCount ++;

		if( spawnCount == spawnLimit )
		{
			spriteRenderer.sprite = launcherEmpty;
			spawnCount = 0;
		}

	}


	 void OnTriggerExit2D(Collider2D other)
    {
       
		if( other.tag == "Enemy" )
		{
				spawnCount ++;
		}

		if( spawnCount == spawnLimit )
		{
				Debug.Log( "Changing Launcher Sprite" );
				animator.SetTrigger( "empty" );
				spawnCount = 0;
		}
    }
	

	
	
}
