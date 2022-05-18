using UnityEngine;

public class LerpMover : MonoBehaviour, IMover
{
    [SerializeField] private Vector2 _secondPosition;
    private Vector2 _firstPosition;
    private bool _moved = false;
    private RectTransform _rectTransform;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _firstPosition = _rectTransform.anchoredPosition;
    }

    public void Move()
    {
        _moved = !_moved;
        _rectTransform.anchoredPosition = _moved ? _secondPosition : _firstPosition;
    }
}
