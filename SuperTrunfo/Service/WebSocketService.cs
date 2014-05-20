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

        private List<Action<Object>> listeners = new List<Action<Object>>();
        
        public void onMessage(Action<Object> listener) {
            listeners.Add(listener);
        }

        public void sendMessage<T>(Message<T> message) {
            webSocket.Send(JsonConvert.SerializeObject(message));
        }

        public void open() {

            webSocket.OnMessage += (sender, message) =>
                listeners.ForEach((listener) => {
                    var messageObject = JsonConvert.DeserializeObject<Message<Object>>(message.Data);

                    listener(JsonConvert.DeserializeObject(message.Data, Type.GetType(messageObject.className)));
                });
            ;

            webSocket.Connect();
        }

        public void onOpen(Action listener) {
            webSocket.OnOpen += (sender, e) => {
                listener();
            };
        }
    }
}
