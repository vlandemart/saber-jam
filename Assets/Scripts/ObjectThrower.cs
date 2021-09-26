using System;
using UnityEngine;
using UnityEngine.UI;

public class ObjectThrower : MonoBehaviour
{
    [SerializeField] private float throwForce = 10;
    [SerializeField] private GameObject targetPositionMarker;
    [SerializeField] private Transform throwableObjectAttachTransform;
    private Text uiThrowableTextBox;

    private InteractibleObjectsProvider _provider;

    private ThrowableObject _currentThrowable;

    public bool IsHoldingObject()
    {
        return _currentThrowable != null;
    }

    private void Awake()
    {
        _provider = gameObject.GetComponent<InteractibleObjectsProvider>();

        var textObject = GameObject.FindWithTag("throwableTextBox");
        if (textObject != null)
        {
            uiThrowableTextBox = textObject.GetComponent<Text>();
        }
    }

    private void Update()
    {
        TrySetText("");

        if (this.IsStunned())
        {
            return;
        }

        ThrowableObject obj = _provider.closestThrowable;
        if (obj != null && _currentThrowable == null && !obj.inSocket)
        {
            TrySetText("Press E to pickup " + obj.throwableText);

            if (Input.GetKeyDown(KeyCode.E))
            {
                TrySetObjectAsCurrent(obj);
            }
        }
        else
        {
            if (_currentThrowable != null)
            {
                TrySetText("Press E to throw");
                DrawAim();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    ThrowObject();
                }
            }
        }
    }

    private void DrawAim()
    {
        Vector3 markerPos = InputManager.Instance.GetCursorPosition();
        Vector3 dir = markerPos - gameObject.transform.position;
        float dist = dir.magnitude;

        if (dist > _provider.maxDistanceToInteractible)
        {
            dir = dir.normalized * _provider.maxDistanceToInteractible;
            markerPos = gameObject.transform.position + dir;
        }

        targetPositionMarker.transform.position = markerPos;
    }

    private void TrySetObjectAsCurrent(ThrowableObject obj)
    {
        if (obj.taken || obj.inSocket)
            return;

        obj.Take(gameObject.GetComponent<Collider>());
        _currentThrowable = obj;

        _currentThrowable.transform.parent = throwableObjectAttachTransform;
        _currentThrowable.transform.position = throwableObjectAttachTransform.position;
        _currentThrowable.transform.rotation = throwableObjectAttachTransform.rotation;

        targetPositionMarker.SetActive(true);
    }

    //Called on LMB event
    public void ThrowObject()
    {
        if (_currentThrowable == null)
            return;

        var startPos = transform.position;
        var throwPos = InputManager.Instance.GetCursorPosition();

        _currentThrowable.transform.LookAt(InputManager.Instance.GetCursorPosition());
        _currentThrowable.GetComponent<Rigidbody>().velocity = (throwPos - startPos).normalized * throwForce;

        _currentThrowable.Throw(gameObject.GetComponent<Collider>());
        _currentThrowable.gameObject.transform.parent = null;
        _currentThrowable = null;

        targetPositionMarker.SetActive(false);
    }

    void TrySetText(string value)
    {
        if (uiThrowableTextBox != null)
        {
            uiThrowableTextBox.text = value;
            uiThrowableTextBox.enabled = value != "";
        }
    }
}