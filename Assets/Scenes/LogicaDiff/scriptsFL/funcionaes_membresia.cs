using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncionesMembresia {

    // Función de membresía booleana
    public double funcion_memebresia_booleana(double x, double x0)
    {
        if (x >= x0)
        {
            return 1;
        }
        return 0.0;
    }

    // Función de membresía booleana inversa
    public double funcion_memebresia_booleana_inversa(double x, double x0)
    {
        if (x >= x0)
        {
            return 0;
        }
        return 1;
    }

    // Función de membresía grado (función lineal)
    public double funcion_membresia_grado(double x, double x0, double x1)
    {
        if (x < x0)
        {
            return 0;
        }
        if (x >= x0 && x <= x1)
        {
            // Fórmula de la pendiente (función lineal)
            return (x - x0) / (x1 - x0); // Lógica lineal
        }
        if (x > x1)
        {
            return 1;
        }
        return 0;
    }

    // Función de membresía grado inversa
    public double funcion_membresia_grado_inversa(double x, double x0, double x1)
    {
        if (x < x0)
        {
            return 1;
        }
        if (x >= x0 && x <= x1)
        {
            // Fórmula de la pendiente (función lineal inversa)
            return 1 - ((x - x0) / (x1 - x0)); // Lógica inversa lineal
        }
        if (x > x1)
        {
            return 0;
        }
        return 0;
    }

    // Función de membresía triangular
    public double funcion_memebresia_triangulo(double x, double x0, double x1, double x2)
    {
        if (x < x0 || x > x2)
        {
            return 0;
        }
        if (x >= x0 && x <= x1)
        {
            return (x - x0) / (x1 - x0); // Lógica de la pendiente ascendente
        }
        if (x >= x1 && x <= x2)
        {
            return (x2 - x) / (x2 - x1); // Lógica de la pendiente descendente
        }
        return 0;
    }

    // Función de membresía trapezoidal
    public double funcion_membresia_trapezoidal(double x, double x0, double x1, double x2, double x3)
    {
        if (x < x0 || x > x3)
        {
            return 0;
        }
        if (x >= x0 && x <= x1)
        {
            return (x - x0) / (x1 - x0); // Lógica de la pendiente ascendente
        }
        if (x >= x1 && x <= x2)
        {
            return 1; // Lógica de la parte superior plana
        }
        if (x >= x2 && x <= x3)
        {
            return (x3 - x) / (x3 - x2); // Lógica de la pendiente descendente
        }
        return 0;
    }

    // Función de membresía gaussiana
    public float funcion_membresia_gaussiana(float x, float centro, float desviacion)
    {
        // Fórmula de la función gaussiana
        return Mathf.Exp(-Mathf.Pow(x - centro, 2) / (2 * Mathf.Pow(desviacion, 2)));
    }
}
