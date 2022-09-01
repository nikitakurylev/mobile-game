using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class ResourceTarget : HumanTarget
{
    [SerializeField] private Storage _storage;
    [SerializeField] private GameObject _dropPrefab;
    [SerializeField] private float _dropDistance = 1f;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private UnityEvent _onChoppedDownForever;
    private int _occupied = 0;
    private static readonly int Harvest1 = Animator.StringToHash("harvest");
    private bool _chopDownForever = false;
    
    public ResourceEnum Resource => _storage.ResourceType;

    public int Priority => _chopDownForever ? 0 : 1;

    private void Awake()
    {
        if (_storage == null)
            throw new UnityException("No Storage Assigned");
        if (_dropPrefab == null)
            throw new UnityException("No Drop Prefab Assigned");
    }

    private void Start()
    {
        _storage.ItemCount = _storage.StorageCapacity;
    }

    public void Chop()
    {
        if(_animator != null)
            _animator.SetTrigger(Harvest1);
        if(_particle != null)
            _particle.Play();
    }
    
    public void Harvest(Vector3 pos)
    {
        _storage.ItemCount--;
        _occupied--;
        if (_occupied < 0)
            throw new UnityException("Harvested more than occupied");
        Instantiate(_dropPrefab,
            pos + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 0)).normalized * _dropDistance + new Vector3(0, 0.5f),
            new Quaternion());
        if (_chopDownForever && _storage.ItemCount == 0)
        {
            _onChoppedDownForever.Invoke();
            gameObject.SetActive(false);
        }
    }

    public void Harvest()
    {
        Harvest(transform.position);
    }

    public void Occupy(int count)
    {
        if (count > GetAvailableResources())
            throw new UnityException("Trying to occupy more than available");
        _occupied += count;
    }
    
    public void Vacant(int count)
    {
        if (count > _occupied)
            throw new UnityException("Trying to vacant more than occupied");
        _occupied -= count;
    }

    public int GetAvailableResources()
    {
        return _storage.ItemCount - _occupied;
    }

    public void ChopDownForever()
    {
        _chopDownForever = true;
    }
}