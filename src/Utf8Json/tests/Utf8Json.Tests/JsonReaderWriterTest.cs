using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using Xunit;

namespace Utf8Json.Tests
{
    public class JsonReaderWriterTest
    {
        JsonReader SameAsReference<T>(T target, ArraySegment<byte> result)
        {
            var reference = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(target));
            result.ToArray().Is(reference);

            return new JsonReader(result.Array, result.Offset);
        }
        /*
        [Fact]
        public void NullTest()
        {
            var writer = new JsonWriter();
            writer.WriteNull();

            var reader = SameAsReference((object)null, writer.GetBuffer());

            reader.ReadIsNull().IsTrue();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void BoolTest(bool target)
        {
            var writer = new JsonWriter();
            writer.WriteBoolean(target);

            var reader = SameAsReference(target, writer.GetBuffer());

            reader.ReadBoolean().Is(target);
        }

        [Theory]
        [InlineData(byte.MinValue)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(99)]
        [InlineData(100)]
        [InlineData(101)]
        [InlineData(byte.MaxValue)]
        public void ByteTest(byte target)
        {
            var writer = new JsonWriter();
            writer.WriteByte(target);

            var reader = SameAsReference(target, writer.GetBuffer());

            reader.ReadByte().Is(target);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(12)]
        [InlineData(123)]
        [InlineData(12345)]
        [InlineData(123456)]
        [InlineData(1234567)]
        [InlineData(12345678)]
        [InlineData(123456789)]
        [InlineData(1234567890)]
        [InlineData(12345678901)]
        [InlineData(123456789012)]
        [InlineData(1234567890123)]
        [InlineData(12345678901234)]
        [InlineData(123456789012345)]
        [InlineData(1234567890123456)]
        [InlineData(12345678901234567)]
        [InlineData(123456789012345678)]
        [InlineData(1234567890123456789)]
        [InlineData(12345678901234567890)]
        [InlineData(ulong.MaxValue)]
        public void UInt64Test(ulong target)
        {
            var writer = new JsonWriter();
            writer.WriteUInt64(target);

            var reader = SameAsReference(target, writer.GetBuffer());

            reader.ReadUInt64().Is(target);
        }

        [Theory]
        [InlineData(long.MinValue)]
        [InlineData(-1234567890123456789)]
        [InlineData(-123456789012345678)]
        [InlineData(-12345678901234567)]
        [InlineData(-1234567890123456)]
        [InlineData(-123456789012345)]
        [InlineData(-12345678901234)]
        [InlineData(-1234567890123)]
        [InlineData(-123456789012)]
        [InlineData(-12345678901)]
        [InlineData(-1234567890)]
        [InlineData(-123456789)]
        [InlineData(-12345678)]
        [InlineData(-1234567)]
        [InlineData(-123456)]
        [InlineData(-12345)]
        [InlineData(-1234)]
        [InlineData(-123)]
        [InlineData(-12)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(12)]
        [InlineData(123)]
        [InlineData(12345)]
        [InlineData(123456)]
        [InlineData(1234567)]
        [InlineData(12345678)]
        [InlineData(123456789)]
        [InlineData(1234567890)]
        [InlineData(12345678901)]
        [InlineData(123456789012)]
        [InlineData(1234567890123)]
        [InlineData(12345678901234)]
        [InlineData(123456789012345)]
        [InlineData(1234567890123456)]
        [InlineData(12345678901234567)]
        [InlineData(123456789012345678)]
        [InlineData(1234567890123456789L)]
        [InlineData(long.MaxValue)]
        public void Int64Test(long target)
        {
            var writer = new JsonWriter();
            writer.WriteInt64(target);

            var reader = SameAsReference(target, writer.GetBuffer());

            reader.ReadInt64().Is(target);
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(-120)]
        [InlineData(10)]
        [InlineData(byte.MaxValue)]
        [InlineData(sbyte.MaxValue)]
        [InlineData(short.MaxValue)]
        [InlineData(int.MaxValue)]
        [InlineData(long.MaxValue)]
        [InlineData(ushort.MaxValue)]
        [InlineData(uint.MaxValue)]
        [InlineData(ulong.MaxValue)]
        public void FloatTest<T>(T value)
        {
            var bin = JsonSerializer.Serialize(value);
            JsonSerializer.Deserialize<float>(bin).Is(Convert.ToSingle(value));
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(-120)]
        [InlineData(10)]
        [InlineData(byte.MaxValue)]
        [InlineData(sbyte.MaxValue)]
        [InlineData(short.MaxValue)]
        [InlineData(int.MaxValue)]
        [InlineData(long.MaxValue)]
        [InlineData(ushort.MaxValue)]
        [InlineData(uint.MaxValue)]
        [InlineData(ulong.MaxValue)]
        public void DoubleTest<T>(T value)
        {
            var bin = JsonSerializer.Serialize(value);
            JsonSerializer.Deserialize<double>(bin).Is(Convert.ToDouble(value));
        }

        [InlineData("\"xyzzy\"")]
        [InlineData("/xyzzy/")]
        [InlineData("'xyzzy'")]
        [InlineData("\r\nxyzzy\r\n")]
        [InlineData("xyz\r\nzy")]
        [InlineData("xy\"zzy")]
        [InlineData("xy\"zz\"y")]
        [Theory]
        public void StringEscaping(string v)
        {
            var js = JsonSerializer.Serialize<string>(v);
            var ok = JsonSerializer.Deserialize<string>(js);
            ok.Is(v);
        }
        */
        [Fact]
        public void LargeString()
        {
            var origstr = new string('a', 99999);
            var str = "\\u0313" + origstr;
            str = "\"" + str + "\"";

            var reader = new JsonReader(Encoding.UTF8.GetBytes(str), 0);
            var aaa = reader.ReadString();

            aaa.Is("\u0313" + origstr);
        }


        [Fact]
        public void LargeString2()
        {
            var origstr = new string('a', 99999);
            var str = "\"" + origstr + "\"";

            var reader = new JsonReader(Encoding.UTF8.GetBytes(str), 0);
            var aaa = reader.ReadString();

            aaa.Is(origstr);
        }

        [Fact]
        public void LargeString3()
        {
            var origstr = new string('a', 999999);
            var str = "\"" + origstr + "\"";

            var reader = new JsonReader(Encoding.UTF8.GetBytes(str), 0);
            var aaa = reader.ReadString();

            aaa.Is(origstr);
        }

        [Fact]
        public void LargeString4()
        {
            var origstr = new string('a', 999999);
            var str = "\"" + origstr + "\"";

            var serialized = JsonSerializer.Serialize(str);
            var deserialized = JsonSerializer.Deserialize<string>(serialized);
        }

        [Fact]
        public void LargeArray()
        {
            var array = Enumerable.Range(1, 100000).ToArray();
            var bin = JsonSerializer.Serialize(array);

            var reader = new JsonReader(bin);
            reader.ReadNextBlock();

            // ok, can read.
            reader.GetCurrentOffsetUnsafe().Is(bin.Length);
        }

        [Fact]
        public void LargeNestedArraySkip()
        {
            var array = Enumerable.Range(1, 100000).Select(x => new int[0]).ToArray();
            var bin = JsonSerializer.Serialize(array);

            var reader = new JsonReader(bin);
            reader.ReadNextBlock();

            // ok, can read.
            reader.GetCurrentOffsetUnsafe().Is(bin.Length);
        }
    }
}
