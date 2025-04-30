using UnityEngine;
using UnityEngine.UI;

public class CarSelection : MonoBehaviour
{
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    private int currentCar;

    private void Awake()
    {
        SelectCar(0);
    }

    private void SelectCar(int _index)
    {
        // Garante que o �ndice est� dentro dos limites
        currentCar = Mathf.Clamp(_index, 0, transform.childCount - 1);

        // ATEN��O: Bot�es agora est�o sempre interag�veis
        previousButton.interactable = true;
        nextButton.interactable = true;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == currentCar);
        }
    }

    public void ChangeCar(int _change)
    {
        currentCar += _change;

        // Cicla os carros (volta ao primeiro se passar do �ltimo e vice-versa)
        if (currentCar >= transform.childCount)
            currentCar = 0;
        else if (currentCar < 0)
            currentCar = transform.childCount - 1;

        SelectCar(currentCar);
    }
}
