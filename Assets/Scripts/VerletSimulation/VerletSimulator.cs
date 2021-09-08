using System.Collections.Generic;
using UnityEngine;

namespace VerletSimulation
{
    public class VerletSimulator
    {
        public List<Dot> Dots { get; } = new List<Dot>();

        private readonly float mass;
        private readonly int iterations;
        private Vector3 currentForce = Vector3.zero;

        public VerletSimulator(float mass, int iterations)
        {
            this.mass = mass;
            this.iterations = iterations;
        }

        public void AddForce(Vector3 force)
        {
            currentForce += force;
        }
        
        public void Simulate(float deltaTime)
        {
            ApplyPhysicsToDots(deltaTime);
            ConstraintLength();
        }

        private void ApplyPhysicsToDots(float deltaTime)
        {
            float squaredDeltaTime = deltaTime * deltaTime;
            Vector3 acceleration = currentForce / mass;
            Vector3 positionVariation = acceleration * squaredDeltaTime;
            
            foreach (Dot dot in Dots)
            {
                if(dot.IsLocked) continue;
                Vector3 oldPosition = dot.CurrentPosition;
                
                dot.CurrentPosition += dot.CurrentPosition - dot.LastPosition;
                dot.CurrentPosition += positionVariation;
                dot.LastPosition = oldPosition;
            }
            
            currentForce = Vector3.zero;
        }
        
        private void ConstraintLength()
        {
            for (int i = 0; i < iterations; i++)
            {
                foreach (Dot dotA in Dots)
                {
                    foreach (DotConnection connection in dotA.Connections)
                    {
                        Dot dotB = connection.Other(dotA);
                        Vector3 center = (dotA.CurrentPosition + dotB.CurrentPosition) / 2f;
                        Vector3 direction = (dotA.CurrentPosition - dotB.CurrentPosition).normalized;
                        Vector3 connectionSize = direction * connection.Length / 2f;

                        if (!dotA.IsLocked) dotA.CurrentPosition = center + connectionSize;
                        if (!dotB.IsLocked) dotB.CurrentPosition = center - connectionSize;
                    }
                }
            }
        }
    }
}
