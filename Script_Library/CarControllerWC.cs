using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerWC : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    private float horizontalInput, verticalInput, currentSteeringAngle, currentBrakingForce;
    private bool isBraking = false;
    private Rigidbody rb;

    [Header("Car Parameters")]
    [SerializeField] private float motorForce;
    [SerializeField] private float brakeForce;
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private Transform centerOfMess;

    [Header("Wheel Colliders")]
    [SerializeField] WheelCollider frontLeftWC;
    [SerializeField] WheelCollider frontRightWC;
    [SerializeField] WheelCollider rearLeftWC;
    [SerializeField] WheelCollider rearRightWC;

    [Header("Wheel Mesh Transforms")]
    [SerializeField] Transform frontLeftTransform;
    [SerializeField] Transform frontRightTransform;
    [SerializeField] Transform rearLeftTransform;
    [SerializeField] Transform rearRightTransform;

    private void Start()
    {
        rb.GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMess.localPosition ;
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }
    private void GetInput()
    {
        horizontalInput = Input.GetAxis(Horizontal);
        verticalInput = Input.GetAxis(Vertical);

        isBraking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        frontLeftWC.motorTorque = verticalInput * motorForce;
        frontRightWC.motorTorque = verticalInput * motorForce;

        currentBrakingForce = isBraking ? brakeForce : 0f;

        ApplyBraking();
    }

    private void ApplyBraking()
    {
        frontLeftWC.brakeTorque = currentBrakingForce;
        frontRightWC.brakeTorque = currentBrakingForce;
        rearLeftWC.brakeTorque = currentBrakingForce;
        rearRightWC.brakeTorque = currentBrakingForce;
    }

    private void HandleSteering()
    {
        currentSteeringAngle = maxSteerAngle * horizontalInput;
        Debug.Log(currentSteeringAngle);
        frontLeftWC.steerAngle = currentSteeringAngle;
        frontRightWC.steerAngle = currentSteeringAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWC, frontLeftTransform);
        UpdateSingleWheel(frontRightWC, frontRightTransform);
        UpdateSingleWheel(rearLeftWC, rearLeftTransform);
        UpdateSingleWheel(rearRightWC, rearRightTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 position;
        Quaternion rotation;

        wheelCollider.GetWorldPose(out position, out rotation);
        wheelTransform.rotation = rotation;
        wheelTransform.position = position;
    }
}
