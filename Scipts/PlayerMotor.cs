using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{   private CharacterController characterController;
    private Vector3 velocity;
    public float speed = 5f;
    private bool isGrounded;
    public float gravity = -9.8f;
    public float height =3f;
    // Start is called before the first frame update
    void Start()
    {
        characterController= GetComponent<CharacterController>();   
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = characterController.isGrounded;   
    }

    //receive inputs from InputManager and apply them to CharacterController
    public void ProcessMove(Vector2 input)
    {
        Vector3 movedirection = Vector3.zero;
        movedirection.x = input.x;  
        movedirection.z = input.y;
        characterController.Move(transform.TransformDirection(movedirection)*speed*Time.deltaTime);
        velocity.y += gravity*Time.deltaTime;
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        characterController.Move(velocity*Time.deltaTime);
        Debug.Log(velocity.y);
    }   
    public void Jump()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(height * -3.0f * gravity);
        }
    }
}
