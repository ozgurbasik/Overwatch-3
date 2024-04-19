using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions Onfoot;
    private PlayerMotor motor;
    private Playerlook look;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        Onfoot = playerInput.OnFoot;
        motor= GetComponent<PlayerMotor>();
        Onfoot.Jump.performed += ctx => motor.Jump();
        look =GetComponent<Playerlook>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //tell the playermotor to move using the value from our movement action.
        motor.ProcessMove(Onfoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.ProcessLook(Onfoot.Look.ReadValue<Vector2>());
    }
    private void OnEnable()
    {
        Onfoot.Enable();
    }
    private void OnDisable()
    {
        Onfoot.Disable();  
    }
}
