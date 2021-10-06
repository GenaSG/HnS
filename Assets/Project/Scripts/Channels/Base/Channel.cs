using System.Collections.Generic;
using System;

[Serializable]
public abstract class Channel<ChannelType>
{
    private static readonly Dictionary<object, Action<object>> contextEvents = new Dictionary<object, Action<object>>();

    private static object globalContextObject = new object();
    private object context;


    public Channel(object context)
    {
        this.context = context;
    }

    public Channel()
    {
        this.context = globalContextObject;
    }

    public bool Init(object context)
    {
        if (this.context == globalContextObject)
        {
            this.context = context;
            return true;
        }
        return false;
    }

    public void AddListener(Action<object> action)
    {

        if (!contextEvents.ContainsKey(context)) contextEvents.Add(context, delegate { });
        contextEvents[context] = (Action<object>)Delegate.Combine(contextEvents[context], action);
    }

    public void RemoveListener(Action<object> action)
    {
        if (!contextEvents.ContainsKey(context)) return;
        contextEvents[context] = (System.Action<object>)Delegate.Remove(contextEvents[context], action);
        if (contextEvents[context].GetInvocationList().Length <= 1) contextEvents.Remove(context);
    }

    public void Invoke(object caller, object target)
    {
        if (!contextEvents.ContainsKey(target)) return;
        contextEvents[target](caller);
    }

    public void Invoke(object caller)
    {
        if (!contextEvents.ContainsKey(context)) return;
        contextEvents[context](caller);
    }

    public void Invoke()
    {
        if (!contextEvents.ContainsKey(context)) return;
        contextEvents[context]((object)this);
    }

}

[Serializable]
public abstract class Channel<ChannelType, T> 
{
    private static readonly Dictionary<object, Action<object, T>> contextEvents = new Dictionary<object, Action<object, T>>();

    private static object globalContextObject = new object();
    private object context;


    public Channel(object context)
    {
        this.context = context;
    }

    public Channel()
    {
        this.context = globalContextObject;
    }

    public bool Init(object context)
    {
        if (this.context == globalContextObject)
        {
            this.context = context;
            return true;
        }
        return false;
    }

    public void AddListener(Action<object, T> action)
    {
        if (!contextEvents.ContainsKey(context)) contextEvents.Add(context, delegate { });
        contextEvents[context] = (Action<object, T>)Delegate.Combine(contextEvents[context], action);
    }

    public void RemoveListener(Action<object, T> action)
    {
        if (!contextEvents.ContainsKey(context)) return;
        contextEvents[context] = (Action<object, T>)Delegate.Remove(contextEvents[context], action);
        if (contextEvents[context].GetInvocationList().Length <= 1) contextEvents.Remove(context);
    }

    public void Invoke(object caller, object target, T payload)
    {
        if (!contextEvents.ContainsKey(target)) return;
        contextEvents[target](caller, payload);
    }

    public void Invoke(object caller, T payload)
    {
        if (!contextEvents.ContainsKey(context)) return;
        contextEvents[context](caller, payload);
    }

    public void Invoke(T payload)
    {
        if (!contextEvents.ContainsKey(context)) return;
        contextEvents[context]((object)this, payload);
    }

}

[Serializable]
public abstract class Channel<ChannelType, T, U>
{
    private static readonly Dictionary<object, Action<object, T, U>> contextEvents = new Dictionary<object, Action<object, T, U>>();

    private static object globalContextObject = new object();
    private object context;


    public Channel(object context)
    {
        this.context = context;
    }

    public Channel()
    {
        this.context = globalContextObject;
    }

    public bool Init(object context)
    {
        if (this.context == globalContextObject)
        {
            this.context = context;
            return true;
        }
        return false;
    }

    public void AddListener(Action<object, T, U> action)
    {

        if (!contextEvents.ContainsKey(context)) contextEvents.Add(context, delegate { });
        contextEvents[context] = (Action<object, T, U>)Delegate.Combine(contextEvents[context], action);
    }

    public void RemoveListener(Action<object, T, U> action)
    {
        if (!contextEvents.ContainsKey(context)) return;
        contextEvents[context] = (Action<object, T, U>)Delegate.Remove(contextEvents[context], action);
        if (contextEvents[context].GetInvocationList().Length <= 1) contextEvents.Remove(context);
    }

    public void Invoke(object caller, object target, T arg0, U arg1)
    {
        if (!contextEvents.ContainsKey(target)) return;
        contextEvents[target](caller, arg0, arg1);
    }

    public void Invoke(object caller, T arg0, U arg1)
    {
        if (!contextEvents.ContainsKey(context)) return;
        contextEvents[context](caller, arg0, arg1);
    }

    public void Invoke(T arg0, U arg1)
    {
        if (!contextEvents.ContainsKey(context)) return;
        contextEvents[context]((object)this, arg0, arg1);
    }

}

[Serializable]
public abstract class Channel<ChannelType, T, U, V>
{
    private static readonly Dictionary<object, Action<object, T, U, V>> contextEvents = new Dictionary<object, Action<object, T, U, V>>();

    private static readonly object globalContextObject = new object();
    private object context;


    public Channel(object context)
    {
        this.context = context;
    }

    public Channel()
    {
        this.context = globalContextObject;
    }

    public bool Init(object context)
    {
        if (this.context == globalContextObject)
        {
            this.context = context;
            return true;
        }
        return false;
    }

    public void AddListener(Action<object, T, U, V> action)
    {

        if (!contextEvents.ContainsKey(context)) contextEvents.Add(context, delegate { });
        contextEvents[context] = (Action<object, T, U, V>)Delegate.Combine(contextEvents[context], action);
    }

    public void RemoveListener(Action<object, T, U, V> action)
    {
        if (!contextEvents.ContainsKey(context)) return;
        contextEvents[context] = (Action<object, T, U, V>)Delegate.Remove(contextEvents[context], action);
        if (contextEvents[context].GetInvocationList().Length <= 1) contextEvents.Remove(context);
    }

    public void Invoke(object caller, object target, T arg0, U arg1, V arg2)
    {
        if (!contextEvents.ContainsKey(target)) return;
        contextEvents[target](caller, arg0, arg1, arg2);
    }

    public void Invoke(object caller, T arg0, U arg1, V arg2)
    {
        if (!contextEvents.ContainsKey(context)) return;
        contextEvents[context](caller, arg0, arg1, arg2);
    }

    public void Invoke(T arg0, U arg1, V arg2)
    {
        if (!contextEvents.ContainsKey(context)) return;
        contextEvents[context]((object)this, arg0, arg1, arg2);
    }

}

[Serializable]
public abstract class Channel<ChannelType, T, U, V, W>
{
    private static readonly Dictionary<object, Action<object, T, U, V, W>> contextEvents = new Dictionary<object, Action<object, T, U, V, W>>();

    private static readonly object globalContextObject = new object();
    private object context;


    public Channel(object context)
    {
        this.context = context;
    }

    public Channel()
    {
        this.context = globalContextObject;
    }

    public bool Init(object context)
    {
        if (this.context == globalContextObject)
        {
            this.context = context;
            return true;
        }
        return false;
    }

    public void AddListener(Action<object, T, U, V, W> action)
    {

        if (!contextEvents.ContainsKey(context)) contextEvents.Add(context, delegate { });
        contextEvents[context] = (Action<object, T, U, V, W>)Delegate.Combine(contextEvents[context], action);
    }

    public void RemoveListener(Action<object, T, U, V, W> action)
    {
        if (!contextEvents.ContainsKey(context)) return;
        contextEvents[context] = (Action<object, T, U, V, W>)Delegate.Remove(contextEvents[context], action);
        if (contextEvents[context].GetInvocationList().Length <= 1) contextEvents.Remove(context);
    }

    public void Invoke(object caller, object target, T arg0, U arg1, V arg2, W arg3)
    {
        if (!contextEvents.ContainsKey(target)) return;
        contextEvents[target](caller, arg0, arg1, arg2, arg3);
    }

    public void Invoke(object caller, T arg0, U arg1, V arg2, W arg3)
    {
        if (!contextEvents.ContainsKey(context)) return;
        contextEvents[context](caller, arg0, arg1, arg2, arg3);
    }

    public void Invoke(T arg0, U arg1, V arg2, W arg3)
    {
        if (!contextEvents.ContainsKey(context)) return;
        contextEvents[context]((object)this, arg0, arg1, arg2, arg3);
    }

}