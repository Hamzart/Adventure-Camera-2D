using UnityEngine;
using UnityEditor;
using System.Collections;

[RequireComponent(typeof(Camera))]
[DisallowMultipleComponent]
[AddComponentMenu("Adventure Camera 2D/Core")]

public class CameraCore : MonoBehaviour
{

     public enum Target
    {
        Mouse,
        GameObject
    }



    [Header("Camera Target")]
    public bool following;
    public Target cameraTarget;
    public Transform target;
    Vector3 targetPosition;
    public float cameraSpeed = 10.0f;

    // Target for the Camera to follow

    private Camera _camera;
    private float _cameraSize;
    private Bounds cameraLimits;

    // Camera Limits
    [Header("Camera Limits")]
    public bool boundToLimits = true;
    public Vector2 max;
    public Vector2 min;

    [Header("Target Offset")]
    public Vector3 offset = new Vector3(0,0,-10);


    [Header("Dead Zone")]
    public bool useDeadZone = false;

    [Range(0, 1)]
    public float width = 0.3f;
    [Range(0, 1)]
    public float height = 0.2f;

    public float deadZoneWidth;
    public float deadZoneHeight;

    [Header("Look Ahead")]
    public bool lookAhead = false;
    public float distance = 1.0f;


    Vector2 boundMin,boundMax;
    public Bounds cameraBounds;
    Transform myposition;



    private void Start()
    {
        myposition = transform;
        _camera = GetComponent<Camera>();
        _camera.orthographic = true;
        _cameraSize = _camera.orthographicSize;
        cameraBounds.SetMinMax(new Vector3(myposition.position.x - CameraWidth() / 2, myposition.position.y - CameraHeight()/2 ),new Vector3(myposition.position.x + CameraWidth() / 2, myposition.position.y + CameraHeight() / 2));
        
    }


    // Draw the Camera Limits Gizmos


#if UNITY_EDITOR
    void OnDrawGizmos()
    {


        myposition = transform;
        _camera = GetComponent<Camera>();
        _camera.orthographic = true;
        _cameraSize = _camera.orthographicSize;




        if (boundToLimits)
        {
            float temp;

            if (min.x > max.x)
            {
                temp = min.x;
                min.x = max.x;
                max.x = temp;
            }

            if (min.y > max.y)
            {
                temp = min.y;
                min.y = max.y;
                max.y = temp;
            }

            cameraLimits.SetMinMax(new Vector3(min.x, min.y, 0), new Vector3(max.x, max.y, 0));

            Gizmos.color = Color.red;
            Gizmos.DrawLine(cameraLimits.min, new Vector3(cameraLimits.min.x, cameraLimits.max.y, 0));
            Gizmos.DrawLine(new Vector3(cameraLimits.min.x, cameraLimits.max.y, 0), cameraLimits.max);
            Gizmos.DrawLine(cameraLimits.max, new Vector3(cameraLimits.max.x, cameraLimits.min.y, 0));
            Gizmos.DrawLine(new Vector3(cameraLimits.max.x, cameraLimits.min.y, 0), cameraLimits.min);

            deadZoneHeight = CameraHeight() * height;
            deadZoneWidth = CameraWidth() * width;

        }

        if(useDeadZone)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(new Vector3(myposition.position.x - deadZoneWidth * 0.5f, myposition.position.y - deadZoneHeight * 0.5f, 0), new Vector3(myposition.position.x - deadZoneWidth * 0.5f, myposition.position.y + deadZoneHeight * 0.5f, 0));
            Gizmos.DrawLine(new Vector3(myposition.position.x - deadZoneWidth * 0.5f, myposition.position.y + deadZoneHeight * 0.5f, 0), new Vector3(myposition.position.x + deadZoneWidth * 0.5f, myposition.position.y + deadZoneHeight * 0.5f, 0));
            Gizmos.DrawLine(new Vector3(myposition.position.x + deadZoneWidth * 0.5f, myposition.position.y + deadZoneHeight * 0.5f, 0), new Vector3(myposition.position.x + deadZoneWidth * 0.5f, myposition.position.y - deadZoneHeight * 0.5f, 0));
            Gizmos.DrawLine(new Vector3(myposition.position.x + deadZoneWidth * 0.5f, myposition.position.y - deadZoneHeight * 0.5f, 0), new Vector3(myposition.position.x - deadZoneWidth * 0.5f, myposition.position.y - deadZoneHeight * 0.5f, 0));


        }

#endif

    }

    void Update()
    {
        if(following)
        {
            Follow();
        }
    }

    public void Follow()
    {
        if(cameraTarget==Target.Mouse)
        {
            targetPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (cameraTarget == Target.GameObject)
        {
            targetPosition = target.position;

        }

        if(boundToLimits)
        {
            myposition.position = Vector3.Lerp(myposition.position, LimitersRegion(min, max), Time.smoothDeltaTime * cameraSpeed);
        }
        else
        {
            myposition.position = Vector3.Lerp(myposition.position, targetPosition, Time.smoothDeltaTime * cameraSpeed);
        }

    }



    public void StopFollow()
    {
        following = false;
    }
    
    public void ZoomIn()
    {
        
    }
    
    public void ZoomOut()
    {
        
    }
    
    public void SetZoom()
    {
        
    }
    
    
    public void Rotate()
    {
        
    }
    
    public void SetRotation()
    {
        
    }
    
    public void SetLimits()
    {
        
    }
    
    public void RemoveLimits()
    {
        
    }
    
    public void Shake()
    {
        
    }

    float CameraWidth()
    {
       //Debug.Log(_camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - _camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x);

        return _camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - _camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
    }

    float CameraHeight()
    {
        //Debug.Log(_camera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - _camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y);
        return _camera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - _camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
    }

    private Vector3 LimitersRegion(Vector2 min,Vector2 max)
    {
        float _minX, _minY;

        boundMin.x = min.x + CameraWidth() / 2;
        boundMax.x = max.x - CameraWidth() / 2;
        boundMin.y = min.y + CameraHeight() / 2;
        boundMax.y = max.y - CameraHeight() / 2;
  
        _minX = Mathf.Clamp(targetPosition.x + offset.x, boundMin.x, boundMax.x);
        _minY = Mathf.Clamp(targetPosition.y + offset.y, boundMin.y, boundMax.y);
        

        return new Vector3(_minX, _minY, offset.z);

        
    }









}


