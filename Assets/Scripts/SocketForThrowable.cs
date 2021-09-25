using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketForThrowable : MonoBehaviour
{
    [SerializeField] private float snapTime = 0.15f;

    [SerializeField] private List<InteractiveResponse> objectsToInteract = new List<InteractiveResponse>();

    Transform throwableTransform;
    Vector3 startingPos;
    Quaternion startingRotation;
    float timeCur;

    private bool done = false;

    private void OnTriggerEnter(Collider other)
    {
        var throwable = other.GetComponent<ThrowableObject>();
        if (throwable == null)
        {
            return;
        }

        if (throwable.taken)
        {
            return;
        }

        throwable.rb.isKinematic = true;

        throwable.rb.velocity = Vector3.zero;
        throwable.rb.angularVelocity = Vector3.zero;

        throwableTransform = throwable.transform;
        startingPos = throwableTransform.position;
        startingRotation = throwableTransform.rotation;
        timeCur = 0.0f;
    }

    void Update()
    {
        if (throwableTransform == null)
        {
            return;
        }

        if (done)
        {
            return;
        }

        if (timeCur > snapTime)
        {
            done = true;
            timeCur = snapTime;

            foreach (var objectToInteract in objectsToInteract)
            {
                objectToInteract.DoResponseAction();
            }

            Debug.Log("Do interaction with " + gameObject.name);
        }

        timeCur += Time.deltaTime;

        float t = timeCur / snapTime;

        var quat = Quaternion.Lerp(startingRotation, this.transform.rotation, t);
        var pos = Vector3.Lerp(startingPos, this.transform.position, t);

        throwableTransform.position = pos;
        throwableTransform.rotation = quat;
    }
}