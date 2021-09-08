namespace VerletSimulation
{
    public class DotConnection
    {
        public Dot DotA { get; }
        public Dot DotB { get; }
        public float Length { get; }

        public DotConnection(Dot dotA, Dot dotB, float length)
        {
            DotA = dotA;
            DotB = dotB;
            Length = length;
        }
        
        public DotConnection(Dot dotA, Dot dotB)
        {
            DotA = dotA;
            DotB = dotB;
            Length = (dotA.CurrentPosition - dotB.CurrentPosition).magnitude;
        }

        public Dot Other(Dot dot) => dot == DotA ? DotB : DotA;
    }
}