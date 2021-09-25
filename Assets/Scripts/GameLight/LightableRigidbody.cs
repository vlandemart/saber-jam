using UnityEngine;

public class LightableRigidbody : MonoBehaviour, IGameLightConeTarget
{
    [SerializeField] private bool isFrozenOnStart = true;
    [SerializeField] private bool isLightFreezingObject = false; //If not - it will unfreeze object
    
    private Vector3 cachedVelocity;
    private Rigidbody objectRigidbody;

    private void Start()
    {
        objectRigidbody = GetComponent<Rigidbody>();
        if (isFrozenOnStart)
            Freeze();
    }

    public void OnHighlightStart()
    {
        if (isLightFreezingObject)
            Freeze();
        else
            Unfreeze();
    }

    public void OnHighlightEnd()
    {
        if (isLightFreezingObject)
            Unfreeze();
        else
            Freeze();
    }

    private void Freeze()
    {
        cachedVelocity = objectRigidbody.velocity;
        objectRigidbody.velocity = Vector3.zero;
        objectRigidbody.isKinematic = true;
    }

    private void Unfreeze()
    {
        objectRigidbody.isKinematic = false;
        objectRigidbody.velocity = cachedVelocity;
    }
}