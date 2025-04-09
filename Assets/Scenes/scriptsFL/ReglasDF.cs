using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReglasDF : MonoBehaviour
{

    // Variables de salida
    double salida_angulo;
    double salida_velocidad;

    // Referencia
    private funcionaes_membresia fm;

    // Reglas difusas
    public void reglasDF(double angulo, double d1, double d2){
        
        // Regla 1
        

    }

     // Funciones de membresía para las entradas

    // Ángulo (alineado)
    public double Alineado(double angulo)
    {
        return fm.funcion_membresia_gaussiana(angulo, 0.17, 0);
    }

    public double PocoAlineado(double angulo)
    {
        return fm.funcion_membresia_gaussiana(angulo, 0.17, 0.5);
    }

    public double MuyDesalineado(double angulo)
    {
        return fm.funcion_membresia_gaussiana(angulo, 0.17, 1);
    }

    // Distancia al objetivo
    public double Lejos(double distancia)
    {
        return fm.funcion_membresia_gaussiana(distancia, 0.17, 0);
    }

    public double Medio(double distancia)
    {
        return fm.funcion_membresia_gaussiana(distancia, 0.17, 0.5);
    }

    public double Cerca(double distancia)
    {
        return fm.funcion_membresia_gaussiana(distancia, 0.17, 1);
    }

    // Distancia a obstáculos
    public double NoHayObstaculo(double distancia)
    {
        return fm.funcion_membresia_gaussiana(distancia, 0.17, 0);
    }

    public double ObstaculoCercano(double distancia)
    {
        return fm.funcion_membresia_gaussiana(distancia, 0.17, 0.5);
    }

    public double ObstaculoMuyCerca(double distancia)
    {
        return fm.funcion_membresia_gaussiana(distancia, 0.17, 1);
    }

    // Funciones de membresía para las salidas

    // Velocidad del vehículo
    public double SinMovimiento(double velocidad)
    {
        return fm.funcion_membresia_gaussiana(velocidad, 0.17, 0);
    }

    public double Lento(double velocidad)
    {
        return fm.funcion_membresia_gaussiana(velocidad, 0.17, 0.5);
    }

    public double Rapido(double velocidad)
    {
        return fm.funcion_membresia_gaussiana(velocidad, 0.17, 1);
    }

    // Ángulo de giro
    public double GiroIzquierda(double anguloGiro)
    {
        return fm.funcion_membresia_gaussiana(anguloGiro, 0.17, 0);
    }

    public double SinGiro(double anguloGiro)
    {
        return fm.funcion_membresia_gaussiana(anguloGiro, 0.17, 0.5);
    }

    public double GiroDerecha(double anguloGiro)
    {
        return fm.funcion_membresia_gaussiana(anguloGiro, 0.17, 1);
    }

}
