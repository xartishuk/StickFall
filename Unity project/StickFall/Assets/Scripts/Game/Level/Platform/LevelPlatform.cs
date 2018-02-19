using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPlatform : BaseObject
{
    #region Fields

    [SerializeField] GameObject _meshes;
    [SerializeField] BoxCollider2D _collider;

    #endregion

    #region Properties

    public float Width
    {
        get;
        private set;
    }

    #endregion



    #region Unity lifecycle

    void Start()
    {

    }
    
    void Update()
    {

    }
    
    #endregion

    #region Public methods

    public void Init(float width)
    {
        Width = width;
        _meshes.transform.localScale = new Vector3(width, 1f, 1f);
        _collider.size = new Vector2(width, 100);
        _collider.offset = new Vector2(0f, -50f);
    }

    #endregion
}
