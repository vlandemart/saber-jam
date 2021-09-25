using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;

public class AiMovement : MonoBehaviour
{
    public Animator Animator;

    private NavMeshAgent _navMeshAgent;

    public bool isEnemyForced = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // SharedGameObject enemy = (SharedGameObject) GetComponent<BehaviorTree>().GetVariable("Enemy");
        // if (enemy.Value == null)
        // {
        //     return;
        // }
        //
        // Vector3 desiredDirection = enemy.Value.transform.position - transform.position;
        // desiredDirection.Normalize();
        // transform.rotation = Quaternion.LookRotation(desiredDirection);
    }

    void FixedUpdate()
    {
        // Vector3 movementVector = _navMeshAgent.velocity;
        // movementVector.Normalize();
        //
        // Vector3 transformed = transform.InverseTransformVector(movementVector);
        // Vector3 animDir = transformed.normalized;
        //
        // Animator.SetFloat("xAxis", animDir.x);
        // Animator.SetFloat("yAxis", animDir.z);
    }
}
