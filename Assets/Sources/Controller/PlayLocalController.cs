using UnityEngine;
using System.Collections;
using SuperTrunfo;

public class PlayLocalController : MonoBehaviour {

    private GameObserver gameObserver = Container.get<GameObserver>();

    public GUITexture active;
    public GUITexture inactive;

    public string eventName;
    
	void OnMouseOver(){
		if(Input.GetMouseButtonDown(0)){
            gameObserver.trigger(Events.START_LOCAL_GAME, null);
		}
	}

}
