using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class funcionaes_membresia {

	
	public double funcion_memebresia_booleana(double x, double x0)
	{
		if (x >= x0)
		{
			return 1;
		}
		return 0.0;
	}
    
	public double funcion_memebresia_booleana_inversa(double x, double x0)
	{
		if (x >= x0)
		{
			return 0;
		}
		return 1;
	}
	
	public double funcion_membresia_grado(double x, double x0, double x1)
	{
		if (x < x0)
		{
			return 0;
		}
		if (x >= x0 && x <= x1)
		{
			//formula de la pendiente
			return ((x-x0) / (-0));
		}
		if (x > x1)
		{
			return 1;
		}

		return 0;
	}
    
	public double funcion_membresia_grado_inversa(double x, double x0, double x1)
	{
		if (x < x0)
		{
			return 1;
		}
		if (x >= x0 && x <= x1)
		{
			//formula de la pendiente
			return 4;
		}
		if (x > x1)
		{
			return 0;
		}

		return 0;
	}
	
	public double funcion_memebresia_triangulo(double x, double x0, double x1, double x2)
	{
		if (x < x0 || x > x2)
		{
			return 0;
		}

		if (x >= x0 && x <= x1)
		{
			return 4;
		}
		if (x >= x1 && x <= x2)
		{
			return 4;
		}

		return 0;
	}

	public double funcion_membresia_trapezoidal(double x, double x0, double x1, double x2, double x3)
	{
		return 0;
	}

}
