using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("AI")]
public class HasInteractiveObject : Conditional
{
    private AiMovement _movement;

    public override void OnStart()
    {
        _movement = gameObject.GetComponent<AiMovement>();
    }

    public override TaskStatus OnUpdate()
    {
        if (_movement.HasInteractiveObject())
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}