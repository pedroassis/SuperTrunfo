using UnityEngine;
using System.Collections;
using SuperTrunfo;

public class GameStartController : MonoBehaviour {

    private GameObserver gameObserver = Container.get<GameObserver>();

	// Use this for initialization
	void Start () {

        gameObserver.addListener(Events.START_LOCAL_GAME, (message) => {
            Application.LoadLevel("NPCChoose");
        });

        gameObserver.addListener(Events.START_MULTIPLAYER_GAME, (message) => {
            Application.LoadLevel("ChooseRoom");
        });
	}
}
