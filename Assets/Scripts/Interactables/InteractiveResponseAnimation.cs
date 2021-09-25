using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveResponseAnimation : InteractiveResponse
{
    public string doAnimationName;
    public string undoAnimationName;
    
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public override void DoResponseAction()
    {
        _animator.Play(doAnimationName);
        Debug.Log("Played animation");
    }

    public override void UndoResponseAction()
    {
        _animator.Play(undoAnimationName);
    }

    public override bool IsAvailable()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).IsName("layer.IDLE");
    }
}
