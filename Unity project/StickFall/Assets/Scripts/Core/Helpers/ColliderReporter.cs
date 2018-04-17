using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColliderType
{
    None = 0,
    PlatformStoper = 1,

    StickChecker = 99,
    StickStoper = 100,
    CameraCollider = 101,
}

public class ColliderReporter : MonoBehaviour
{
    [SerializeField] BaseObject _colliderHandler;
    [SerializeField] ColliderType _colliderType;

    public BaseObject ColliderHandler
    {
        get
        {
            return _colliderHandler;
        }
    }

    public ColliderType ColliderType
    {
        get
        {
            return _colliderType;
        }
    }

}
