using System;
using UnityEngine;

[Serializable]
public class SpringMassSystem
{
    private const float GravityConstant = 9.81f;
    
    public Vector3 springAnchorPosition;
    public float springConstant = 5f;
    public float damping = 0.1f;
    public float restLength = 1f;
    public MassPoint massPoint;

    public void Initialize()
    {
        massPoint.position += springAnchorPosition;
    }

    public void Integrate(float timeStep)
    {
        Vector3 delta = massPoint.position - springAnchorPosition;
        float currentLength = delta.magnitude;

        if (currentLength == 0f)
            return;

        Vector3 direction = delta / currentLength;
        float extension = currentLength - restLength;
        
        Vector3 springForce = -springConstant * extension * direction;
        Vector3 dampingForce = -damping * massPoint.velocity;
        Vector3 gravityForce = new Vector3(0f, -GravityConstant * massPoint.mass, 0f);

        Vector3 force = springForce + dampingForce + gravityForce;
        Vector3 acceleration = force / massPoint.mass;

        massPoint.velocity += acceleration * timeStep;
        massPoint.position += massPoint.velocity * timeStep;
    }

}