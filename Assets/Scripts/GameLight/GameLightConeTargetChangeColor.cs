using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLightConeTargetChangeColor : MonoBehaviour, IGameLightConeTarget
{
  [SerializeField] private Color newColor;
  
  private Color defaultColor;

  private void Start()
  {
    defaultColor = GetComponent<MeshRenderer>().material.color;
  }

  public void OnHighlightStart()
  {
    // Debug.Log("kek OnHighlightStart");
    GetComponent<MeshRenderer>().material.color = newColor;
  }

  public void OnHighlightEnd()
  {
    // Debug.Log("kek OnHighlightEnd");
    GetComponent<MeshRenderer>().material.color = defaultColor;
  }
}
