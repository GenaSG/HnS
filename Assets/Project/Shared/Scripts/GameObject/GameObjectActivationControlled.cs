using UnityEngine;

public class GameObjectActivationControlled : MonoBehaviour
{
    [SerializeField]
    private GameObject enableGameObject;
    [SerializeField]
    private GameObject disableGameObject;

    private void OnEnable()
    {
        if (enableGameObject) enableGameObject.SetActive(true);
        if (disableGameObject) disableGameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (enableGameObject) enableGameObject.SetActive(false);
        if (disableGameObject) disableGameObject.SetActive(true);
    }
}
