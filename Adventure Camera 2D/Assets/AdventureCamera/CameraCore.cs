using UnityEngine;

[RequireComponent(typeof(Camera))]
[DisallowMultipleComponent]
[AddComponentMenu("2DCameraKit/GenericCamera")]

public class CameraCore : MonoBehaviour
{
    [Header("Camera Movement Behavior")]
    [Space(10f)]
    [SerializeField] private bool followMouse;
    [SerializeField] private bool followTargets;
    public Transform[] targets;
    
    [SerializeField] Collider2D cameraRegion;
    public Vector3 cameraOffset;

    private Camera _camera;
    private float _cameraSize;
    private float _cameWidth, _cameHeight;
    private Vector2 _boundsMin, _boundsMax;
    
    
    //private bool _isInTask;
    private Vector3 _target;
    private Vector3 _zoomPosition;
    
    [SerializeField] [Range(0.1f, 5f)] private float cameraSpeed;

    [SerializeField] [Range(0f, 1f)] private float resetMargin;
    
    [SerializeField] [Range(0.1f, 5f)] private float zoomSpeed;
    [SerializeField] [Range(0.1f, 5f)] private float maxZoom;
    [SerializeField]  private bool boundToRegion;
    [SerializeField]  private bool focusOnZoom = true;


    private void Start()
    {
        _camera = GetComponent<Camera>();
        _camera.orthographic = true;
        _cameraSize = _camera.orthographicSize;
        AutoCameraSize(_camera);
        SetBounds(cameraRegion.bounds);
        // ClampBounds();
    }

    private void Update()

    {
        Follow();


        if (Input.GetMouseButton(1))
        {
            ZoomIn(maxZoom);
        }

        else
        {
            if (_cameraSize > _camera.orthographicSize)
            {
                ZoomOut(_cameraSize);
                //CameraMove();
            }
            //else
                //_zoomPosition = transform.position;
        }
    }

     private void CameraMove()
    {
        if (Vector3.Distance(_target, transform.position) > resetMargin)
        {
            if (boundToRegion) ClampBounds(_boundsMin,_boundsMax);
            _target = new Vector3(_target.x + cameraOffset.x, _target.y + cameraOffset.y, cameraOffset.z);
            transform.position = Vector3.Lerp(transform.position, _target, Time.smoothDeltaTime * cameraSpeed);
        }

        else
        {
            transform.position = _target;
        }
    }


    private void ClampBounds(Vector2 boundMin,Vector2 boundMax)
    {
       float _minX, _minY;

        _minX = Mathf.Clamp(_target.x, boundMin.x, boundMax.x);
        _minY = Mathf.Clamp(_target.y, boundMin.y, boundMax.y);
        _target = new Vector3(_minX, _minY, 0);
    }

    private void Follow()
    {
        if (followTargets & (targets.Length > 0))
        {
            _target = cameraOffset;
            for (var i = 0; i < targets.Length; i++) _target += targets[i].position;


            if (followMouse & Input.mousePresent)
            {
                _target /= targets.Length + 1;
                _target += _camera.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                _target /= targets.Length;
            }

            CameraMove();
        }
        else if (followMouse & !followTargets & Input.mousePresent)
        {
            if (!boundToRegion)
                _target = _camera.ScreenToWorldPoint(Input.mousePosition);
            else
                _target = _camera.ScreenToWorldPoint(Input.mousePosition);
            CameraMove();
        }
    }


    private void ZoomIn(float _zoom)
    {
        _camera.orthographicSize = Vector3.Lerp(new Vector3(_camera.orthographicSize, 0, 0),
            new Vector3(_zoom, 0, 0), Time.deltaTime * zoomSpeed).x;

        if (focusOnZoom)
        {
            //SetBounds();
        }
        else
        {
            AutoCameraSize(_camera);
        }
    }

    private void ZoomOut(float zoom)
    {
        
        AutoCameraSize(_camera);

        if (_cameraSize - _camera.orthographicSize > resetMargin)
        {
            
           // CenterCamera();
            _camera.orthographicSize = Vector3.Lerp(new Vector3(_camera.orthographicSize, 0, 0),
                new Vector3(zoom, 0, 0), Time.deltaTime * zoomSpeed).x;
        }
        else
        {
            SetZoom(_cameraSize);
           // CenterCamera();
        }

       
    }

    private void SetZoom(float zoom)
    {
        _camera.orthographicSize = zoom;
    }

    private void AutoCameraSize(Camera myCamera)
    {
        _cameWidth = myCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x -
                     myCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        _cameHeight = myCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y -
                      myCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

        SetBounds(cameraRegion.bounds);
    }

    private void CenterCamera()
    {
        _camera.transform.position = Vector3.Lerp(_camera.transform.position,
            new Vector3(_zoomPosition.x, _zoomPosition.y, cameraOffset.z),
            Time.deltaTime * cameraSpeed);
    }

    private void SetBounds(Bounds region)
    {
        _boundsMin.x = region.min.x + _cameWidth / 2;
        _boundsMax.x = region.max.x - _cameWidth / 2;
        _boundsMin.y = region.min.y + _cameHeight / 2;
        _boundsMax.y = region.max.y - _cameHeight / 2;
    }

    
}