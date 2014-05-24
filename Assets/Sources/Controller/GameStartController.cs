using UnityEngine;
using System.Collections.Generic;
using SuperTrunfo;

public class GameStartController : MonoBehaviour {

    private GameObserver gameObserver = Container.get<GameObserver>();

    private TurnService turnService = Container.get<TurnService>();

    private LocalPlayerService localPlayer = Container.get<LocalPlayerService>();

    private NPCPlayerService NPCPlayer = Container.get<NPCPlayerService>();

    private OnlinePlayerService onlinePlayer = Container.get<OnlinePlayerService>();

    private WebSocketService webSocketService = Container.get<WebSocketService>();

	void Start () {

        gameObserver.addListener("GUI.SinglePlayer", (message) => {
            Debug.Log("Play local");
            Application.LoadLevel("MainSingle");
        }, this);

        gameObserver.addListener("GUI.MainMenu", (message) => {
            Debug.Log("MainMenu");
            Application.LoadLevel("MainMenu");
        }, this);

        gameObserver.addListener("GUI.Multiplayer", (message) => {
            Debug.Log("Multiplayer");
            Application.LoadLevel("MainMultiplayer");
        }, this);

        gameObserver.addListener("GUI.Quit", (message) => {
            Debug.Log("Quit");
            Application.Quit();
        }, this);

        gameObserver.addListener("GUI.CreateRoom", (message) => {

            GameObject game = GameObject.Find("Multiplayer");

            webSocketService.onMessage("roomAdded", (roomMessage) => {

            });

            turnService.isMaster = true;

            MultiplayerScene scene = game.GetComponent<MultiplayerScene>() as MultiplayerScene;

            Debug.Log("CreateRoom");
            if (scene.userInput.Length > 0) {
                List<Player> players = new List<Player>();

                players.Add(onlinePlayer.createPlayer());
                Room room = new Room(System.Guid.NewGuid().ToString(), scene.userInput, players);

                turnService.currentRoom = room;

                webSocketService.sendMessage<Room>(new Message<Room>("createRoom", room, "SuperTrunfo.Room"));

                Application.LoadLevel("WaitRoom");
            }

        }, this);

        gameObserver.addListener("GUI.NPCOne", (message) => {

            Debug.Log("One");

            turnService.currentRoom = new Room(System.Guid.NewGuid().ToString(), "LocalPlay", new List<Player>());

            turnService.addPlayer(localPlayer.createPlayer());

            turnService.addPlayer(NPCPlayer.createPlayer());

            turnService.startGame();

            Application.LoadLevel("GamePlay");
        }, this);

        gameObserver.addListener("GUI.NPCTwo", (message) => {

            turnService.addPlayer(localPlayer.createPlayer());

            turnService.addPlayer(NPCPlayer.createPlayer());

            turnService.addPlayer(NPCPlayer.createPlayer());

            Application.LoadLevel("GamePlay");
        }, this);

        gameObserver.addListener("GUI.NPCThree", (message) => {

            turnService.addPlayer(localPlayer.createPlayer());

            turnService.addPlayer(NPCPlayer.createPlayer());

            turnService.addPlayer(NPCPlayer.createPlayer());

            turnService.addPlayer(NPCPlayer.createPlayer());

            Application.LoadLevel("GamePlay");
        }, this);

	}
    public void OnDestroy () {
        Debug.Log(this.GetType().Name + " was destroyed");

        gameObserver.removeListeners(this);
	}
}
