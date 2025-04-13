using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class LogicaDropdowns : MonoBehaviour
{
    public TMP_Dropdown dropdownAngulo;
    public TMP_Dropdown dropdownDistanciaObjetivo;
    public TMP_Dropdown dropdownDistanciaObstaculo;

    public TMP_Text salida1;
    public TMP_Text salida2;

    // Referencia al sistema difuso
    public SistemaDifuso sistemaDifuso;

    void Start()
    {
        // Limpia los dropdowns por si ya tienen opciones
        dropdownAngulo.ClearOptions();
        dropdownDistanciaObjetivo.ClearOptions();
        dropdownDistanciaObstaculo.ClearOptions();

        // Opciones para ángulo
        List<string> opcionesAngulo = new List<string>
        {
            "alineado",
            "poco_alineado",
            "muy_desalineado"
        };

        // Opciones para distancia al objetivo
        List<string> opcionesDistanciaObjetivo = new List<string>
        {
            "lejos",
            "medio",
            "cerca"
        };

        // Opciones para distancia a obstáculo
        List<string> opcionesDistanciaObstaculo = new List<string>
        {
            "no_hay_obstaculo",
            "obstaculo_cercano",
            "obstaculo_muy_cerca"
        };

        // Asigna a los dropdowns
        dropdownAngulo.AddOptions(opcionesAngulo);
        dropdownDistanciaObjetivo.AddOptions(opcionesDistanciaObjetivo);
        dropdownDistanciaObstaculo.AddOptions(opcionesDistanciaObstaculo);
    }

    // Método para leer los valores seleccionados en los Dropdowns y enviarlos al sistema difuso
    public void ObtenerValoresYEvaluar()
    {
        // Obtener las opciones seleccionadas
        string valorAngulo = dropdownAngulo.options[dropdownAngulo.value].text;
        string valorDistanciaObjetivo = dropdownDistanciaObjetivo.options[dropdownDistanciaObjetivo.value].text;
        string valorDistanciaObstaculo = dropdownDistanciaObstaculo.options[dropdownDistanciaObstaculo.value].text;

        // Convertir las opciones seleccionadas a valores flotantes o adecuarlos según lo necesites
        float angulo = ConvertirValorAngulo(valorAngulo);
        float distanciaObjetivo = ConvertirValorDistancia(valorDistanciaObjetivo);
        float distanciaObstaculo = ConvertirValorDistanciaObstaculo(valorDistanciaObstaculo);

        // Guardar resultados
        float[] salidas = new float[2];

        // Llamar al método de evaluación del sistema difuso
        if (sistemaDifuso != null)
        {
            salidas = sistemaDifuso.EvaluarSistema(angulo, distanciaObjetivo, distanciaObstaculo);
        }

        salida1.text = $"Velocidad : {salidas[0]}";
        salida2.text = $"Ángulo : {salidas[1]}";
    }

    // Método para convertir el valor de ángulo en un valor numérico
    float ConvertirValorAngulo(string valor)
    {
        switch (valor)
        {
            case "alineado": return 0;
            case "poco_alineado": return 30;
            case "muy_desalineado": return 70;
            default: return 0;
        }
    }

    // Método para convertir la distancia al objetivo en un valor numérico
    float ConvertirValorDistancia(string valor)
    {
        switch (valor)
        {
            case "cerca": return 10;
            case "medio": return 30;
            case "lejos": return 50;
            default: return 30;
        }
    }

    // Método para convertir la distancia al obstáculo en un valor numérico
    float ConvertirValorDistanciaObstaculo(string valor)
    {
        switch (valor)
        {
            case "no_hay_obstaculo": return 30;
            case "obstaculo_cercano": return 15;
            case "obstaculo_muy_cerca": return 5;
            default: return 30;
        }
    }
}
