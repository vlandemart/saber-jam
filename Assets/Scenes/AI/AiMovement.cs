using System.Collections.Generic;
using BehaviorDesigner.Runtime;
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
        List<GameObjectWithDist> availableInteractiveObject = GetAvailableInteractiveObject();
        return availableInteractiveObject.Count != 0;
    }

    public Vector3 GetInteractiveObjectPosition()
    {
        List<GameObjectWithDist> availableInteractiveObject = GetAvailableInteractiveObject();
        availableInteractiveObject.Sort((p1, p2) => p1.dist.CompareTo(p2.dist));

        return availableInteractiveObject[0].GameObject.transform.position;
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
        if (this.IsStunned())
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.velocity = Vector3.zero;
            GetComponent<BehaviorTree>().DisableBehavior();
            // _navMeshAgent.enabled = !this.IsStunned();
        }
        else
        {
            _navMeshAgent.isStopped = false;
            GetComponent<BehaviorTree>().EnableBehavior();
        }

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

        if (this.IsStunned())
        {
            Animator.SetFloat("velocity", 0);
        }
    }

    private List<GameObjectWithDist> GetAvailableInteractiveObject()
    {
        List<GameObjectWithDist> list = new List<GameObjectWithDist>();
        if (interactiveObjects == null)
        {
            return list;
        }

        foreach (var interactiveObject in interactiveObjects)
        {
            ThrowableObject throwableObject = interactiveObject.GetComponent<ThrowableObject>();
            if (throwableObject != null && !throwableObject.taken && throwableObject.rb.velocity.magnitude < 0.5)
            {
                NavMeshPath path = new NavMeshPath();

                if (NavMesh.CalculatePath(transform.position, interactiveObject.transform.position, NavMesh.AllAreas,
                    path))
                {
                    float pathLength = GetPathLength(path);

                    GameObjectWithDist gameObjectWithDist = new GameObjectWithDist();
                    gameObjectWithDist.dist = pathLength;
                    gameObjectWithDist.GameObject = interactiveObject;
                    list.Add(gameObjectWithDist);
                }
            }
        }

        return list;
    }

    public static float GetPathLength(NavMeshPath path)
    {
        float lng = 0.0f;

        if (path.status != NavMeshPathStatus.PathInvalid)
        {
            for (int i = 1; i < path.corners.Length; ++i)
            {
                lng += Vector3.Distance(path.corners[i - 1], path.corners[i]);
            }
        }

        return lng;
    }
}

class GameObjectWithDist
{
    public GameObject GameObject;
    public float dist;
}