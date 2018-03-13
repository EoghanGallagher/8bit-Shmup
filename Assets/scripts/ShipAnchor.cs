using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class Destination
{
	public int id;
	public Transform destination;
	
}

public class ShipAnchor : MonoBehaviour {
		
	[SerializeField]
	private float speed;

	public List<Destination> destinations;
	// Use this for initialization

	[SerializeField]
	private Transform currentDestination;
	
	
	private void OnEnable()
	{
		EventManager.StartListening( "StartMoving" , StartMoving );
		EventManager.StartListening( "StopMoving" , StopMoving );
		EventManager.StartListening( "UpdateDestination" , UpdateDestination );
	}

	private void OnDisable()
	{
		EventManager.StopListening( "StartMoving" , StartMoving );
		EventManager.StopListening( "StopMoving" , StopMoving );
		EventManager.StopListening( "UpdateDestination" , UpdateDestination );
	}

	private void Awake()
	{
		DontDestroyOnLoad( gameObject );
	}
	void Start()
	{
		//Set initial Destination
		currentDestination = destinations[0].destination;
	}
	
	
	
	// Update is called once per frame

	void FixedUpdate () 
	{	
		transform.position = Vector2.MoveTowards( transform.position , currentDestination.position, speed * Time.fixedDeltaTime );	
	}

	private void UpdateDestination( int x )
	{
		
		currentDestination = destinations[x].destination;
		Debug.Log( "Updating Destination : " + currentDestination.name );
		StopMoving( 0 );
	}

	private void StartMoving( int x)
	{
		speed = 1;
	}


	private void StopMoving( int x )
	{
		Debug.Log( "Stopped Moving" );
		speed = 0;
	}
}
