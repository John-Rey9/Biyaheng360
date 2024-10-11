using UnityEngine;
using System.Collections;
using System.Text;

public class LogitechSteeringWheel : MonoBehaviour
{

    LogitechGSDK.LogiControllerPropertiesData properties;

    public float xAxes, GasInput, ReverseInput, BrakeInput, ClutchInput;

    public bool HShift = true;

    public int CurrentGear;

    private string propertiesEdit;


    private void Start()
    {
        LogitechGSDK.LogiSteeringInitialize(false);

        // Check if force feedback is supported
        if (LogitechGSDK.LogiHasForceFeedback(0)) // Assuming controller index 0
        {
            // Enable force feedback
            LogitechGSDK.LogiPlaySpringForce(0, 0, 80, 90); // Example spring force with half intensity and stiffness
        }
        else
        {
            Debug.LogWarning("Force feedback is not supported on this controller.");
        }
    }

    void Update()
    {
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {
            LogitechGSDK.DIJOYSTATE2ENGINES rec;
            rec = LogitechGSDK.LogiGetStateUnity(0);

            HShifter(rec);

            xAxes = rec.lX / 32768f;

            if (rec.lY > 0)
            {
                GasInput = 0;
            }
            else if (rec.lY < 0)
            {
                GasInput = rec.lY / -32768f;
            }

            if (rec.lY > 0)
            {
                ReverseInput = 0;
            }
            else if (rec.lY < 0)
            {
                ReverseInput = -rec.lY / 32768f;
            }

            if (rec.lRz > 0)
            {
                BrakeInput = 0;

            }
            else if (rec.lRz < 0)
            {
                BrakeInput = rec.lRz / -32768f;
            }

            if (rec.rglSlider[0] > 0)
            {
                ClutchInput = 0;
            }
            else if (rec.rglSlider[0] < 0)
            {
                ClutchInput = rec.rglSlider[0] / -32768f;
            }

        }
        else
        {
            print("No Steering Wheel connected!");
        }
    }

    void HShifter(LogitechGSDK.DIJOYSTATE2ENGINES shifter)
    {
    // Check for the right shifter (Button 4)
        if (shifter.rgbButtons[4] == 128)
        {
            CurrentGear = 1;
        }

        if (shifter.rgbButtons[5] == 128)
        { 
            CurrentGear = -1;
        }

        if (shifter.rgbButtons[23] == 128)
        {
            CurrentGear = 0;
        }

    }

}