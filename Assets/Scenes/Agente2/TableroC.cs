using UnityEngine;
using System.Collections.Generic;

public class TableroC : MonoBehaviour
{
    public GameObject agente;
    public GameObject casillaPrefab;
    public CasillaC objetivoCasilla;
    public Dictionary<Vector2Int, CasillaC> casillas = new Dictionary<Vector2Int, CasillaC>();

    [SerializeField] private int filas = 4;
    [SerializeField] private int columnas = 4;
    [SerializeField] private float espacio = 1f;

    void Start()
    {
        GenerarTablero();
        ColocarAgenteYObjetivo();
    }

    void GenerarTablero()
    {
        float xStart = -((columnas - 1) * espacio) / 2;
        float yStart = ((filas - 1) * espacio) / 2;

        for (int i = 0; i < filas; i++)
        {
            for (int j = 0; j < columnas; j++)
            {
                Vector2 posicion = new Vector2(xStart + j * espacio, yStart - i * espacio);
                GameObject casillaObj = Instantiate(casillaPrefab, posicion, Quaternion.identity, transform);
                casillaObj.name = $"Casilla ({j}, {i})";

                CasillaC casilla = casillaObj.GetComponent<CasillaC>();
                casilla.fila = j;
                casilla.columna = i;
                casillas[new Vector2Int(j, i)] = casilla;
            }
        }
    }

    void ColocarAgenteYObjetivo()
    {
        Vector2Int inicio = new Vector2Int(0, 0);
        Vector2Int destino = new Vector2Int(Random.Range(0, columnas), Random.Range(0, filas));

        if (casillas.ContainsKey(inicio))
            agente.transform.position = casillas[inicio].transform.position;

        if (casillas.ContainsKey(destino))
        {
            objetivoCasilla = casillas[destino];
            objetivoCasilla.MarcarComoObjetivo();
        }
    }
}