using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperTrunfo
{
    class Configuration
    {
        public static bool isConfigured = false;

        public static void configure() {

            if (Configuration.isConfigured){
                return;
            }
            isConfigured = true;

            Container.set(new GameObserver());

            Container.set(new DataSourceStrategy<Card>());

            Container.set(new WebSocketService());

            Container.set(new TurnService());

            Container.set(new LocalPlayerService());

            Container.set(new NPCPlayerService());

            Container.set(new OnlinePlayerService());

            Container.set(new TimeoutService());
        }
    }
}
