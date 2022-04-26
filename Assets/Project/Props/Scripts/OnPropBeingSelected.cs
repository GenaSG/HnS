using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleEventBus;

public class OnPropBeingSelected : MonoBehaviour
{
	[SerializeField] 
	private CameraFollowProfile camProfile;
	[SerializeField]
	private GameObject eventChannel;
	
	void OnValidate()
	{
		eventChannel = gameObject;
	}
	
	void OnEnable()
	{
		EventBus<OnSelectedBy>.Subscribe(SelectedBy);
	}
	
	void SelectedBy(object caller, OnSelectedBy player, object target)
	{
		if(target != (object)eventChannel) return;
		EventBus<OnCameraProfileUpdated>.Raise(this,new OnCameraProfileUpdated() {profile = camProfile}, player.user);
	}
	
	
	void OnDisable() 
	{
		EventBus<OnSelectedBy>.UnSubscribe(SelectedBy);
	}
	
}
