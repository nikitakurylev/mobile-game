using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorBooleanSetter : MonoBehaviour
{
    private Animator _animator;
    private void OnValidate()
    {
        if (GetComponent<Animator>() == null)
            throw new UnityException("No Animator attached");
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ToggleBoolean(string boolName)
    {
        _animator.SetBool(boolName, !_animator.GetBool(boolName));
    }
}
