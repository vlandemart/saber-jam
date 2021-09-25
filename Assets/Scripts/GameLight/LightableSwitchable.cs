using UnityEngine;

public class LightableSwitchable : MonoBehaviour, IGameLightConeTarget
{
    [SerializeField] private GameObject objectToSwitchTo;
    [SerializeField] private bool changePosition = true;
    [SerializeField] private bool changeRotation = true;

    public void OnHighlightStart()
    {
        gameObject.SetActive(false);
        objectToSwitchTo.gameObject.SetActive(true);

        if (changePosition)
            objectToSwitchTo.transform.position = transform.position;
        if (changeRotation)
            objectToSwitchTo.transform.rotation = transform.rotation;
    }

    public void OnHighlightEnd()
    {
        gameObject.SetActive(true);
        objectToSwitchTo.gameObject.SetActive(false);

        if (changePosition)
            transform.position = objectToSwitchTo.transform.position;
        if (changeRotation)
            transform.rotation = objectToSwitchTo.transform.rotation;
    }
}
