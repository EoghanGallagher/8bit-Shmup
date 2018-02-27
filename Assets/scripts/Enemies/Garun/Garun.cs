using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garun : MonoBehaviour , IDestroyable
{
	[SerializeField]
	private float horizontalSpeed;
	
	[SerializeField]
	private float verticalSpeed;

	[SerializeField]
	private float amplitude;
	private Rigidbody2D rigidbody2D;

	public Vector2 tempPosition;

	public Animator animator;

	// Use this for initialization
	void Start () 
	{
		
		rigidbody2D = GetComponent< Rigidbody2D >();
		tempPosition = transform.position;
		animator = GetComponent<Animator>( );
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		tempPosition.x = horizontalSpeed;
		tempPosition.y = Mathf.Sin( Time.realtimeSinceStartup * verticalSpeed ) * amplitude ;
		rigidbody2D.velocity = tempPosition;
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
}
