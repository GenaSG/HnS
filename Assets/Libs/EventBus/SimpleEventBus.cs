namespace SimpleEventBus{
	using System;
	using System.Collections.Generic;

    public static class EventBus<T>
    {
        private static Action<object, T, object> listeners = delegate { };

        public static void Raise(object caller, T payload, object target = null)
        {
            listeners(caller, payload, target);
        }

        public static void Subscribe(Action<object, T, object> listener)
        {
            listeners += listener;
        }

        public static void UnSubscribe(Action<object, T, object> listener)
        {
            listeners -= listener;
        }
    }

	public static class HierarchicalEventBus<T>
	{
		private static readonly Dictionary<object,Action<object,T>> channels;
		private static readonly object global = new object();
		
		static HierarchicalEventBus(){
			channels = new Dictionary<object, Action<object, T>>();
			channels.Add(global,delegate {});
		}
		
		
		public static void Subscribe(Action<object,T> listener){
			Subscribe(global,listener);
		}
		
		
		public static void UnSubscribe(Action<object,T> listener){
			UnSubscribe(global,listener);
		}
		
		
		public static void Subscribe(object channel, Action<object,T> listener){
			if(!channels.ContainsKey(channel)) channels.Add(channel,delegate {});
			channels[channel] += listener;
		}
		
		
		public static void UnSubscribe(object channel, Action<object,T> listener){
			if(!channels.ContainsKey(channel)) return;
			channels[channel] -= listener;
			if(((Delegate)channels[channel]).GetInvocationList().Length == 1) channels.Remove(channel);
		}
		
		
		public static void Raise(object channel,object caller, T payload){
			if(!channels.ContainsKey(channel)) return;
			channels[channel](caller,payload);
		}
		
		public static void Raise(object caller, T payload){
            if (!channels.ContainsKey(global)) return;
            channels[global](caller,payload);
		}

	}

}
