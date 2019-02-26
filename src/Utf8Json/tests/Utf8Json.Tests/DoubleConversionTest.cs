using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Utf8Json.Internal.DoubleConversion;
using Xunit;
using Utf8Json.Internal;

namespace Utf8Json.Tests
{
    public class DoubleConversionTest
    {
        public static bool Approximately(float a, float b)
        {
            return Math.Abs(b - a) < Math.Max(1E-06f * Math.Max(Math.Abs(a), Math.Abs(b)), float.Epsilon * 8f);
        }
        public static bool Approximately(double a, double b)
        {
            return Math.Abs(b - a) < Math.Max(1E-06 * Math.Max(Math.Abs(a), Math.Abs(b)), double.Epsilon * 8.0);
        }

        static string GetString(double v)
        {
            byte[] buf = null;
            var len = NumberConverter.WriteDouble(ref buf, 0, v);
            return Encoding.UTF8.GetString(buf, 0, len);
        }

        static string GetString(float v)
        {
            byte[] buf = null;
            var len = NumberConverter.WriteSingle(ref buf, 0, v);
            return Encoding.UTF8.GetString(buf, 0, len);
        }

        [Fact]
        public void Double()
        {
            // testdatagen, https://github.com/ufcpp/UfcppSample/blob/master/Demo/2017/TypeRepositoryBenchmarks/Grisu3DoubleConversion/TestData.cs

            var r = new Random();
            var n = 10000;

            var x = new double[3 * n + 1];
            x[0] = 2e15;
            var i = 1;
            for (; i <= n; i++) x[i] = r.Next() * Math.Pow(10, r.Next(1, 15));
            for (; i <= 2 * n; i++) x[i] = (2 * r.NextDouble() - 1) * Math.Pow(10, r.Next(-300, 300));
            for (; i <= 3 * n; i++) x[i] = (2 * r.NextDouble() - 1) * Math.Pow(10, r.Next(-5, 15));

            foreach (var item in x.Concat(new[] { double.Epsilon, double.MaxValue, double.MinValue }))
            {
                var actual = GetString(item);
                var y = double.Parse(actual);
                var diff = Math.Abs((item - y) / item);
                if (diff > 1E-15) throw new Exception(item + "      :   " + diff.ToString());

                if (!(item == double.MaxValue || item == double.MinValue))
                {
                    var buf = Encoding.UTF8.GetBytes(item.ToString());
                    var buf2 = Enumerable.Range(1, 100).Select(z => (byte)z).Concat(buf).ToArray();
                    var d2 = NumberConverter.ReadDouble(buf2, 100, out var _);
                    Approximately(y, d2).IsTrue();
                }
            }

            // same
            foreach (var item in new[] { double.NaN, double.NegativeInfinity, double.PositiveInfinity, 0.000, })
            {
                GetString(item).Is(item.ToString());
            }

            // has e
            foreach (var item in new[] { 1000000000000000.0, 0.00001 })
            {
                GetString(item).Contains("E").IsTrue();
            }
        }

        [Fact]
        public void Float()
        {
            var r = new Random();
            var n = 10000;

            var y = new float[3 * n + 1];
            y[0] = 2e7f;
            var i = 1;
            for (; i <= n; i++) y[i] = (float)(r.Next() * Math.Pow(10, r.Next(1, 7)));
            for (; i <= 2 * n; i++) y[i] = (float)((2 * r.NextDouble() - 1) * Math.Pow(10, r.Next(-35, 35)));
            for (; i <= 3 * n; i++) y[i] = (float)((2 * r.NextDouble() - 1) * Math.Pow(10, r.Next(-5, 7)));

            foreach (var item in y.Concat(new[] { float.Epsilon, float.MaxValue, float.MinValue }))
            {
                var actual = GetString(item);
                var y2 = float.Parse(actual);
                var diff = Math.Abs((item - y2) / item);
                if (diff > 2E-7) throw new Exception(item + "      :   " + diff.ToString());

                var buf = Encoding.UTF8.GetBytes(item.ToString());
                var buf2 = Enumerable.Range(1, 100).Select(z => (byte)z).Concat(buf).ToArray();
                var d2 = NumberConverter.ReadSingle(buf2, 100, out var _);
                Approximately(y2, d2).IsTrue();
            }

            // same
            foreach (var item in new[] { float.NaN, float.NegativeInfinity, float.PositiveInfinity, 0.000f, })
            {
                GetString(item).Is(item.ToString());
            }

            // has e
            foreach (var item in new[] { 1000000000000000.0f, 0.00001f })
            {
                GetString(item).Contains("E").IsTrue();
            }
        }
    }
}
