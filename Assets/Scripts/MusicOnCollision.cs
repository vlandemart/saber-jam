using UnityEngine;

public class MusicOnCollision : MonoBehaviour
{
    [SerializeField] private AudioSource soundToPlay;

    private bool hasPlayed;

    private void OnCollisionEnter(Collision other)
    {
        if (hasPlayed)
            return;

        hasPlayed = true;
        soundToPlay.Play();
    }
    
}