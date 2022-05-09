using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceIndex : MonoBehaviour
{
    [SerializeField] private ResourceEnum[] _blockToResource;

    private static ResourceIndex instance = null;

    public static ResourceIndex Instance => instance;

    public static ResourceEnum[] BlockToResource => Instance._blockToResource;

    private void OnValidate()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
    }

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
    }
}
