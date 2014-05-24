using System;

namespace SuperTrunfo
{
	public enum Events
	{
        GAME_OVER,
        DAMAGE,

        END_TURN,
        TURN_WINNER,
        NEXT_PLAYERS_TURN,
        LOCAL_PLAYERS_TURN,
        CARD_TO_TABLE,
        NPC_TURN,
        TRUMPH_ON_GAME,

        FINISH_LEVEL,
        START_LOCAL_GAME,
        START_MULTIPLAYER_GAME,
        PAUSE_GAME,
		RESUME_GAME,

        NEW_ENEMY,
        ENEMY_DOWN,
		NEW_WAVE,
		END_WAVE,
		BUY,
		NOT_ENOUGHT_CASH
	}
}

