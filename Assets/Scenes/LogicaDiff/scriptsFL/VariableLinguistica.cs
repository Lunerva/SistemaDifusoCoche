using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class VariableLinguistica
{
    public string nombre;
    public List<ConjuntoDifuso> conjuntos;

    public VariableLinguistica(string nombre)
    {
        this.nombre = nombre;
        this.conjuntos = new List<ConjuntoDifuso>();
    }

    public void AgregarConjunto(ConjuntoDifuso conjunto)
    {
        conjuntos.Add(conjunto);
    }

    public Dictionary<string, double> Evaluar(double x)
    {
        Dictionary<string, double> resultados = new Dictionary<string, double>();
        foreach (var conjunto in conjuntos)
        {
            resultados[conjunto.nombre] = conjunto.Evaluar(x);
        }
        return resultados;
    }
}
