﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ThrowableObject : MonoBehaviour
{
    public string throwableText = "trhow me";
    [NonSerialized] public Rigidbody rb;

    public bool taken;
    public bool inSocket;
    private Collider coll;


    [SerializeField] private float stunSpeedThreshold = 0.3f;

    private void Awake()
    {
        coll = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    public void Take(Collider carrier)
    {
        rb.isKinematic = true;
        taken = true;

        gameObject.transform.rotation = Quaternion.identity;
        Physics.IgnoreCollision(carrier, coll, true);
    }

    public void Throw(Collider carrier)
    {
        rb.isKinematic = false;
        taken = false;

        StartCoroutine(EnableCollision(carrier));
    }

    private IEnumerator EnableCollision(Collider carrier)
    {
        yield return new WaitForSeconds(0.5f);
        Physics.IgnoreCollision(carrier, coll, false);
    }

    private void OnCollisionEnter(Collision other)
    {
        var stuneable = other.gameObject.GetComponent<Stuneable>();
        if (stuneable == null)
        {
            return;
        }

        if (rb.velocity.magnitude > stunSpeedThreshold)
        {
            stuneable.Stun();
        }
        else
        {
            Debug.Log("too little speed for stun" + rb.velocity.magnitude);
        }
    }
}