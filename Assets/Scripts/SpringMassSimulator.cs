using System;
using System.Collections.Generic;
using UnityEngine;

public class SpringMassSimulator : MonoBehaviour
{
    [Header("Config")] 
    [SerializeField] private float timeStep = 0.02f;
    [SerializeField] private List<SpringMassSystem> massSystems = new();

    [Header("Visualization")]
    [SerializeField] private GameObject anchorPrefab;
    [SerializeField] private GameObject massPrefab;
    [SerializeField] private Gradient velocityGradient; // Color based on speed
    [SerializeField] private Color lineColor;
    [SerializeField] private float maxSpeedForColor = 10f;
    [SerializeField] private Material lineMaterial;

    private SpringMassSystem[] systems;
    private List<LineRenderer> lines = new();
    private List<Transform> anchorObjects = new();
    private List<Transform> massObjects = new();

    private void Start()
    {
        systems = massSystems.ToArray();
        
        foreach (var system in systems)
        {
            system.Initialize();

            // Instantiate visuals
            Transform anchorObj = Instantiate(anchorPrefab, system.springAnchorPosition, Quaternion.identity).transform;
            Transform massObj = Instantiate(massPrefab, system.massPoint.position, Quaternion.identity).transform;

            anchorObjects.Add(anchorObj);
            massObjects.Add(massObj);

            // Create line renderer for spring
            var lineGO = new GameObject("SpringLine");
            var line = lineGO.AddComponent<LineRenderer>();
            line.material = lineMaterial;
            line.startWidth = 0.05f;
            line.endWidth = 0.05f;
            line.positionCount = 2;
            line.useWorldSpace = true;
            lines.Add(line);
        }
    }

    private void Update()
    {
        for (int i = 0; i < systems.Length; i++)
        {
            var system = systems[i];
            system.Integrate(timeStep);
            
            var massObj = massObjects[i];
            var anchorObj = anchorObjects[i];
            var line = lines[i];

            massObj.position = system.massPoint.position;
            anchorObj.position = system.springAnchorPosition;
            
            UpdateSpringLine(line, system.springAnchorPosition, system.massPoint.position, coilTurns: 8, radius: 0.05f, pointsPerTurn: 12);

            float speed = system.massPoint.velocity.magnitude;
            float t = Mathf.Clamp01(speed / maxSpeedForColor);
            Color color = velocityGradient.Evaluate(t);

            var massRenderer = massObj.GetComponent<Renderer>();
            if (massRenderer != null)
                massRenderer.material.color = color;

            line.startColor = lineColor;
            line.endColor = lineColor;
        }
    }
    // for coil look of line P(t)=A+(B−A)⋅t+R⋅(sin(ωt)⋅n1​+cos(ωt)⋅n2​)
    private void UpdateSpringLine(LineRenderer line, Vector3 start, Vector3 end, int coilTurns = 8, float radius = 0.1f, int pointsPerTurn = 10)
    {
        Vector3 dir = end - start;
        float length = dir.magnitude;
        if (length < 0.001f)
        {
            line.positionCount = 2;
            line.SetPosition(0, start);
            line.SetPosition(1, end);
            return;
        }

        dir.Normalize();
        
        Vector3 arbitrary = Mathf.Abs(Vector3.Dot(dir, Vector3.up)) > 0.99f ? Vector3.right : Vector3.up;
        Vector3 n1 = Vector3.Cross(dir, arbitrary).normalized;
        Vector3 n2 = Vector3.Cross(dir, n1).normalized;

        int totalPoints = coilTurns * pointsPerTurn + 1;
        line.positionCount = totalPoints;

        for (int i = 0; i < totalPoints; i++)
        {
            float t = (float)i / (totalPoints - 1);
            float angle = t * coilTurns * Mathf.PI * 2f;
            Vector3 offset = n1 * (Mathf.Sin(angle) * radius) + n2 * (Mathf.Cos(angle) * radius);
            Vector3 pos = start + dir * (t * length) + offset;
            line.SetPosition(i, pos);
        }
    }

}
