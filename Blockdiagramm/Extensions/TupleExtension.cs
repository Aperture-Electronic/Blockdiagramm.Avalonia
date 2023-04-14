namespace Blockdiagramm.Extensions
{
    public static class TupleExtension
    {
        public static Avalonia.Point ToPoint(this (double x, double y) p)
            => new(p.x, p.y);

        public static (double, double) MinMax(this (double x, double y) n)
            => n.x > n.y ? (n.y, n.x) : (n.x, n.y);
    }
}
