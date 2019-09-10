using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
[DisallowMultipleComponent]
[AddComponentMenu("Adventure Camera 2D/Zomming")]


public class Zooming : MonoBehaviour
{
    [HideInInspector]
    public CameraCore cam;

    public float zoomSpeed = 0.2f;
    void Start()
    {
        cam = GetComponent<CameraCore>();
    }

  
      
        
    

    void SetZoom(float zoom)
    {
        if(zoom >0)
        cam.cameraSize = zoom;
    }

    void SmoothZoom(float targetZoom)
    {
        if (cam.cameraSize > targetZoom)
        {
            if ((cam.cameraSize - targetZoom) <= 0.01f)
            {
                cam.cameraSize = targetZoom;
            }
            else
            {
                cam.cameraSize = Vector3.Lerp(new Vector3(cam.cameraSize, 0, 0), new Vector3(targetZoom, 0, 0), Time.deltaTime * zoomSpeed).x;
            }
        }
        else if (cam.cameraSize < targetZoom)
        {
            if ((targetZoom - cam.cameraSize) <= 0.01f)
            {
                cam.cameraSize = targetZoom;

            }
            else
            {
                cam.cameraSize = Vector3.Lerp(new Vector3(cam.cameraSize, 0, 0), new Vector3(targetZoom, 0, 0), Time.deltaTime * zoomSpeed).x;
            }
        }
    }

    
}

