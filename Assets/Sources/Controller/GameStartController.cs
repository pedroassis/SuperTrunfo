using UnityEngine;
using System.Collections;
using SuperTrunfo;

public class GameStartController : MonoBehaviour {

    private GameObserver gameObserver = Container.get<GameObserver>();

    private TurnService turnService = Container.get<TurnService>();

    private LocalPlayerService localPlayer = Container.get<LocalPlayerService>();

    private NPCPlayerService NPCPlayer = Container.get<NPCPlayerService>();

	void Start () {

        gameObserver.addListener("GUI.SinglePlayer", (message) => {
            Debug.Log("Play local");
            Application.LoadLevel("MainSingle");
        });

        gameObserver.addListener("GUI.MainMenu", (message) => {
            Debug.Log("MainMenu");
            Application.LoadLevel("MainMenu");
        });

        gameObserver.addListener("GUI.Multiplayer", (message) => {
            Debug.Log("Multiplayer");
            Application.LoadLevel("MainMultiplayer");
        });

        gameObserver.addListener("GUI.Quit", (message) => {
            Debug.Log("Quit");
            Application.Quit();
        });

        gameObserver.addListener("GUI.NPCOne", (message) => {

            Debug.Log("One");

            turnService.addPlayer(localPlayer.createPlayer());

            turnService.addPlayer(NPCPlayer.createPlayer());

            Application.LoadLevel("GamePlay");
        });

        gameObserver.addListener("GUI.NPCTwo", (message) => {

            turnService.addPlayer(localPlayer.createPlayer());

            turnService.addPlayer(NPCPlayer.createPlayer());

            turnService.addPlayer(NPCPlayer.createPlayer());

            Application.LoadLevel("GamePlay");
        });

        gameObserver.addListener("GUI.NPCThree", (message) => {

            turnService.addPlayer(localPlayer.createPlayer());

            turnService.addPlayer(NPCPlayer.createPlayer());

            turnService.addPlayer(NPCPlayer.createPlayer());

            turnService.addPlayer(NPCPlayer.createPlayer());

            Application.LoadLevel("GamePlay");
        });

        gameObserver.addListener(Events.START_MULTIPLAYER_GAME, (message) => {
            Application.LoadLevel("ChooseRoom");
        });
	}
}
