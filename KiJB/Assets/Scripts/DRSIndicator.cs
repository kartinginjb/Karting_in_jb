using UnityEngine;
using UnityEngine.UI;

public class DRSUI : MonoBehaviour
{
    public GameObject drsOff;
    public GameObject drsOn;
    public GameObject drsText; // Referência ao texto

    public turbo[] drsSources;

    void Update()
    {
        bool podeUsarDRS = false;

        foreach (var sistema in drsSources)
        {
            if (sistema != null && sistema.podeUsarDRS)
            {
                podeUsarDRS = true;
                break;
            }
        }

        if (podeUsarDRS)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                drsOff.SetActive(false);
                drsOn.SetActive(true);
            }
            else
            {
                drsOff.SetActive(true);
                drsOn.SetActive(false);
            }

            drsText.SetActive(true);
        }
        else
        {
            drsOff.SetActive(false);
            drsOn.SetActive(false);
            drsText.SetActive(false); // Esconde o texto
        }
    }
}