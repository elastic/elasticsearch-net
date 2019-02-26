using System;
using Xunit;
using System.Collections.Generic;
using System.Text;

namespace Utf8Json.Tests
{
    public class ReadIsInTest
    {
        static (int, int[]) Array(int[] xs)
        {
            var list = new List<int>();
            var json = JsonSerializer.Serialize(xs);
            var reader = new JsonReader(json);
            var c = 0;
            while (reader.ReadIsInArray(ref c))
            {
                list.Add(reader.ReadInt32());
            }
            return (c, list.ToArray());
        }

        static (int, Dictionary<string, int>) Object(object xs)
        {
            var list = new Dictionary<string, int>();
            var json = JsonSerializer.Serialize(xs);
            var reader = new JsonReader(json);
            var c = 0;
            while (reader.ReadIsInObject(ref c))
            {
                var k = reader.ReadPropertyName();
                var v = reader.ReadInt32();
                list.Add(k, v);
            }
            return (c, list);
        }

        [Fact]
        public void IsInArrayTest()
        {
            var empty = Array(new int[0]);
            var one = Array(new int[] { 1 });
            var five = Array(new int[] { 1, 10, 100, 1000, 10000 });

            empty.Item1.Is(0); empty.Item2.Length.Is(0);
            one.Item1.Is(1); one.Item2.Is(1);
            five.Item1.Is(5); five.Item2.Is(1, 10, 100, 1000, 10000);
        }

        [Fact]
        public void IsInObjectTest()
        {
            var empty = Object(new { });
            var one = Object(new { OK = 1 });
            var five = Object(new { A = 1, B = 10, C = 100, D = 1000, E = 10000 });

            empty.Item1.Is(0); empty.Item2.Count.Is(0);
            one.Item1.Is(1); one.Item2.Count.Is(1);
            one.Item2["OK"].Is(1);

            five.Item1.Is(5); five.Item2.Count.Is(5);
            five.Item2["A"].Is(1);
            five.Item2["B"].Is(10);
            five.Item2["C"].Is(100);
            five.Item2["D"].Is(1000);
            five.Item2["E"].Is(10000);
        }
    }
}
