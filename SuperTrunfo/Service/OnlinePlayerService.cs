﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperTrunfo.Assets.Service
{
    class OnlinePlayerService : PlayerService{
        
        private TurnService  turnService;

        private GameObserver gameObserver;

        private WebSocketService socket;
        
        public OnlinePlayerService() {
            turnService     = Container.get<TurnService>();
            gameObserver    = Container.get<GameObserver>();
            socket          = Container.get<WebSocketService>();
        }

        public Player createPlayer(){
            return new Player(System.Guid.NewGuid().ToString(), new List<Card>(), null, PlayerType.REMOTE);
        }

        public void play(Player player){
            
        }
    }
}
