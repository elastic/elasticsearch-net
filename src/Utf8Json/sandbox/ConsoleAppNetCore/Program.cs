using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using Utf8Json;
using Utf8Json.Formatters;
using Utf8Json.ImmutableCollection;
using Utf8Json.Resolvers;
using System.Linq;
using System.Text;


public class FooException : Exception
{
    public int Bar { get; set; }
}

namespace ConsoleAppNetCore
{
    public abstract class JsonDBItem
    {
        public int ID { get { return _id; } }
        private int _id = -1;
    }

    public class Card : JsonDBItem
    {
        public string Name { get { return name; } }
        private string name = "0";
    }


    public class CustomPoint
    {
        public readonly int X;
        public readonly int Y;

        public CustomPoint(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        // used this constructor.
        [SerializationConstructor]
        public CustomPoint(int x)
        {
            this.X = x;
        }
    }

    public enum MyEnum
    {
        Fruit, Orange, Grape
    }

    public class Sample
    {
        public Sample Child { get; set; }
    }

    public class Foo
    {
        [JsonFormatter(typeof(StaticNullableFormatter<DateTime>), typeof(DateTimeFormatter), new[] { "yyyy-mm-dd" })]
        public DateTime MyProperty { get; set; }
    }
    public class MyClassZZZ
    {
        //public int[] foo { get; set; }
        public int z { get; set; }
    }

    public interface IInterface
    {
        int MyProperty { get; set; }
    }

    public abstract class MyAbstract
    {
        public virtual int MyProperty { get; set; }
    }

    public class NonConstructor : MyAbstract
    {
        public override int MyProperty { get; set; }

        NonConstructor()
        {
            Console.WriteLine("private constructor");
        }

        public static NonConstructor Create()
        {
            return new NonConstructor();
        }
    }

    // trying self decoding test, not yet completed...
    public class JsonUtf8Encoding : Encoding
    {
        const int UTF8_ACCEPT = 0;
        const int UTF8_REJECT = 1;

        static readonly ushort[] utf8d = new ushort[]{
          0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, // 00..1f
          0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, // 20..3f
          0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, // 40..5f
          0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, // 60..7f
          1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9, // 80..9f
          7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7, // a0..bf
          8,8,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2, // c0..df
          0xa,0x3,0x3,0x3,0x3,0x3,0x3,0x3,0x3,0x3,0x3,0x3,0x3,0x4,0x3,0x3, // e0..ef
          0xb,0x6,0x6,0x6,0x5,0x8,0x8,0x8,0x8,0x8,0x8,0x8,0x8,0x8,0x8,0x8, // f0..ff
          0x0,0x1,0x2,0x3,0x5,0x8,0x7,0x1,0x1,0x1,0x4,0x6,0x1,0x1,0x1,0x1, // s0..s0
          1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,1,1,1,1,0,1,0,1,1,1,1,1,1, // s1..s2
          1,2,1,1,1,1,1,2,1,2,1,1,1,1,1,1,1,1,1,1,1,1,1,2,1,1,1,1,1,1,1,1, // s3..s4
          1,2,1,1,1,1,1,1,1,2,1,1,1,1,1,1,1,1,1,1,1,1,1,3,1,3,1,1,1,1,1,1, // s5..s6
          1,3,1,1,1,1,1,3,1,3,1,1,1,1,1,1,1,3,1,1,1,1,1,1,1,1,1,1,1,1,1,1, // s7..s8
        };

        public static unsafe uint decode(uint* state, uint* codep, uint @byte)
        {
            uint type = utf8d[@byte];

            *codep = unchecked((uint)unchecked((*state != UTF8_ACCEPT)
                ? (@byte & 0x3fu) | (*codep << 6)
                : (0xff >> (int)type) & (@byte)));

            *state = utf8d[256 + *state * 16 + type];
            return *state;
        }

        public static unsafe int countCodePoints(char* s, int count)
        {
            uint codepoint;
            uint state = 0;

            for (count = 0; *s != '0'; ++s)
            {
                if (decode(&state, &codepoint, *s) != 0)
                {
                    count += 1;
                }
            }

            return count;
        }


        #region decode

        // called from GetString.
        // return CharCount is \" ... \" unescaped.
        public override int GetCharCount(byte[] bytes, int index, int count)
        {
            return 9999;
            //return Encoding.UTF8.GetCharCount(bytes, index, count);
        }

        public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
        {
            throw new NotImplementedException();
        }

        #endregion


        public override int GetMaxByteCount(int charCount)
        {
            throw new NotImplementedException();
        }

        public override int GetMaxCharCount(int byteCount)
        {
            throw new NotImplementedException();
        }
        public override int GetByteCount(char[] chars, int index, int count)
        {
            throw new NotImplementedException();
        }

        public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
        {
            throw new NotImplementedException();
        }
    }

    public class Dto
    {
        public bool t;
    }




    public class Test
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
    }

    public class MyEnumType
    {
        public MyEnum YeaHore { get; set; }
    }

    public class Program
    {
        public const char HighSurrogateStart = '\ud800';
        public const char HighSurrogateEnd = '\udbff';
        public const char LowSurrogateStart = '\udc00';
        public const char LowSurrogateEnd = '\udfff';

        public static uint ToCodePoint(byte[] utf8Bytes, out int readSize)
        {
            var index = 0;
            var x1 = utf8Bytes[index];
            if (x1 <= 0x7F)
            {
                readSize = 1;
                return x1;
            }
            else
            {
                if (x1 < 0xC2)
                {
                    readSize = 1;
                    goto ERROR;
                }

                if (x1 <= 0xDF)
                {
                    readSize = 2;
                    var x2 = utf8Bytes[index + 1];
                    if (0x80 <= x2 && x2 <= 0xBF)
                    {
                        return ((uint)(x1 & 0x1F) << 6) | (uint)(x2 & 0x3F);
                    }
                }
                else if (x1 <= 0xEF)
                {
                    readSize = 3;
                    var x2 = utf8Bytes[index + 1];
                    if (0x80 <= x2 && x2 <= 0xBF)
                    {
                        var x3 = utf8Bytes[index + 2];
                        if (0x80 <= x3 && x3 <= 0xBF)
                        {
                            return ((uint)(x1 & 0x0F) << 12) | (uint)((x2 & 0x3F) << 6) | (uint)(x3 & 0x3F);
                        }
                    }
                }
                else if (x1 <= 0xF4)
                {
                    readSize = 4;
                    var x2 = utf8Bytes[index + 1];
                    if (0x80 <= x2 && x2 <= 0xBF)
                    {
                        var x3 = utf8Bytes[index + 2];
                        if (0x80 <= x3 && x3 <= 0xBF)
                        {
                            var x4 = utf8Bytes[index + 3];
                            if (0x80 <= x4 && x4 <= 0xBF)
                            {
                                return ((uint)(x1 & 0x07) << 18) | (uint)((x2 & 0x3F) << 12) | (uint)((x3 & 0x3F) << 6) | (uint)(x4 & 0x3F);
                            }
                        }
                    }
                }

                readSize = 1;
            }

            ERROR:
            return 0xFFFD;
        }

        //public static char[] ToUtf16(uint unicodeCodePoint)
        //{
        //    if (0xD800 <= unicodeCodePoint && unicodeCodePoint <= 0xDFFF)
        //    {
        //        // surrogate pair

        //    }
        //    else if (unicodeCodePoint < 0xFFFF)
        //    {
        //        // BMP
        //        return (char)unicodeCodePoint;
        //    }

        //    return (char)0xFFFD;
        //}

        public class MyClass
        {
            public int Id { get; set; }
        }

        public class A
        {
            public A()
            {

            }
        }

        public class B
        {

        }

        public class C : A
        {
        }

        public class D
        {
            public D()
            {
                Console.WriteLine("go");
            }
        }

        public class E
        {
            int x;

            public E()
            {
                x = 9;
            }
        }

        public abstract class TestBase
        {
            public Guid Id { get; protected set; }
        }

        public class MyTest : TestBase
        {
            public void SetId(Guid id)
            {
                Id = id;
            }
        }

        public sealed class Entry
        {
            public int Number { get; set; }
            public Exception Exception { get; set; }
        }


        static unsafe void Main(string[] args)
        {

            var huga = JsonSerializer.Serialize<Card>(new Card());
            

        }

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

        static void TestDTUtcNow()
        {
            Console.WriteLine("DateTime.UtcNow");
            var dto = DateTime.UtcNow;
            var serialized = Utf8Json.JsonSerializer.ToJsonString(dto);
            var deSerialized = Utf8Json.JsonSerializer.Deserialize<DateTime>(serialized);
            var serialized2 = Utf8Json.JsonSerializer.ToJsonString(deSerialized);
            if (serialized2 != serialized)
            {
                Console.WriteLine("Reference: {0:yyyy-MM-ddTHH:mm:ss.fffffffK}", dto);
                Console.WriteLine("Output: {0} vs {1}", serialized, serialized2);
            }
        }


        static void TestDTOUtcNow()
        {
            Console.WriteLine("DateTimeOffset.UtcNow");
            var dto = DateTimeOffset.UtcNow;
            var serialized = Utf8Json.JsonSerializer.ToJsonString(dto);
            var deSerialized = Utf8Json.JsonSerializer.Deserialize<DateTimeOffset>(serialized);
            var serialized2 = Utf8Json.JsonSerializer.ToJsonString(deSerialized);
            if (serialized2 != serialized)
            {
                Console.WriteLine("Reference: {0:yyyy-MM-ddTHH:mm:ss.fffffffK}", dto);
                Console.WriteLine("Output: {0} vs {1}", serialized, serialized2);
            }
        }

        static void TestDTNow()
        {
            Console.WriteLine("DateTime.Now");
            var dto = DateTime.Now;
            var serialized = Utf8Json.JsonSerializer.ToJsonString(dto);
            var deSerialized = Utf8Json.JsonSerializer.Deserialize<DateTime>(serialized);
            var serialized2 = Utf8Json.JsonSerializer.ToJsonString(deSerialized);
            if (serialized2 != serialized)
            {
                Console.WriteLine("Reference: {0:yyyy-MM-ddTHH:mm:ss.fffffffK}", dto);
                Console.WriteLine("Output: {0} vs {1}", serialized, serialized2);
            }
        }


        static void TestDTONow()
        {
            Console.WriteLine("DateTimeOffset.Now");
            var dto = DateTimeOffset.Now;
            var serialized = Utf8Json.JsonSerializer.ToJsonString(dto);
            var deSerialized = Utf8Json.JsonSerializer.Deserialize<DateTimeOffset>(serialized);
            var serialized2 = Utf8Json.JsonSerializer.ToJsonString(deSerialized);
            if (serialized2 != serialized)
            {
                Console.WriteLine("Reference: {0:yyyy-MM-ddTHH:mm:ss.fffffffK}", dto);
                Console.WriteLine("Output: {0} vs {1}", serialized, serialized2);
            }
        }
    }

    public class Person
    {
        public int Age { get; set; }
        public string Name { get; set; }

        [JsonFormatter(typeof(DateTimeFormatter), "yyyy-MM-dd")]
        public DateTime Birth { get; set; }
    }


    public class FileInfoFormatter<T> : IJsonFormatter<FileInfo>
    {
        public void Serialize(ref JsonWriter writer, FileInfo value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            // if target type is primitive, you can also use writer.Write***.
            formatterResolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.FullName, formatterResolver);
        }

        public FileInfo Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            // if target type is primitive, you can also use reader.Read***.
            var path = formatterResolver.GetFormatterWithVerify<string>().Deserialize(ref reader, formatterResolver);
            return new FileInfo(path);
        }
    }


    // if serializing, choosed CustomObjectFormatter.
    [JsonFormatter(typeof(CustomObjectFormatter))]
    public class CustomObject
    {
        string internalId;

        public CustomObject()
        {
            this.internalId = Guid.NewGuid().ToString();
        }

        // serialize/deserialize internal field.
        class CustomObjectFormatter : IJsonFormatter<CustomObject>
        {
            public void Serialize(ref JsonWriter writer, CustomObject value, IJsonFormatterResolver formatterResolver)
            {
                formatterResolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.internalId, formatterResolver);
            }

            public CustomObject Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
            {
                var id = formatterResolver.GetFormatterWithVerify<string>().Deserialize(ref reader, formatterResolver);
                return new CustomObject { internalId = id };
            }
        }
    }



    // create custom composite resolver per project is recommended way.
    // let's try to copy and paste:)
    public class ProjectDefaultResolver : IJsonFormatterResolver
    {
        public static IJsonFormatterResolver Instance = new ProjectDefaultResolver();

        // configure your resolver and formatters.
        static IJsonFormatter[] formatters = new IJsonFormatter[]{
        new DateTimeFormatter("yyyy-MM-dd HH:mm:ss")
    };

        static readonly IJsonFormatterResolver[] resolvers = new[]
        {
        ImmutableCollectionResolver.Instance,
        EnumResolver.UnderlyingValue,
        StandardResolver.AllowPrivateExcludeNullSnakeCase
    };

        ProjectDefaultResolver()
        {
        }

        public IJsonFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly IJsonFormatter<T> formatter;

            static FormatterCache()
            {
                foreach (var item in formatters)
                {
                    foreach (var implInterface in item.GetType().GetTypeInfo().ImplementedInterfaces)
                    {
                        var ti = implInterface.GetTypeInfo();
                        if (ti.IsGenericType && ti.GenericTypeArguments[0] == typeof(T))
                        {
                            formatter = (IJsonFormatter<T>)item;
                            return;
                        }
                    }
                }

                foreach (var item in resolvers)
                {
                    var f = item.GetFormatter<T>();
                    if (f != null)
                    {
                        formatter = f;
                        return;
                    }
                }
            }
        }
    }
}
