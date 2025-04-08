using System.Collections.Generic;
using UnityEngine;

public class MoveAndSwitchObjects : MonoBehaviour
{
    public List<GameObject> objects; // Lista de objetos
    public float moveSpeed = 2f; // Velocidade de movimento
    public float pontoA = -10f; // Ponto A no eixo X
    public float pontoB = 10f; // Ponto B no eixo X

    private int currentIndex = 0; // Índice do objeto atual
    private bool movingToB = true; // Direção do movimento (true = indo para B, false = indo para A)

    void Start()
    {
        // Certifique-se de que apenas o primeiro objeto está ativo no início
        for (int i = 1; i < objects.Count; i++)
        {
            objects[i].SetActive(false);
        }

        // Posiciona o primeiro objeto no ponto A
        objects[currentIndex].transform.position = new Vector3(pontoA, objects[currentIndex].transform.position.y, objects[currentIndex].transform.position.z);
    }

    void Update()
    {
        // Movimenta o objeto atual
        MoveObject();

        // Verifica se o objeto atingiu um dos pontos (A ou B)
        if (movingToB && objects[currentIndex].transform.position.x >= pontoB)
        {
            SwitchToNextObject();
        }
        else if (!movingToB && objects[currentIndex].transform.position.x <= pontoA)
        {
            SwitchToNextObject();
        }
    }

    void MoveObject()
    {
        // Calcula a direção do movimento
        float direction = movingToB ? 1 : -1;

        // Move o objeto atual apenas no eixo X
        Vector3 newPosition = objects[currentIndex].transform.position;
        newPosition.x += direction * moveSpeed * Time.deltaTime;
        objects[currentIndex].transform.position = newPosition;
    }

    void SwitchToNextObject()
    {
        // Desativa o objeto atual
        objects[currentIndex].SetActive(false);

        // Avança para o próximo objeto na lista
        currentIndex = (currentIndex + 1) % objects.Count;

        // Ativa o próximo objeto
        objects[currentIndex].SetActive(true);

        // Reposiciona o novo objeto no ponto A
        objects[currentIndex].transform.position = new Vector3(pontoA, objects[currentIndex].transform.position.y, objects[currentIndex].transform.position.z);

        // Reinicia o movimento para o ponto B
        movingToB = true;
    }
}