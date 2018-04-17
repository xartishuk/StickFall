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
        Stoper = 2,
        Checker = 3,
        Perfect = 4,

    }

    [System.Serializable]
    struct PlatformAnchors
    {
        public PlatformAnchorsTypes _type;
        public Transform _transform;
    }

    [System.Serializable]
    struct MeshAndCollider
    {
        public GameObject _gameObject;
        public BoxCollider2D _collider;
    }

    #region Fields

    [Header("Mesh GO")]
    [SerializeField] MeshAndCollider _platform;
    [SerializeField] MeshAndCollider _perfect;

    [SerializeField] StickController _stick;


    [SerializeField] List<PlatformAnchors> _anchors;
    [SerializeField] Vector2 perfectSize;

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

    public Vector3 PerfectPosition
    {
        get
        {
            return _perfect._collider.transform.position;
        }
    }

    public Vector2 PerfectSize
    {
        get
        {
            return perfectSize;
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
        _platform._gameObject.transform.localScale = new Vector3(Width, 1f, 1f);
        _platform._collider.size = new Vector2(Width, 100);
        _platform._collider.offset = new Vector2(0f, -50f);

        PositionizeAnchor(PlatformAnchorsTypes.Stick);
        PositionizeAnchor(PlatformAnchorsTypes.Stoper);
        PositionizeAnchor(PlatformAnchorsTypes.Perfect);

        _perfect._gameObject.transform.localScale = new Vector3(perfectSize.x, perfectSize.y, 1f);
        _perfect._collider.size = new Vector2(perfectSize.x, perfectSize.y);
        _perfect._collider.offset = new Vector2(perfectSize.x, perfectSize.y * 0.5f);

        _perfect._collider.gameObject.SetActive(true);
        _perfect._gameObject.gameObject.SetActive(true);

        _platform._collider.gameObject.SetActive(true);
        _platform._gameObject.gameObject.SetActive(true);
        
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

            case PlatformAnchorsTypes.Checker:
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

    public void Perfect()
    {
        _perfect._collider.gameObject.SetActive(false);
        _perfect._gameObject.gameObject.SetActive(false);
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
            StickController.StartGrow();
            Debug.Log("StartGrow");
        }
    }


    void GameManager_OnStickStopGrow ()
    {
        if (IsActive)
        {
            StickController.StopGrow();
            StickController.FallStick();
            Debug.Log("StopGrow");
        }
    }

}
