using System.Globalization;

namespace CSGSI
{
    /// <summary>
    /// A 3 dimensional vector.
    /// </summary>
    public struct Vector3
    {
        /// <summary>
        /// The X component of the vector (left/right).
        /// </summary>
        public double X;

        /// <summary>
        /// The Y component of the vector (up/down).
        /// </summary>
        public double Y;

        /// <summary>
        /// The Z component of the vector (forward/backward).
        /// </summary>
        public double Z;

        /// <summary>
        /// Returns a Vector3 instance with X, Y and Z set to 0.
        /// </summary>
        public static Vector3 Empty => new Vector3(0, 0, 0);

        /// <summary>
        /// Initializes a new vector with the given X, Y and Z components.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Initializes a new <see cref="Vector3"/> from the given component string.
        /// </summary>
        /// <param name="components">A string in the format "x,y,z". Whitespace is permitted.</param>
        /// <returns>A new <see cref="Vector3"/> with the given component values.</returns>
        public static Vector3 FromComponentString(string components)
        {
            string[] xyz = components.Split(',');
            if (xyz.Length != 3)
                return Vector3.Empty;

            if (double.TryParse(xyz[0].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double x) &&
               double.TryParse(xyz[1].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double y) &&
               double.TryParse(xyz[2].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double z))
            {
                return new Vector3(x, y, z);
            }

            return Vector3.Empty;
        }

        /// <summary>
        /// Generates a string formatted like so: <para/>
        /// (X, Y, Z)
        /// </summary>
        /// <returns>The string representation of this vector.</returns>
        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }
}