namespace SimpleEventBus{
	using System;
	using System.Collections.Generic;
	
	public static class EventBus<T>
	{
		private static readonly Dictionary<object,Action<object,T>> channels;
		private static readonly object global = new object();
		
		static EventBus(){
			channels = new Dictionary<object, Action<object, T>>();
			channels.Add(global,delegate {});
		}
		
		
		public static void Subscribe(Action<object,T> listener){
			Subscribe(global,listener);
		}
		
		
		public static void Unsubscribe(Action<object,T> listener){
			Unsubscribe(global,listener);
		}
		
		
		public static void Subscribe(object channel, Action<object,T> listener){
			if(!channels.ContainsKey(channel)) channels.Add(channel,delegate {});
			channels[channel] += listener;
		}
		
		
		public static void Unsubscribe(object channel, Action<object,T> listener){
			if(!channels.ContainsKey(channel)) return;
			channels[channel] -= listener;
			if(((Delegate)channels[channel]).GetInvocationList().Length == 1) channels.Remove(channel);
		}
		
		
		public static void Raise(object channel,object caller, T payload){
			if(!channels.ContainsKey(channel)) return;
			channels[channel](caller,payload);
		}
		
		public static void Raise(object caller, T payload){
			channels[global](caller,payload);
		} 
	}

}
