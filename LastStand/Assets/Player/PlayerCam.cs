using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] Transform camHolder;

    [SerializeField] Transform PlayerOBJ;
    [SerializeField] float MouseSens;
    float xRotate;
    float yRotate;

    Camera _cam;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraFunctions();
    }
    void CameraFunctions() {   
        LinkCameraToPlayer();
        RotateCam();
        checkForInteractable();
    }
    void LinkCameraToPlayer() => transform.position = camHolder.position;
    void RotateCam() {
        xRotate += Input.GetAxisRaw("Mouse X");
        yRotate += Input.GetAxisRaw("Mouse Y");

        PlayerOBJ.transform.rotation = Quaternion.Euler(0, xRotate, 0);
        transform.rotation = Quaternion.Euler(-yRotate, xRotate, 0);
    }
    public bool checkForInteractable() {
        RaycastHit hit;
        if(Physics.Raycast(_cam.transform.position, _cam.transform.forward, out hit, 3f)) { 
            if(hit.transform.tag == "AmmoCrate") {
                Debug.Log("canInteract");
                return true;
            }
        }return false;
    }
}
