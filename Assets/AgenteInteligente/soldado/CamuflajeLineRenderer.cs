using UnityEngine;

public class CamuflajeLineRenderer : MonoBehaviour
{
    public int manchas = 10; // Cantidad de manchas de camuflaje
    public float radio = 1.0f; // Tamaño de cada mancha
    public Color colorMancha = Color.green;

    void Start()
    {
        dibujarManchas();
    }

    void dibujarManchas()
    {
        for (int i = 0; i < manchas; i++)
        {
            GameObject mancha = new GameObject("Mancha" + i);
            LineRenderer lineRenderer = mancha.AddComponent<LineRenderer>();
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.positionCount = 10;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = colorMancha;
            lineRenderer.endColor = colorMancha;
            lineRenderer.SetPosition(i, new Vector3(5*i,0.1f,5*i));

        }
    }
}