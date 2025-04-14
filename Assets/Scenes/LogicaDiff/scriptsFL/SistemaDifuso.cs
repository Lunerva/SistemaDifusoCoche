using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SistemaDifuso : MonoBehaviour
{
    // entrada
    VariableLinguistica angulo;
    VariableLinguistica distanciaObjetivo;
    VariableLinguistica distanciaObstaculo;

    //salida
    VariableLinguistica velocidadAvance;
    VariableLinguistica direccionGiro;

    void Start()
    {
        // Variable lingüística: Ángulo
        angulo = new VariableLinguistica("ángulo");
        angulo.AgregarConjunto(new ConjuntoDifuso("alineado", x => new FuncionesMembresia().funcion_membresia_gaussiana(x, 0, 10)));
        angulo.AgregarConjunto(new ConjuntoDifuso("poco_alineado", x => new FuncionesMembresia().funcion_membresia_gaussiana(x, 30, 10)));
        angulo.AgregarConjunto(new ConjuntoDifuso("muy_desalineado", x => new FuncionesMembresia().funcion_membresia_gaussiana(x, 70, 10)));

        // Variable lingüística: Distancia al objetivo
        distanciaObjetivo = new VariableLinguistica("distancia_objetivo");
        distanciaObjetivo.AgregarConjunto(new ConjuntoDifuso("cerca", x => new FuncionesMembresia().funcion_membresia_gaussiana(x, 10, 5)));
        distanciaObjetivo.AgregarConjunto(new ConjuntoDifuso("medio", x => new FuncionesMembresia().funcion_membresia_gaussiana(x, 30, 7)));
        distanciaObjetivo.AgregarConjunto(new ConjuntoDifuso("lejos", x => new FuncionesMembresia().funcion_membresia_gaussiana(x, 50, 7)));

        // Variable lingüística: Distancia al obstáculo
        distanciaObstaculo = new VariableLinguistica("distancia_obstaculo");
        distanciaObstaculo.AgregarConjunto(new ConjuntoDifuso("muy_cerca", x => new FuncionesMembresia().funcion_membresia_gaussiana(x, 5, 3)));
        distanciaObstaculo.AgregarConjunto(new ConjuntoDifuso("cercano", x => new FuncionesMembresia().funcion_membresia_gaussiana(x, 15, 5)));
        distanciaObstaculo.AgregarConjunto(new ConjuntoDifuso("no_hay_obstaculo", x => new FuncionesMembresia().funcion_membresia_gaussiana(x, 30, 7)));

        // Variable lingüística: Velocidad de avance
        velocidadAvance = new VariableLinguistica("velocidad_avance");
        velocidadAvance.AgregarConjunto(new ConjuntoDifuso("detenerse", x => new FuncionesMembresia().funcion_membresia_gaussiana(x, 0, 1.5f)));
        velocidadAvance.AgregarConjunto(new ConjuntoDifuso("avanzar_lento", x => new FuncionesMembresia().funcion_membresia_gaussiana(x, 5, 2)));
        velocidadAvance.AgregarConjunto(new ConjuntoDifuso("avanzar_rapido", x => new FuncionesMembresia().funcion_membresia_gaussiana(x, 10, 3f)));

        // Variable lingüística: Dirección de giro
        direccionGiro = new VariableLinguistica("direccion_giro");
        direccionGiro.AgregarConjunto(new ConjuntoDifuso("girar_izquierda", x => new FuncionesMembresia().funcion_membresia_gaussiana(x, -45, 15)));
        direccionGiro.AgregarConjunto(new ConjuntoDifuso("no_girar", x => new FuncionesMembresia().funcion_membresia_gaussiana(x, 0, 10)));
        direccionGiro.AgregarConjunto(new ConjuntoDifuso("girar_derecha", x => new FuncionesMembresia().funcion_membresia_gaussiana(x, 45, 15)));

    }

    public float[] EvaluarSistema(float valorAngulo, float valorDistanciaObjetivo, float valorDistanciaObstaculo)
    {
        // Aquí llamas a EvaluarReglas y Defuzzificar
        var resultados = EvaluarReglas(valorAngulo, valorDistanciaObjetivo, valorDistanciaObstaculo);

        float velocidadFinal = Defuzzificar(velocidadAvance, resultados.velocidad, 0f, 10f, 100);
        float giroFinal = Defuzzificar(direccionGiro, resultados.giro, -45f, 45f, 100);

        Debug.Log("Velocidad final: " + velocidadFinal);
        Debug.Log("Giro final: " + giroFinal);

        // Devolver un array con las dos salidas
        return new float[] { velocidadFinal, giroFinal };
    }

    // metodo que aplica las reglas difusas y devuelve las salidas activadas
    (Dictionary<string, float> velocidad, Dictionary<string, float> giro) EvaluarReglas(float a, float dObj, float dObs)
    {
        // se evalúan los grados de pertenencia
        var gradosAngulo = angulo.Evaluar(a);
        var gradosObjetivo = distanciaObjetivo.Evaluar(dObj);
        var gradosObstaculo = distanciaObstaculo.Evaluar(dObs);

        // diccionarios para guardar activaciones de salida
        var salidaVelocidad = new Dictionary<string, float>();
        var salidaGiro = new Dictionary<string, float>();

        // funcion auxiliar para agregar activaciones
        void Asignar(Dictionary<string, float> dict, string etiqueta, float valor)
        {
            if (dict.ContainsKey(etiqueta))
                dict[etiqueta] = Mathf.Max(dict[etiqueta], valor); // toma el maximo si ya existe
            else
                dict[etiqueta] = valor;
        }

        // regla 1: alineado, objetivo cerca, sin obstáculo → avanzar lento, no girar
        float r1 = Mathf.Min((float)gradosAngulo["alineado"], (float)gradosObjetivo["cerca"], (float)gradosObstaculo["no_hay_obstaculo"]);
        Asignar(salidaVelocidad, "avanzar_lento", r1);
        Asignar(salidaGiro, "no_girar", r1);

        // regla 2: alineado, objetivo lejos, sin obstáculo → avanzar rápido, no girar
        float r2 = Mathf.Min((float)gradosAngulo["alineado"], (float)gradosObjetivo["lejos"], (float)gradosObstaculo["no_hay_obstaculo"]);
        Asignar(salidaVelocidad, "avanzar_rapido", r2);
        Asignar(salidaGiro, "no_girar", r2);

        // regla 3: muy desalineado, objetivo medio, sin obstáculo → avanzar lento, girar derecha
        float r3 = Mathf.Min((float)gradosAngulo["muy_desalineado"], (float)gradosObjetivo["medio"], (float)gradosObstaculo["no_hay_obstaculo"]);
        Asignar(salidaVelocidad, "avanzar_lento", r3);
        Asignar(salidaGiro, "girar_derecha", r3);

        // regla 4: muy desalineado, objetivo medio, obstáculo cercano → detenerse, girar derecha
        float r4 = Mathf.Min((float)gradosAngulo["muy_desalineado"], (float)gradosObjetivo["medio"], (float)gradosObstaculo["cercano"]);
        Asignar(salidaVelocidad, "detenerse", r4);
        Asignar(salidaGiro, "girar_derecha", r4);

        // regla 5: poco alineado, objetivo medio, sin obstáculo → avanzar lento, girar izquierda
        float r5 = Mathf.Min((float)gradosAngulo["poco_alineado"], (float)gradosObjetivo["medio"], (float)gradosObstaculo["no_hay_obstaculo"]);
        Asignar(salidaVelocidad, "avanzar_lento", r5);
        Asignar(salidaGiro, "girar_izquierda", r5);

        // regla 6: poco alineado, objetivo cerca, obstáculo muy cerca → detenerse, girar izquierda
        float r6 = Mathf.Min((float)gradosAngulo["poco_alineado"], (float)gradosObjetivo["cerca"], (float)gradosObstaculo["muy_cerca"]);
        Asignar(salidaVelocidad, "detenerse", r6);
        Asignar(salidaGiro, "girar_izquierda", r6);

        // regla 7: alineado, objetivo medio, obstáculo cercano → avanzar lento, no girar
        float r7 = Mathf.Min((float)gradosAngulo["alineado"], (float)gradosObjetivo["medio"], (float)gradosObstaculo["cercano"]);
        Asignar(salidaVelocidad, "avanzar_lento", r7);
        Asignar(salidaGiro, "no_girar", r7);

        // regla 8: alineado, objetivo cerca, obstáculo muy cerca → detenerse, no girar
        float r8 = Mathf.Min((float)gradosAngulo["alineado"], (float)gradosObjetivo["cerca"], (float)gradosObstaculo["muy_cerca"]);
        Asignar(salidaVelocidad, "detenerse", r8);
        Asignar(salidaGiro, "no_girar", r8);

        // regla 9: poco alineado, objetivo lejos, sin obstáculo → avanzar_rapido, girar izquierda
        float r9 = Mathf.Min((float)gradosAngulo["poco_alineado"], (float)gradosObjetivo["lejos"], (float)gradosObstaculo["no_hay_obstaculo"]);
        Asignar(salidaVelocidad, "avanzar_rapido", r9);
        Asignar(salidaGiro, "girar_izquierda", r9);

        // regla 10: muy desalineado, objetivo lejos, obstáculo cercano → avanzar_lento, girar derecha
        float r10 = Mathf.Min((float)gradosAngulo["muy_desalineado"], (float)gradosObjetivo["lejos"], (float)gradosObstaculo["cercano"]);
        Asignar(salidaVelocidad, "avanzar_lento", r10);
        Asignar(salidaGiro, "girar_derecha", r10);

        // regla 11: muy desalineado, objetivo cerca, obstáculo muy cerca → detenerse, girar derecha
        float r11 = Mathf.Min((float)gradosAngulo["muy_desalineado"], (float)gradosObjetivo["cerca"], (float)gradosObstaculo["muy_cerca"]);
        Asignar(salidaVelocidad, "detenerse", r11);
        Asignar(salidaGiro, "girar_derecha", r11);

        // regla 12: poco alineado, objetivo cerca, sin obstáculo → avanzar lento, girar izquierda
        float r12 = Mathf.Min((float)gradosAngulo["poco_alineado"], (float)gradosObjetivo["cerca"], (float)gradosObstaculo["no_hay_obstaculo"]);
        Asignar(salidaVelocidad, "avanzar_lento", r12);
        Asignar(salidaGiro, "girar_izquierda", r12);

        // regla 13: muy desalineado, objetivo lejos, no hay obstáculo → avanzar rapido, girar derecha
        float r13 = Mathf.Min((float)gradosAngulo["muy_desalineado"], (float)gradosObjetivo["lejos"], (float)gradosObstaculo["no_hay_obstaculo"]);
        Asignar(salidaVelocidad, "avanzar_rapido", r13);
        Asignar(salidaGiro, "girar_derecha", r13);

        // regla 14: poco alineado, objetivo medio, obstáculo cercano → avanzar lento, girar izquierda
        float r14 = Mathf.Min((float)gradosAngulo["poco_alineado"], (float)gradosObjetivo["medio"], (float)gradosObstaculo["cercano"]);
        Asignar(salidaVelocidad, "avanzar_lento", r14);
        Asignar(salidaGiro, "girar_izquierda", r14);

        // regla 15: alineado, objetivo lejos, obstáculo muy cerca → detenerse, no girar
        float r15 = Mathf.Min((float)gradosAngulo["alineado"], (float)gradosObjetivo["lejos"], (float)gradosObstaculo["muy_cerca"]);
        Asignar(salidaVelocidad, "detenerse", r15);
        Asignar(salidaGiro, "no_girar", r15);

        // regla 16: poco alineado, objetivo lejos, obstáculo cercano → avanzar lento, girar izquierda
        float r16 = Mathf.Min((float)gradosAngulo["poco_alineado"], (float)gradosObjetivo["lejos"], (float)gradosObstaculo["cercano"]);
        Asignar(salidaVelocidad, "avanzar_lento", r16);
        Asignar(salidaGiro, "girar_izquierda", r16);

        // regla 17: muy desalineado, objetivo cerca, obstáculo cercano → detenerse, girar derecha
        float r17 = Mathf.Min((float)gradosAngulo["muy_desalineado"], (float)gradosObjetivo["cerca"], (float)gradosObstaculo["cercano"]);
        Asignar(salidaVelocidad, "detenerse", r17);
        Asignar(salidaGiro, "girar_derecha", r17);

        // regla 18: alineado, objetivo medio, no hay obstáculo → avanzar rapido, no girar
        float r18 = Mathf.Min((float)gradosAngulo["alineado"], (float)gradosObjetivo["medio"], (float)gradosObstaculo["no_hay_obstaculo"]);
        Asignar(salidaVelocidad, "avanzar_rapido", r18);
        Asignar(salidaGiro, "no_girar", r18);
        // regla 19: poco alineado, objetivo medio, obstáculo muy cerca → detenerse, girar izquierda
        float r19 = Mathf.Min((float)gradosAngulo["poco_alineado"], (float)gradosObjetivo["medio"], (float)gradosObstaculo["muy_cerca"]);
        Asignar(salidaVelocidad, "detenerse", r19);
        Asignar(salidaGiro, "girar_izquierda", r19);
        // regla 20: alineado, objetivo medio, obstáculo muy cerca → detenerse, no girar
        float r20 = Mathf.Min((float)gradosAngulo["alineado"], (float)gradosObjetivo["medio"], (float)gradosObstaculo["muy_cerca"]);
        Asignar(salidaVelocidad, "detenerse", r20);
        Asignar(salidaGiro, "no_girar", r20);
        // regla 21: poco alineado, objetivo medio, obstáculo muy cerca → detenerse, girar izquierda
        float r21 = Mathf.Min((float)gradosAngulo["poco_alineado"], (float)gradosObjetivo["medio"], (float)gradosObstaculo["muy_cerca"]);
        Asignar(salidaVelocidad, "detenerse", r21);
        Asignar(salidaGiro, "girar_izquierda", r21);
        // regla 22: muy desalineado, objetivo medio, obstáculo muy cerca → detenerse, girar derecha
        float r22 = Mathf.Min((float)gradosAngulo["muy_desalineado"], (float)gradosObjetivo["medio"], (float)gradosObstaculo["muy_cerca"]);
        Asignar(salidaVelocidad, "detenerse", r22);
        Asignar(salidaGiro, "girar_derecha", r22);
        // regla 23: alineado, objetivo lejos, obstáculo cercano → avanzar lento, no girar
        float r23 = Mathf.Min((float)gradosAngulo["alineado"], (float)gradosObjetivo["lejos"], (float)gradosObstaculo["cercano"]);
        Asignar(salidaVelocidad, "avanzar_lento", r23);
        Asignar(salidaGiro, "no_girar", r23);
        // regla 24: poco alineado, objetivo cerca, obstáculo cercano → avanzar lento, girar izquierda
        float r24 = Mathf.Min((float)gradosAngulo["poco_alineado"], (float)gradosObjetivo["cerca"], (float)gradosObstaculo["cercano"]);
        Asignar(salidaVelocidad, "avanzar_lento", r24);
        Asignar(salidaGiro, "girar_izquierda", r24);
        // regla 25: muy desalineado, objetivo medio, obstáculo cercano → avanzar lento, girar derecha
        float r25 = Mathf.Min((float)gradosAngulo["muy_desalineado"], (float)gradosObjetivo["medio"], (float)gradosObstaculo["cercano"]);
        Asignar(salidaVelocidad, "avanzar_lento", r25);
        Asignar(salidaGiro, "girar_derecha", r25);
        // regla 26: poco alineado, objetivo medio, obstáculo cercano → avanzar lento, girar izquierda
        float r26 = Mathf.Min((float)gradosAngulo["poco_alineado"], (float)gradosObjetivo["medio"], (float)gradosObstaculo["cercano"]);
        Asignar(salidaVelocidad, "avanzar_lento", r26);
        Asignar(salidaGiro, "girar_izquierda", r26);
        // regla 27: muy desalineado, objetivo lejos, obstáculo muy cerca → detenerse, girar derecha
        float r27 = Mathf.Min((float)gradosAngulo["muy_desalineado"], (float)gradosObjetivo["lejos"], (float)gradosObstaculo["muy_cerca"]);
        Asignar(salidaVelocidad, "detenerse", r27);
        Asignar(salidaGiro, "girar_derecha", r27);


        return (salidaVelocidad, salidaGiro);
    }

    float Defuzzificar(VariableLinguistica variable, Dictionary<string, float> activaciones, float xMin, float xMax, int pasos)
    {
        float numerador = 0f;
        float denominador = 0f;
        float paso = (xMax - xMin) / pasos;

        for (int i = 0; i <= pasos; i++)
        {
            float x = xMin + i * paso;
            float mu = 0f;

            foreach (var par in activaciones)
            {
                string etiqueta = par.Key;
                float grado = par.Value;
                float pertenencia = (float)variable.Evaluar(x)[etiqueta];

                mu = Mathf.Max(mu, Mathf.Min(grado, pertenencia));
            }

            numerador += x * mu;
            denominador += mu;
        }

        return (denominador == 0f) ? 0f : numerador / denominador;
    }
}
