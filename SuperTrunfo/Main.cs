using System.IO;
using System.Collections.Generic;
using System;

namespace SuperTrunfo
{
	class MainClass
	{


		public static void Main ()
		{
            
            //WebSocketService socket = Container.get<WebSocketService>();

            //socket.onMessage((message) => Console.WriteLine(message));

            //Card card = new Card("A1", "21", 2121, 212121, 21212121, 21212, 2121212121);

            //Message<Card> messageObj = new Message<Card>();

            //messageObj.className = card.GetType().FullName;

            //messageObj.name = "card";

            //messageObj.message = card;

            //socket.onOpen(() => socket.sendMessage(messageObj));

            //socket.open();      
     
            /*
             * Should really use a Test framework ;)
             * But no time, I guess
             */

            Configuration.configure();

            DataSource dataSource = Container.get<FileDataSource>();

            List<Card> cards = new List<Card>();

            cards.Add(new Card("A1", "21", 2121, 212121, 21212121, 21212, 2121212121));
            cards.Add(new Card("A2", "32", 3232, 323232, 32323232, 3232323, 32333232));
            cards.Add(new Card("A3", "21", 2121, 212121, 21212121, 212121221, 2121212122));
            cards.Add(new Card("A4", "54", 5454, 545454, 54545454, 54545445, 545445454));
            cards.Add(new Card("A5", "21", 2121, 212121, 21212121, 2121212121, 2121212121));

            dataSource.setDataSource(cards);

            cards = dataSource.getDataSource<Card>();

            cards.ForEach((card) =>
            {
                Console.WriteLine(card.name);
            });

            DataSourceStrategy<Card> strategy = Container.get<DataSourceStrategy<Card>>();

            Console.WriteLine(strategy.getById("A2").id);
            Console.WriteLine(strategy.getById("A2").name);
            Console.WriteLine(strategy.getById("A2").hability);
            Console.WriteLine(strategy.getById("A2").force);
            Console.WriteLine(strategy.getById("A2").velocity);
            Console.WriteLine(strategy.getById("A2").equipment);
            Console.WriteLine(strategy.getById("A2").inteligence);

            Console.ReadKey();
		}
	}
}
