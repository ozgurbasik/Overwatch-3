using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerlook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProcessLook(Vector2 input) {
        float MouseX =input.x;
        float MouseY =input.y;
        //calculate camera rotation for lookin up and down
        xRotation -= (MouseY*Time.deltaTime)*ySensitivity;
        xRotation=Mathf.Clamp(xRotation, -80f, 80f);
        //apply this to our camera transform.
        cam.transform.localRotation=Quaternion.Euler(xRotation,0,0);
        //rotate player to look left and right 
        transform.transform.Rotate(Vector3.up*( MouseX*Time.deltaTime)*xSensitivity);


    }
}
