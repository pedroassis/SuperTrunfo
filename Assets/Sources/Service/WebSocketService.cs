using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSocketSharp;
using Newtonsoft.Json;

namespace SuperTrunfo
{
    class WebSocketService
    {

        private WebSocket webSocket = new WebSocket("ws://localhost:81");

        private GameObserver gameObserver = Container.get<GameObserver>();
        
        public void onMessage(String name, Action<Object> listener) {
            gameObserver.addListener(name, listener);
        }

        public void sendMessage<T>(Message<T> message) {
            webSocket.Send(JsonConvert.SerializeObject(message));
        }

        public void open() {

            webSocket.OnMessage += (sender, message) => {
                var messageObject = JsonConvert.DeserializeObject<Message<Object>>(message.Data);

                try
                {
                    var d1 = typeof(Message<>);

                    Type[] typeArgs = { Type.GetType(messageObject.className) };

                    var typeBuilt = d1.MakeGenericType(typeArgs);

                    var messageEvent = JsonConvert.DeserializeObject(message.Data, typeBuilt);

                    gameObserver.trigger(messageObject.name, messageEvent);                    

                }
                catch (Exception e) {
                    UnityEngine.Debug.Log(e.StackTrace);
                }
            };

            webSocket.Connect();
        }

        public void onOpen(Action listener) {
            webSocket.OnOpen += (sender, e) => {
                listener();
            };
        }
    }
}
