using UnityEngine;

public class PlaySoundOnAnim : MonoBehaviour
{
    [SerializeField] private AudioSource sound;

    public void PlaySound()
    {
        sound.pitch = Random.Range(.9f, 1.1f);
        sound.Play();
    }
}
