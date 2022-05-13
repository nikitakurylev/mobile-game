using UnityEngine;

public class ActionListenerForwarder : MonoBehaviour, IActionListener
{
    [SerializeField] private MonoBehaviour _listener;
    private IActionListener _actionListener;

    private void OnValidate()
    {
        if (_listener == null)
            throw new UnityException("No Movement Listener assigned");
        GameObject listenerGameObject = _listener.gameObject;
        _listener = listenerGameObject.GetComponent<IActionListener>() as MonoBehaviour;
        if (_listener == null)
            throw new UnityException("No IMovementListener attached to " + listenerGameObject.name);
    }

    private void Awake()
    {
        GameObject listenerGameObject = _listener.gameObject;
        _actionListener = listenerGameObject.GetComponent<IActionListener>();
    }

    public void OnActionFinished()
    {
        _actionListener.OnActionFinished();
    }
}
