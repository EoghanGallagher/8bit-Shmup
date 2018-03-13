
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	
	
	[SerializeField]
	private Transform target;
	
	[SerializeField]
	private float smoothSpeed = 0.125f;
	// Use this for initialization

	public Vector3 offset; 

	void Awake()
	{
		DontDestroyOnLoad( gameObject );
	}


   

	void FixedUpdate()
	{
		
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothPosition = Vector3.Lerp( transform.position , desiredPosition , smoothSpeed );

		transform.position = smoothPosition;

	}
}
