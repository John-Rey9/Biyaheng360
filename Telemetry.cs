using UnityEngine;

[ExecuteInEditMode]
public class Telemetry : MonoBehaviour
{

    string apiMode = "api";  //constant to identify the package
    public string game = "Biyaheng360";  //constant to identify the game
    public string vehicle = "AutomaticCar";  //constant to identify the vehicle
    public string location = "BatangasCity";  //constant to identify the location
    uint apiVersion = 102;  //constant of the current version of the api

    //gets the vehicle body to send speed to SRS
	Rigidbody vehicleBody;

    void Start ()
    {
        vehicleBody = GetComponent<Rigidbody> ();
    }

    // Update is called once per frame
    void Update()
    {
        // Gather realistic telemetry data
        float speedKPH = vehicleBody.velocity.magnitude * 3.6f; // Convert m/s to km/h
        float rpm = Mathf.Clamp(speedKPH * 10f, 0f, 8000f); // RPM increases with speed (just a placeholder)
        int gear = CalculateGear(speedKPH);
        float pitch = transform.eulerAngles.x;
        float roll = transform.eulerAngles.z;
        float yaw = transform.rotation.eulerAngles.y; // Yaw rotation around the vertical axis
        float lateralVelocity = Mathf.Clamp(vehicleBody.angularVelocity.y * 10f, -10f, 10f); // Placeholder for lateral velocity
        float lateralAcceleration = Mathf.Clamp((vehicleBody.angularVelocity.y - vehicleBody.angularVelocity.y) / Time.deltaTime * 100f, -100f, 100f); // Placeholder for lateral acceleration
        float verticalAcceleration = Mathf.Clamp((vehicleBody.velocity.y - Physics.gravity.y) * 10f, -10f, 10f); // Placeholder for vertical acceleration
        float longitudinalAcceleration = Mathf.Clamp((vehicleBody.velocity.z - vehicleBody.velocity.z) / Time.deltaTime * 10f, -10f, 10f); // Placeholder for longitudinal acceleration
        float suspensionTravel = Mathf.Clamp(Random.Range(-0.1f, 0.1f), -10f, 10f); // Placeholder for suspension travel
        uint wheelTerrainType = 2; // Assume all wheels are on asphalt

        // Send telemetry data
        SimRacingStudio.SimRacingStudio_SendTelemetry(
            apiMode.PadRight(3).ToCharArray(),
            apiVersion,
            game.PadRight(50).ToCharArray(),
            vehicle.PadRight(50).ToCharArray(),
            location.PadRight(50).ToCharArray(),
            speedKPH,
            rpm,
            8000f, // Placeholder for maximum RPM
            gear,
            pitch,
            roll,
            yaw,
            lateralVelocity,
            lateralAcceleration,
            verticalAcceleration,
            longitudinalAcceleration,
            suspensionTravel,
            suspensionTravel,
            suspensionTravel,
            suspensionTravel,
            wheelTerrainType,
            wheelTerrainType,
            wheelTerrainType,
            wheelTerrainType
        );
    }

    int CalculateGear(float speedKPH)
    {
        // Placeholder function to calculate gear based on speed
        if (speedKPH < 10f)
            return 1;
        else if (speedKPH < 20f)
            return 2;
        else if (speedKPH < 30f)
            return 3;
        else if (speedKPH < 40f)
            return 4;
        else if (speedKPH < 50f)
            return 5;
        else
            return 6;
    }
}
