using UnityEngine;

public class DRSactivation : MonoBehaviour
{
    public Transform drsFlap;             // Parte móvel do DRS
    public Vector3 openRotation;          // Ângulo do DRS aberto
    public Vector3 closedRotation;        // Ângulo do DRS fechado
    public float openSpeed = 5f;          // Velocidade de animação
    public turbo[] drs;                   // Agora é um array de turbos

    void Update()
    {
        Quaternion targetRotation;

        bool algumPodeUsarDRS = false;

        foreach (var sistemaDRS in drs)
        {
            if (sistemaDRS != null && sistemaDRS.podeUsarDRS)
            {
                algumPodeUsarDRS = true;
                break;
            }
        }

        if (algumPodeUsarDRS && Input.GetKey(KeyCode.LeftShift))
        {
            // DRS activo e Shift pressionado: abre
            targetRotation = Quaternion.Euler(openRotation);
        }
        else
        {
            // Fora da zona DRS ou Shift não pressionado: fecha
            targetRotation = Quaternion.Euler(closedRotation);
        }

        drsFlap.localRotation = Quaternion.Lerp(drsFlap.localRotation, targetRotation, Time.deltaTime * openSpeed);
    }
}
