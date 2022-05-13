using UnityEngine;

[RequireComponent(typeof(HumanMovement))]
public class AnimationVelocity : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private HumanMovement _humanMovement;
    private static readonly int Velocity = Animator.StringToHash("velocity");

    private void OnValidate()
    {
        if (GetComponent<HumanMovement>() == null)
            throw new UnityException("No Human Movement");
    }

    private void Awake()
    {
        if(_animator == null)
            throw new UnityException("No Animator");
        _humanMovement = GetComponent<HumanMovement>();
    }

    void FixedUpdate()
    {
        _animator.SetFloat(Velocity, _humanMovement.GetSpeed());
    }
}
