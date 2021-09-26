using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Domain : MonoBehaviour
{
    public UnityEvent OnPlayerEnter;
    public UnityEvent OnGameEnd;
    public bool drawInGame = false;

    private bool isTransitioningToNextLevel = false;

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

            isTransitioningToNextLevel = true;
        }
    }

    private void Update()
    {
        if (isTransitioningToNextLevel && Input.GetKeyDown(KeyCode.E))
        {
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            buildIndex++;

            if (buildIndex >= SceneManager.sceneCountInBuildSettings)
            {
                // ETO FINAL!!!!
                OnGameEnd.Invoke();
                return;
            }

            SceneManager.LoadScene(buildIndex);
        }
    }
}