using Avalonia;
using System;
using System.Globalization;
using System.Numerics;

namespace Blockdiagramm.Renderer
{
    public struct Point<T> : IEquatable<Point<T>>, IEquatable<Point>
        where T : IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable,
        IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>, IComparisonOperators<T, T, bool>, IEqualityOperators<T, T, bool>, IDecrementOperators<T>,
        IDivisionOperators<T, T, T>, IIncrementOperators<T>, IModulusOperators<T, T, T>, IMultiplicativeIdentity<T, T>,
        IMultiplyOperators<T, T, T>, ISubtractionOperators<T, T, T>, IUnaryNegationOperators<T, T>, IUnaryPlusOperators<T, T>, IExponentialFunctions<T>
    {
        private readonly T _x;
        private readonly T _y;

        /// <summary>
        /// Initializes a new instance of the <see cref="Point<typeparamref name="T"/>"/> structure.
        /// </summary>
        /// <param name="x">The X position.</param>
        /// <param name="y">The Y position.</param>
        public Point(T x, T y)
        {
            _x = x;
            _y = y;
        }

        /// <summary>
        /// Gets the X position.
        /// </summary>
        public T X => _x;

        /// <summary>
        /// Gets the Y position.
        /// </summary>
        public T Y => _y;

        /// <summary>
        /// Negates a point.
        /// </summary>
        /// <param name="a">The point.</param>
        /// <returns>The negated point.</returns>
        public static Point<T> operator -(Point<T> a) => new Point<T>(-a._x, -a._y);

        /// <summary>
        /// Checks for equality between two <see cref="Point"/>s.
        /// </summary>
        /// <param name="left">The first point.</param>
        /// <param name="right">The second point.</param>
        /// <returns>True if the points are equal; otherwise false.</returns>
        public static bool operator ==(Point<T> left, Point<T> right) => left.Equals(right);

        /// <summary>
        /// Checks for inequality between two <see cref="Point"/>s.
        /// </summary>
        /// <param name="left">The first point.</param>
        /// <param name="right">The second point.</param>
        /// <returns>True if the points are unequal; otherwise false.</returns>
        public static bool operator !=(Point<T> left, Point<T> right) => !(left == right);

        /// <summary>
        /// Adds two points.
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <returns>A point that is the result of the addition.</returns>
        public static Point<T> operator +(Point<T> a, Point<T> b) => new Point<T>(a._x + b._x, a._y + b._y);

        /// <summary>
        /// Subtracts two points.
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <returns>A point that is the result of the subtraction.</returns>
        public static Point<T> operator -(Point<T> a, Point<T> b) => new Point<T>(a._x - b._x, a._y - b._y);

        /// <summary>
        /// Multiplies a point by a factor coordinate-wise
        /// </summary>
        /// <param name="p">Point<T> to multiply</param>
        /// <param name="k">Factor</param>
        /// <returns>Point<T>s having its coordinates multiplied</returns>
        public static Point<T> operator *(Point<T> p, T k) => new Point<T>(p.X * k, p.Y * k);

        /// <summary>
        /// Multiplies a point by a factor coordinate-wise
        /// </summary>
        /// <param name="p">Point<T> to multiply</param>
        /// <param name="k">Factor</param>
        /// <returns>Point<T>s having its coordinates multiplied</returns>
        public static Point<T> operator *(T k, Point<T> p) => new Point<T>(p.X * k, p.Y * k);

        /// <summary>
        /// Divides a point by a factor coordinate-wise
        /// </summary>
        /// <param name="p">Point<T> to divide by</param>
        /// <param name="k">Factor</param>
        /// <returns>Point<T>s having its coordinates divided</returns>
        public static Point<T> operator /(Point<T> p, T k) => new Point<T>(p.X / k, p.Y / k);

        /// <summary>
        /// Returns a boolean indicating whether the point is equal to the other given point.
        /// </summary>
        /// <param name="other">The other point to test equality against.</param>
        /// <returns>True if this point is equal to other; False otherwise.</returns>
        public bool Equals(Point<T> other) =>
            // ReSharper disable CompareOfFloatsByEqualityOperator
            _x == other._x &&
                   _y == other._y;// ReSharper enable CompareOfFloatsByEqualityOperator

        public bool Equals(Point other)
        {
            Point p = new(_x.ToDouble(null), _y.ToDouble(null));
            return p.Equals(other);
        }

        /// <summary>
        /// Checks for equality between a point and an object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// True if <paramref name="obj"/> is a point that equals the current point.
        /// </returns>
        public override bool Equals(object? obj) => obj is Point<T> other && Equals(other);

        /// <summary>
        /// Returns a hash code for a <see cref="Point"/>.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode() => HashCode.Combine(_x, _y);

        /// <summary>
        /// Returns the string representation of the point.
        /// </summary>
        /// <returns>The string representation of the point.</returns>
        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "{0}, {1}", _x, _y);

        /// <summary>
        /// Returns a new point with the specified X coordinate.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <returns>The new point.</returns>
        public Point<T> WithX(T x) => new Point<T>(x, _y);

        /// <summary>
        /// Returns a new point with the specified Y coordinate.
        /// </summary>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The new point.</returns>
        public Point<T> WithY(T y) => new Point<T>(_x, y);

        /// <summary>
        /// Deconstructs the point into its X and Y coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        public void Deconstruct(out T x, out T y)
        {
            x = _x;
            y = _y;
        }
    }
}
