using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ConjuntoDifuso
{
    public string nombre;
    public System.Func<double, double> funcionMembresia;

    public ConjuntoDifuso(string nombre, System.Func<double, double> funcion)
    {
        this.nombre = nombre;
        this.funcionMembresia = funcion;
    }

    public double Evaluar(double x)
    {
        return funcionMembresia(x);
    }
}
