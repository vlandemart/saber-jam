using System.Collections;
using System.Collections.Generic;
using Sigtrap.Relays;
using UnityEngine;

public class TestFileForEvents : MonoBehaviour
{
    private Relay OnEventTest = new Relay();

    void Start()
    {
        OnEventTest.AddOnce(DoOnce);
    }

    private void DoOnce()
    {
        Debug.Log("Do shit");
    }
    
    void Update()
    {
        OnEventTest?.Dispatch();
    }
}
