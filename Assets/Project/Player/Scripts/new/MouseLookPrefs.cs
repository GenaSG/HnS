using UnityEngine;

[CreateAssetMenu(fileName = "MouseLookPrefs", menuName = "Prefs/MouseLook")]
public class MouseLookPrefs : ScriptableObject
{
	public float VerticalSensitivity = 3f;
	public float HorizontalSensitivity = 3f;
	public bool InvertVertical = false;
	public bool ApplyVerticalLimits = true;
	public Vector2 VerticalLimits = new Vector2(-90f, 90f);
	public bool ApplyHorizontalLimits = false;
	public Vector2 HorizontalLimits = new Vector2(-360f, 360f);
}