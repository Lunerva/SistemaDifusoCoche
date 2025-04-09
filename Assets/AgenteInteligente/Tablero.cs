using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tablero : MonoBehaviour
{

	public GameObject agente;
	// public GameObject objetivo;
	
	public GameObject casilla;
	public Casilla objetivoCasilla; // Nuevo campo para almacenar la casilla objetivo

	private int fil = 4;
	private int col = 4;
	private float espacio = 1f;

	private float x = -2f;
	private float y = 1f;

	public Dictionary<Vector2Int, Casilla> casillas = new Dictionary<Vector2Int, Casilla>();


	void Start()
	{
		GenerarTablero();
		ColocarAgente();
	}

	private void GenerarTablero()
	{
		float xStart = -((col - 1) * espacio) / 2;
		float yStart = ((fil - 1) * espacio) / 2;

		for (int i = 0; i < fil; i++)
		{
			for (int j = 0; j < col; j++)
			{
				Vector2 posicion = new Vector2(xStart + j * espacio, yStart - i * espacio);
				GameObject obj = Instantiate(casilla, posicion, Quaternion.identity);
				obj.name = "Casilla:" + j + "," + i;
				obj.transform.parent = transform;

				Casilla casillaScript = obj.GetComponent<Casilla>();
				if (casillaScript != null)
				{
					casillaScript.confCasilla(j, i, posicion); // Guardamos la posición
					casillas[new Vector2Int(j, i)] = casillaScript;
				}
			}
		}
	}

	
	private void ColocarAgente()
	{
		Vector2Int inicio = new Vector2Int(0, 0);
		Vector2Int destino = new Vector2Int(Random.Range(0,col), Random.Range(0,fil)); 
		if (casillas.ContainsKey(inicio))
		{
			agente.transform.position = casillas[inicio].transform.position;
			// agente.transform.position = new Vector3(agente.transform.position.x, agente.transform.position.y, -1);
		}

		if (casillas.ContainsKey(destino))
		{
			objetivoCasilla = casillas[destino]; // Asignamos la casilla objetivo
			objetivoCasilla.MarcarComoObjetivo();

		}
	}


}
