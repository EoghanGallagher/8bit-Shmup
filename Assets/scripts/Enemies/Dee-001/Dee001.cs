using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dee001 : MonoBehaviour 
{

	[SerializeField]
	private Sprite facingUp, facingDiagonal, facingForward;

	private Transform playerShip;

	private Transform _transform;
	// Use this for initialization
	void Start () 
	{
		playerShip = GameObject.FindGameObjectWithTag( "Player" ).transform;
		_transform = transform;

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{

		if( playerShip.position.x < transform.position.x )
		{
			
		}
		
	}
}
