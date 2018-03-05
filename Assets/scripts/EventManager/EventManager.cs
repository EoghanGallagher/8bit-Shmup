using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ThisEvent : UnityEvent<int>{}

public class EventManager : MonoBehaviour 
{
	

	
	[SerializeField]
	private Dictionary <string , ThisEvent> eventDictionary;

	private static EventManager eventManager;

	public static EventManager instance
	{
		get
		{
			if( !eventManager )
			{
				eventManager = FindObjectOfType( typeof (EventManager) ) as EventManager;
				
				if( !eventManager )
				{
					Debug.Log( "There needs to be one active EventManaget script on a GameObject in your scene" );
				}
				else
				{
					eventManager.Init();
				}

			}

			return eventManager;
		}
	}

	void Init()
	{
		if( eventDictionary == null )
		{
			eventDictionary = new Dictionary<string , ThisEvent>();
		}
	}

	

	
	//Single Parameter Int
	public static void StartListening( string eventName , UnityAction<int> listener ) 
	{
		ThisEvent thisEvent = null;

		if( instance.eventDictionary.TryGetValue( eventName , out thisEvent ) )
		{
			thisEvent.AddListener( listener );
		}
		else
		{
			thisEvent = new ThisEvent();
			thisEvent.AddListener( listener );
			instance.eventDictionary.Add( eventName , thisEvent );
		}
	}

	//Single Parameter Int
	public static void StopListening( string eventName , UnityAction<int> listener )
	{
		if( eventManager == null ) return;

		ThisEvent thisEvent = null;

		if( instance.eventDictionary.TryGetValue( eventName , out thisEvent ) )
		{
			thisEvent.RemoveListener( listener );
		}
	}


	//Single Parameter Int
	public static void TriggerEvent( string eventName , int value )
	{

		ThisEvent thisEvent = null;
		if( instance.eventDictionary.TryGetValue( eventName , out thisEvent ) )
		{
			thisEvent.Invoke( value );
		}
	}

}
