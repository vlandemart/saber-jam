using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveResponseSwitchGo : InteractiveResponse
{
    public GameObject defaultGo;
    public GameObject switchedGo;

    private void Start()
    {
        defaultGo.SetActive(true);
        switchedGo.SetActive(false);
    }

    public override void DoResponseAction()
    {
        defaultGo.SetActive(false);
        switchedGo.SetActive(true);
    }

    public override void UndoResponseAction()
    {
        Debug.Assert(false);
    }
}