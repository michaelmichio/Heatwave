using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInput : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;

    private const float defaultCameraDistance = 7;
    private const float defaultCameraRotate = 60;
    private const float defaultMinZoom = -2;
    private const float defaultMaxZoom = -12;
    
    private Vector3 defaultPosition;
    private Vector3 previousPosition;

    private const float DOUBLE_CLICK_TIME = .4f;
    private float lastClickTime;

    private const float MIN_UP_ROTATION = 0;
    private const float MAX_DOWN_ROTATION = 0;
    private float zoom = 0;
    
    void Start() {
        cam.transform.position = target.position;
        cam.transform.Rotate(new Vector3(1, 0, 0), defaultCameraRotate);
        cam.transform.Translate(new Vector3(0, 0, -defaultCameraDistance));
        previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
    }
    
    void Update() {

        // Left Click
        if (Input.GetMouseButtonDown(0)) {
        }

        // Right Click
        else if (Input.GetMouseButtonDown(1)) {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);

            // double right click
            float timeSinceLastClick = Time.time - lastClickTime;
            if (timeSinceLastClick <= DOUBLE_CLICK_TIME) {
            }
            lastClickTime = Time.time;
        }

        //
        else if (Input.GetMouseButton(1)) {
            Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;
            
            // float rotationAroundXAxis = direction.y * 180; // camera moves vertically
            float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
            
            cam.transform.position = target.position;
            
            // cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
            cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World); // <— This is what makes it work!
            
            cam.transform.Translate(new Vector3(0, 0, -defaultCameraDistance + zoom));
            
            previousPosition = newPosition;
        }

        // Scroll Down
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f ) {
            
            // Rotate Down
            if (Input.GetKey(KeyCode.LeftShift)) {
                if (cam.transform.localEulerAngles.x - 5 > 30 - 0.1f) {
                    cam.transform.position = target.position;
                    cam.transform.Rotate(new Vector3(1, 0, 0), -5);
                    cam.transform.Translate(new Vector3(0, 0, -defaultCameraDistance + zoom));
                }
            }
            // Zoom out
            else if (-defaultCameraDistance + zoom > defaultMaxZoom) {
                zoom = zoom - 0.25f;
                cam.transform.Translate(new Vector3(0, 0, -0.25f));
            }

        }

        // Scroll Up
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f ) {
            
            // Rotate Up
            if (Input.GetKey(KeyCode.LeftShift)) {
                if (cam.transform.localEulerAngles.x + 5 < 75 + 0.1f) {
                    cam.transform.position = target.position;
                    cam.transform.Rotate(new Vector3(1, 0, 0), 5);
                    cam.transform.Translate(new Vector3(0, 0, -defaultCameraDistance + zoom));
                }
            }
            // Zoom in
            else if (-defaultCameraDistance + zoom < defaultMinZoom) {
                zoom = zoom + 0.25f;
                cam.transform.Translate(new Vector3(0, 0, 0.25f));
            }

        }

    }
}