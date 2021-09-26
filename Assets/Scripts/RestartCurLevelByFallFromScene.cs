using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartCurLevelByFallFromScene : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < -100)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}