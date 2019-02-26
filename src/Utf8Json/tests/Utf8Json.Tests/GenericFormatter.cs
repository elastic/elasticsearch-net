using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Utf8Json.Tests
{
    public class GenericFormatters
    {
        T Convert<T>(T value)
        {
            return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(value));
        }

        public static object[] tupleTestData = new object[]
        {
            new object[] { Tuple.Create(1) },
            new object[] { Tuple.Create(1,2) },
            new object[] { Tuple.Create(1,2,3) },
            new object[] { Tuple.Create(1,2,3,4) },
            new object[] { Tuple.Create(1,2,3,4,5) },
            new object[] { Tuple.Create(1,2,3,4,5,6) },
            new object[] { Tuple.Create(1,2,3,4,5,6,7) },
            new object[] { Tuple.Create(1,2,3,4,5,6,7,8) },
        };

        [Theory]
        [MemberData(nameof(tupleTestData))]
        public void TupleTest<T>(T data)
        {
            Convert(data).IsStructuralEqual(data);
        }

        public static object[] valueTupleTestData = new object[]
        {
            new object[] { ValueTuple.Create(1),null },
            new object[] { ValueTuple.Create(1,2),null },
            new object[] { ValueTuple.Create(1,2,3),null },
            new object[] { ValueTuple.Create(1,2,3,4),null },
            new object[] { ValueTuple.Create(1,2,3,4,5) ,null},
            new object[] { ValueTuple.Create(1,2,3,4,5,6) ,null},
            new object[] { ValueTuple.Create(1,2,3,4,5,6,7) ,null},
            new object[] { ValueTuple.Create(1,2,3,4,5,6,7,8) ,null},
        };

        [Theory]
        [MemberData(nameof(valueTupleTestData))]
        public void TupleTest<T>(T data, T? @nullData)
            where T : struct
        {
            Convert(data).IsStructuralEqual(data);
            Convert(@nullData).IsNull();
        }

        public static object[] keyValuePairData = new object[]
        {
            new object[] { new KeyValuePair<int, int>(1,2), null },
            new object[] { new KeyValuePair<int, int>(3,4), new KeyValuePair<int, int>(5,6) },
        };

        [Theory]
        [MemberData(nameof(keyValuePairData))]
        public void KeyValuePairTest<T>(T t, T? t2)
            where T : struct
        {
            Convert(t).IsStructuralEqual(t);
            Convert(t2).IsStructuralEqual(t2);
        }

        public static object[] byteArraySegementData = new object[]
        {
            new object[] { new ArraySegment<byte>(new byte[] { 0, 0, 1, 2, 3 }, 2, 3), null, new byte[] { 1, 2, 3 }  },
            new object[] { new ArraySegment<byte>(new byte[0], 0, 0), null, new byte[0] },
        };

        [Theory]
        [MemberData(nameof(byteArraySegementData))]
        public void ByteArraySegmentTest(ArraySegment<byte> t, ArraySegment<byte>? t2, byte[] reference)
        {
            JsonSerializer.Serialize(t).Is(JsonSerializer.Serialize(reference));
            Convert(t).Array.Is(reference);
            // MessagePackBinary.IsNil(JsonSerializer.Serialize(t2), 0).IsTrue();
        }


    }
}
