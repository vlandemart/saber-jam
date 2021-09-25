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
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out hit, 999f, interactibleLayer))
        {
            return;
        }

        GameObject obj = hit.collider.gameObject;
        InteractiveObject otherInteractive = obj.GetComponent<InteractiveObject>();
        if (otherInteractive == null)
        {
            return;
        }
        
        if (Vector3.Distance(gameObject.transform.position, hit.collider.transform.position) >
            maxDistanceToInteractible)
        {
            return;
        }

        if (otherInteractive.IsCanInteract())
        {
            TrySetText(otherInteractive.interactionText);
        }
        else
        {
            TrySetText(otherInteractive.interactionText + "(unavailable)");
        }

        if (Input.GetMouseButtonDown(0) && otherInteractive.canBeActivatedWithMouse)
        {
            Interact(otherInteractive);
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