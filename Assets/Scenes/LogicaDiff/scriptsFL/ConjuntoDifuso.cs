using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ConjuntoDifuso
{
    public string nombre;
    public System.Func<double, double> funcionMembresia;
    //constructor que asigna el nombre y la función de membresía al conjunto difuso
    public ConjuntoDifuso(string nombre, System.Func<double, double> funcion)
    {
        this.nombre = nombre;
        this.funcionMembresia = funcion;
    }

    //evalúa el valor de pertenencia del conjunto difuso dado un valor de entrada
    public double Evaluar(double x)
    {
        return funcionMembresia(x);
    }

}
