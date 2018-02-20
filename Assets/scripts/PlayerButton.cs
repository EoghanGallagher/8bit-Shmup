using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerButton : MonoBehaviour, ISelectHandler
{

	[SerializeField]
	private Transform shipIcon;

	[SerializeField]
	private Transform p1Anchor , p2Anchor;
	

	public void OnSelect(BaseEventData eventData)
    {
        
		if( gameObject.name == "Player1Button" )
			shipIcon.position = p1Anchor.position;
		
		if( gameObject.name == "Player2Button" )
			shipIcon.position = p2Anchor.position;

    }
	
}
