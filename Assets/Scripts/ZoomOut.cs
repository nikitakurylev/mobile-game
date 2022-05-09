using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ZoomOut : MonoBehaviour
{
    [SerializeField] float _speed = 0.5f;
    private Camera _camera;
    private float _fov = 0f;
    
    private void OnValidate()
    {
        if (GetComponent<Camera>() == null)
            throw new UnityException("No Camera");
    }

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _fov = _camera.fieldOfView;
    }

    private IEnumerator ChangeFov()
    {
        while (Mathf.Abs(_camera.fieldOfView - _fov) > 0.05f)
        {
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _fov, _speed * Time.deltaTime);
            yield return null;
        }

        _camera.fieldOfView = _fov;
    }

    public void SetFov(float fov)
    {
        _fov = fov;
        StopAllCoroutines();
        StartCoroutine(ChangeFov());
    }
}
