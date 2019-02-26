using System;
using Utf8Json.Internal;
using Xunit;

namespace Utf8Json.Tests
{
    public class ReadNextBlockSegmentTest
    {
        [JsonFormatter(typeof(ContainerFormatter))]
        public class Container
        {
            public string Type { get; set; }
            public IValue Value { get; set; }

            public class ContainerFormatter : IJsonFormatter<Container>
            {
                static readonly AutomataDictionary automata = new AutomataDictionary();

                static ContainerFormatter()
                {
                    automata.Add("Type", 0);
                    automata.Add("Value", 1);
                }


                public Container Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
                {
                    if (reader.ReadIsNull()) return null;

                    string type = null;
                    ArraySegment<byte> valueSegment = default(ArraySegment<byte>);

                    var count = 0;
                    while (reader.ReadIsInObject(ref count))
                    {
                        var propName = reader.ReadPropertyNameSegmentRaw();
                        var i = -1;
                        automata.TryGetValue(propName, out i);
                        switch (i)
                        {
                            case 0:
                                type = reader.ReadString();
                                break;
                            case 1:
                                valueSegment = reader.ReadNextBlockSegment();
                                break;
                            default:
                                reader.ReadNextBlock();
                                break;
                        }
                    }

                    var result = new Container { Type = type };

                    switch (type)
                    {
                        case "TypeA":
                            {
                                var childReader = new JsonReader(valueSegment.Array, valueSegment.Offset);
                                result.Value = formatterResolver.GetFormatterWithVerify<ValueTypeA>().Deserialize(ref childReader, formatterResolver);
                            }
                            break;
                        case "TypeB":
                            {
                                var childReader = new JsonReader(valueSegment.Array, valueSegment.Offset);
                                result.Value = formatterResolver.GetFormatterWithVerify<ValueTypeB>().Deserialize(ref childReader, formatterResolver);
                            }
                            break;
                        default:
                            break;
                    }

                    return result;
                }

                public void Serialize(ref JsonWriter writer, Container value, IJsonFormatterResolver formatterResolver)
                {
                    throw new NotImplementedException();
                }
            }
        }

        public interface IValue
        {
        }

        public class ValueTypeA : IValue
        {
            public int Foo { get; set; }
        }
        public class ValueTypeB : IValue
        {
            public string Hoge { get; set; }
        }

        [Fact]
        public void Test()
        {
            var json = @"
            {
                ""Type"": ""TypeA"",
                ""Value"": {
                    ""Foo"": 10
                }
            }
            ";

            var container = JsonSerializer.Deserialize<Container>(json);
            container.Type.Is("TypeA");
            container.Value.IsInstanceOf<ValueTypeA>().Foo.Is(10);

            json = @"
            {
                ""Value"": {
                    ""Hoge"": ""mogemoge""
                },
                ""Type"": ""TypeB""
            }
            ";

            container = JsonSerializer.Deserialize<Container>(json);
            container.Type.Is("TypeB");
            container.Value.IsInstanceOf<ValueTypeB>().Hoge.Is("mogemoge");
        }
    }
}
