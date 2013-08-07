using System;

namespace Nest.Dsl.Factory
{
    public static class EqualExtensions
    {
        public static bool IsEqualTo(this double a, double b, double margin)
        {
            return Math.Abs(a - b) < margin;
        }

        public static bool IsEqualTo(this double a, double b)
        {
            return Math.Abs(a - b) < double.Epsilon;
        }
    }

    public class Point
    {
        public double Lat;
        public double Lon;

        public Point()
        {
            
        }

        public Point(double lat, double lon)
        {
            Lat = lat;
            Lon = lon;
        }

        public override bool Equals(object o)
        {
            if (this == o) return true;

            if (o == null || GetType() != o.GetType()) return false;

            var point = (Point)o;

            if (!point.Lat.IsEqualTo(Lat)) return false;
            if (!point.Lon.IsEqualTo(Lon)) return false;

            return true;
        }

        public override int GetHashCode()
        {
            var temp = Math.Abs(Lat - +0.0d) > double.Epsilon ? BitConverter.DoubleToInt64Bits(Lat) : 0L;
            var result = (int)(temp ^ (temp >> 32));
            temp = Math.Abs(Lon - +0.0d) > double.Epsilon ? BitConverter.DoubleToInt64Bits(Lon) : 0L;
            result = 31 * result + (int)(temp ^ (temp >> 32));
            return result;
        }

        public override string ToString()
        {
            return "[" + Lat + ", " + Lon + "]";
        }
    }
}