using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    private Camera activeCamera;

    void Start()
    {
        // Define a câmara ativa inicial
        activeCamera = camera1;
        camera1.enabled = true;
        camera2.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        if (activeCamera == camera1)
        {
            camera1.enabled = false;
            camera2.enabled = true;
            activeCamera = camera2;
        }
        else 
        {
            camera2.enabled = false;
            camera1.enabled = true;
            activeCamera = camera1;
        }
    }
}
