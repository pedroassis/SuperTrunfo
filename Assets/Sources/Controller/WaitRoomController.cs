using UnityEngine;
using System.Collections;
using SuperTrunfo;

public class WaitRoomController : MonoBehaviour {

    private GameObserver            gameObserver = Container.get<GameObserver>();

    private TurnService             turnService = Container.get<TurnService>();

    public GUITexture               playButton;

    private bool                    isEnabled;

    private bool finishedWaiting;

    public WaitRoomController(){

        gameObserver.addListener("gameStarted", (message) => {

            turnService.startGame();

            finishedWaiting = true;

        }, this);

        gameObserver.addListener("playerAdded", (playerMessage) =>{

            var player = ((Message<Player>) playerMessage).message;

            turnService.addPlayer(player);

            isEnabled = true;
        }, this);
	}

    void Update() {
        playButton.enabled = isEnabled && turnService.isMaster;

        if (finishedWaiting)
        {
            Application.LoadLevel("GamePlay");
        }
    }

    public void OnDestroy () {
        Debug.Log(this.GetType().Name + " was destroyed");

        gameObserver.removeListeners(this);
	}

}
