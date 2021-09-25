using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Domain : MonoBehaviour
{
    public UnityEvent OnPlayerEnter;
    public bool drawInGame = false;

    private void Start()
    {
        if(!drawInGame)
            GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponentInParent<PersonalMovement>())
        {        
            OnPlayerEnter.Invoke();
        }
    }
}
