using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera camera1;              // 3ª pessoa
    public Camera camera2;              // 1ª pessoa
    public GameObject piloto;      // Modelo do piloto

    private Camera activeCamera;

    void Start()
    {
        // Define a câmara ativa inicial
        activeCamera = camera1;
        camera1.enabled = true;
        camera2.enabled = false;

        // Ativa o piloto na 3ª pessoa
        if (piloto != null)
            piloto.SetActive(true);
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
            // Vai para 1ª pessoa
            camera1.enabled = false;
            camera2.enabled = true;
            activeCamera = camera2;

            // Desativa o piloto
            if (piloto != null)
                piloto.SetActive(false);
        }
        else
        {
            // Volta para 3ª pessoa
            camera2.enabled = false;
            camera1.enabled = true;
            activeCamera = camera1;

            // Ativa o piloto
            if (piloto != null)
                piloto.SetActive(true);
        }
    }
}
