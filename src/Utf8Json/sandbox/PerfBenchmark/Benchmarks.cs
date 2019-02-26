using BenchmarkDotNet.Attributes;
using MessagePack.Resolvers;
using System;
using System.IO;
using System.Text;
using Utf8Json;

namespace PerfBenchmark
{
    [Config(typeof(BenchmarkConfig))]
    public class SerializeBenchmark
    {
        public static TargetClass obj1;
        public static TargetClassContractless objContractless;

        static Utf8Json.IJsonFormatterResolver jsonresolver = Utf8Json.Resolvers.StandardResolver.Default;
        Encoding utf8 = Encoding.UTF8;

        static SerializeBenchmark()
        {
            var rand = new Random(34151513);
            obj1 = TargetClass.Create(rand);
            objContractless = new TargetClassContractless(obj1);
        }

        [Benchmark(Baseline = true)]
        public byte[] Utf8JsonSerializer()
        {
            return Utf8Json.JsonSerializer.Serialize(obj1, jsonresolver);
        }

        [Benchmark]
        public byte[] MessagePackCSharp()
        {
            return MessagePack.MessagePackSerializer.Serialize(obj1);
        }

        [Benchmark]
        public byte[] MessagePackCSharpContractless()
        {
            return MessagePack.MessagePackSerializer.Serialize(objContractless, MessagePack.Resolvers.ContractlessStandardResolver.Instance);
        }

        [Benchmark]
        public void Protobufnet()
        {
            using (var ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(ms, obj1);
            }
        }

        [Benchmark]
        public byte[] Jil()
        {
            return utf8.GetBytes(global::Jil.JSON.Serialize(obj1));
        }

        [Benchmark]
        public void JilTextWriter()
        {
            using (var ms = new MemoryStream())
            using (var sw = new StreamWriter(ms, utf8))
            {
                global::Jil.JSON.Serialize(obj1, sw);
            }
        }

        [Benchmark]
        public byte[] NetJson()
        {
            return utf8.GetBytes(NetJSON.NetJSON.Serialize(obj1));
        }

        [Benchmark]
        public byte[] JsonNet()
        {
            return utf8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(obj1));
        }
    }

    [Config(typeof(BenchmarkConfig))]
    public class DeserializeBenchmark
    {
        static byte[] json = new SerializeBenchmark().Utf8JsonSerializer();
        static byte[] proto;
        static byte[] msgpack1 = new SerializeBenchmark().MessagePackCSharp();
        static byte[] msgpack2 = new SerializeBenchmark().MessagePackCSharpContractless();
        static Utf8Json.IJsonFormatterResolver jsonresolver = Utf8Json.Resolvers.StandardResolver.Default;
        static Encoding utf8 = Encoding.UTF8;

        static DeserializeBenchmark()
        {
            using (var ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(ms, SerializeBenchmark.obj1);
                proto = ms.ToArray();
            }
        }

        [Benchmark(Baseline = true)]
        public TargetClass Utf8JsonSerializer()
        {
            return Utf8Json.JsonSerializer.Deserialize<TargetClass>(json, jsonresolver);
        }

        [Benchmark]
        public TargetClass MessagePackCSharp()
        {
            return MessagePack.MessagePackSerializer.Deserialize<TargetClass>(msgpack1);
        }

        [Benchmark]
        public TargetClassContractless MessagePackCSharpContractless()
        {
            return MessagePack.MessagePackSerializer.Deserialize<TargetClassContractless>(msgpack2, MessagePack.Resolvers.ContractlessStandardResolver.Instance);
        }

        [Benchmark]
        public TargetClass Protobufnet()
        {
            using (var ms = new MemoryStream())
            {
                return ProtoBuf.Serializer.Deserialize<TargetClass>(ms);
            }
        }

        [Benchmark]
        public TargetClass Jil()
        {
            return global::Jil.JSON.Deserialize<TargetClass>(utf8.GetString(json));
        }

        [Benchmark]
        public TargetClass JilTextReader()
        {
            using (var ms = new MemoryStream(json))
            using (var sr = new StreamReader(ms, utf8))
            {
                return global::Jil.JSON.Deserialize<TargetClass>(sr);
            }
        }

        [Benchmark]
        public TargetClass JsonNet()
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<TargetClass>(utf8.GetString(json));
        }

        [Benchmark]
        public TargetClass NetJson()
        {
            return NetJSON.NetJSON.Deserialize<TargetClass>(utf8.GetString(json));
        }
    }
}