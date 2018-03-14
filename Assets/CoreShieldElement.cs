using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreShieldElement : MonoBehaviour {

	[SerializeField]
	private int id ;
	void OnEnable()
	{
		EventManager.StartListening( "Deactivate" , Deactivate );
	}

	void OnDisable()
	{
		EventManager.StopListening( "Deactivate" , Deactivate );
	}
	
	private void Deactivate( int x )
	{
		if( x == id )
		{
			gameObject.SetActive( false );
		}
	}
	
	
}
