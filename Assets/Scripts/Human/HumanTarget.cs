using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class HumanTarget : MonoBehaviour
{
    [SerializeField] private Transform[] _characterSlots;
    private HumanController[] _humanControllers;
    private int _currentCharacterSlot = 0;
    
    public Transform TargetTransform(HumanController humanController)
    {
        if (_characterSlots.Length == 0)
            return transform;
        _humanControllers ??= new HumanController[_characterSlots.Length];
     
        int index = _humanControllers.ToList().IndexOf(humanController);
        if (index == -1)
            index = (_currentCharacterSlot + 1) % _characterSlots.Length;

        _currentCharacterSlot = index;
        _humanControllers[_currentCharacterSlot] = humanController;
        
        return _characterSlots[_currentCharacterSlot];
    }
}