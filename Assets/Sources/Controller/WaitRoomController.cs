using UnityEngine;
using System.Collections;
using SuperTrunfo;

public class WaitRoomController : MonoBehaviour {

    private GameObserver            gameObserver = Container.get<GameObserver>();

    private TurnService             turnService = Container.get<TurnService>();

    public GUITexture               playButton;

    private bool                    isEnabled;

    public WaitRoomController(){

        gameObserver.addListener("roomCreated", (message) => {

        });

        gameObserver.addListener("playerAdded", (playerMessage) =>{

            var player = ((Message<Player>)playerMessage).message;

            turnService.addPlayer(player);

            isEnabled = true;
        });
	}

    void Update() {
        playButton.enabled = isEnabled;
    }

}
