using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour {

	
	private UnityAction scoreListener;


	[SerializeField]
	private Text scoreTxt , hiScoreTxt;

	[SerializeField]
	private int currentScore;
	
	[SerializeField]
	private int HiScore = 0;



	public void Awake()
	{
		
	}
	
	public void OnEnable()
	{
		EventManager.StartListening( "Score" , UpdateScore );
	}

	public void OnDisable()
	{
		EventManager.StopListening( "Score" , UpdateScore );
	}
	// Use this for initialization
	void Start () 
	{
	
	}
	
	

	public void UpdateScore( int score  )
	{
	
		currentScore = int.Parse( scoreTxt.text ); 

		currentScore  = currentScore + score;

		scoreTxt.text = currentScore.ToString( "0000000" );

		UpdateHiScore();
	 
	}

	private void  UpdateHiScore()
	{
		if( currentScore > HiScore )
		{
			hiScoreTxt.text = currentScore.ToString( "0000000" );
		}
	}
}
