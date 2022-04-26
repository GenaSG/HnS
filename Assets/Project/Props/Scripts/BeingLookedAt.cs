using HighlightPlus;
using SimpleEventBus;
using UnityEngine;

public class BeingLookedAt : MonoBehaviour
{
    [SerializeField] private GameObject eventChannel;
	[SerializeField] private HighlightEffect highlightEffect;
	[SerializeField] private bool canBeHighlighted = true;

    void OnValidate()
    {
        eventChannel = gameObject;
        if(highlightEffect == null) highlightEffect = GetComponent<HighlightEffect>();
    }

    void OnEnable()
    {
	    EventBus<OnBeingLookedAt>.Subscribe(LookedAt);
	    EventBus<OnSelectedBy>.Subscribe(SelectedBy);
    }

    void OnDisable()
    {
	    EventBus<OnBeingLookedAt>.UnSubscribe(LookedAt);
	    EventBus<OnSelectedBy>.UnSubscribe(SelectedBy);
    }

	private void SelectedBy(object caller, OnSelectedBy player, object target)
	{
		if(target != (object)eventChannel) return;
		
		bool selected = highlightEffect.enabled && player.user != null;
		bool unselected = !highlightEffect.enabled && player.user == null;
		
		if(selected){
			highlightEffect.enabled = false;
			canBeHighlighted = false;
		}
		else if(unselected)
		{
			highlightEffect.enabled = true;
			canBeHighlighted = true;
		}

	}

    private void LookedAt(object caller, OnBeingLookedAt lookingAt, object target)
	{
		if(target != (object)eventChannel) return;
		bool enabledCondition = lookingAt.observer != null && !highlightEffect.enabled && canBeHighlighted;
		bool disableCondition = lookingAt.observer == null && highlightEffect.enabled && canBeHighlighted;


        if(enabledCondition)
        {
	        highlightEffect.enabled = true;
        }
        else if (disableCondition)
        {
	        highlightEffect.enabled = false;
        }
    }
}
