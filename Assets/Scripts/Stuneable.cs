using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stuneable : MonoBehaviour
{
    [SerializeField] private float stunTime = 2.0f;
    [SerializeField] private bool stunFromEditor;

    [SerializeField] private GameObject stubEffectGo;

    float stunEndTime;

    public bool IsStunned()
    {
        return Time.time < stunEndTime;
    }

    public void Stun()
    {
        if (!IsStunned())
        {
            stunEndTime = Time.time + stunTime;
            GetComponent<ObjectThrower>()?.ThrowObject();
        }
    }

    private void Update()
    {
        if (stunFromEditor)
        {
            stunFromEditor = false;
            Stun();
        }

        stubEffectGo.SetActive(IsStunned());
    }
}

public static class ShitExtensions
{
    public static bool IsStunned(this MonoBehaviour beh)
    {
        var stuneable = beh.GetComponent<Stuneable>();
        return stuneable != null && stuneable.IsStunned();
    }
}