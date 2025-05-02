using UnityEngine;

public class TrocarParaCamera3 : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    public Camera camera3;

    private Camera cameraAnterior;

    void Start()
    {
        // Garante que só uma câmara está ativa no início (ex: camera1)
        camera1.enabled = true;
        camera2.enabled = false;
        camera3.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            // Guarda a câmara que estava ativa antes de mudar
            if (camera1.enabled)
                cameraAnterior = camera1;
            else if (camera2.enabled)
                cameraAnterior = camera2;

            // Ativa a câmara 3
            camera1.enabled = false;
            camera2.enabled = false;
            camera3.enabled = true;
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            // Volta à câmara anterior
            camera1.enabled = false;
            camera2.enabled = false;
            camera3.enabled = false;

            if (cameraAnterior != null)
                cameraAnterior.enabled = true;
        }
    }
}
