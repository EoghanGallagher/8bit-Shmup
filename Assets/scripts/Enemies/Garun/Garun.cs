using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garun : BaseCharacter
{
	[SerializeField]
	private float horizontalSpeed;
	
	[SerializeField]
	private float verticalSpeed;

	[SerializeField]
	private float amplitude;
	private Rigidbody2D rigidbody2d;

	public Vector2 tempPosition;


	// Use this for initialization
	void Start () 
	{
		
		rigidbody2d = GetComponent< Rigidbody2D >();
		tempPosition = transform.position;
		animator = GetComponent<Animator>( );
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		tempPosition.x = horizontalSpeed;
		tempPosition.y = Mathf.Sin( Time.realtimeSinceStartup * verticalSpeed ) * amplitude ;
		rigidbody2d.velocity = tempPosition;
	}

	public override void Destroy()
	{
		animator.SetTrigger( "death" );
		base.Destroy();
	}

}
