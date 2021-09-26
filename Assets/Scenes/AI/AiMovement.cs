using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiMovement : MonoBehaviour
{
    public Animator Animator;

    private NavMeshAgent _navMeshAgent;

    private GameObject[] interactiveObjects;

    public string interactiveObjectTag;

    public bool HasInteractiveObject()
    {
        List<GameObject> availableInteractiveObject = GetAvailableInteractiveObject();
        return availableInteractiveObject.Count != 0;
    }

    public Vector3 GetInteractiveObjectPosition()
    {
        List<GameObject> availableInteractiveObject = GetAvailableInteractiveObject();
        return availableInteractiveObject[0].transform.position;
    }

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        //_navMeshAgent.updateRotation = false;

        interactiveObjects = GameObject.FindGameObjectsWithTag(interactiveObjectTag);
        //NavMesh.SamplePosition
    }

    public float range = 10f;

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }

        result = Vector3.zero;
        return false;
    }

    void Update()
    {
        Vector3 point;
        if (RandomPoint(transform.position, range, out point))
        {
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
        }
    }

    void FixedUpdate()
    {
        Vector3 movementVector = _navMeshAgent.velocity;
        // movementVector.Normalize();
        //
        // Vector3 transformed = transform.InverseTransformVector(movementVector);
        // Vector3 animDir = transformed.normalized;
        //
        // Animator.SetFloat("xAxis", animDir.x);
        // Animator.SetFloat("yAxis", animDir.z);
        Animator.SetFloat("velocity", movementVector.magnitude);
    }

    private List<GameObject> GetAvailableInteractiveObject()
    {
        List<GameObject> list = new List<GameObject>();
        if (interactiveObjects == null)
        {
            return list;
        }

        foreach (var interactiveObject in interactiveObjects)
        {
            ThrowableObject throwableObject = interactiveObject.GetComponent<ThrowableObject>();
            if (throwableObject != null && !throwableObject.taken)
            {
                list.Add(interactiveObject);
            }
        }

        return list;
    }
}