using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreator
{
    public static GameObject CreateObject(GameObject prefab, Transform _transform, int preInit = 1)
    {
        Debug.Log("Try to create pool " + prefab.name);
        //ObjectPool pool;
        //pool = PoolManager.Instance.FindPool(prefab);

        //if(pool == null)
        //{
        //    pool = PoolManager.Instance.PoolForObject(prefab);
        //}

        //GameObject obj = pool.Pop();

        GameObject obj = Object.Instantiate(prefab);

        obj.transform.position = _transform.position;

        obj.transform.parent = _transform;

        return obj;
    }
    
}
