using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBonusPowerUp : MonoBehaviour {

	[SerializeField]
	private int score;
	// Use this for initialization

	private int bonuslevelId;

	[SerializeField]
	private SpriteRenderer bonusSpriteRenderer;

	[SerializeField]
	private Sprite bonusSprite;	
	void Start () 
	{
		bonusSpriteRenderer.sprite = null;
		
		score = 5000;
	}
	
	// Update is called once per frame
	private void OnTriggerEnter2D( Collider2D other )
	{
		if( other.tag == "Player" )
		{
			//Play PowerUp sound

			//Update Score
			EventManager.TriggerEvent( "Score" , score );

			//Notify Game manager that Bonus level is now unlocked

			//Deactivate Self
			StartCoroutine( "ShowSprite" );
		}
	}

	IEnumerator ShowSprite()
	{
		
		bonusSpriteRenderer.sprite = bonusSprite;
		yield return new WaitForSeconds( 3.0f );

		gameObject.SetActive( false );

	}
}
