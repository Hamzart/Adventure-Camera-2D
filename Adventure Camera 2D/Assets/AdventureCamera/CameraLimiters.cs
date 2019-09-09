using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraCore))]
[DisallowMultipleComponent]
[AddComponentMenu("Adventure Camera 2D/Limiters")]
public class CameraLimiters : MonoBehaviour
{

    [Header("Camera Limits")]
    public bool boundToLimits = true;
    public float height = 20.0f;
    public float width = 40.0f;

    Bounds cameraLimits;
    Vector2 boundMin, boundMax;
    private Bounds cameraBounds;
    public Vector2 limiterPos;

    [HideInInspector]
    public CameraCore cam;

    void Start()
    {

        cam = GetComponent<CameraCore>();

    }
    public void SetZoom(float zoom)
    {
        cam.cameraSize = zoom;

    }


    //DRAW Camera Limits Zone ( Red Box )
    void OnDrawGizmos()
    {
        cam = GetComponent<CameraCore>();


        if (boundToLimits)
        {

            float temp;

            

            cameraLimits.SetMinMax(new Vector3(limiterPos.x - width/2, limiterPos.y - height/2, 0), new Vector3(limiterPos.x + width / 2, limiterPos.y + height / 2, 0));

            Gizmos.color = Color.red;
            Gizmos.DrawLine(cameraLimits.min, new Vector3(cameraLimits.min.x, cameraLimits.max.y, 0));
            Gizmos.DrawLine(new Vector3(cameraLimits.min.x, cameraLimits.max.y, 0), cameraLimits.max);
            Gizmos.DrawLine(cameraLimits.max, new Vector3(cameraLimits.max.x, cameraLimits.min.y, 0));
            Gizmos.DrawLine(new Vector3(cameraLimits.max.x, cameraLimits.min.y, 0), cameraLimits.min);



        }
    }
         public Vector3 LimitersRegion()
        {

        cameraBounds.SetMinMax(new Vector3(cam.GetPosition().position.x - cam.CameraWidth() / 2, cam.GetPosition().position.y - cam.CameraHeight() / 2), new Vector3(cam.GetPosition().position.x + cam.CameraWidth() / 2, cam.GetPosition().position.y + cam.CameraHeight() / 2));

        float _minX, _minY;

            boundMin.x = cameraLimits.min.x + cam.CameraWidth() / 2;
            boundMax.x = cameraLimits.max.x - cam.CameraWidth() / 2;
            boundMin.y = cameraLimits.min.y + cam.CameraHeight() / 2;
            boundMax.y = cameraLimits.max.y - cam.CameraHeight() / 2;

            _minX = Mathf.Clamp(cam.targetPosition.x + cam.offset.x, boundMin.x, boundMax.x);
            _minY = Mathf.Clamp(cam.targetPosition.y + cam.offset.y, boundMin.y, boundMax.y);

            return new Vector3(_minX, _minY, cam.offset.z);

         }
}
