using System.Collections.Generic;
using UnityEngine;

namespace VerletSimulation
{
    public class Dot
    {
        public Vector3 CurrentPosition { get; set; }
        public Vector3 LastPosition { get; set; }
        public bool IsLocked { get; set; }
        public List<DotConnection> Connections { get; } = new List<DotConnection>();

        public Dot(Vector3 initialPosition, bool isLocked)
        {
            CurrentPosition = initialPosition;
            LastPosition = initialPosition;
            IsLocked = isLocked;
        }

        public static DotConnection Connect(Dot dotA, Dot dotB, float length = -1f)
        {
            DotConnection dotConnection = length < 0f 
                ? new DotConnection(dotA, dotB) 
                : new DotConnection(dotA, dotB, length);
            
            dotA.Connections.Add(dotConnection);
            dotB.Connections.Add(dotConnection);
            return dotConnection;
        }

        public static void Disconnect(DotConnection dotConnection)
        {
            List<DotConnection> dotAConnections = dotConnection.DotA.Connections;
            List<DotConnection> dotBConnections = dotConnection.DotB.Connections;
            
            if(dotAConnections.Contains(dotConnection)) dotAConnections.Remove(dotConnection);
            if(dotBConnections.Contains(dotConnection)) dotBConnections.Remove(dotConnection);
        }
    }
}