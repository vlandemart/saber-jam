using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInteraction : MonoBehaviour
{
    public LayerMask interactibleLayer;
    public float maxDistanceToInteractible = 2f;
    
    private IThrowable _currentThrowable;
    private Text uiInteractiveTextBox;

    public Transform throwableObjectAttachTransform;

    private Transform cameraTfr;
    
    private void Awake()
    {
        var textObject = GameObject.FindWithTag("interactableTextBox");
        if (textObject != null)
        {
            uiInteractiveTextBox = textObject.GetComponent<Text>();
        }

        cameraTfr = Camera.main.transform;
    }
    
    public void Interact(InteractiveObject otherInteractive)
    {
        if (_currentThrowable != null)
        {
            ((MonoBehaviour) _currentThrowable).transform.parent = null;
            _currentThrowable.Throw(cameraTfr.forward);
            _currentThrowable = null;
            return;
        }
        
        if (!otherInteractive.IsCanInteract())
        {
            return;
        }

        IThrowable throwable = otherInteractive as IThrowable;

        if (throwable != null)
        {
            throwable.Take();
            otherInteractive.transform.parent = throwableObjectAttachTransform;
            otherInteractive.transform.localPosition = Vector3.zero;
            _currentThrowable = throwable;
            return;
        }
        
        otherInteractive.TryDoInteract();
    }

    // Update is called once per frame
    void Update()
    {
        TrySetText("");

        Collider[] colliders =
            Physics.OverlapSphere(gameObject.transform.position, maxDistanceToInteractible, interactibleLayer);
        if (colliders.Length == 0)
        {
            return;
        }

        float minDist = float.MaxValue;
        InteractiveObject chosenObject = null;
        foreach (Collider coll in colliders)
        {
            InteractiveObject interactive = coll.GetComponent<InteractiveObject>();
            if (interactive == null)
            {
                continue;
            }

            float distance = Vector3.Distance(gameObject.transform.position, coll.transform.position);
            if (distance < minDist)
            {
                chosenObject = interactive;
            }
        }

        if (chosenObject == null)
        {
            return;
        }

        if (chosenObject.IsCanInteract())
        {
            TrySetText("Press 'E' to " + chosenObject.interactionText);
        }
        else
        {
            TrySetText(chosenObject.interactionText + " (unavailable)");
        }

        if (Input.GetKeyDown(KeyCode.E) && chosenObject.canBeActivatedWithMouse)
        {
            Interact(chosenObject);
        }
    }

    void TrySetText(string value)
    {
        if (uiInteractiveTextBox != null)
        {
            uiInteractiveTextBox.text = value;
            uiInteractiveTextBox.enabled = value != "";
        }
    }
}