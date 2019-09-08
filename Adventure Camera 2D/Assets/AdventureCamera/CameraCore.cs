using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Camera))]
[DisallowMultipleComponent]
[AddComponentMenu("Adventure Camera 2D/Core")]

public class CameraCore : MonoBehaviour
{
    public bool boundToRegion = true;
    private Camera _camera;
    private float _cameraSize;
     Bounds cameraLimits;

    // Camera Limits
    public Vector2 max;
    public Vector2 min;




    private void Start()
    {
        _camera = GetComponent<Camera>();
        _camera.orthographic = true;
        _cameraSize = _camera.orthographicSize;

    }

    void OnDrawGizmos()
    {
        if(boundToRegion)
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
            
        }
        
    }

    public void StartFollow()
    {
        
    }

    public void StopFollow()
    {
        
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

}