using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildPanel : StorageIndicator
{
    private static BuildPanel instance = null;
    public static BuildPanel Instance => instance;
    private Vector3 _hidingPlace;
    private Vector3 _worldPos;
    private Vector3 _offset;
    private Camera _mainCamera;
    [SerializeField] private List<BarIndicator> _barIndicators;
    [SerializeField] private List<TextIndicator> _textIndicators;
    [SerializeField] private Text _upgradeName;
    [SerializeField] private Transform _progressPanel;
    [SerializeField] private Image _progressBar;
    [SerializeField] private Button _debugSkip;
#if DEVELOPMENT_BUILD || UNITY_EDITOR
    private HashSet<Storage> _storages;
#endif
    private RectTransform _rectTransform;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

        _mainCamera = Camera.main;
        _rectTransform = GetComponent<RectTransform>();
        _hidingPlace = transform.position;
        
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        _storages = new HashSet<Storage>();
        _debugSkip.gameObject.SetActive(true);
        _debugSkip.onClick.AddListener(Skip);
#endif
    }

#if DEVELOPMENT_BUILD || UNITY_EDITOR
    private void Skip()
    {
        foreach (Storage storage in _storages)
        {
            storage.ItemCount = storage.StorageCapacity;
        }

        foreach (HumanController humanController in FindObjectsOfType<HumanController>())
        {
            humanController.InventoryCount = 0;
            humanController.InventoryResource = ResourceEnum.None;
        }
        HumanPlanner.CancelAll();
    }
#endif

    private void UpdatePos()
    {
        if (_mainCamera != null)
        {
            transform.position = _mainCamera.WorldToScreenPoint(_worldPos) + _offset;
            var rect = _rectTransform.parent.GetComponent<RectTransform>().rect;
            _rectTransform.anchoredPosition = new Vector2(
                Mathf.Clamp(_rectTransform.anchoredPosition.x, rect.min.x + _rectTransform.rect.min.x, rect.max.x - _rectTransform.rect.max.x),
                Mathf.Clamp(_rectTransform.anchoredPosition.y, rect.min.y + _rectTransform.rect.min.y, rect.max.y - _rectTransform.rect.max.y));
        }
    }

    private void Update()
    {
        if (_mainCamera.transform.hasChanged)
        {
            UpdatePos();
            _mainCamera.transform.hasChanged = false;
        }
    }

    public override void UpdateIndicator(Storage storage)
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        _storages.Add(storage);
#endif
        _barIndicators[(int) storage.ResourceType].UpdateIndicator(storage);
        _textIndicators[(int) storage.ResourceType].UpdateIndicator(storage);
    }

    public void SetName(string name)
    {
        _upgradeName.text = name;
    }

    public void SetPos(Vector3 position, Vector3 offset)
    {
        _offset = offset;
        _worldPos = position;
        UpdatePos();
    }

    public void ShowProgressBar()
    {
        _progressPanel.position = transform.position;
        transform.position = _hidingPlace;
    }

    public void HideProgressBar()
    {
        transform.position = _hidingPlace;
        _progressPanel.position = _hidingPlace;
    }

    public void SetProgress(float progress)
    {
        _progressBar.fillAmount = progress;
    }
}