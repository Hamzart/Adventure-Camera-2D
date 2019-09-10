using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraCore))]
[DisallowMultipleComponent]
[AddComponentMenu("Adventure Camera 2D/DeadZone")]
public class DeadZone :  MonoBehaviour 
{

    [Header("Dead Zone")]
    public bool useDeadZone = false;
    [Range(0, 1)]
    public float width = 0.3f;
    [Range(0, 1)]
    public float height = 0.2f;

    public bool ignoreY = false;
    [HideInInspector]
    public  float deadZoneWidth;
    [HideInInspector]
    public float deadZoneHeight;

    [HideInInspector]
    public CameraCore cam;
    void Start()
    {
        cam = GetComponent<CameraCore>();
    }

    void OnDrawGizmos()
    {
        cam = GetComponent<CameraCore>();
        deadZoneHeight = cam.CameraHeight() * height;
        deadZoneWidth = cam.CameraWidth() * width;

        if (useDeadZone)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(new Vector3(cam.GetPosition().position.x - deadZoneWidth * 0.5f, cam.GetPosition().position.y - deadZoneHeight * 0.5f, 0), new Vector3(cam.GetPosition().position.x - deadZoneWidth * 0.5f, cam.GetPosition().position.y + deadZoneHeight * 0.5f, 0));
            Gizmos.DrawLine(new Vector3(cam.GetPosition().position.x - deadZoneWidth * 0.5f, cam.GetPosition().position.y + deadZoneHeight * 0.5f, 0), new Vector3(cam.GetPosition().position.x + deadZoneWidth * 0.5f, cam.GetPosition().position.y + deadZoneHeight * 0.5f, 0));
            Gizmos.DrawLine(new Vector3(cam.GetPosition().position.x + deadZoneWidth * 0.5f, cam.GetPosition().position.y + deadZoneHeight * 0.5f, 0), new Vector3(cam.GetPosition().position.x + deadZoneWidth * 0.5f, cam.GetPosition().position.y - deadZoneHeight * 0.5f, 0));
            Gizmos.DrawLine(new Vector3(cam.GetPosition().position.x + deadZoneWidth * 0.5f, cam.GetPosition().position.y - deadZoneHeight * 0.5f, 0), new Vector3(cam.GetPosition().position.x - deadZoneWidth * 0.5f, cam.GetPosition().position.y - deadZoneHeight * 0.5f, 0));


        }
    }
}
