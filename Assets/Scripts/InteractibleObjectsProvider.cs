using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleObjectsProvider : MonoBehaviour
{
    public LayerMask interactibleLayer;
    public float maxDistanceToInteractible = 2f;

    [NonSerialized] public InteractiveObject closestInteractive;
    [NonSerialized] public ThrowableObject closestThrowable;

    void Update()
    {
        Collider[] colliders =
            Physics.OverlapSphere(gameObject.transform.position + new Vector3(0, maxDistanceToInteractible / 2, 0),
                maxDistanceToInteractible, interactibleLayer);

        closestInteractive = GetClosestObject<InteractiveObject>(colliders);
        closestThrowable = GetClosestObject<ThrowableObject>(colliders);
    }

    private T GetClosestObject<T>(Collider[] objects)
    {
        float minDist = float.MaxValue;
        T chosenObject = default(T);
        foreach (Collider coll in objects)
        {
            T objectCasted = coll.GetComponent<T>();
            if (objectCasted == null)
            {
                continue;
            }

            if (!IsObjectRaycastable(coll.gameObject))
                continue;

            float distance = Vector3.Distance(gameObject.transform.position, coll.transform.position);
            if (distance < minDist)
            {
                chosenObject = objectCasted;
            }
        }

        return chosenObject;
    }

    private bool IsObjectRaycastable(GameObject objectToCheck)
    {
        Debug.DrawLine(transform.position, objectToCheck.transform.position, Color.blue);

        var origin = transform.position;
        var direction = objectToCheck.transform.position - transform.position;
        Ray ray = new Ray(origin, direction);

        var oldLayer = gameObject.layer;
        gameObject.layer = Physics.IgnoreRaycastLayer;

        if (!Physics.Raycast(ray, out var hit, direction.magnitude))
        {
            return false;
        }

        gameObject.layer = oldLayer;

        return hit.transform.gameObject == objectToCheck;
    }
}