using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("AI")]
public class GetEnemyTargetPos : Action
{
    public SharedGameObject enemyTarget;
    public SharedVector3 targetPosition;

    /// <summary>
    /// Cache the component references.
    /// </summary>
    public override void OnAwake()
    {
    }

    public override void OnStart()
    {
        targetPosition.Value = enemyTarget.Value.transform.position;
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}