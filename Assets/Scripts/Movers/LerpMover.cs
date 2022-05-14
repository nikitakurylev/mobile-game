using UnityEngine;

public class LerpMover : MonoBehaviour, IMover
{
    [SerializeField] private Vector3 _secondPosition;
    private Vector3 _firstPosition;
    private bool _moved = false; 
    
    private void Awake()
    {
        _firstPosition = transform.position;
    }

    public void Move()
    {
        _moved = !_moved;
        transform.position = _moved ? _secondPosition : _firstPosition;
    }
}
