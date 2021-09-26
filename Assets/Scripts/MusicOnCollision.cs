using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MusicOnCollision : MonoBehaviour
{
    [SerializeField] private AudioSource soundToPlay;
    [SerializeField] private bool isOnlyOnce;
    [SerializeField] private float minVelocity = 3f;

    private bool hasPlayed;

    private void OnCollisionEnter(Collision other)
    {
        if (isOnlyOnce && hasPlayed)
            return;
        
        if (GetComponent<Rigidbody>().velocity.magnitude < minVelocity)
            return;

        PlaySound();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isOnlyOnce && hasPlayed)
            return;
        
        PlaySound();
    }

    private void PlaySound()
    {
        hasPlayed = true;
        soundToPlay.pitch = Random.Range(.9f, 1.1f);
        soundToPlay.Play();
    }
    
}