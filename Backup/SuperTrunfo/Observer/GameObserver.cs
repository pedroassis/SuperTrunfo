using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class GameObserver
	{
		public GameObserver ()
		{
		}
		
		public static GameObserver INSTANCE = new GameObserver();
		
		public void addListener(Events evento, Action<Object> listener){
			if(!listeners.ContainsKey(evento)){
				listeners[evento] = new List<Action<Object>>();
			}
			listeners[evento].Add(listener);
		}
		
		public void removeListener(Events evento, Action<Object> listener){
			if(!listeners.ContainsKey(evento)){
				listeners[evento] = new List<Action<Object>>();
			}
			listeners[evento].Remove(listener);
		}
		
		public void trigger(Events evento, Object message){
			if(listeners.ContainsKey(evento)){
				listeners[evento].ForEach(delegate(Action<Object> listener){
					listener(message);
				});
			}
		}
		
		private Dictionary<Events, List<Action<Object>>> listeners = new Dictionary<Events, List<Action<Object>>>();
	}
}

