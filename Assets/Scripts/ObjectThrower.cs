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
    private bool isAiming;

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
        
        ThrowableObject obj = _provider.closestThrowable;
        if (obj != null && _currentThrowable == null)
        {
            TrySetText(obj.throwableText);
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                TrySetObjectAsCurrent(obj);
            }
        }
        else
        {
            if (_currentThrowable != null && isAiming)
            {
                targetPositionMarker.transform.position = InputManager.Instance.GetCursorPosition();                
            }
        }
    }

    private void Start()
    {
        InputManager.Instance.OnRightMouseButtonDown.AddListener(StartAiming);
        InputManager.Instance.OnRightMouseButtonUp.AddListener(StopAiming);
        InputManager.Instance.OnLeftMouseButtonDown.AddListener(ThrowObject);
    }

    private void TrySetObjectAsCurrent(ThrowableObject obj)
    {
        if (obj.taken)
            return;
        
        obj.Take();
        _currentThrowable = obj;
        
        _currentThrowable.transform.parent = throwableObjectAttachTransform;
        _currentThrowable.transform.localPosition = Vector3.zero;
    }

    //Called on LMB event
    private void ThrowObject()
    {
        if (!isAiming)
            return;

        if (_currentThrowable == null)
            return;
        
        var startPos = transform.position;
        var throwPos = InputManager.Instance.GetCursorPosition();
        
        _currentThrowable.transform.LookAt(InputManager.Instance.GetCursorPosition());
        _currentThrowable.GetComponent<Rigidbody>().velocity = (throwPos - startPos).normalized * throwForce;
        
        _currentThrowable.Throw();
        _currentThrowable.gameObject.transform.parent = null;
        _currentThrowable = null;
    }

    //Called on RMB event
    private void StartAiming()
    {
        isAiming = true;
        targetPositionMarker.SetActive(true);
    }

    //Called on RMB event
    private void StopAiming()
    {
        isAiming = false;
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