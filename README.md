# 🧮 Spring–Mass Simulator (Unity 3D)

A real-time spring–mass simulation built in Unity, visualized with 3D meshes and a coil-shaped LineRenderer.  
It demonstrates principles of classical mechanics, Hooke’s Law, damping, and numerical motion integration.
<img width="1919" height="883" alt="spring-mass" src="https://github.com/user-attachments/assets/913e4b61-9d9f-4532-b1cf-b72feba53ecf" />

---

https://github.com/user-attachments/assets/03aae041-4805-4fd0-ac11-f0796b75b529



## ⚙️ Overview

This project simulates a single or multiple spring–mass systems influenced by:

- Newton’s Second Law of Motion  
- Hooke’s Law of Elasticity  
- Damping Force  
- Gravitational Force  

Each mass point changes color based on its velocity using a gradient, visualizing kinetic energy in real time.

---

## 🧠 Core Physics Concepts

The simulation models these relationships:

- Spring Force (Hooke’s Law)  
- Damping Force proportional to velocity  
- Gravitational Force acting downward  
- Net Force = sum of spring, damping, and gravity  
- Acceleration = Force / Mass  
- Velocity and position updated using Euler integration  

The motion approximates a damped harmonic oscillator.

---

## 🧩 Numerical Integration

Uses the semi-explicit Euler method:

1. Compute total force and acceleration.  
2. Update velocity using acceleration and time step.  
3. Update position using new velocity.

This simple integrator is efficient and stable for small time steps.

---

## 🧵 Coil Visualization

The spring is drawn as a 3D helix between the anchor and mass point.  
Its shape is generated procedurally using sine and cosine waves around the main spring axis.  
Adjustable parameters include number of turns, radius, and points per turn.

---

## 🎨 Visual Elements

| Element | Description |
|----------|-------------|
| Anchor | Fixed point of the spring |
| Mass Point | Simulated particle affected by forces |
| Coil | LineRenderer generated helix between anchor and mass |
| Gradient | Mass color changes with velocity magnitude |

---

## 📘 References

- Effective Mass in Spring–Mass Systems (Wikipedia)  
  https://en.wikipedia.org/wiki/Effective_mass_(spring%E2%80%93mass_system)  
- Motion of a Spring–Mass System – LibreTexts Physics  
  https://phys.libretexts.org/Courses/Berea_College/Introductory_Physics%3A_Berea_College/13%3A_Simple_Harmonic_Motion/13.01%3A_The_motion_of_a_spring-mass_system  

---

## 🧰 Implementation

Core C# scripts:
- SpringMassSimulator.cs — Main update loop and visualization  
- SpringMassSystem.cs — Physics logic (forces, integration)  
- MassPoint.cs — State container for position, velocity, and mass  

Visualization uses:
- Mesh prefabs for anchor and mass  
- LineRenderer for dynamic coil rendering  
- Gradient material for velocity-based color  

---

## 🧪 Features

- Realistic spring–mass motion with damping and gravity  
- Procedural 3D coil visualization  
- Velocity-based color feedback  
- Easily configurable spring parameters  

---

## 🧭 License

MIT License © 2025.  
Use freely for educational, research, or simulation projects.

---

