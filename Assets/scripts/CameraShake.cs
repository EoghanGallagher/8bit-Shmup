using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour 
{

	private void OnEnable()
	{
		EventManager.StartListening( "StartShake" , StartShake );
	}

	private void OnDisable()
	{
		EventManager.StopListening( "StartShake" , StartShake );
	}
	
	IEnumerator Shake( float duration , float magnitude )
	{
		Vector3 originalPos = transform.localPosition;

		float elapsed = 0.0f;

		while( elapsed < duration )
		{
			float x = Random.Range( -1f , 1f ) * magnitude;
			float y = Random.Range( -1f , 1f ) * magnitude;

			transform.localPosition = new Vector3( x, y , originalPos.z );
			elapsed += Time.deltaTime;
			yield return null;
		}

		transform.localPosition = originalPos;

		yield break;
	}

	private void StartShake( int x )
	{
		StartCoroutine( Shake( 0.15f , 0.5f ) );

	}




}
