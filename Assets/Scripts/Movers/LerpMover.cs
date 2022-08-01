using UnityEngine;

public class LerpMover : MonoBehaviour
{
    [SerializeField] private Vector2 _secondPosition;
    private Vector2 _firstPosition;
    [SerializeField] private bool _moved = false;
    private RectTransform _rectTransform;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        if (_moved)
        {
            _firstPosition = _secondPosition;
            _secondPosition = _rectTransform.anchoredPosition;
        }
        else
            _firstPosition = _rectTransform.anchoredPosition;
    }

    public void Move(bool moved)
    {
        if (_moved != moved)
        {
            _moved = !_moved;
            _rectTransform.anchoredPosition = _moved ? _secondPosition : _firstPosition;
        }
    }
}
