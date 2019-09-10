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
    public Vector3 targetPosition;
    public float cameraSpeed = 10.0f;

    // Target for the Camera to follow

    public Camera camera;
    public float cameraSize;



    // Camera Limits 
    bool boundToLimitsAvailable = false;
    CameraLimiters limiter;
    // Dead Zone
    bool deadZoneAvailable = false;
    DeadZone deadZone;



    [Header("Target Offset")]
    public Vector3 offset = new Vector3(0,0,-10);


    

    [Header("Look Ahead")]
    public bool lookAhead = false;
    public float distance = 1.0f;


    
    private Transform myposition;
    Vector3 aimPosition;


    private void Start()
    {
        camera = GetComponent<Camera>();
        myposition = transform;
        camera.orthographic = true;
        cameraSize = camera.orthographicSize;

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

    }


    // Draw the Camera Limits Gizmos


#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        camera = GetComponent<Camera>();
        myposition = transform;
        camera.orthographic = true;
        cameraSize = camera.orthographicSize;

        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(new Vector3(aimPosition.x ,aimPosition.y,0 ), new Vector3(0.2f, 0.2f, 0));

    }

#endif

    void Update()
    {
        if(following)
        {
            if (cameraTarget == Target.Mouse)
            {
                aimPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            }
            else if (cameraTarget == Target.GameObject)
            {
                aimPosition = target.position;

            }

            if(deadZoneAvailable)
            {
                if(deadZone.useDeadZone)
                {
                    if( aimPosition.x < GetPosition().position.x - deadZone.deadZoneWidth * 0.5f |
                        aimPosition.x > GetPosition().position.x + deadZone.deadZoneWidth * 0.5f |
                        aimPosition.y < GetPosition().position.y - deadZone.deadZoneHeight * 0.5f |
                        aimPosition.y > GetPosition().position.y + deadZone.deadZoneHeight * 0.5f)
                    {

                        targetPosition = aimPosition;
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
       
       if(boundToLimitsAvailable )
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

    public float CameraWidth()
    {
       //Debug.Log(_camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - _camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x);

        return camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
    }

    public float CameraHeight()
    {
        //Debug.Log(_camera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - _camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y);
        return camera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
    }

    public Transform GetPosition()
    {
        return myposition;
    }

    




}


