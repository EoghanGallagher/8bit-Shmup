
using UnityEngine;

public class ShipAnchor : MonoBehaviour {
	
	[SerializeField]
	private float speed;

	public Transform destination;
	// Use this for initialization
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		transform.position = Vector2.MoveTowards( transform.position , destination.position, speed * Time.fixedDeltaTime );	
	}
}
