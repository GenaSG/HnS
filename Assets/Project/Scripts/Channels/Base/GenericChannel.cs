using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public abstract class GenericChannel<ChannelType,T> : MonoBehaviour
{
	[SerializeField]
	private bool localContext;
	[SerializeField]
	private bool invokeLocalEvents;
	[SerializeField]
	private UnityEvent<object,T> OnLocalEvent;
	
	private static object globalContextObject = new object();
	
	private object Context {
		get {
			return localContext ?  (object) transform.root : globalContextObject;
		}
	}
	
	
	private static Dictionary<object, Action<object,T>> contextEvents = new Dictionary<object, Action<object,T>>();
	
	public event Action<object,T> OnEvent 
	{
		add {
			
			if(!contextEvents.ContainsKey(Context)) contextEvents.Add(Context, delegate {});
			contextEvents[Context] += value;
		}
		remove {
			contextEvents[Context] -= value;		
		}
	}
	
	// This function is called when the object becomes enabled and active.
	protected void OnEnable()
	{
		if(invokeLocalEvents) OnEvent+= OnLocalEvent.Invoke;
	}
	
	// This function is called when the behaviour becomes disabled () or inactive.
	protected void OnDisable()
	{
		if(invokeLocalEvents) OnEvent-= OnLocalEvent.Invoke;
	}
	
	
	public void Invoke(object caller, object target, T payload){
		if(!contextEvents.ContainsKey(target)) return;
		contextEvents[target](caller,payload);
	}
	
	public void Invoke(object caller, T payload){
		if(!contextEvents.ContainsKey(Context)) return;
		contextEvents[Context](caller,payload);
	}
	
	public void Invoke(T payload){
		if(!contextEvents.ContainsKey(Context)) return;
		contextEvents[Context]((object)this,payload);
	}
	

	
}
