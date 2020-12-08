using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joycon_Crosshair : MonoBehaviour
{
    [Range(0.0f, 25.0f)]
    public float rayLength;
    public Camera mainCamera;
    public bool onScreen;
    public Vector3 pointOnScreen;

    private RaycastHit aim;
    public bool colliding;
    public GameObject pointingObject;

    void Start()
    {
        
    }

    void Update()
    {
        Debug.DrawRay(transform.position, -transform.up * rayLength, Color.red, 0.1f);

        if (Physics.Raycast(transform.position, -transform.up, out aim, rayLength))
        {
            colliding = true;
            pointingObject = aim.transform.gameObject;
        }
        else
        {
            pointingObject = null;
        }

        if (colliding)
        {
            Vector3 screenPos = mainCamera.WorldToViewportPoint(aim.point);
            if (screenPos.x > 0 && screenPos.x < 1 && screenPos.y > 0 && screenPos.y < 1 && screenPos.z > 0)
            {
                onScreen = true;
                pointOnScreen = aim.point;
            }
            else
            {
                onScreen = false;
            }
        }
    }
}
