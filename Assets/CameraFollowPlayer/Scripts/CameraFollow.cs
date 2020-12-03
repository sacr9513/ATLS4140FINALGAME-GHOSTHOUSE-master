using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    private Camera myCamera;
    private Func<Vector3> GetCameraFollowPositionFunc;
    private Func<float> GetCameraZoomFunc;

    //Functions below set camera target meaning we can have the camera follow a different target if we want
    //GetCameraFollowPositionFunc is basically the player object/position (for now...)
    
    public void Setup(Func<Vector3> GetCameraFollowPositionFunc, Func<float> GetCameraZoomFunc) {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
        this.GetCameraZoomFunc = GetCameraZoomFunc;
    }
    private void Start() {
        myCamera = transform.GetComponent<Camera>();
    }
    public void SetCameraFollowPosition(Vector3 cameraFollowPosition) {
        SetGetCameraFollowPositionFunc(() => cameraFollowPosition);
    }
    public void SetGetCameraFollowPositionFunc(Func<Vector3> GetCameraFollowPositionFunc) {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
    }
    public void SetCameraZoom(float cameraZoom) {
        SetGetCameraZoomFunc(() => cameraZoom);
    }
    public void SetGetCameraZoomFunc(Func<float> GetCameraZoomFunc) {
        this.GetCameraZoomFunc = GetCameraZoomFunc;
    }

    // Update is called once per frame

    void Update() {
        HandleMovement();
        HandleZoom();
    }

    private void HandleMovement() {
        Vector3 cameraFollowPosition = GetCameraFollowPositionFunc();
        transform.position = cameraFollowPosition + new Vector3(0,1,-2); //camera offset


        //BELOW: code for smoother camera model (still buggy) but will slowly trail behind player object
        //          meaning player object won't always be in the center of the screen



        //cameraFollowPosition.z = transform.position.z;
        //Vector3 cameraMoveDir = (cameraFollowPosition - transform.position).normalized;
        //float distance = Vector3.Distance(cameraFollowPosition, transform.position);
        //float cameraMoveSpeed = 2f;
        //if (distance > 0.1f) {
        //    Vector3 newCameraPosition = transform.position + cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;
        //    float distanceAfterMoving = Vector3.Distance(newCameraPosition, cameraFollowPosition);
        //    if (distanceAfterMoving > distance) {
        //        // Overshot the target
        //        newCameraPosition = cameraFollowPosition;
        //    }
        //    transform.position = newCameraPosition;
        //}
    }
    private void HandleZoom() {
        float cameraZoom = GetCameraZoomFunc();
        float cameraZoomDifference = cameraZoom - myCamera.orthographicSize;
        float cameraZoomSpeed = 1f;
        myCamera.orthographicSize += cameraZoomDifference * cameraZoomSpeed * Time.deltaTime;
        if (cameraZoomDifference > 0) {
            if (myCamera.orthographicSize > cameraZoom) {
                myCamera.orthographicSize = cameraZoom;
            }
        } else {
            if (myCamera.orthographicSize < cameraZoom) {
                myCamera.orthographicSize = cameraZoom;
            }
        }
    }
}
