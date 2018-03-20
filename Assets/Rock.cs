using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : BaseCharacter
{

	private Rigidbody2D r2d;
	private Collider2D col2D;

	[SerializeField]
	private float force = 1.00f,
	xForceMin = 11.0f,
	xForceMax = 15.0f;
	
	bool isOkToFly = true;

	private bool isCoRoutineRunning = false;

	private Vector2 movementDirection;

	void OnEnable()
	{
		StartCoroutine( "LaunchRock" );
		ScoreValue = 100;
		isHit = false;
	}

	// Use this for initialization
	void Start ()
	{
		if( !isCoRoutineRunning )
			StartCoroutine( "LaunchRock" );	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if( isOkToFly )
			r2d.AddForce( movementDirection * force , ForceMode2D.Impulse );
			
	}

	private IEnumerator LaunchRock()
	{
		isCoRoutineRunning = true;
	    
		
		r2d = GetComponent<Rigidbody2D>();
		col2D = GetComponent<Collider2D>();
		animator = GetComponent<Animator>();

		r2d.constraints = RigidbodyConstraints2D.None;

		movementDirection = new Vector2( Random.Range( -1.0f , 1.0f  ) , 1 );

		force = Random.Range( xForceMin , xForceMax );

		isOkToFly = true;

		yield return null;

		isOkToFly = false;
		col2D.isTrigger = true;

		isCoRoutineRunning = false;

		yield break;
	}

	private bool isHit; 
	public override void Destroy()
	{
		
		if( !isHit )
		{
			isHit = true;

			
			//Update score with this enemies score value
			EventManager.TriggerEvent( "Score" , ScoreValue );
			
			r2d.constraints = RigidbodyConstraints2D.FreezeAll;
			
			//Trigger death animation
			animator.SetTrigger( "death" );
			
			//Use parents destroy method
			base.Destroy();

		}
		
	}


}
