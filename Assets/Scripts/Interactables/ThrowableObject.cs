using System;
using UnityEngine;
using UnityEngine.UI;

public class ThrowableObject : MonoBehaviour
{
    public string throwableText = "trhow me";
    [NonSerialized] public Rigidbody rb;

    public bool taken;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Take()
    {
        rb.isKinematic = true;
        taken = true;

        gameObject.transform.rotation = Quaternion.identity;
    }

    public void Throw()
    {
        rb.isKinematic = false;
        taken = false;
    }
}