using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private float deltaTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        int largura = Screen.width, altura = Screen.height;

        GUIStyle estilo = new GUIStyle();

        Rect rect = new Rect(10, 10, largura, altura * 2 / 100);
        estilo.alignment = TextAnchor.UpperLeft;
        estilo.fontSize = altura * 2 / 50;
        estilo.normal.textColor = Color.white;

        float fps = 1.0f / deltaTime;
        string texto = string.Format("FPS: {0:0.}", fps);
        GUI.Label(rect, texto, estilo);
    }
}
