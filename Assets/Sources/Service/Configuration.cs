﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperTrunfo
{
    class Configuration
    {
        public static void configure() {
            
            Container.set(new DataSourceStrategy<Card>());

            Container.set(new GameObserver());

            Container.set(new TurnService());

            Container.set(new WebSocketService());
        }
    }
}