using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavigationMovement : HumanMovement
{
    private NavMeshAgent _agent;
    private bool _isMoving;
    private Transform _target;

    private void OnValidate()
    {
        if (GetComponent<NavMeshAgent>() == null)
            throw new UnityException("No NavMeshAgent");
    }

    protected new void Awake()
    {
        base.Awake();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            if (_target.hasChanged)
            {
                _target.hasChanged = false;
                _agent.SetDestination(_target.position);
            }
            
            // Check if we've reached the destination
            else if (!_agent.pathPending)
            {
                if (_agent.remainingDistance <= _agent.stoppingDistance)
                {
                    if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                    {
                        transform.LookAt(_target);
                        InvokeListeners();
                        _isMoving = false;
                    }
                }
            }
        }
    }

    public override void MoveTo(Transform targetTransform)
    {
        _agent.speed = 3 + SaveManager.GetData("speed");
        _agent.SetDestination(targetTransform.position);
        _target = targetTransform;
        _target.hasChanged = false;
        _isMoving = true;
    }

    public override float GetSpeed()
    {
        float sqrMagnitude = _agent.velocity.sqrMagnitude;
        if (sqrMagnitude > 0.5f && _agent.remainingDistance > _agent.radius)
            return sqrMagnitude / (_agent.speed * _agent.speed);
        return 0;
    }
}
