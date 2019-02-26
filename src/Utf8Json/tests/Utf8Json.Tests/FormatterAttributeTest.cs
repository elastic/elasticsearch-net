using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utf8Json.Resolvers;
using Xunit;

namespace Utf8Json.Tests
{
    public class SpecifiedFormatterResolverTest
    {
        [JsonFormatter(typeof(NoObjectFormatter))]
        class CustomClassObject
        {
            int X;

            public CustomClassObject(int x)
            {
                this.X = x;
            }

            public int GetX()
            {
                return X;
            }

            class NoObjectFormatter : IJsonFormatter<CustomClassObject>
            {
                public CustomClassObject Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
                {
                    var r = reader.ReadInt32();
                    return new CustomClassObject(r);
                }

                public void Serialize(ref JsonWriter writer, CustomClassObject value, IJsonFormatterResolver formatterResolver)
                {
                    writer.WriteInt32(value.X);
                }
            }
        }

        [JsonFormatter(typeof(CustomStructObjectFormatter))]
        struct CustomStructObject
        {
            int X;

            public CustomStructObject(int x)
            {
                this.X = x;
            }

            public int GetX()
            {
                return X;
            }

            class CustomStructObjectFormatter : IJsonFormatter<CustomStructObject>
            {
                public CustomStructObject Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
                {
                    var r = reader.ReadInt32();
                    return new CustomStructObject(r);
                }

                public void Serialize(ref JsonWriter writer, CustomStructObject value, IJsonFormatterResolver formatterResolver)
                {
                    writer.WriteInt32(value.X);
                }
            }
        }


        [JsonFormatter(typeof(CustomInterfaceObjectFormatter))]
        interface ICustomInterfaceObject
        {
            int A { get; }
        }

        class CustomInterfaceObjectFormatter : IJsonFormatter<ICustomInterfaceObject>
        {
            public ICustomInterfaceObject Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
            {
                var r = reader.ReadInt32();
                return new InheritDefault(r);
            }

            public void Serialize(ref JsonWriter writer, ICustomInterfaceObject value, IJsonFormatterResolver formatterResolver)
            {
                 writer.WriteInt32( value.A);
            }
        }

        class InheritDefault : ICustomInterfaceObject
        {
            public int A { get; }

            public InheritDefault(int a)
            {
                this.A = a;
            }
        }

        class HogeMoge : ICustomInterfaceObject
        {
            public int A { get; set; }
        }


        T Convert<T>(T value)
        {
            return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(value, StandardResolver.Default), StandardResolver.Default);
        }

        [Fact]
        public void CustomFormatters()
        {
            Convert(new CustomClassObject(999)).GetX().Is(999);
            Convert(new CustomStructObject(1234)).GetX().Is(1234);
            Convert<ICustomInterfaceObject>(new HogeMoge { A = 999 }).A.Is(999);
        }

}

}
