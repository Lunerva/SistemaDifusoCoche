using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class LogicaDropdowns : MonoBehaviour
{
    public TMP_Dropdown dropdownAngulo;
    public TMP_Dropdown dropdownDistanciaObjetivo;
    public TMP_Dropdown dropdownDistanciaObstaculo;

    void Start()
    {
        // limpia los dropdowns por si ya tienen opciones
        dropdownAngulo.ClearOptions();
        dropdownDistanciaObjetivo.ClearOptions();
        dropdownDistanciaObstaculo.ClearOptions();

        // opciones para ángulo
        List<string> opcionesAngulo = new List<string>
        {
            "alineado",
            "poco_alineado",
            "muy_desalineado"
        };

        // opciones para distancia al objetivo
        List<string> opcionesDistanciaObjetivo = new List<string>
        {
            "lejos",
            "medio",
            "cerca"
        };

        // opciones para distancia a obstáculo
        List<string> opcionesDistanciaObstaculo = new List<string>
        {
            "no_hay_obstaculo",
            "obstaculo_cercano",
            "obstaculo_muy_cerca"
        };

        // asigna a los dropdowns
        dropdownAngulo.AddOptions(opcionesAngulo);
        dropdownDistanciaObjetivo.AddOptions(opcionesDistanciaObjetivo);
        dropdownDistanciaObstaculo.AddOptions(opcionesDistanciaObstaculo);
    }

    // funciones auxiliares para obtener los valores seleccionados si se requieren
    public float ObtenerValorAngulo()
    {
        return dropdownAngulo.value switch
        {
            0 => 0f,    // alineado
            1 => 0.5f,  // poco_alineado
            2 => 1f,    // muy_desalineado
            _ => 0f
        };
    }

    public float ObtenerValorDistanciaObjetivo()
    {
        return dropdownDistanciaObjetivo.value switch
        {
            0 => 0f,    // lejos
            1 => 0.5f,  // medio
            2 => 1f,    // cerca
            _ => 0f
        };
    }

    public float ObtenerValorDistanciaObstaculo()
    {
        return dropdownDistanciaObstaculo.value switch
        {
            0 => 0f,    // no hay obstáculo
            1 => 0.5f,  // obstáculo cercano
            2 => 1f,    // obstáculo muy cerca
            _ => 0f
        };
    }
}
