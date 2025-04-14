using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MovimientoCoche : MonoBehaviour
{
    public Button siguienteMovimientoBtn;
    public GameObject coche;
    public GameObject obstaculo1;
    public GameObject destino;

    [SerializeField] private SistemaDifuso sistemaDifuso;
    private Vector3 posicionDestino;
    private Vector3 posicionCoche;

    private float ampGiro = 45f;
    public TMP_Text fin;

    void Start()
    {
        posicionDestino = destino.transform.position;
        posicionCoche = coche.transform.position;
        siguienteMovimientoBtn.onClick.AddListener(MoverCoche);
    }

    void MoverCoche()
    {
        // 1. Calcular valores crudos
        float rawAngulo = CalcularDiferenciaDeAngulo(coche.transform.right, destino.transform.position - coche.transform.position);
        float rawDistanciaObjetivo = Vector3.Distance(coche.transform.position, posicionDestino);
        float rawDistanciaObstaculo = Vector3.Distance(coche.transform.position, obstaculo1.transform.position);

        // 2. Convertir a la escala usada en el sistema difuso (igual que en la GUI)
        float valorAngulo = ConvertirAnguloParaSistemaDifuso(rawAngulo);
        float valorDistanciaObjetivo = ConvertirDistanciaParaSistemaDifuso(rawDistanciaObjetivo);
        float valorDistanciaObstaculo = ConvertirDistanciaObstaculoParaSistemaDifuso(rawDistanciaObstaculo);

        // 3. Evaluar sistema difuso
        var resultados = sistemaDifuso.EvaluarSistema(valorAngulo, valorDistanciaObjetivo, valorDistanciaObstaculo);
        // escalar de 0–10 (valor del sistema difuso) a 0–1.5 unidades físicas reales
        float velocidad = resultados[0];
        float velocidadEscalada = Mathf.InverseLerp(0f, 10f, velocidad) * 1.5f;
        float giro = resultados[1];

        // Aplicar movimientos
        float giroAmplificado = 0f;
        if (giro > 0)
        {
            giroAmplificado = giro + ampGiro;
        }

        // 1. Rotación primero
        coche.transform.Rotate(0, 0, giroAmplificado); // El signo negativo corrige la dirección del giro

        // 2. Movimiento en la dirección actual del coche
        Vector3 movimiento = coche.transform.right * velocidadEscalada; // Multiplicador ajustable
        coche.transform.Translate(movimiento, Space.World);

        // Verificación de llegada
        if (Vector3.Distance(coche.transform.position, posicionDestino) < 0.8f)
        {
            fin.text = "El vehiculo llego al destino";
            siguienteMovimientoBtn.gameObject.SetActive(false);
            fin.gameObject.SetActive(true);
        }

        Debug.Log($"Movimiento: {velocidad}, Giro: {giroAmplificado}"); // Mejor formato de log
        Debug.Log($"Entradas convertidas - Angulo: {valorAngulo} | DistObj: {valorDistanciaObjetivo} | DistObs: {valorDistanciaObstaculo}");
    }

    // Devuelve el ángulo con signo para determinar dirección
    float CalcularDiferenciaDeAngulo(Vector3 direccionActual, Vector3 direccionAlObjetivo)
    {
        // calcular el ángulo en el plano 2D (eje Z)
        float angulo = Vector3.SignedAngle(direccionActual, direccionAlObjetivo, Vector3.forward);

        // convertir el ángulo en un valor positivo absoluto
        angulo = Mathf.Abs(angulo);

        // mapear de [0°, 90°] a [0, 100] para lógica difusa
        return Mathf.Clamp(angulo * (100f / 90f), 0f, 100f);
    }

    // Funciones de conversión
    float ConvertirAnguloParaSistemaDifuso(float anguloReal)
    {
        // Mapear a las etiquetas: "alineado", "poco_alineado", "muy_desalineado"
        return Mathf.Abs(anguloReal); // O usa lógica más compleja si es necesario
    }

    float ConvertirDistanciaParaSistemaDifuso(float distanciaReal)
    {
        // Escalar a los rangos de la GUI: "cerca"=10, "medio"=30, "lejos"=50
        return Mathf.Clamp(distanciaReal * 5f, 0f, 50f); // Ejemplo: si en GUI 10 unidades = 2 unidades reales
    }

    // asume que la distancia máxima física que puede haber es 6 unidades
    float ConvertirDistanciaObstaculoParaSistemaDifuso(float distanciaReal)
    {
        float distanciaEscalada = Mathf.Lerp(5f, 30f, Mathf.InverseLerp(0f, 5f, distanciaReal));
        return Mathf.Clamp(distanciaEscalada, 5f, 30f);
    }

}