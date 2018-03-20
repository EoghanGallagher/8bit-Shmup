using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockLauncher : MonoBehaviour {

	private WeaponSystem weaponSystem;

	[SerializeField]
	private float speed , delay ;


	void OnEnable()
	{
		EventManager.StartListening( "StartEruption" , StartEruption );
	}

	void OnDisable()
	{
		EventManager.StopListening( "StartEruption" , StartEruption );
	}

	
	private void StartEruption( int x)
	{
		StartCoroutine( Eruption() );
	}


	private IEnumerator Eruption()
	{
		yield return new WaitForSeconds( delay );
		weaponSystem = GetComponent<WeaponSystem>();
		int count = 0;
		
		while( count < 250 )
		{
			yield return new WaitForSeconds( speed );
			weaponSystem.Rock();
			count++;
		}
	}
	
	
}
