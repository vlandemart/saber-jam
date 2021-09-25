using System;
using UnityEngine;

public class ObjectThrower : MonoBehaviour
{
    [SerializeField] private float throwAngle;
    [SerializeField] private GameObject targetPositionMarker;

    private ThrowableObjectGetter throwableObjectGetter;
    private bool isAiming;

    private void Awake()
    {
        throwableObjectGetter = GetComponent<ThrowableObjectGetter>();
    }

    private void Update()
    {
        if (!isAiming)
            return;
        targetPositionMarker.transform.position = InputManager.Instance.GetCursorPosition();
    }

    private void Start()
    {
        InputManager.Instance.OnRightMouseButtonDown.AddListener(StartAiming);
        InputManager.Instance.OnRightMouseButtonUp.AddListener(StopAiming);
        InputManager.Instance.OnLeftMouseButtonDown.AddListener(ThrowObject);
    }

    //Called on LMB event
    private void ThrowObject()
    {
        if (!isAiming)
            return;

        var objectToThrow = throwableObjectGetter.GetThrowableObject();
        if (objectToThrow == null)
            return;
        
        var startPos = transform.position;
        var throwPos = InputManager.Instance.GetCursorPosition();
        var calculatedVelocity = ExtraGameMath.CalculateParabolicTrajectory(startPos, throwPos, throwAngle);
        
        objectToThrow.transform.LookAt(InputManager.Instance.GetCursorPosition());
        objectToThrow.velocity = objectToThrow.transform.TransformDirection(calculatedVelocity);
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
}