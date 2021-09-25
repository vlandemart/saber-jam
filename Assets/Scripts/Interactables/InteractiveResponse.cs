using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveResponse : MonoBehaviour
{
    public virtual void DoResponseAction()
    {
        
    }

    public virtual void UndoResponseAction()
    {
        
    }

    public virtual bool IsAvailable()
    {
        return false;
    }
}
