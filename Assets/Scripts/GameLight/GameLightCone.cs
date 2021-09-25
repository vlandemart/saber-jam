using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameLightConeTarget
{
  void OnHighlightStart();
  void OnHighlightEnd();
}

public class GameLightCone : MonoBehaviour
{
  private void OnTriggerEnter(Collider other)
  {
    // Debug.Log("kek trigger enter");
    other.GetComponent<IGameLightConeTarget>()?.OnHighlightStart();
  }

  private void OnTriggerExit(Collider other)
  {
    // Debug.Log("kek trigger exit");
    other.GetComponent<IGameLightConeTarget>()?.OnHighlightEnd();
  }
}
