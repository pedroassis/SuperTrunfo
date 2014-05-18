using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketIOClient;
using SocketIOClient.Messages;
using Newtonsoft.Json;

namespace SuperTrunfo
{
    class WebSocketService
    {
        
        private Client 
            webSocket = new Client("http://localhost:81");

        private List<Action<Object>> listeners = new List<Action<object>>();
        
        public void onMessage(Action<Object> listener) {
            listeners.Add(listener);
        }

        public void sendMessage(Object message) {
            webSocket.Emit("news", message.ToString());
        }

        public void open() {

            webSocket.On("news", (data) => {
                listeners.ForEach((listener) => listener(data.MessageText));
            });

            webSocket.Connect();
        }

        public void onOpen(Action listener) {
            webSocket.On("connect", (sender) => {
                listener();
            });
        }
    }
}
