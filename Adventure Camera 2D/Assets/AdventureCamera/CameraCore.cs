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

    public enum LookAhead
    {
        Scale,
        Rotation,
        Velocity,
    }

    // Camera
    [Header("Camera Settings")]
    Camera _camera;
    public float cameraSize = 5.0f;
    public bool active;
    public float cameraSpeed = 10.0f;



    // Target for the Camera to follow
    [Header("Camera Target")]
    public Target cameraTarget;
    public Transform target;
    [HideInInspector]
    public Vector3 targetPosition;


    // Camera Limits 
    bool boundToLimitsAvailable = false;
    CameraLimiters limiter;
    // Dead Zone
    bool deadZoneAvailable = false;
    DeadZone deadZone;


    // Camera Offset
    [Header("Offset")]
    public Vector3 offset = new Vector3(0,0,-10);


    // LookAhead Only works with GameObject as a target
    [Header("Look Ahead")]
    public bool lookAhead = false;
    public LookAhead detectionType;
    public float targetDirection = 1.0f;
    public float aheadDistance = 1.0f;


    
    private Transform myposition;
    Vector3 aimPosition;
    Zooming zoomer;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        myposition = transform;
        _camera.orthographic = true;
        _camera.orthographicSize = cameraSize;

        // Checking for modules 

        if (GetComponent<DeadZone>() != null)

        {
            deadZoneAvailable = true;
            deadZone = GetComponent<DeadZone>();
        }

        if (GetComponent<CameraLimiters>() != null)

        {
            boundToLimitsAvailable = true;
            limiter = GetComponent<CameraLimiters>();
        }

        if (GetComponent<Zooming>() != null)

        {
            zoomer = GetComponent<Zooming>();
        }

    }


    // Draw the Camera Limits Gizmos


#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        _camera = GetComponent<Camera>();
        myposition = transform;
        _camera.orthographic = true;
        _camera.orthographicSize = cameraSize;


        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(new Vector3(aimPosition.x ,aimPosition.y,0 ), new Vector3(0.2f, 0.2f, 0));

    }

#endif

    void Update()
    {
        
 
            if (Input.GetMouseButton(1))
            {
                SendMessage("SmoothZoom", 3);
            }
            else if (!Input.GetMouseButton(1))
            {
                SendMessage("SmoothZoom", 5);
            }




            if (active)
        {
            SetLookAhead();
            if (cameraTarget == Target.Mouse)
            {
                aimPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            }
            else if (cameraTarget == Target.GameObject)
            {
                aimPosition = target.position;
                if (lookAhead)
                {
                    aimPosition.x = aimPosition.x + (aheadDistance * targetDirection);
                }
            }
            

            if(deadZoneAvailable)
            {
                if(deadZone.useDeadZone)
                {
                    if (deadZone.ignoreY)
                    {
                        if (aimPosition.x < GetPosition().position.x - deadZone.deadZoneWidth * 0.5f |
                            aimPosition.x > GetPosition().position.x + deadZone.deadZoneWidth * 0.5f)
                        {
                            targetPosition = new Vector3(aimPosition.x, myposition.position.y, targetPosition.z);
                        }
                    }

                    else
                    {
                        if ( aimPosition.x < GetPosition().position.x - deadZone.deadZoneWidth * 0.5f |
                            aimPosition.x > GetPosition().position.x + deadZone.deadZoneWidth * 0.5f | 
                            aimPosition.y < GetPosition().position.y - deadZone.deadZoneHeight * 0.5f |
                            aimPosition.y > GetPosition().position.y + deadZone.deadZoneHeight * 0.5f )
                        {
                            targetPosition = aimPosition;
                        }

                    }



                }

                else
                {
                    targetPosition = aimPosition;
                }
            }
            else
            {

                targetPosition = aimPosition;

            }

            Follow();

        }
    }

    public void Follow()
    {

       if (boundToLimitsAvailable )

        {
            if(limiter.boundToLimits)
            {
                myposition.position = Vector3.Lerp(myposition.position, limiter.LimitersRegion(), Time.smoothDeltaTime * cameraSpeed);
            }
            else
            {
                myposition.position = Vector3.Lerp(myposition.position, new Vector3(targetPosition.x + offset.x, targetPosition.y + offset.y,targetPosition.z + offset.z), Time.smoothDeltaTime * cameraSpeed);

            }

        }
        else if(!boundToLimitsAvailable)
        {

             myposition.position = Vector3.Lerp(myposition.position, targetPosition + offset, Time.smoothDeltaTime * cameraSpeed);
            
        }

    }

    void SetLookAhead()
    {
        float tmp = target.transform.eulerAngles.y;

        if (cameraTarget == Target.GameObject)
        {
            if (detectionType == LookAhead.Rotation)
            {
                //Debug.Log(targetDirection);

                if (tmp > 90 & tmp < 270)
                {
                    targetDirection = -1.0f;
                }
                else
                {

                    targetDirection = 1.0f;
                }
            }

            else if (detectionType == LookAhead.Scale)
            {
                if (target.transform.localScale.x > 0)
                {
                    targetDirection = 1.0f;
                }
                else
                {
                    targetDirection = -1.0f;
                }
            }
            else if (detectionType == LookAhead.Velocity)
            {
                if (target.GetComponent<Rigidbody2D>() != null)
                {
                    if(target.GetComponent<Rigidbody2D>().velocity.x > 0)
                    {
                        targetDirection = 1.0f;

                    }
                    else
                    {
                        targetDirection = -1.0f;

                    }
                }
            }
        }

        
    }

    public void StopFollow()
    {
        active = false;
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

    public float CameraWidth()
    {
       //Debug.Log(_camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - _camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x);

        return _camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - _camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
    }

    public float CameraHeight()
    {
        //Debug.Log(_camera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - _camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y);
        return _camera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - _camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
    }

    public Transform GetPosition()
    {
        return myposition;
    }

    public Camera GetCamera()
    {
        return _camera;
    }

    




}


