using UnityEngine;
using System.Collections;

public class MusicPeriodic : MonoBehaviour
{
    [SerializeField] private AudioSource sound;
    [SerializeField] private float playPeriod;

    private void Start()
    {
        StartCoroutine(PlaySound());
    }

    private IEnumerator PlaySound()
    {
        while (true)
        {
            sound.pitch = Random.Range(.9f, 1.1f);
            sound.Play();
            yield return new WaitForSeconds(playPeriod);
        }
    }
}