using UnityEngine;
using System.Collections;
using SuperTrunfo;

public class NPCController : MonoBehaviour {

    private TurnService turnService;

    private GameObserver gameObserver;

    private NPCPlayerService npcService;

	// Use this for initialization
	public NPCController () {

        turnService = Container.get<TurnService>();

        gameObserver = Container.get<GameObserver>();

        npcService = Container.get<NPCPlayerService>();

        gameObserver.addListener(Events.NEXT_PLAYERS_TURN, (playerMessage) => {
            Player player = playerMessage as Player;

            if (player.playerType == PlayerType.NPC) {

                npcService.play(player);
            }
        });

	}
	
	// Update is called once per frame
}
