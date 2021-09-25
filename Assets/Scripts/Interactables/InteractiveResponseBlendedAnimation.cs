using System;
using UnityEngine;

public class InteractiveResponseBlendedAnimation : InteractiveResponse
{
    public float animationSpeed = 1f;
    public string blendingParam = "blendParam";
    public Animator _animator;
    
    private float blendParam = .0f;

    private int currIncrement = -1;

    private void Update()
    {
        blendParam = Mathf.Clamp(blendParam + animationSpeed * Time.deltaTime * currIncrement, 0, 1) ;
        _animator.SetFloat(blendingParam, blendParam);
    }

    public override void DoResponseAction()
    {
        currIncrement = 1;
    }

    public override void UndoResponseAction()
    {
        currIncrement = -1;
    }
}