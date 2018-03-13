using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dee001 : BaseCharacter 
{

	[SerializeField]
	private bool isFlippedHorizontally = false;

	private Transform playerShip;

	private Transform _transform;
	// Use this for initialization

	[SerializeField]
	private Transform muzzle;

	private Animator anim;

	private float m_Angle;
	void Start () 
	{
		playerShip = GameObject.FindGameObjectWithTag( "Player" ).transform;
		_transform = transform;

		anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () 
	{

		FlipHorizontally();
		
		Aim();

	}

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
		 Debug.Log( "Position Muzzle : " + muzzle.name );
		
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

	 void Flip()
     {
         Vector2 theScale = _transform.localScale;
         theScale.x *= -1;
         _transform.localScale = theScale;
     }


	public override void Destroy()
	{
		base.Destroy();	
		
	}

	
}
