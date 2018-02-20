
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

	void Start()
	{
		playerOne.Select();
	}

	public void TestButton()
	{
		Debug.Log( "Player One Button Clicked Ok" );

		StartCoroutine( Blink( playerOne ) );
		
	}

	public void TestButton2()
	{
		Debug.Log( "Player Two Button Clicked Ok" );

		StartCoroutine( Blink( playerTwo ) );
	}


	public IEnumerator Blink( Button button )
	{
		int x = 0;

		while( x <= 6 )
		{
			button.gameObject.SetActive( false );
			yield return new WaitForSeconds( 0.15f );
			
			button.gameObject.SetActive( true );
			yield return new WaitForSeconds( 0.15f );
			
			x++;
		}

		SceneManager.LoadScene( "level_1" );
	}
	
}
