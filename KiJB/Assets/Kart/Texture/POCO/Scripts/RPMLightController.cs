using UnityEngine;      

public class RPMLightController : MonoBehaviour
{
    public Light rpmLight; // Assign the light in the Inspector
    private float rpm; // Variable to hold the RPM value
    public float activationRPM = 5000f; // RPM threshold to activate the light
    private bool isLightActivated = false; // Flag to avoid unnecessary light toggling

    void Update()
    {
        // Check if the RPM exceeds the activation threshold
        if (rpm >= activationRPM && !isLightActivated)
        {
            rpmLight.enabled = true; // Turn on the light
            isLightActivated = true; // Set the flag to true
        }
        else if (rpm < activationRPM && isLightActivated)
        {
            rpmLight.enabled = false; // Turn off the light
            isLightActivated = false; // Reset the flag
        }
    }

    // Method to update the RPM value
    public void UpdateRPM(float newRPM)
    {
        rpm = newRPM; // Update the RPM variable
    }
}
