using UnityEngine;

[System.Serializable]
public class MouseLookProvider
{
    [SerializeField]
    private MouseLookPrefs prefs;
    public MouseLookPrefs MouseLookPreferences { get { return prefs; } set { if (value != null) prefs = value; } }

    public Vector3 UpdateMouseLook(float vertical, float horizontal, Vector3 currentAngles)
    {
        currentAngles.x += (prefs.InvertVertical ? 1 : -1) * vertical * prefs.VerticalSensitivity;
        currentAngles.y += horizontal * prefs.HorizontalSensitivity;
        if (prefs.ApplyVerticalLimits)
        {
            currentAngles.x = Mathf.Clamp(currentAngles.x + 360f, prefs.VerticalLimits.x + 360f, prefs.VerticalLimits.y + 360f) - 360f;
        }
        if (prefs.ApplyHorizontalLimits)
        {
            currentAngles.y = Mathf.Clamp(currentAngles.y + 360f, prefs.HorizontalLimits.x + 360f, prefs.HorizontalLimits.y + 360f) - 360f;
        }

        return currentAngles;
    }
}