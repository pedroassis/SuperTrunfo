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

    void Start() {
        inactiveTex = inactive.texture;
        activeTex   = active.texture;
    }

    public string eventName;
    
	void OnMouseOver(){
		if(Input.GetMouseButtonDown(0)){
            this.gameObject.guiTexture.texture = activeTex;
            TimeoutService.setTimeout(() => {
                this.gameObject.guiTexture.texture = inactiveTex;

                gameObserver.trigger(eventName, eventName);

            }, 250);
		}
	}

}
