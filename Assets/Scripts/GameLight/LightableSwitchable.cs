using System;
using UnityEngine;
using UnityEngine.Serialization;

public class LightableSwitchable : MonoBehaviour, IGameLightConeTarget
{
    [SerializeField] private GameObject defaultGameObject;

    [SerializeField] [FormerlySerializedAs("objectToSwitchTo")]
    private GameObject lightedGameObject;

    [SerializeField] private bool changePosition = true;
    [SerializeField] private bool changeRotation = true;

    private void Start()
    {
        defaultGameObject.SetActive(true);
        lightedGameObject.SetActive(false);
    }

    public void OnHighlightStart()
    {
        defaultGameObject.SetActive(false);
        lightedGameObject.SetActive(true);

        if (changePosition)
            lightedGameObject.transform.position = transform.position;
        if (changeRotation)
            lightedGameObject.transform.rotation = transform.rotation;
    }

    public void OnHighlightEnd()
    {
        defaultGameObject.SetActive(true);
        lightedGameObject.SetActive(false);

        if (changePosition)
            transform.position = lightedGameObject.transform.position;
        if (changeRotation)
            transform.rotation = lightedGameObject.transform.rotation;
    }
}