using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour, IPoolCallback, IOnColliderHandler
{
    #region Fields

    float localTimescale;

    Transform cachedTransform;

    #endregion

    #region Properties
    
    
    protected Transform CachedTransform
    {
        get
        {
            if (cachedTransform == null)
            {
                cachedTransform = transform;
            }

            return cachedTransform;
        }
    }

    public float LocalTimescale
    {
        get
        {
            return localTimescale;
        }

        set
        {
            localTimescale = value;
        }
    }

    #endregion

    #region Unity lifecycle

    void Update()
    {
        CustomUpdate(LocalTimescale * Time.deltaTime);
    }

    void LateUpdate()
    {
        CustomLateUpdate(LocalTimescale * Time.deltaTime);
    }

    #endregion

    #region Virtual methods

    protected virtual void CustomUpdate(float deltaTime)
    {

    }

    protected virtual void CustomLateUpdate(float deltaTime)
    {
        
    }

    #endregion

 

    #region IPoolCallback

    public void OnCreateFromPool()
    {
        LocalTimescale = 1.0f;
    }
    public void OnReturnToPool()
    {

    }

    public void OnPop()
    {

    }
    public void OnPush()
    {

    }

    #endregion

    #region IOnColliderHandler

    public virtual void OnChildCollisionEnter(Collision collision)
    {

    }

    public virtual void OnChildCollisionExit(Collision collision)
    {

    }


    public virtual void OnChildTriggerEnter(Collider other)
    {

    }

    public virtual void OnChildTriggerExit(Collider other)
    {

    }

    #endregion
}
