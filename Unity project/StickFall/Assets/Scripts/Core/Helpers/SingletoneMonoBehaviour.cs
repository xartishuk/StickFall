
using System;
using UnityEngine;
public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
//#warning ToDo instanciate
    static T _instance;
    public static T Instance
    {
        get
        {
            return _instance;
        }
    }


    public static T InstanceIfExist
    {
        get
        {
            return _instance;
        }
    }

    public SingletonMonoBehaviour()
    {
        _instance = this as T;
    }

    protected virtual void Awake()
    {
        _instance = this as T;
    }
}

