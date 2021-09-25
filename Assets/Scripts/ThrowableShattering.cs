using UnityEngine;

public class ThrowableShattering : MonoBehaviour
{
    //Maybe if some MIT package will arrive
    [SerializeField] private ParticleSystem shatterEffect;

    private void OnCollisionEnter(Collision other)
    {
        var createdEffect = Instantiate(shatterEffect, transform.position, transform.rotation);
        createdEffect.Play();
        Destroy(this.gameObject);
    }
}