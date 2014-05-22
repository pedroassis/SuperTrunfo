using UnityEngine;
using System.Collections;
using SuperTrunfo;

public class PlayLocalController : MonoBehaviour {

    private GameObserver    gameObserver;

    public GUITexture active;
    public GUITexture inactive;

    public Texture activeTex;
    public Texture inactiveTex;

    public float widthPercent;

    public PlayLocalController() {
        Configuration.configure();
        gameObserver = Container.get<GameObserver>();
    }

    public string eventName;
    public object eventMessage;
    
	void OnMouseOver(){
		if(Input.GetMouseButtonDown(0)){
            this.gameObject.guiTexture.texture = activeTex;
            TimeoutService.setTimeout(() => {
                this.gameObject.guiTexture.texture = inactiveTex;

                Debug.Log("GUI." + eventName);
                gameObserver.trigger("GUI." + eventName, eventMessage);

            }, 250);
		}
	}

    void OnGUI()
    {

        float width = Screen.width * widthPercent;

        float imageWidth = inactive.pixelInset.width;

        float imageHeight = inactive.pixelInset.height;

        float norm = imageWidth / width;

        float newHeight = imageHeight / norm;

        inactive.pixelInset = new Rect(-width / 2, -newHeight / 2, width, newHeight);

        //Debug.Log(width);
        //Debug.Log(imageWidth);
        //Debug.Log(imageHeight);
        //Debug.Log(norm);
        //Debug.Log(norm * imageHeight);

    }


}
