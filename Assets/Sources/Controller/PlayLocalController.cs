using UnityEngine;
using System.Collections;
using SuperTrunfo;

public class PlayLocalController : MonoBehaviour {

    private GameObserver    gameObserver;

    public GUITexture active;
    public GUITexture inactive;

    public Texture activeTex;
    public Texture inactiveTex;

    public PlayLocalController() {
        Configuration.configure();
        gameObserver = Container.get<GameObserver>();
    }

    public string eventName;
    
	void OnMouseOver(){
		if(Input.GetMouseButtonDown(0)){
            this.gameObject.guiTexture.texture = activeTex;
            TimeoutService.setTimeout(() => {
                this.gameObject.guiTexture.texture = inactiveTex;

                Debug.Log("GUI." + eventName);
                gameObserver.trigger("GUI." + eventName, eventName);

            }, 250);
		}
	}

}
