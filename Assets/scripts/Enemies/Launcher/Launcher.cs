using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour , IDestroyable
{

	private int ScoreValue;

	[SerializeField]
	private bool isFlipped;
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
		ScoreValue = 1000;
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	public void Destroy()
	{
		EventManager.TriggerEvent( "Score" , ScoreValue );

		Spawner spawner = GetComponent<Spawner>();

		if( spawner != null )
			spawner.Destroy();

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


	 void OnTriggerEnter2D(Collider2D other)
    {
       
		if( other.name == "LauncherSpawn(Clone)" )
		{
			spawnCount ++;

			if( isFlipped )
			{
				LauncherSpawn spawn = other.GetComponent<LauncherSpawn>();
				spawn.SetFlipped( true );
			}

		}

		if( spawnCount == spawnLimit )
		{
			animator.SetTrigger( "empty" );
			spawnCount = 0;
		}
    }
	

	
	
}
