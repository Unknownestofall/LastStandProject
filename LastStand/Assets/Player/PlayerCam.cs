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
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CameraFunctions();
    }
    void CameraFunctions() {   
        LinkCameraToPlayer();
        RotateCam();
    }
    void LinkCameraToPlayer() => transform.position = camHolder.position;

    void RotateCam() {
        xRotate += Input.GetAxisRaw("Mouse X");
        yRotate += Input.GetAxisRaw("Mouse Y");

        PlayerOBJ.transform.rotation = Quaternion.Euler(0, xRotate, 0);
        transform.rotation = Quaternion.Euler(-yRotate, xRotate, 0);
    }
}
