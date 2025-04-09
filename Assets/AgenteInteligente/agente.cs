using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class agente : MonoBehaviour
{
    public Tablero tablero;
    private Vector2Int posicionActual = new Vector2Int(0, 0);

    private List<Casilla> abierta = new List<Casilla>();
    private HashSet<Casilla> cerrada = new HashSet<Casilla>();
    private List<Casilla> camino = new List<Casilla>();
    
    public GameObject canva;

    void Start()
    {
        tablero = FindObjectOfType<Tablero>(); // Encuentra el tablero en la escena
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Ejecutar A* con la barra espaciadora
        {
            buscarCamino_Aestrella();
        }
        if (Input.GetKeyDown(KeyCode.R)) // Reiniciar escena con R
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void buscarCamino_Aestrella()
    {
        abierta.Clear();
        cerrada.Clear();

        Casilla inicio = tablero.casillas[posicionActual];
        Casilla objetivo = tablero.objetivoCasilla;

        abierta.Add(inicio);

        while (abierta.Count > 0)
        {
            Casilla actual = ObtenerMejorNodo();

            if (actual == objetivo)
            {
                ReconstruirCamino(actual);
                return;
            }

            abierta.Remove(actual);
            cerrada.Add(actual);

            foreach (Casilla vecino in ObtenerVecinos(actual))
            {
                if (cerrada.Contains(vecino) || !vecino.transitable)
                    continue;

                float nuevoCostoG = actual.costoG + Vector2Int.Distance(
                    new Vector2Int((int)actual.posicion.x, (int)actual.posicion.y), 
                    new Vector2Int((int)vecino.posicion.x, (int)vecino.posicion.y)
                );

                if (!abierta.Contains(vecino) || nuevoCostoG < vecino.costoG)
                {
                    vecino.distanciaManhattan(objetivo, nuevoCostoG);
                    vecino.padre = actual;

                    vecino.actualizarCosto();

                    if (!abierta.Contains(vecino))
                        abierta.Add(vecino);
                }
                vecino.dibujarTexto(canva,actual);
            }
            actual.dibujarTexto(canva,actual);
        }
    }

    Casilla ObtenerMejorNodo()
    {
        Casilla mejor = abierta[0];

        foreach (Casilla nodo in abierta)
        {
            if (nodo.costoF < mejor.costoF || (nodo.costoF == mejor.costoF && nodo.costoH < mejor.costoH))
                mejor = nodo;
        }
        return mejor;
    }

    private void ReconstruirCamino(Casilla nodo)
    {
        // private List<Casilla> camino = new List<Casilla>();
        camino.Clear();
        while (nodo != null)
        {
            camino.Add(nodo);
            nodo = nodo.padre;
        }
        camino.Reverse();
        StartCoroutine(MoverAgente());
    }

    private IEnumerator MoverAgente()
    {
        foreach (Casilla casilla in camino)
        {
            transform.position = casilla.transform.position;
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
            casilla.MarcarComoVisitada();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private List<Casilla> ObtenerVecinos(Casilla nodo)
    {
        List<Casilla> vecinos = new List<Casilla>();
        Vector2Int[] direcciones = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
    
        foreach (Vector2Int dir in direcciones)
        {
            Vector2Int posVecino = new Vector2Int(nodo.fila, nodo.columna) + dir;

            if (tablero.casillas.ContainsKey(posVecino) && tablero.casillas[posVecino].transitable)
            {
                vecinos.Add(tablero.casillas[posVecino]);
            }
        }
        return vecinos;
    }

}
