using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxManager : SingletonMonoBehaviour<ParalaxManager>
{
    //[System.Serializable]
    //class ParalaxInfo
    //{
    //    public GameObject prefab;
    //    public Transform transform;
    //}

    [SerializeField] List<ParalixebleObject> paralaxObjects;

    //List<ParalaxInfo> paralaxObjects;

    private void Awake()
    {
        paralaxObjects.ForEach((obj) =>
        {
            obj.Init();
            //obje
        });
    }

}
