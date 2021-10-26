using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Messaging.Typed{

	public static class EventSystem
	{
        public static readonly Dictionary<object, Dictionary<System.Type, object>> events = new Dictionary<object, Dictionary<Type, object>>();
        public static readonly object globalScope = new object();

		public static void Add<T>(this GameObject go, Action<T> l){
			System.Type type = typeof(T);
            if (!events.ContainsKey(globalScope)) events.Add(globalScope, new Dictionary<Type, object>());
            if (!events.ContainsKey(go)) events.Add(go, new Dictionary<Type, object>());

            if (!events[globalScope].ContainsKey(type))
            {
                events[globalScope].Add(type, new Action<T>(delegate (T payload) { }));
			}
            if (!events[go].ContainsKey(type))
            {
                events[go].Add(type, new Action<T>(delegate (T payload) { }));
            }
            events[globalScope][type] = Delegate.Combine((events[globalScope][type] as Action<T>),l);
            events[go][type] = Delegate.Combine((events[globalScope][type] as Action<T>), l);
        }
		
		public static void Remove<T>(this GameObject go, Action<T> l){
			System.Type type = typeof(T);
            if (!events.ContainsKey(globalScope)) return;
            if (!events.ContainsKey(go)) return;
            if (!events[globalScope].ContainsKey(type)) return;
            if (!events[go].ContainsKey(type)) return;
            if (!events.ContainsKey(type) || events[type] == null) return;
            events[globalScope][type] = Delegate.Remove((events[globalScope][type] as Action<T>), l);
            if ((events[globalScope][type] as Action<T>).GetInvocationList().Length == 1) events[globalScope].Remove(type);
            if (events[globalScope].Count == 0) events.Remove(globalScope);
            events[go][type] = Delegate.Remove((events[globalScope][type] as Action<T>), l);
            if ((events[go][type] as Action<T>).GetInvocationList().Length == 1) events[go].Remove(type);
            if(events[go].Count == 0) events.Remove(go);
        }
		
		public static void BroadcastMessage<T>(this GameObject go, T payload){
			System.Type type = typeof(T);
			object o;
            if (!events.ContainsKey(globalScope)) return;
			if(events[globalScope].TryGetValue(type, out o)){
				(o as Action<T>)(payload);
			}
		}

        public static void SendMessage<T>(this GameObject go, T payload)
        {
            System.Type type = typeof(T);
            object o;
            if (!events.ContainsKey(go)) return;
            if (events[go].TryGetValue(type, out o))
            {
                (o as Action<T>)(payload);
            }
        }

    }
}
