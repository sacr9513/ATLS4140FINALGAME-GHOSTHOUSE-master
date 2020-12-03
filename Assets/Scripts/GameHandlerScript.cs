using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandlerScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform playerTransform;
    public CameraFollow cameraFollow;

    [SerializeField] private float zoom = 30f; // sets standard zoom at start
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
        cameraFollow.Setup(() => playerTransform.position, () => zoom); // sets camera to follow player object
        
    }

    // Update is called once per frame
    private void Update()
    {
        HandleZoomButtons();
    }

    private void HandleZoomButtons(){
        if (Input.GetKeyDown(KeyCode.Z)) {
                ZoomOut();
            }
            if (Input.GetKeyDown(KeyCode.X)) {
                ZoomIn();
            }
    }

    private void ZoomIn() {
        zoom -= 10f;
        if (zoom < 10f) zoom = 10f;
    }
    private void ZoomOut() {
        zoom += 10f;
        if (zoom > 60f) zoom = 60f;
    }
}
