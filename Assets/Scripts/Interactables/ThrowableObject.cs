using System;
using UnityEngine;

namespace Interactables
{
    public class ThrowableObject : InteractiveObject, IThrowable
    {
        [NonSerialized] public Rigidbody rb;

        public bool taken;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void Take()
        {
            rb.isKinematic = true;
            taken = true;
        }

        public void Throw(Vector3 direction)
        {
            rb.isKinematic = false;
            rb.velocity = direction * 20;
            taken = false;
        }

        public override bool IsCanInteract()
        {
            return !taken;
        }
    }
}