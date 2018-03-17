using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPlatform : BaseObject
{

    [System.Serializable]
    enum PlatformAnchorsTypes
    {
        None    = 0,
        Stick   = 1,
        Stoper  = 2,

    }

    [System.Serializable]
    struct PlatformAnchors
    {
        public PlatformAnchorsTypes _type;
        public Transform _transform;
    }

    #region Fields

    [SerializeField] GameObject _meshes;
    [SerializeField] BoxCollider2D _collider;
    [SerializeField] StickController _stick;


    [SerializeField] List<PlatformAnchors> _anchors;

    bool isStickGrow;


    #endregion

    #region Properties

    public float Width
    {
        get;
        private set;
    }

    public bool IsActive
    {
        get;
        set;
    }

    public StickController StickController
    {
        get
        {
            return _stick;
        }
    }

    #endregion



    #region Unity lifecycle

    void OnEnable()
    {
        GameManager.OnStickStartGrow += GameManager_OnStickStartGrow;
        GameManager.OnStickStopGrow += GameManager_OnStickStopGrow;
        IsActive = false;
    }
    void OnDisable()
    {
        GameManager.OnStickStartGrow -= GameManager_OnStickStartGrow;
        GameManager.OnStickStopGrow -= GameManager_OnStickStopGrow;
        IsActive = false;
    }




    void Start()
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
        PositionizeAnchor(PlatformAnchorsTypes.Stick);

        PositionizeAnchor(PlatformAnchorsTypes.Stoper);
    }

    void PositionizeAnchor(PlatformAnchorsTypes _anchorType)
    {
        PlatformAnchors anchor = TransformAnchorByType(_anchorType);

        switch(_anchorType)
        {
            case PlatformAnchorsTypes.Stick:
                anchor._transform.localPosition = new Vector3(Width * 0.5f, -10f, 0f);
                break;

            case PlatformAnchorsTypes.Stoper:
                anchor._transform.localPosition = new Vector3(Width * 0.5f, 0f, 0f);
                break;
        }
    }

    void InitializeAnchor(PlatformAnchorsTypes _anchorType)
    {
        PlatformAnchors anchor = TransformAnchorByType(_anchorType);

        switch(_anchorType)
        {
            case PlatformAnchorsTypes.Stick:
                break;
            case PlatformAnchorsTypes.Stoper:
                break;
        }
    }

    #endregion

    PlatformAnchors TransformAnchorByType(PlatformAnchorsTypes _anchorType)
    {
        return _anchors.Find(obj => obj._type == _anchorType);
    }

    void GameManager_OnStickStartGrow ()
    {
        if (IsActive)
        {
            _stick.StartGrow();
            Debug.Log("StartGrow");
        }
    }


    void GameManager_OnStickStopGrow ()
    {
        if (IsActive)
        {
            _stick.StopGrow();
            _stick.FallStick();
            Debug.Log("StopGrow");
        }
    }

}
