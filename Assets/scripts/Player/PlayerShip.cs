﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
 public class Boundary
 {
     public float minX = -3.0f, maxX = 3.0f, minY = -3.0f, maxY = 3.0f;
 }

public class PlayerShip : MonoBehaviour , IDestroyable , IFireable, ISubject
{

	public Boundary boundary;

	[SerializeField]
	private float speed;

	private Rigidbody2D rigidBody2D;

	private float horiz;
	private float vert;

	[SerializeField]
	private Animator animator;

	private Transform _transform;


	[SerializeField]
	private Transform shipAnchor;

	 private float myTime = 0.0F;
	public float fireDelta = 0.2F;

    private float nextFire = 0.2F;

	private WeaponSystem weaponSystem , missileSystem , angledWeaponSystem;

	[SerializeField]
	private GameObject playerShipMuzzle , MissileLauncher , angledMuzzle;

	

	[SerializeField]
	List<IObserver> observers;

	public GameObject option1;
	public GameObject option2;


	private bool isLaserActive = false;
	private bool isAngledWeaponActive = false;


	void OnEnable()
	{
		EventManager.StartListening( "Lazer" , ToggleLazer );
		EventManager.StartListening( "Missile" , ToggleMissile );
		EventManager.StartListening( "Option1" , ToggleOption1 );
		EventManager.StartListening( "Option2" , ToggleOption2 );
		EventManager.StartListening( "SpeedUp" , SpeedUp );
		EventManager.StartListening( "Double" , ToggleDouble );
	}

	void OnDisable()
	{
		EventManager.StopListening( "Lazer" , ToggleLazer );
		EventManager.StopListening( "Missile" , ToggleMissile );
		EventManager.StopListening( "Option1" , ToggleOption1);
		EventManager.StopListening( "Option2" , ToggleOption2);
		EventManager.StopListening( "SpeedUp" , SpeedUp );
		EventManager.StopListening( "Double" , ToggleDouble );
	}
	// Use this for initialization

	private void Awake()
	{
		DontDestroyOnLoad( gameObject );
	}
	private void Start () 
	{

		observers = new List<IObserver>();

		if( option1.activeSelf )
			Register( option1.GetComponent<IObserver>() );
		
		if( option1.activeSelf )
			Register( option2.GetComponent<IObserver>()  );

		//Get Attached RigidBody
		rigidBody2D = GetComponent<Rigidbody2D>();

		//Get Attached Animator Control
		animator = GetComponent<Animator>();

		//Cache Transform
		_transform = transform;

		//Get Attached Weapon System
		weaponSystem = playerShipMuzzle.GetComponent<WeaponSystem>();

		missileSystem = MissileLauncher.GetComponent<WeaponSystem>();

		angledWeaponSystem = angledMuzzle.GetComponent<WeaponSystem>();

	}

	private void Update()
	{

		if (Input.GetKeyDown( KeyCode.M ) )
		{
			EventManager.TriggerEvent( "Missile" , 0 );
		}

		if (Input.GetKeyDown( KeyCode.O ) )
		{
			EventManager.TriggerEvent( "Option1" , 0 );
		}

		if (Input.GetKeyDown( KeyCode.I ) )
		{
			EventManager.TriggerEvent( "Option2" , 0 );
		}

		if( Input.GetKeyDown( KeyCode.C ) )
		{
			EventManager.TriggerEvent( "ActivatePowerUp" , 0 );
		}

		if( Input.GetKeyDown( KeyCode.L ) )
		{
			EventManager.TriggerEvent( "Lazer" , 1 );
		}


	}

	[SerializeField]
	private bool isAxisRaw = false;
	private void FixedUpdate()
	{
		if( isAxisRaw )
		{
			horiz = Input.GetAxisRaw( "Horizontal" );
			vert = Input.GetAxisRaw( "Vertical" );
		}
		else
		{
			horiz = Input.GetAxis( "Horizontal" );
			vert = Input.GetAxis( "Vertical" );
		}

		rigidBody2D.velocity = new Vector2( horiz  , vert ) * speed ;

		AnimateShipMovement( vert );

		RestrictShipMovement();

		myTime = myTime + Time.deltaTime;

		/*if( Input.GetButton( "Jump" ) && myTime > nextFire )
		{
			nextFire = myTime + fireDelta;
			Fire();
			nextFire = nextFire - myTime;
            myTime = 0.0F;
		}*/

		if( Input.GetButton( "Jump" ) )
		{
			Fire();
		}

	}


	public void Fire()
	{
		
		if( isLaserActive )
		{
			weaponSystem.Laser();
			EventManager.TriggerEvent( "FireLaser" , 0 );
		}
		else
		{
			weaponSystem.Bullet();
			EventManager.TriggerEvent( "FireBullet" , 0 );
		}
	

		
		if( MissileLauncher.activeSelf )
		{
			missileSystem.Missile();
			EventManager.TriggerEvent( "FireMissile" , 0 );
		}


		if( angledMuzzle.activeSelf && !isLaserActive )
		{
			angledWeaponSystem.AngledBullet();
		}
	
	}


	//Change Ship Orientation basd on vertical movement
	private void AnimateShipMovement( float verticalPosition )
	{
		if( verticalPosition > 0f )
		{
			animator.SetBool( "ascending" , true );
		}
		else if( verticalPosition == 0f )
		{
			animator.SetBool( "ascending" , false );
			animator.SetBool( "descending" , false );
		}
		else
		{
			animator.SetBool( "descending" , true );
		}
	}

	//Make sure ship doesnt move beyond screen borders.
	private void RestrictShipMovement()
	{
		Vector3 pos = Camera.main.WorldToViewportPoint ( transform.position );
         pos.x = Mathf.Clamp( pos.x , 0.05f , 0.95f );
         pos.y = Mathf.Clamp( pos.y , 0.15f , 0.95f );
         transform.position = Camera.main.ViewportToWorldPoint( pos );
	}


	public void Destroy()
	{
		StartCoroutine( PlayerDeath() );	
	}

	//Player Death Animation and Cleanup
	private IEnumerator PlayerDeath()
	{
	
	if( option1.activeSelf )
		{
			option1.SetActive( false );
		}

		if( option2.activeSelf )
		{
			option2.SetActive( false );
		}

		animator.SetTrigger( "shipExplodes" );
		yield return new WaitForSeconds( 1.0f );
		gameObject.SetActive( false );
	} 


	private void OnTriggerEnter2D( Collider2D other )
	{
		if( other.tag == "Terrain" )
		{
			speed = 0;
			Destroy( );
		}
	}

	//Observer Pattern
	public void Register( IObserver observer )
	{
		observers.Add( observer );
		
	}

	public void UnRegister( IObserver observer )
	{
		observers.Remove( observer );
	}

	public void NotifyObserver()
	{
		foreach( IObserver observer in observers )
		{
			observer.OnNotify();
		}
	}

	public void ToggleMissile( int x )
	{
		if( MissileLauncher.activeSelf )
			MissileLauncher.SetActive( false );
		else
			MissileLauncher.SetActive( true );

		//EventManager.TriggerEvent( "ResetPowerUpCount" , 0 );	
	}

	public void ToggleOption1( int x )
	{
		if( option1.activeSelf )
			option1.SetActive( false );
		else
			option1.SetActive( true );
	}

	public void ToggleOption2( int x )
	{
		if( option2.activeSelf )
			option2.SetActive( false );
		else
			option2.SetActive( true );
	}

	public void ToggleLazer( int x )
	{
		if( x == 0 )
		{
			isLaserActive = false;
		}
		else
		{
			isLaserActive = true;
			EventManager.TriggerEvent( "DoubleStatus" , 0 );
		
		}
	}

	public void ToggleDouble( int x )
	{
		
		isLaserActive = false;

		
		if( x == 0 )
		{
			angledMuzzle.SetActive( false);	
				
		}
		else
		{
			angledMuzzle.SetActive( true );		
		}
			
		

	}

	public void SpeedUp( int x )
	{
		speed += x;
		//EventManager.TriggerEvent( "ResetPowerUpCount" , 0 );	
	}
}
