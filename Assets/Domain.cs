using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Domain : MonoBehaviour
{
    public UnityEvent OnPlayerEnter;
    public bool drawInGame = false;

    // [SerializeField] private bool loadNextLevel;
    [SerializeField] private float loadNextLevelDelay = 3.0f;

    private void Start()
    {
        if (!drawInGame)
            GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponentInParent<PersonalMovement>())
        {
            OnPlayerEnter.Invoke();

            StartCoroutine(Corouteen());
        }
    }

    IEnumerator Corouteen()
    {
        yield return new WaitForSeconds(loadNextLevelDelay);
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        buildIndex++;

        if (buildIndex >= SceneManager.sceneCountInBuildSettings)
        {
            // ETO FINAL!!!!
            yield break;
        }

        SceneManager.LoadScene(buildIndex);
    }
}