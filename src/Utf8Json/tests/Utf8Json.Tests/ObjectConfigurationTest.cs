using System;
using Xunit;
using System.Collections.Generic;
using System.Text;
using Utf8Json.Formatters;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Utf8Json.Tests
{
    public class ToaruClass
    {
        public string MyProperty1 { get; set; }
        public string MyProperty2 { get; set; }
        public string MyProperty3 { get; set; }
    }

    public class ToaruClass2
    {
        [JsonFormatter(typeof(AlwaysMinusOne))]
        public int MyProperty1 { get; set; }
        [JsonFormatter(typeof(DateTimeFormatter), "yyMMdd")]
        public DateTime MyProperty2 { get; set; }

        int hiddenPrivate;

        public void SetHidden(int x)
        {
            hiddenPrivate = x;
        }

        public int GEtHidden()
        {
            return hiddenPrivate;
        }
    }


    public class EscapeNamed
    {
        [DataMember(Name = "\"あいう\tえお\t\"")]
        public string MyProperty1 { get; set; }
        [DataMember(Name = "\"\\uD840\\uDC0B\"うおかabc")]
        public string MyProperty2 { get; set; }
    }

    public class AlwaysMinusOne : IJsonFormatter<int>
    {
        public int Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadInt32();
        }

        public void Serialize(ref JsonWriter writer, int value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteInt32(-1);
        }
    }


    public class ObjectConfigurationTest
    {
        [Fact]
        public void NameMutateAndExcludeNull()
        {
            JsonSerializer.ToJsonString(new ToaruClass { MyProperty1 = "foo", MyProperty2 = "bar", MyProperty3 = "baz" }, Utf8Json.Resolvers.StandardResolver.Default)
                .Is(@"{""MyProperty1"":""foo"",""MyProperty2"":""bar"",""MyProperty3"":""baz""}");
            JsonSerializer.ToJsonString(new ToaruClass { MyProperty1 = "foo", MyProperty2 = "bar", MyProperty3 = "baz" }, Utf8Json.Resolvers.StandardResolver.CamelCase)
                .Is(@"{""myProperty1"":""foo"",""myProperty2"":""bar"",""myProperty3"":""baz""}");
            JsonSerializer.ToJsonString(new ToaruClass { MyProperty1 = "foo", MyProperty2 = "bar", MyProperty3 = "baz" }, Utf8Json.Resolvers.StandardResolver.SnakeCase)
                .Is(@"{""my_property1"":""foo"",""my_property2"":""bar"",""my_property3"":""baz""}");

            JsonSerializer.ToJsonString(new ToaruClass { MyProperty1 = null, MyProperty2 = "bar", MyProperty3 = "baz" }, Utf8Json.Resolvers.StandardResolver.ExcludeNull)
               .Is(@"{""MyProperty2"":""bar"",""MyProperty3"":""baz""}");
            JsonSerializer.ToJsonString(new ToaruClass { MyProperty1 = "foo", MyProperty2 = null, MyProperty3 = "baz" }, Utf8Json.Resolvers.StandardResolver.ExcludeNullCamelCase)
                .Is(@"{""myProperty1"":""foo"",""myProperty3"":""baz""}");
            JsonSerializer.ToJsonString(new ToaruClass { MyProperty1 = "foo", MyProperty2 = "bar", MyProperty3 = null }, Utf8Json.Resolvers.StandardResolver.ExcludeNullSnakeCase)
                .Is(@"{""my_property1"":""foo"",""my_property2"":""bar""}");


            JsonSerializer.ToJsonString(new ToaruClass { MyProperty1 = null, MyProperty2 = null, MyProperty3 = null }, Utf8Json.Resolvers.StandardResolver.AllowPrivateExcludeNull)
                .Is(@"{}");
        }


        [Fact]
        public void AttrEtc()
        {
            var now = DateTime.Now;
            var tc2 = new ToaruClass2 { MyProperty1 = 9999, MyProperty2 = now };
            tc2.SetHidden(1000);

            var js = JsonSerializer.ToJsonString(tc2, Utf8Json.Resolvers.StandardResolver.AllowPrivateSnakeCase);
            js.Is($@"{{""my_property1"":-1,""my_property2"":""{now.ToString("yyMMdd")}"",""hidden_private"":1000}}");


            var jjj = JsonSerializer.Deserialize<ToaruClass2>(js, Utf8Json.Resolvers.StandardResolver.AllowPrivateSnakeCase);
            jjj.MyProperty1.Is(-1);
            jjj.GEtHidden().Is(1000);
        }

        [Fact]
        public void Escape()
        {
            var s1 = Encoding.UTF8.GetBytes("\"あいうえお\"");
            var s2 = Encoding.UTF8.GetBytes("\"あいう\\tえお\"");
            var s3 = Encoding.UTF8.GetBytes("\"あいう\tえお\t\"");
            var s4 = Encoding.UTF8.GetBytes("\"\\u3042\\u3044\\u3046えお\"");
            var s5 = Encoding.UTF8.GetBytes("\"\\uD840\\uDC0B\"");

            var str1 = new JsonReader(s1, 0).ReadString();
            var str2 = new JsonReader(s2, 0).ReadString();
            var str3 = new JsonReader(s3, 0).ReadString();
            var str4 = new JsonReader(s4, 0).ReadString();
            var str5 = new JsonReader(s5, 0).ReadString();

            var st1 = JsonConvert.DeserializeObject<string>(Encoding.UTF8.GetString(s1));
            var st2 = JsonConvert.DeserializeObject<string>(Encoding.UTF8.GetString(s2));
            var st3 = JsonConvert.DeserializeObject<string>(Encoding.UTF8.GetString(s3));
            var st4 = JsonConvert.DeserializeObject<string>(Encoding.UTF8.GetString(s4));
            var st5 = JsonConvert.DeserializeObject<string>(Encoding.UTF8.GetString(s5));

            str1.Is(st1);
            str2.Is(st2);
            str3.Is(st3);
            str4.Is(st4);
            str5.Is(st5);


            var named = new EscapeNamed { MyProperty1 = "foo", MyProperty2 = "baz" };
            var bbb = JsonConvert.DeserializeObject<EscapeNamed>(JsonConvert.SerializeObject(named));
            bbb.MyProperty1.Is(named.MyProperty1);
            bbb.MyProperty2.Is(named.MyProperty2);
        }
    }
}
