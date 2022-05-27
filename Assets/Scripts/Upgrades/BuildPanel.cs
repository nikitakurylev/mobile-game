using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildPanel : StorageIndicator
{
    private static BuildPanel instance = null;
    public static BuildPanel Instance => instance;
    private Vector3 _hidingPlace;
    [SerializeField] private List<BarIndicator> _barIndicators;
    [SerializeField] private List<TextIndicator> _textIndicators;
    [SerializeField] private Text _upgradeName;
    [SerializeField] private Transform _progressPanel;
    [SerializeField] private Transform _progressBar;
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

        _hidingPlace = transform.position;
    }

    public override void UpdateIndicator(Storage storage)
    {
        _barIndicators[(int) storage.ResourceType].UpdateIndicator(storage);
        _textIndicators[(int) storage.ResourceType].UpdateIndicator(storage);
    }

    public void SetName(string name)
    {
        _upgradeName.text = name;
    }

    public void SetPos(Vector3 position)
    {
        if (Camera.main != null) 
            transform.position = Camera.main.WorldToScreenPoint(position);
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
        _progressBar.transform.localScale = new Vector3(progress, 1, 1);
    }
}