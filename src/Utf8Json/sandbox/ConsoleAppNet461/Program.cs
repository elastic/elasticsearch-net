using System.Security;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CsProj;
using System;
using System.IO;
using System.Text;
using Utf8Json;
using Utf8Json.Formatters;
using Utf8Json.Internal;
using System.Collections.Generic;
using MessagePack.Resolvers;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Utf8Json.Resolvers;


// [assembly: AllowPartiallyTrustedCallers]
// [assembly: SecurityTransparent]
[assembly: SecurityRules(SecurityRuleSet.Level2, SkipVerificationInFullTrust = true)]

class Program
{
    static void Main(string[] args)
    {
        var switcher = new BenchmarkSwitcher(new[]
        {
            typeof(AssemblyVsDynamicMethod),
            typeof(SerializeCheck),
            typeof(DeserializeCheck),
            typeof(DoubleConvertBenchmark),
            typeof(StringToDoubleBenchmark),
            typeof(SwitchVsIf),
            typeof(SwitchVsSwitch),
        });

        //args = new string[] { "0" };

#if DEBUG


// {"Name":"foo"}
JsonSerializer.ToJsonString(new MyPerson { Name = "foo", Addresses = new string[0] });
        
// {"Name":"bar","Addresses":["tokyo","kyoto"]}
JsonSerializer.ToJsonString(new MyPerson { Name = "bar", Addresses = new[] { "tokyo", "kyoto" } });


#else
        switcher.Run(args);
#endif
    }
}

public class Hoge
{
    public string name;
    public string Name { get { return name; } }
}


public class MyPerson
{
    public string Name { get; set; }
    public string[] Addresses { get; set; }

    // ShouldSerialize*membername**
    // method must be `public` and return `bool` and parameter less.
    public bool ShouldSerializeAddresses()
    {
        if (Addresses != null && Addresses.Length != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class Hoge2
{
    public string name;
    public string _Name { get { return name; } }

    public Hoge2(string name)
    {
        this.name = name;
    }
}


public class Person
{
    public int Age { get; set; }
    public string Name { get; set; }
}

public class Person2
{
    public int? Age { get; set; }
    public string Name { get; set; }
}
public interface IInterface
{
    string Huga { get; }
}

public class MyClassInter : IInterface
{
    [System.Runtime.Serialization.DataMember]
    public string Huga { get; set; }
}

public class MyResolver2 : IJsonFormatterResolver
{
    // set your own composite formatter/resolvers.
    static IJsonFormatter[] formatters = new IJsonFormatter[] { };
    static IJsonFormatterResolver[] resolvers = new IJsonFormatterResolver[] { };

    public IJsonFormatter<T> GetFormatter<T>()
    {
        return Cache<T>.formatter;
    }

    static class Cache<T>
    {
        public static readonly IJsonFormatter<T> formatter;

        static Cache()
        {
            foreach (var item in formatters)
            {
                // using System.Reflection;
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

public enum MyEnum : int
{
    Apple = 100,
    Orange = 3500
}

public class SimplePerson
{
    [DataMember(Name = "i_do\tnt_know")]
    public int Age { get; set; }
    public string FirstName { get; set; }
    [IgnoreDataMember]
    public string LastName { get; set; }
    public MyEnum FavoriteFruit { get; set; }

    //readonly int f;
    //readonly int z;


    public SimplePerson()
    {
        
    }

    public SimplePerson(bool b)
    {
        //z = 1234;
    }

    public SimplePerson(int f)
    {
        //this.f = f;
    }

    public void ShoutFAndZ()
    {
        //Console.WriteLine((f, z));
    }
}

public class MyResolver : IJsonFormatterResolver
{
    SimplePersonFormatter f = new SimplePersonFormatter();

    public IJsonFormatter<T> GetFormatter<T>()
    {
        return (IJsonFormatter<T>)(object)f;
    }
}

[MessagePack.MessagePackObject]
public class SimplePersonMsgpack
{
    [MessagePack.Key(0)]
    public int Age { get; set; }
    [MessagePack.Key(1)]
    public string FirstName { get; set; }
    [MessagePack.Key(2)]
    public string LastName { get; set; }
}


public class BenchmarkConfig : ManualConfig
{
    public BenchmarkConfig()
    {
        Add(MarkdownExporter.GitHub);
        Add(MemoryDiagnoser.Default);

        var baseJob = Job.ShortRun.WithLaunchCount(1).WithTargetCount(1).WithWarmupCount(1);
        Add(baseJob.With(Runtime.Clr).With(Jit.RyuJit).With(Platform.X64));
        // Add(baseJob.WithLaunchCount(1).WithTargetCount(1).WithWarmupCount(1).With(Runtime.Core).With(CsProjCoreToolchain.NetCoreApp20));
    }
}

[Config(typeof(BenchmarkConfig))]
public class DoubleConvertBenchmark
{
    const double value = 12345.6789;

    public DoubleConvertBenchmark()
    {

    }

    //[Benchmark]
    //public byte[] DoubleToStringConverter()
    //{
    //    byte[] buf = new byte[20];
    //    Utf8Json.Internal.DoubleConversion.DoubleToStringConverter.GetBytes(ref buf, 0, value);
    //    return buf;
    //}

    //[Benchmark]
    //public string DoubleToStringConverterToString()
    //{
    //    return Utf8Json.Internal.DoubleConversion.DoubleToStringConverter.GetString(value);
    //}

    [Benchmark]
    public byte[] StandardToStringUtf8()
    {
        return Encoding.UTF8.GetBytes(value.ToString());
    }

    [Benchmark]
    public string StandardToString()
    {
        return value.ToString();
    }
}


[Config(typeof(BenchmarkConfig))]
public class StringToDoubleBenchmark
{
    const double value = 12345.6789;
    static readonly byte[] strBytes = Encoding.UTF8.GetBytes(value.ToString());
    static readonly string str = value.ToString();

    public StringToDoubleBenchmark()
    {

    }

    [Benchmark]
    public double DoubleToStringConverter()
    {
        return NumberConverter.ReadDouble(strBytes, 0, out var _);
    }

    [Benchmark]
    public double DoubleParse()
    {
        return Double.Parse(str);
    }

    [Benchmark]
    public Double DoubleParseWithDecode()
    {
        return Double.Parse(Encoding.UTF8.GetString(strBytes));
    }


}



[Config(typeof(BenchmarkConfig))]
public class SwitchVsIf
{
    byte c = byte.Parse("8");

    [Benchmark]
    public bool SwitchOpt()
    {
        switch (c)
        {
            case (byte)'0':
            case (byte)'1':
            case (byte)'2':
            case (byte)'3':
            case (byte)'4':
            case (byte)'5':
            case (byte)'6':
            case (byte)'7':
            case (byte)'8':
            case (byte)'9':
                return true;
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
            case 15:
            case 16:
            case 17:
            case 18:
            case 19:
            case 20:
            case 21:
            case 22:
            case 23:
            case 24:
            case 25:
            case 26:
            case 27:
            case 28:
            case 29:
            case 30:
            case 31:
            case 32:
            case 33:
            case 34:
            case 35:
            case 36:
            case 37:
            case 38:
            case 39:
            case 40:
            case 41:
            case 42:
            case 43:
            case 44:
            case 45:
            case 46:
            case 47:
            default:
                return false;
        }
    }

    [Benchmark]
    public bool StandardSwitch()
    {
        switch (c)
        {
            case (byte)'0':
            case (byte)'1':
            case (byte)'2':
            case (byte)'3':
            case (byte)'4':
            case (byte)'5':
            case (byte)'6':
            case (byte)'7':
            case (byte)'8':
            case (byte)'9':
                return true;
            default:
                return false;
        }
    }

    [Benchmark]
    public bool If()
    {
        return (byte)'0' <= c && c <= (byte)'9';
    }
}

[Config(typeof(BenchmarkConfig))]
public class SwitchVsSwitch
{
    string value;
    int i;

    public SwitchVsSwitch()
    {
        value = "abcdefghijklmnopqrstu";
        i = int.Parse("5");
    }

    [Benchmark]
    public bool SwitchOptimized()
    {
        switch (value[i])
        {
            case '"':
                return true;
            case '\\':
                return true;
            case '\b':
                return true;
            case '\f':
                return true;
            case '\n':
                return true;
            case '\r':
                return true;
            case '\t':
                return true;
            // use switch jumptable
            case (char)0:
            case (char)1:
            case (char)2:
            case (char)3:
            case (char)4:
            case (char)5:
            case (char)6:
            case (char)7:
            case (char)11:
            case (char)14:
            case (char)15:
            case (char)16:
            case (char)17:
            case (char)18:
            case (char)19:
            case (char)20:
            case (char)21:
            case (char)22:
            case (char)23:
            case (char)24:
            case (char)25:
            case (char)26:
            case (char)27:
            case (char)28:
            case (char)29:
            case (char)30:
            case (char)31:
            case (char)32:
            case (char)33:
            case (char)35:
            case (char)36:
            case (char)37:
            case (char)38:
            case (char)39:
            case (char)40:
            case (char)41:
            case (char)42:
            case (char)43:
            case (char)44:
            case (char)45:
            case (char)46:
            case (char)47:
            case (char)48:
            case (char)49:
            case (char)50:
            case (char)51:
            case (char)52:
            case (char)53:
            case (char)54:
            case (char)55:
            case (char)56:
            case (char)57:
            case (char)58:
            case (char)59:
            case (char)60:
            case (char)61:
            case (char)62:
            case (char)63:
            case (char)64:
            case (char)65:
            case (char)66:
            case (char)67:
            case (char)68:
            case (char)69:
            case (char)70:
            case (char)71:
            case (char)72:
            case (char)73:
            case (char)74:
            case (char)75:
            case (char)76:
            case (char)77:
            case (char)78:
            case (char)79:
            case (char)80:
            case (char)81:
            case (char)82:
            case (char)83:
            case (char)84:
            case (char)85:
            case (char)86:
            case (char)87:
            case (char)88:
            case (char)89:
            case (char)90:
            case (char)91:
            default:
                return false;
        }
    }

    [Benchmark]
    public bool SwitchStandard()
    {
        switch (value[i])
        {
            case '"':
                return true;
            case '\\':
                return true;
            case '\b':
                return true;
            case '\f':
                return true;
            case '\n':
                return true;
            case '\r':
                return true;
            case '\t':
                return true;
            default:
                return false;
        }
    }
}


[Config(typeof(BenchmarkConfig))]
public class AssemblyVsDynamicMethod
{
    SimplePerson p = new SimplePerson { Age = 99, FirstName = "foo", LastName = "baz" };

    [Benchmark(Baseline = true)]
    public byte[] DefaultResolver()
    {
        return JsonSerializer.Serialize(p, Utf8Json.Resolvers.StandardResolver.Default);
    }

    [Benchmark()]
    public byte[] AllowPrivate()
    {
        return JsonSerializer.Serialize(p, Utf8Json.Resolvers.StandardResolver.AllowPrivate);
    }
}



[Config(typeof(BenchmarkConfig))]
public class SerializeCheck
{
    byte[] cache = new byte[10000];
    SimplePerson p = new SimplePerson { Age = 99, FirstName = "foo", LastName = "baz" };
    SimplePersonMsgpack p2 = new SimplePersonMsgpack { Age = 99, FirstName = "foo", LastName = "baz" };
    IJsonFormatter<SimplePerson> formatter = new SimplePersonFormatter();
    Encoding utf8 = Encoding.UTF8;

    MyResolver resolver = new MyResolver();

    [Benchmark(Baseline = true)]
    public byte[] Utf8JsonSerializer()
    {
        return JsonSerializer.Serialize(p, resolver);
    }

    [Benchmark]
    public byte[] Utf8JsonSerializer_Generated()
    {
        return JsonSerializer.Serialize(p, Utf8Json.Resolvers.StandardResolver.Default);
    }

    [Benchmark]
    public byte[] MessagePackCSharp()
    {
        return MessagePack.MessagePackSerializer.Serialize(p2);
    }

    [Benchmark]
    public byte[] MessagePackCSharpContractless()
    {
        return MessagePack.MessagePackSerializer.Serialize(p, MessagePack.Resolvers.ContractlessStandardResolver.Instance);
    }

    //[Benchmark]
    //public byte[] _Jil()
    //{
    //    return utf8.GetBytes(Jil.JSON.Serialize(p));
    //}

    //[Benchmark]
    //public void _JilTextWriter()
    //{
    //    using (var ms = new MemoryStream())
    //    using (var sw = new StreamWriter(ms, utf8))
    //    {
    //        Jil.JSON.Serialize(p, sw);
    //    }
    //}

    //[Benchmark]
    //public byte[] _JsonNet()
    //{
    //    return utf8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(p));
    //}

    //[Benchmark]
    //public byte[] _NetJson()
    //{
    //    return utf8.GetBytes(NetJSON.NetJSON.Serialize(p));
    //}
}

[Config(typeof(BenchmarkConfig))]
public class DeserializeCheck
{
    byte[] json = new SerializeCheck().Utf8JsonSerializer();
    byte[] msgpack1 = new SerializeCheck().MessagePackCSharp();
    byte[] msgpack2 = new SerializeCheck().MessagePackCSharpContractless();
    IJsonFormatter<SimplePerson> formatter = new SimplePersonFormatter();
    MyResolver resolver = new MyResolver();

    Encoding utf8 = Encoding.UTF8;

    [Benchmark(Baseline = true)]
    public SimplePerson SugoiJsonSerializer()
    {
        return JsonSerializer.Deserialize<SimplePerson>(json, resolver);
    }

    [Benchmark]
    public SimplePersonMsgpack MessagePackCSharp()
    {
        return MessagePack.MessagePackSerializer.Deserialize<SimplePersonMsgpack>(msgpack1);
    }

    [Benchmark]
    public SimplePerson MessagePackCSharpContractless()
    {
        return MessagePack.MessagePackSerializer.Deserialize<SimplePerson>(msgpack2, MessagePack.Resolvers.ContractlessStandardResolver.Instance);
    }

    [Benchmark]
    public SimplePerson _Jil()
    {
        return Jil.JSON.Deserialize<SimplePerson>(utf8.GetString(json));
    }

    [Benchmark]
    public SimplePerson _JilTextReader()
    {
        using (var ms = new MemoryStream(json))
        using (var sr = new StreamReader(ms, utf8))
        {
            return Jil.JSON.Deserialize<SimplePerson>(sr);
        }
    }

    [Benchmark]
    public SimplePerson _JsonNet()
    {
        return Newtonsoft.Json.JsonConvert.DeserializeObject<SimplePerson>(utf8.GetString(json));
    }

    [Benchmark]
    public SimplePerson _NetJson()
    {
        return NetJSON.NetJSON.Deserialize<SimplePerson>(utf8.GetString(json));
    }
}

public class SimplePersonFormatter : IJsonFormatter<SimplePerson>
{
    readonly byte[][] nameCaches;
    readonly byte[][] nameCaches2;
    readonly AutomataDictionary dictionary;

    public SimplePersonFormatter()
    {
        // escaped string byte cache with "{" and ","
        nameCaches = new byte[3][]
        {
            JsonWriter.GetEncodedPropertyNameWithBeginObject("Age"), // {\"Age\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("FirstName"), // ",\"FirstName\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("LastName"), // ",\"LastName\":
        };
        dictionary = new AutomataDictionary
        {
            {  JsonWriter.GetEncodedPropertyNameWithoutQuotation("Age"), 0 },
            {  JsonWriter.GetEncodedPropertyNameWithoutQuotation("FirstName"), 1 },
            {  JsonWriter.GetEncodedPropertyNameWithoutQuotation("LastName"), 2 },
        };

        nameCaches2 = new byte[][]
        {
            JsonWriter.GetEncodedPropertyName("Age"), // 
            JsonWriter.GetEncodedPropertyName("FirstName"), // 
            JsonWriter.GetEncodedPropertyName("LastName"), // 
        };
    }

    public void Serialize(ref JsonWriter writer, SimplePerson value, IJsonFormatterResolver formatterResolver)
    {
        if (value == null) { writer.WriteNull(); return; }

        UnsafeMemory64.WriteRaw7(ref writer, nameCaches[0]); // optimize byte->byte copy we know src size.
        writer.WriteInt32(value.Age);
        UnsafeMemory64.WriteRaw13(ref writer, nameCaches[1]);
        writer.WriteString(value.FirstName);
        UnsafeMemory64.WriteRaw12(ref writer, nameCaches[2]);
        writer.WriteString(value.LastName);

        writer.WriteEndObject();
    }



    public void _SerializePattern2Test(ref JsonWriter writer, SimplePerson value, IJsonFormatterResolver formatterResolver)
    {
        if (value == null) { writer.WriteNull(); return; }

        bool wrote = false;
        writer.WriteBeginObject();
        // if(value.Age != nul)
        {
            if (wrote == false)
            {
                wrote = true;
                writer.WriteValueSeparator();
            }

            UnsafeMemory64.WriteRaw7(ref writer, nameCaches2[0]);
            writer.WriteInt32(value.Age);
        }
        if (value.FirstName != null)
        {
            if (wrote == false)
            {
                wrote = true;
                writer.WriteValueSeparator();
            }

            UnsafeMemory64.WriteRaw13(ref writer, nameCaches2[1]);
            writer.WriteString(value.FirstName);
        }
        if (value.LastName != null)
        {
            if (wrote == false)
            {
                wrote = true;
                writer.WriteValueSeparator();
            }

            UnsafeMemory64.WriteRaw12(ref writer, nameCaches2[2]);
            writer.WriteString(value.LastName);
        }

        writer.WriteEndObject();
    }

    public SimplePerson Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
    {
        if (reader.ReadIsNull()) return null;

        reader.ReadIsBeginObjectWithVerify(); // "{"

        int age = default(int);
        string firstName = default(string);
        string lastName = default(string);

        var count = 0;
        while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref count)) // "}", skip "," when count != 0
        {
            // automata lookup
            var key = reader.ReadPropertyNameSegmentRaw();

            int switchKey;
            if (!dictionary.TryGetValue(key, out switchKey)) switchKey = -1;

            switch (switchKey)
            {
                case 0:
                    age = reader.ReadInt32();
                    break;
                case 1:
                    firstName = reader.ReadString();
                    break;
                case 2:
                    lastName = reader.ReadString();
                    break;
                default:
                    reader.ReadNextBlock();
                    break;
            }
        }

        var result = new SimplePerson() { Age = age, FirstName = firstName, LastName = lastName };
        return result;
    }
}

namespace Utf8Json.Formatters
{

    public class TargetClassContractless
    {
        public sbyte Number1 { get; set; }
        public short Number2 { get; set; }
        public int Number3 { get; set; }
        public long Number4 { get; set; }
        public byte Number5 { get; set; }
        public ushort Number6 { get; set; }
        public uint Number7 { get; set; }
        public ulong Number8 { get; set; }
        //public float Number9 { get; set; }
        //public double Number10 { get; set; }
        public string Str { get; set; }
        public int[] Array { get; set; }

        public TargetClassContractless()
        {

        }

        public TargetClassContractless(TargetClass tc)
        {
            this.Number1 = tc.Number1;
            this.Number2 = tc.Number2;
            this.Number3 = tc.Number3;
            this.Number4 = tc.Number4;
            this.Number5 = tc.Number5;
            this.Number6 = tc.Number6;
            this.Number7 = tc.Number7;
            this.Number8 = tc.Number8;
            //this.Number9 = tc.Number9;
            //this.Number10 = tc.Number10;
            this.Str = tc.Str;
            this.Array = tc.Array;
        }
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct LongUnion
    {
        [FieldOffset(0)]
        public int Int1;
        [FieldOffset(1)]
        public int Int2;

        [FieldOffset(0)]
        public float Float;

        [FieldOffset(0)]
        public double Double;

        [FieldOffset(0)]
        public ulong Long;
    }

    public class TargetClass
    {
        public sbyte Number1 { get; set; }
        public short Number2 { get; set; }

        public int Number3 { get; set; }

        public long Number4 { get; set; }

        public byte Number5 { get; set; }
        public ushort Number6 { get; set; }
        public uint Number7 { get; set; }
        public ulong Number8 { get; set; }
        //[Key(8)]
        //[ProtoBuf.ProtoMember(9)]
        //public float Number9 { get; set; }
        //[Key(9)]
        //[ProtoBuf.ProtoMember(10)]
        //public double Number10 { get; set; }
        public string Str { get; set; }
        public int[] Array { get; set; }

        public static TargetClass Create(Random random)
        {
            unchecked
            {
                return new TargetClass
                {
                    Number1 = (sbyte)random.Next(),
                    Number2 = (short)random.Next(),
                    Number3 = (int)random.Next(),
                    Number4 = (long)new LongUnion { Int1 = random.Next(), Int2 = random.Next() }.Long,
                    Number5 = (byte)random.Next(),
                    Number6 = (ushort)random.Next(),
                    Number7 = (uint)random.Next(),
                    Number8 = (ulong)new LongUnion { Int1 = random.Next(), Int2 = random.Next() }.Long,
                    //Number9 = (float)new LongUnion { Int1 = random.Next(), Int2 = random.Next() }.Float,
                    //Number10 = (double)new LongUnion { Int1 = random.Next(), Int2 = random.Next() }.Double,
                    //Str = "FooBarBazBaz",
                    //Array = new[] { 1, 10, 100, 1000, 10000, 100000 }
                };
            }
        }
    }

    public sealed class DynamicCodeDumper_TargetClassContractlessFormatter3 : IJsonFormatter<TargetClassContractless>
    {
        private readonly byte[][] stringByteKeys;
        public DynamicCodeDumper_TargetClassContractlessFormatter3()
        {
            this.stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("Number1"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Number2"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Number3"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Number4"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Number5"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Number6"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Number7"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Number8"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Str"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Array"),
            };
        }
        public void Serialize(ref JsonWriter ptr, TargetClassContractless targetClassContractless, IJsonFormatterResolver jsonFormatterResolver)
        {
            if (targetClassContractless == null)
            {
                ptr.WriteNull();
                return;
            }
            UnsafeMemory64.WriteRaw11(ref ptr, this.stringByteKeys[0]);
            ptr.WriteSByte(targetClassContractless.Number1);
            UnsafeMemory64.WriteRaw11(ref ptr, this.stringByteKeys[1]);
            ptr.WriteInt16(targetClassContractless.Number2);
            UnsafeMemory64.WriteRaw11(ref ptr, this.stringByteKeys[2]);
            ptr.WriteInt32(targetClassContractless.Number3);
            UnsafeMemory64.WriteRaw11(ref ptr, this.stringByteKeys[3]);
            ptr.WriteInt64(targetClassContractless.Number4);
            UnsafeMemory64.WriteRaw11(ref ptr, this.stringByteKeys[4]);
            ptr.WriteByte(targetClassContractless.Number5);
            UnsafeMemory64.WriteRaw11(ref ptr, this.stringByteKeys[5]);
            ptr.WriteUInt16(targetClassContractless.Number6);
            UnsafeMemory64.WriteRaw11(ref ptr, this.stringByteKeys[6]);
            ptr.WriteUInt32(targetClassContractless.Number7);
            UnsafeMemory64.WriteRaw11(ref ptr, this.stringByteKeys[7]);
            ptr.WriteUInt64(targetClassContractless.Number8);
            UnsafeMemory64.WriteRaw7(ref ptr, this.stringByteKeys[8]);
            ptr.WriteString(targetClassContractless.Str);
            UnsafeMemory64.WriteRaw9(ref ptr, this.stringByteKeys[9]);
            JsonFormatterResolverExtensions.GetFormatterWithVerify<int[]>(jsonFormatterResolver).Serialize(ref ptr, targetClassContractless.Array, jsonFormatterResolver);
            ptr.WriteEndObject();
        }
        public unsafe TargetClassContractless Deserialize(ref JsonReader ptr, IJsonFormatterResolver jsonFormatterResolver)
        {
            if (ptr.ReadIsNull())
            {
                return null;
            }
            ptr.ReadIsBeginObjectWithVerify();
            byte[] bufferUnsafe = ptr.GetBufferUnsafe();
            string str = default;
            int[] array = default;
            sbyte number = default;
            short number2 = default;
            int number3 = default;
            long number4 = default;
            byte number5 = default;
            ushort number6 = default;
            uint number7 = default;
            ulong number8 = default;
            fixed (byte* ptr2 = &bufferUnsafe[0])
            {
                int num = default;
                while (!ptr.ReadIsEndObjectWithSkipValueSeparator(ref num))
                {
                    ArraySegment<byte> arraySegment = ptr.ReadPropertyNameSegmentRaw();
                    byte* ptr3 = ptr2 + arraySegment.Offset;
                    int count = arraySegment.Count;
                    if (count != 0)
                    {
                        ulong key = AutomataKeyGen.GetKey(ref ptr3, ref count);
                        if (key < 14762478557558094uL)
                        {
                            if (key < 13918053627426126uL)
                            {
                                if (count == 0)
                                {
                                    if (key == 7500883uL)
                                    {
                                        str = ptr.ReadString();
                                        continue;
                                    }
                                    if (key == 521325933121uL)
                                    {
                                        array = JsonFormatterResolverExtensions.GetFormatterWithVerify<int[]>(jsonFormatterResolver).Deserialize(ref ptr, jsonFormatterResolver);
                                        continue;
                                    }
                                }
                            }
                            else
                            {
                                if (count == 0)
                                {
                                    if (key == 13918053627426126uL)
                                    {
                                        number = ptr.ReadSByte();
                                        continue;
                                    }
                                    if (key == 14199528604136782uL)
                                    {
                                        number2 = ptr.ReadInt16();
                                        continue;
                                    }
                                    if (key == 14481003580847438uL)
                                    {
                                        number3 = ptr.ReadInt32();
                                        continue;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (key < 15325428510979406uL)
                            {
                                if (count == 0)
                                {
                                    if (key == 14762478557558094uL)
                                    {
                                        number4 = ptr.ReadInt64();
                                        continue;
                                    }
                                    if (key == 15043953534268750uL)
                                    {
                                        number5 = ptr.ReadByte();
                                        continue;
                                    }
                                }
                            }
                            else
                            {
                                if (count == 0)
                                {
                                    if (key == 15325428510979406uL)
                                    {
                                        number6 = ptr.ReadUInt16();
                                        continue;
                                    }
                                    if (key == 15606903487690062uL)
                                    {
                                        number7 = ptr.ReadUInt32();
                                        continue;
                                    }
                                    if (key == 15888378464400718uL)
                                    {
                                        number8 = ptr.ReadUInt64();
                                        continue;
                                    }
                                }
                            }
                        }
                    }
                    ptr.ReadNextBlock();
                }
            }
            return new TargetClassContractless
            {
                Number1 = number,
                Number2 = number2,
                Number3 = number3,
                Number4 = number4,
                Number5 = number5,
                Number6 = number6,
                Number7 = number7,
                Number8 = number8,
                Str = str,
                Array = array
            };
        }
    }
}
