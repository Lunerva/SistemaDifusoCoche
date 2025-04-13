using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableLinguistica
{
    public string nombre; //nombre de la variable linguistica
    public List<ConjuntoDifuso> conjuntos; //lista de conjuntos difusos asociados

    //constructor que inicializa el nombre y la lista de conjuntos
    public VariableLinguistica(string nombre)
    {
        this.nombre = nombre;
        this.conjuntos = new List<ConjuntoDifuso>();
    }

    //agrega un conjunto difuso a la variable linguistica
    public void AgregarConjunto(ConjuntoDifuso conjunto)
    {
        conjuntos.Add(conjunto);
    }

    //evalua todos los conjuntos difusos con un valor de entrada x
    //devuelve un diccionario con el nombre del conjunto y su grado de membresia
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
