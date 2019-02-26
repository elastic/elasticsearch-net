using System;
using System.Collections.Generic;
using System.Text;
using Utf8Json;
using Jil;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Attributes.Jobs;

namespace PerfBenchmark
{
    public struct MyModel
    {
        public string X { get; set; }
        public string Y { get; set; }
        public int Z { get; set; }
    }
    public class JsonSerializeBench
    {
        [Params(1, 10000)]
        public int LoopNum { get; set; }

        static string json;
        static byte[] jsonBytes;

        public JsonSerializeBench()
        {
            var m = new MyModel();
            m.X = $"abcdefg";
            m.Y = $"@aiweraw";
            m.Z = int.MaxValue;
            json = Jil.JSON.Serialize(m);
            jsonBytes = Encoding.UTF8.GetBytes(json);
        }

        [Benchmark]
        public void JilSerialize_String()
        {
            var m = new MyModel();
            m.X = $"abcdefg";
            m.Y = $"@aiweraw";
            m.Z = int.MaxValue;
            for (int i = 0; i < LoopNum; i++)
            {
                Jil.JSON.Serialize(m);
            }
        }
        [Benchmark]
        public void JilSerialize_String_UTf8()
        {
            var m = new MyModel();
            m.X = $"abcdefg";
            m.Y = $"@aiweraw";
            m.Z = int.MaxValue;
            for (int i = 0; i < LoopNum; i++)
            {
                Encoding.UTF8.GetBytes(Jil.JSON.Serialize(m));
            }
        }
        [Benchmark]
        public void Utf8JsonSerialize()
        {
            var m = new MyModel();
            m.X = $"abcdefg";
            m.Y = $"@aiweraw";
            m.Z = int.MaxValue;
            for (int i = 0; i < LoopNum; i++)
            {
                Utf8Json.JsonSerializer.Serialize(m);
            }
        }
        [Benchmark]
        public void JilJsonDeserialize_String()
        {
            for (int i = 0; i < LoopNum; i++)
            {
                Jil.JSON.Deserialize<MyModel>(json);
            }
        }
        [Benchmark]
        public void JilJsonDeserialize_Utf8()
        {
            for (int i = 0; i < LoopNum; i++)
            {
                Jil.JSON.Deserialize<MyModel>(Encoding.UTF8.GetString(jsonBytes));
            }
        }
        [Benchmark]
        public void Utf8JsonDeserialize()
        {
            for (int i = 0; i < LoopNum; i++)
            {
                Utf8Json.JsonSerializer.Deserialize<MyModel>(jsonBytes);
            }
        }
    }
}
