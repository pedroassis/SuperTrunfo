using UnityEngine;
using System.Collections.Generic;
using SuperTrunfo;

public class GameStartController : MonoBehaviour {

    private GameObserver gameObserver = Container.get<GameObserver>();

    private TurnService turnService = Container.get<TurnService>();

    private LocalPlayerService localPlayer = Container.get<LocalPlayerService>();

    private NPCPlayerService NPCPlayer = Container.get<NPCPlayerService>();

    private WebSocketService webSocketService = Container.get<WebSocketService>();

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

        gameObserver.addListener("GUI.CreateRoom", (message) => {
            Debug.Log("CreateRoom");

            webSocketService.onMessage("roomAdded", (roomMessage) => {
                Debug.Log(roomMessage);
            });

            webSocketService.open();

            webSocketService.sendMessage<Room>(new Message<Room>("createRoom", new Room("fghg425", "Room Teste", new List<Player>()), "SuperTrunfo.Room"));

        });

        gameObserver.addListener("GUI.NPCOne", (message) => {

            Debug.Log("One");

            turnService.currentRoom = new Room(System.Guid.NewGuid().ToString(), "LocalPlay", new List<Player>());

            turnService.addPlayer(localPlayer.createPlayer());

            turnService.addPlayer(NPCPlayer.createPlayer());

            turnService.startGame();

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

	}
}
