using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Casilla : MonoBehaviour
{
    public int fila, columna;
    public float costoG, costoH, costoF;
    public bool transitable = true;
    public bool esObjetivo = false; // Nueva variable para marcar el objetivo
    public Casilla padre;
    public Vector3 posicion; // Cambio de Vector2 a Vector2Int
    public Text texto;

    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        texto = GetComponentInChildren<Text>();
    }

    public void dibujarTexto(GameObject canva, Casilla casilla)
    {
        GameObject texObj = new GameObject("costo");
        texObj.transform.SetParent(canva.transform);
        
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(casilla.posicion);
        texObj.transform.position = screenPosition;
        Debug.Log("x:"+casilla.posicion.x +"\n y:"+ casilla.posicion.y +"\n z:"+ casilla.posicion.z);
        
        Text texto1 = texObj.AddComponent<Text>();
        texto1.alignment = TextAnchor.MiddleCenter;
        texto1.text = "g:" + this.costoG + ", h:" + this.costoH + ", f:" + this.costoF;
        texto1.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        texto1.fontSize = 10;
        texto1.color = Color.black;
    }


    public void actualizarCosto()
    {
        if (texto != null)
        {
            texto.text = "g:" + costoG + "\n h:" + costoH + "\n f:" + costoF;
        }
    }

    public void confCasilla(int x, int y, Vector3 pos)
    {
        fila = x;
        columna = y;
        posicion = pos; // Guardamos la posición de la casilla
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtiene el SpriteRenderer
    }

    public void distanciaManhattan(Casilla objetivo, float costoGpadre)
    {
        costoG = costoGpadre + 1;
        costoH = Mathf.Abs(objetivo.fila - fila) + Mathf.Abs(objetivo.columna - columna);
        costoF = costoH + costoG;
    }

    public void MarcarComoObjetivo()
    {
        esObjetivo = true;
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.yellow; // Cambia el color de la casilla objetivo a amarillo
        }
    }
    
    public void MarcarComoVisitada()
    {
        GetComponent<SpriteRenderer>().color = Color.green; // Cambia el color de la casilla
    }

}