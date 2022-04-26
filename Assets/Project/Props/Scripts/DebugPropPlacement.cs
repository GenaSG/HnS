using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleEventBus;

public class DebugPropPlacement : MonoBehaviour
{
	[SerializeField]
	private GameObject[] prefabs;
	[SerializeField]
	private Vector2Int gridDimensions = new Vector2Int(){x=100,y=100};
	[SerializeField]
	private int gridStep = 5;
	

	
	[ContextMenu("Place prefabs")]
	void PlacePrefabs()
	{
		CleanUp();
		
		int gridCellsCount = gridDimensions.x * gridDimensions.y /(gridStep * gridStep); 
		if(prefabs.Length > gridCellsCount){
			Debug.LogError($"There are more prefabs to place than available grid cells. Prefabs length = {prefabs.Length}. Available grid cells = {gridCellsCount}");
			return;
		}
		

		
		for(int i = 0; i < prefabs.Length; i++)
		{
			Vector3 position = new Vector3(
				(i*gridStep)%gridDimensions.x - gridDimensions.x/2,
				transform.position.y,
				((i*gridStep/gridDimensions.x)*gridStep)%gridDimensions.y - gridDimensions.y/2);
			
			Instantiate(prefabs[i],position,transform.rotation,transform);
		}
	}
	
	void CleanUp()
	{
		foreach(Transform t in transform)
		{
			DestroyImmediate(t.gameObject);
		}
		
	}
	
	void OnEnable()
	{
		var props = new GameObject[transform.childCount];
		
		for(int i =0; i < props.Length;i++)
		{
			props[i] = transform.GetChild(i).gameObject;
		}
		EventBus<OnMapGenerated>.Raise(this,new OnMapGenerated(){
			props = props,
			levelGeometry = new GameObject[0]
			});
	}
	
	void OnDisable()
	{
		EventBus<OnMapCleared>.Raise(this,new OnMapCleared(){});
	}
}
