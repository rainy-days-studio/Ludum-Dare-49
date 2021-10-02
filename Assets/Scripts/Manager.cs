using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _Instance;
    public static T Instance { get { return _Instance; } }

    void Awake()
    {
        _Instance = (T)FindObjectOfType(typeof(T));
    }

    void OnDestroy()
    {
        _Instance = null;
    }
}