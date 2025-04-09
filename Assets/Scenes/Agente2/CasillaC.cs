using UnityEngine;
using UnityEngine.UI;

public class CasillaC : MonoBehaviour
{
    public int fila, columna;
    public float costoG, costoH, costoF;
    public bool transitable = true;
    public bool esObjetivo;
    public CasillaC padre;
    public Text texto;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        texto = GetComponentInChildren<Text>();
        actualizarCosto();
    }

    public void dibujarTexto(GameObject canvas)
    {
        if (texto != null)
        {
            texto.text = $"G: {costoG}\nH: {costoH}\nF: {costoF}";
            texto.transform.SetParent(canvas.transform, false);
            texto.rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(transform.position);
        }
    }

    public void actualizarCosto() => texto.text = $"G: {costoG}\nH: {costoH}\nF: {costoF}";

    public void distanciaManhattan(CasillaC objetivo, float costoGpadre)
    {
        costoG = costoGpadre + 1;
        costoH = Mathf.Abs(objetivo.fila - fila) + Mathf.Abs(objetivo.columna - columna);
        costoF = costoG + costoH;
    }

    public void MarcarComoObjetivo()
    {
        esObjetivo = true;
        spriteRenderer.color = Color.yellow;
    }

    public void MarcarComoVisitada() => spriteRenderer.color = Color.green;
}