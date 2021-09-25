using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameLightMover : MonoBehaviour
{
  private Vector3 initPos;

  void Start()
  {
      initPos = transform.position;
  }
  
  void Update()
  {
    transform.position = new Vector3(initPos.x, initPos.y, initPos.z + 2 * (Mathf.Sin(Time.time * 0.3f) + 1));
  }
}
