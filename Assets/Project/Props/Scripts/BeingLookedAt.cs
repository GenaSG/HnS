using HighlightPlus;
using SimpleEventBus;
using UnityEngine;

public class BeingLookedAt : MonoBehaviour
{
    [SerializeField] private GameObject eventChannel;
    [SerializeField] private HighlightEffect highlightEffect;

    void OnValidate()
    {
        eventChannel = gameObject;
        if(highlightEffect == null) highlightEffect = GetComponent<HighlightEffect>();
    }

    void OnEnable()
    {
        EventBus<OnBeingLookedAt>.Subscribe(LookedAt);
    }

    void OnDisable()
    {
        EventBus<OnBeingLookedAt>.UnSubscribe(LookedAt);
    }

    private void LookedAt(object caller, OnBeingLookedAt lookingAt, object target)
    {
        bool enabledCondition = target == (object)eventChannel && lookingAt.observer != null && !highlightEffect.enabled;
        bool disableCondition = target == (object)eventChannel && lookingAt.observer == null && highlightEffect.enabled;

        if(enabledCondition)
        {
            highlightEffect.enabled = true;
            Debug.Log($"{gameObject} being looked at by {lookingAt.observer}. Enable highlight");
        }
        else if (disableCondition)
        {
            highlightEffect.enabled = false;
            Debug.Log($"{gameObject} being looked at by {lookingAt.observer}. Disabling highlight");
        }
    }
}
