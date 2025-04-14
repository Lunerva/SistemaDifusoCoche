# 🚗 Sistema de Estacionamiento Autónomo con Lógica Difusa en Unity

Este proyecto implementa un sistema de estacionamiento autónomo en un entorno simulado 2D desarrollado en **Unity**, utilizando un modelo de **lógica difusa personalizado en C#**, sin librerías externas. El objetivo es simular cómo un vehículo puede avanzar hacia un destino mientras evita obstáculos y ajusta su velocidad y dirección de forma inteligente y progresiva.

---

## 📌 Características

- ✅ Implementación manual de lógica difusa: fuzzificación, reglas, agregación y defuzzificación (método del centroide).
- ✅ Entrada por:
  - Ángulo entre dirección del vehículo y el destino.
  - Distancia al objetivo.
  - Distancia a obstáculos.
- ✅ Salidas:
  - Velocidad de avance.
  - Dirección de giro.
- ✅ Entorno visual e interactivo en Unity 2D.
- ✅ Control por pasos con botón "Siguiente movimiento".
- ✅ Posibilidad de arrastrar el coche con el cursor para cambiar su posición en tiempo real.
- ✅ Botón para reiniciar la escena y probar diferentes escenarios.

---

## 🧠 Modelo de Lógica Difusa

- **Variables de entrada:**
  - `ángulo` → (alineado, poco alineado, muy desalineado)
  - `distancia_objetivo` → (cerca, medio, lejos)
  - `distancia_obstáculo` → (muy cerca, cercano, no hay obstáculo)

- **Variables de salida:**
  - `velocidad` → (sin movimiento, poca, mucha)
  - `giro` → valor continuo dentro de un rango definido

- **Funciones de membresía:**
  - Gaussianas implementadas de forma manual.
  - Lógica difusa basada en conjuntos y reglas codificadas por el desarrollador.

---

## 🔧 Tecnologías utilizadas

- **Motor de juego**: Unity 2D
- **Lenguaje**: C# (sin librerías externas para lógica difusa)
- **Entorno de desarrollo**: Unity Editor (compatible con Unity 2020+)

---

## 📷 Capturas de pantalla

![Screen Shot 2025-04-13 at 20 21 17](https://github.com/user-attachments/assets/d23bdafd-96d7-4d03-8111-b18f482e9367)
