using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AgentC : MonoBehaviour
{
    public TableroC tablero;
    private Vector2Int posicionActual = new Vector2Int(0, 0);
    private List<CasillaC> abierta = new List<CasillaC>();
    private HashSet<CasillaC> cerrada = new HashSet<CasillaC>();
    private List<CasillaC> camino = new List<CasillaC>();
    public GameObject canvas;

    void Start() => tablero = FindObjectOfType<TableroC>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) buscarCamino_Aestrella();
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void buscarCamino_Aestrella()
    {
        abierta.Clear();
        cerrada.Clear();
        CasillaC inicio = tablero.casillas[posicionActual];
        CasillaC objetivo = tablero.objetivoCasilla;
        abierta.Add(inicio);

        while (abierta.Count > 0)
        {
            CasillaC actual = ObtenerMejorNodo();
            if (actual == objetivo)
            {
                ReconstruirCamino(actual);
                return;
            }
            abierta.Remove(actual);
            cerrada.Add(actual);

            foreach (CasillaC vecino in ObtenerVecinos(actual))
            {
                if (cerrada.Contains(vecino) || !vecino.transitable) continue;

                float nuevoCostoG = actual.costoG + Vector2Int.Distance(
                    new Vector2Int(actual.fila, actual.columna),
                    new Vector2Int(vecino.fila, vecino.columna)
                );

                if (!abierta.Contains(vecino) || nuevoCostoG < vecino.costoG)
                {
                    vecino.distanciaManhattan(objetivo, nuevoCostoG);
                    vecino.padre = actual;
                    vecino.actualizarCosto();
                    if (!abierta.Contains(vecino)) abierta.Add(vecino);
                }
                vecino.dibujarTexto(canvas);
            }
        }
    }

    CasillaC ObtenerMejorNodo()
    {
        CasillaC mejor = abierta[0];
        foreach (CasillaC nodo in abierta)
            if (nodo.costoF < mejor.costoF || (nodo.costoF == mejor.costoF && nodo.costoH < mejor.costoH))
                mejor = nodo;
        return mejor;
    }

    void ReconstruirCamino(CasillaC nodo)
    {
        camino.Clear();
        while (nodo != null)
        {
            camino.Add(nodo);
            nodo = nodo.padre;
        }
        camino.Reverse();
        StartCoroutine(MoverAgente());
    }

    IEnumerator MoverAgente()
    {
        foreach (CasillaC casilla in camino)
        {
            transform.position = new Vector3(casilla.transform.position.x, casilla.transform.position.y, -1);
            casilla.MarcarComoVisitada();
            yield return new WaitForSeconds(0.5f);
        }
    }

    List<CasillaC> ObtenerVecinos(CasillaC nodo)
    {
        List<CasillaC> vecinos = new List<CasillaC>();
        Vector2Int[] direcciones = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        foreach (Vector2Int dir in direcciones)
        {
            Vector2Int posVecino = new Vector2Int(nodo.fila, nodo.columna) + dir;
            if (tablero.casillas.ContainsKey(posVecino) && tablero.casillas[posVecino].transitable)
                vecinos.Add(tablero.casillas[posVecino]);
        }
        return vecinos;
    }
}