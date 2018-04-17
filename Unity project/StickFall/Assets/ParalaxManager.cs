using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxManager : SingletonMonoBehaviour<ParalaxManager>
{
    
    [SerializeField] List<ParalixebleObject> paralaxObjects;
    

    private void Awake()
    {
        paralaxObjects.ForEach((obj) =>
        {
            obj.Init();
        });
    }


    private void Update()
    {
        float deltaTime = Time.deltaTime;
        paralaxObjects.ForEach((obj) =>
        {
            obj.CustomUpdate(deltaTime);
        });
    }

}
