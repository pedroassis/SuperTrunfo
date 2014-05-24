using System;
using System.Collections.Generic;

namespace SuperTrunfo
{
	public class GameObserver
	{
		public GameObserver ()
		{
		}
		
		
		public void addListener(Events evento, Action<Object> listener){
			if(!listeners.ContainsKey(evento)){
				listeners[evento] = new List<Action<Object>>();
			}
			listeners[evento].Add(listener);
		}
		public void addListener(Events evento, Action<Object> listener, Object context){
			if(!listeners.ContainsKey(evento)){
				listeners[evento] = new List<Action<Object>>();
			}
            listeners[evento].Add(listener);
            addBinder(context, listener);
		}
		
		public void addListener(Object evento, Action<Object> listener){
			if(!customListeners.ContainsKey(evento)){
                customListeners[evento] = new List<Action<Object>>();
			}
            customListeners[evento].Add(listener);
		}
		
		public void addListener(Object evento, Action<Object> listener, Object context){
			if(!customListeners.ContainsKey(evento)){
                customListeners[evento] = new List<Action<Object>>();
			}
            customListeners[evento].Add(listener);
            addBinder(context, listener);
		}
		
		public void removeListener(Events evento, Action<Object> listener){
			if(!listeners.ContainsKey(evento)){
				listeners[evento] = new List<Action<Object>>();
			}
			listeners[evento].Remove(listener);
		}
		
		public void removeListener(Object evento, Action<Object> listener){
			if(!customListeners.ContainsKey(evento)){
                customListeners[evento] = new List<Action<Object>>();
			}
            customListeners[evento].Remove(listener);
		}
		
		public void trigger(Events evento, Object message){
			if(listeners.ContainsKey(evento)){
				listeners[evento].ForEach(delegate(Action<Object> listener){
					listener(message);
				});
			}
		}
		
		public void trigger(Object evento, Object message){
            UnityEngine.Debug.Log(evento);
            UnityEngine.Debug.Log(customListeners.ContainsKey(evento));
			if(customListeners.ContainsKey(evento)){
				customListeners[evento].ForEach(delegate(Action<Object> listener){
                    try
                    {
                        listener(message);
                    }
                    catch (Exception ex) {
                        UnityEngine.Debug.Log("Exception " + ex.Message);
                        UnityEngine.Debug.Log("Exception " + ex.StackTrace);
                    }
				});
			}
		}

        public void removeListeners(Object context) { 

            if(contextBinder.ContainsKey(context)){
                contextBinder[context].ForEach((listener) => {
                    foreach(Object key in customListeners.Keys){
                        if (customListeners[key].Contains(listener)) {
                            customListeners[key].Remove(listener);
                        }
                    }
                });
                contextBinder.Remove(context);
            }
        }

        private void addBinder(Object context, Action<Object> listener) {
            
			if(!contextBinder.ContainsKey(context)){
                contextBinder[context] = new List<Action<Object>>();
			}
            contextBinder[context].Add(listener);
        }

        private Dictionary<Events, List<Action<Object>>> listeners = new Dictionary<Events, List<Action<Object>>>();

        private Dictionary<Object, List<Action<Object>>> customListeners = new Dictionary<Object, List<Action<Object>>>();

        private Dictionary<Object, List<Action<Object>>> contextBinder = new Dictionary<Object, List<Action<Object>>>();


	}
}

