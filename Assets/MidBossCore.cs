using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MidBossCore : BaseCharacter
{


	[SerializeField]
	private int coreCount = 4;
	// Use this for initialization

	[SerializeField]
	private GameObject vulnerableCore;

	private WeaponSystem missileLauncher;

	[SerializeField]
	private GameObject muzzle;

	private Vector2  movementDirection; 

	private Rigidbody2D r2d;

	private float speed = 2;


	

	void OnEnable()
	{
		EventManager.StartListening( "CoreShieldDeath" , EnableCoreCollider );
		EventManager.StartListening( "CoreDeath" , CoreDeath );
		EventManager.StartListening( "FireMissile" , Fire  );
	}

	void OnDisable()
	{
		EventManager.StopListening( "CoreShieldDeath" , EnableCoreCollider );
		EventManager.StopListening( "CoreDeath" , CoreDeath );
		EventManager.StopListening( "FireMissile" , Fire  );
	}

	
	private int count = 0;
	private IEnumerator Start()
	{
		ScoreValue = 10000;

		animator = GetComponent<Animator>();

		missileLauncher = muzzle.GetComponent<WeaponSystem>();

		if( !vulnerableCore )
		{
			Debug.Log( "Vulnerbale Core not Set..." );
			yield break;
		}

		r2d = GetComponent<Rigidbody2D>();

		if(!r2d)
		{
			Debug.Log("RigidBody 2D not found");
			yield break;
		} 

		movementDirection = Vector2.left;

		yield return new WaitForSeconds( 3.0f );

		movementDirection = Vector2.zero;

		for( int x = 0; x < 4; x ++ )
		{
			Fire( 0 );
			Debug.Log("Firing Missile");
			yield return new WaitForSeconds( 1.0f );
		}

	
	}

	private void Update()
	{
		if (Input.GetKeyDown( KeyCode.X ) )
		{
			Fire( 0 ); 
		}
	}

	private void FixedUpdate()
	{
		r2d.velocity = movementDirection * speed;
	}

	private void Fire( int x )
	{
		missileLauncher.MidBossMissile();
	}


	private void EnableCoreCollider( int x)
	{
		Debug.Log("Core is now Vulnerable");
		Collider2D col2d = vulnerableCore.GetComponent<Collider2D>();
		col2d.enabled = true;
	}

	private void CoreDeath(int x )
	{
		StartCoroutine( CountDown() );
	}

	private IEnumerator CountDown()
	{
		yield return new WaitForSeconds(3.0f);
		Destroy();
	}

	public override void Destroy()
	{
		EventManager.TriggerEvent( "Score" , ScoreValue );
		animator.SetTrigger( "death" );
		base.Destroy();
	}

}
