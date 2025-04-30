using UnityEngine;

public class DRSactivation : MonoBehaviour
{
    public Transform drsFlap;             // Parte móvel do DRS
    public Vector3 openRotation;          // Ângulo do DRS aberto
    public Vector3 closedRotation;        // Ângulo do DRS fechado
    public float openSpeed = 5f;          // Velocidade de animação
    public turbo drs;                     // Referência ao script turbo

    void Update()
    {
        Quaternion targetRotation;

        if (drs != null && drs.podeUsarDRS && Input.GetKey(KeyCode.LeftShift))
        {
            // DRS ativo e Shift pressionado  abre
            targetRotation = Quaternion.Euler(openRotation);
        }
        else
        {
            // Fora da zona DRS ou Shift não pressionado  fecha
            targetRotation = Quaternion.Euler(closedRotation);
        }

        drsFlap.localRotation = Quaternion.Lerp(drsFlap.localRotation, targetRotation, Time.deltaTime * openSpeed);
    }
}
