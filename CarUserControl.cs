using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Vehicles.Car;

[RequireComponent(typeof(CarController))]
public class CarUserControl : MonoBehaviour
{
    private CarController m_Car; // the car controller we want to use
    private LogitechSteeringWheel logitechSteeringWheel; // Reference to the Logitech Steering Wheel script

    private void Awake()
    {
        // Get the car controller
        m_Car = GetComponent<CarController>();

        // Get reference to the Logitech Steering Wheel script
        logitechSteeringWheel = GetComponent<LogitechSteeringWheel>();
    }

    private void FixedUpdate()
    {
        // Get input from the Logitech Steering Wheel
        float h = logitechSteeringWheel.xAxes;
        float handbrake = 0f; // No handbrake input from the steering wheel

        // Get input from the accelerator and brake pedals
        float gasInput = logitechSteeringWheel.GasInput;
        float brakeInput = logitechSteeringWheel.BrakeInput;
        float reverseInput = logitechSteeringWheel.ReverseInput;

        // Determine the direction of movement based on gas and brake input

        int currentGear = logitechSteeringWheel.CurrentGear;
        float v = 0f;

        // Move the car only if the gas pedal is pressed
        if (logitechSteeringWheel.CurrentGear != 0 && gasInput > 0)
        {
            if (logitechSteeringWheel.CurrentGear == -1)
            {
                // Car is in reverse gear, make it move backward
                v = reverseInput;
            }
            else
            {
                // Car is in forward gear, make it move forward
                v = gasInput;
            }
        }
        else if (logitechSteeringWheel.CurrentGear == -1)
        {
            // Car is in reverse gear, apply brake input to stop it from moving backward
            v = -brakeInput;
        }
        else if (brakeInput > 0)
        {
            // Car is in forward gear, apply brake input as usual
            v = -1f;
        }
            // Move the car
        m_Car.Move(h, v, v, handbrake);
    }
}