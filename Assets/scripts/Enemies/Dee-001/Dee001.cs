using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dee001 : BaseCharacter , IFireable
{

	[SerializeField]
	private bool isFlippedHorizontally = false;

	private Transform playerShip , _transform;
	// Use this for initialization

	[SerializeField]
	private Transform muzzle;

	private Collider2D coll2D;

	private Animator anim;

	private float m_Angle;

	[SerializeField]
	private float fireDelay = 2.0f;

	private WeaponSystem weaponSystem;

	void OnEnable()
	{
		if( !coll2D.enabled )
			coll2D.enabled = true;

		ScoreValue = 100;	
	}
	
	IEnumerator Start () 
	{
		ScoreValue = 100;
		//Find Player
		playerShip = GameObject.FindGameObjectWithTag( "Player" ).transform;
		_transform = transform;

		anim = GetComponent<Animator>();

		weaponSystem = muzzle.GetComponent<WeaponSystem>();

		coll2D = GetComponent<Collider2D>();

		if( weaponSystem == null )
		{
			Debug.Log( "Weapon System is missing from DEE-01" );
			yield break;
		}

		while( gameObject.activeSelf)
		{
			yield return new WaitForSeconds( fireDelay );
			Fire();
		}


	}
	
	// Update is called once per frame
	void Update () 
	{

		FlipHorizontally();
		
		Aim();

	}

	//Flip Dee-01 to face player
	private void FlipHorizontally()
	{
		if( playerShip.position.x < transform.position.x )
		{
			if( !isFlippedHorizontally )
			{
				Flip();
				isFlippedHorizontally = true;	
			}
		}
		else
		{	
			if( isFlippedHorizontally )
			{
				Flip();
				isFlippedHorizontally = false;
			}
		}
	}


	//Aim dee-01 turret 
	//Get current angle between ship and Dee-01
	//Pass result to animator
	private void Aim()
	{
		 
		 m_Angle = Vector2.Angle( playerShip.position , _transform.position );

		 anim.SetFloat( "angle" , m_Angle );

	}

	//Called by Animator when an animation change occurs
	private void PositionMuzzle(  )
	{
		 muzzle = _transform.GetChild(0);
		
		 if( m_Angle > 0.0f &&  m_Angle <= 6.0f )
		 {
			 muzzle.localPosition = new Vector2( 0.3f , 0.335f );
		 }
		 else if( m_Angle > 6.0f && m_Angle <= 21  )
		 {
			 muzzle.localPosition = new Vector2( 0.29f , 0.55f );
		 }
		 else if( m_Angle > 21 )
		 {
			 muzzle.localPosition = new Vector2( 0.1f , 0.6f );
		 }
	}

	//Flip sprite
	 void Flip()
     {
         Vector2 theScale = _transform.localScale;
         theScale.x *= -1;
         _transform.localScale = theScale;
     }


	
	private bool isHit; 
	public override void Destroy()
	{
		
		if( !isHit )
		{
			isHit = true;
			coll2D.enabled = false;

			Debug.Log( "DEE-001 " + ScoreValue );
			EventManager.TriggerEvent( "Score" , ScoreValue );
			
			animator.SetTrigger( "Death" );
			
			base.Destroy();

		}
		
	}

	public void Fire()
	{
		weaponSystem.EnemyBullet();
	}

	
}
