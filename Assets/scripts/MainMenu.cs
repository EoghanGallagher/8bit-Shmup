
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour 
{

	[SerializeField]
	public Button playerOne;
	[SerializeField]
	public  Button playerTwo;


	[SerializeField]
	public float delay = 0.1f;

	void Start()
	{
		//Select Player one button as default
		playerOne.Select();
	}

	public void PlayerOne()
	{
		StartCoroutine( Blink( playerOne ) );
	}

	public void PlayerTwo()
	{
		Debug.Log( "Player Two Button Clicked Ok" );

		

		StartCoroutine( Blink( playerTwo ) );
	}


	public IEnumerator Blink( Button button )
	{
		int x = 0;

		//Trigger Audio here
		EventManager.TriggerEvent( "PlaySound" , 1 );
		
		while( x <= 3 )
		{
			button.gameObject.SetActive( false );
			yield return new WaitForSeconds( delay );
			
			button.gameObject.SetActive( true );
			yield return new WaitForSeconds( delay );
			
			x++;
		}

		SceneManager.LoadScene( "level_1" );
	}
	
}
