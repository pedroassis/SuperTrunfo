using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace SuperTrunfo
{
	public class TimeoutService
	{
        private static GameObserver gameObserver = Container.get<GameObserver>();
		
		static TimeoutService (){
			watcher.Start();
			
			gameObserver.addListener(Events.PAUSE_GAME, delegate(object obj) {
				watcher.Stop();
			});
			
			gameObserver.addListener(Events.RESUME_GAME, delegate(object obj) {
				watcher.Start();
			});
		}
		
		private static readonly Stopwatch watcher = new Stopwatch();
		
		private static readonly Dictionary<Action, long> handlers = new  Dictionary<Action, long>();
		
		public static void setTimeout(Action action, long delay){
			long whenToStop = watcher.ElapsedMilliseconds + delay;
			handlers[action] = whenToStop;
		}
		
		public static void check(){
			
			List<Action> toRemove = new List<Action>();

			
			foreach (Action action in handlers.Keys) {
                long whenToStop = handlers[action];
				if(watcher.ElapsedMilliseconds > whenToStop){
                    action();
					toRemove.Add(action);
				}
			}
			toRemove.ForEach((action) => {
				handlers.Remove(action);
			});
		}
	}
}

