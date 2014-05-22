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
		
		public void addListener(Object evento, Action<Object> listener){
			if(!customListeners.ContainsKey(evento)){
                customListeners[evento] = new List<Action<Object>>();
			}
            customListeners[evento].Add(listener);
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

        private Dictionary<Events, List<Action<Object>>> listeners = new Dictionary<Events, List<Action<Object>>>();

        private Dictionary<Object, List<Action<Object>>> customListeners = new Dictionary<Object, List<Action<Object>>>();
	}
}

