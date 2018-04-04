using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent ( typeof(Animator))]
public class ShipAnimator : MonoBehaviour 
{
	private Animator animator;
	private ShipInput shipInput;

	void Awake()
	{
		animator = GetComponent<Animator>();
		shipInput = GetComponent<ShipInput>();
	}

	// Update is called once per frame
	void Update () 
	{
		AnimateShipMovement( shipInput.Vertical );
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
}
