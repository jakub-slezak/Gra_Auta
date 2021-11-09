using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController1 : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float momentsteeringAngle;
    private float momentalbrakeforce;
    private bool isBraking;
    private bool isEBrake;
    private float predkoscObort;

    [SerializeField] private float rpms;
    [SerializeField] private bool ham;
    [SerializeField] private float test;

    [SerializeField] private float motorforce;
    [SerializeField] private float breakforce;
    [SerializeField] private float MaxSteeringAngle;

    [SerializeField] private WheelCollider FrontLeftWheelCollider;
    [SerializeField] private WheelCollider FrontRightWheelCollider;
    [SerializeField] private WheelCollider RearLeftWheelCollider;
    [SerializeField] private WheelCollider RearRightWheelCollider;

    [SerializeField] private Transform FrontLeftWheelTransform;
    [SerializeField] private Transform FrontRightWheelTransform;
    [SerializeField] private Transform RearLeftWheelTransform;
    [SerializeField] private Transform RearRightWheelTransform;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();


    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBraking = Input.GetKey(KeyCode.DownArrow);
        isEBrake = Input.GetKey(KeyCode.Space);
    }


    private void HandleMotor()
    {
        ham = isBraking;


        if (verticalInput != 0 && predkoscObort < 2000)
        {
                FrontLeftWheelCollider.motorTorque = verticalInput * motorforce;
                FrontRightWheelCollider.motorTorque = verticalInput * motorforce;
                RearLeftWheelCollider.motorTorque = verticalInput * motorforce * 1.1f;
                RearRightWheelCollider.motorTorque = verticalInput * motorforce * 1.1f;
            test = motorforce;
       }
        rpms = FrontLeftWheelCollider.rpm;
        //if (FrontLeftWheelCollider.rpm >= 0 && isBraking == true)
        // {
        momentalbrakeforce = isBraking ? breakforce : 0f;
            Applybrake();
            isBraking = false;

       // }

       // if (isEBrake == true)
       // {
            momentalbrakeforce = isEBrake ? breakforce : 0f;
            ApplyEBrake();
        //}           
        
    }

    private void Applybrake()
    {
        FrontLeftWheelCollider.brakeTorque = momentalbrakeforce;
        FrontRightWheelCollider.brakeTorque = momentalbrakeforce;
        RearLeftWheelCollider.brakeTorque = momentalbrakeforce;
        RearRightWheelCollider.brakeTorque = momentalbrakeforce;
    }
    private void ApplyEBrake()
    {
        FrontLeftWheelCollider.brakeTorque = momentalbrakeforce * 0.5f;
        FrontRightWheelCollider.brakeTorque = momentalbrakeforce * 0.5f;
        RearLeftWheelCollider.brakeTorque = momentalbrakeforce * 2.5f;
        RearRightWheelCollider.brakeTorque = momentalbrakeforce * 2.5f;
    }

    private void HandleSteering()
    {
        momentsteeringAngle = MaxSteeringAngle * horizontalInput;
        FrontLeftWheelCollider.steerAngle = momentsteeringAngle;
        FrontRightWheelCollider.steerAngle = momentsteeringAngle;
    }
    private void UpdateWheels()
    {
        UpdateSingleWheel(FrontLeftWheelCollider, FrontLeftWheelTransform);
        UpdateSingleWheel(FrontRightWheelCollider, FrontRightWheelTransform);
        UpdateSingleWheel(RearLeftWheelCollider, RearLeftWheelTransform);
        UpdateSingleWheel(RearRightWheelCollider, RearRightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
