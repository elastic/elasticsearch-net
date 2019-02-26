using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Utf8Json.Tests
{
    public class DynamicObjectFallbackTestContainer
    {
        public int MyProperty { get; set; }

        public object MoreObject { get; set; }
    }


    public class DynamicObjectFallbackTest
    {
        [Fact]
        public void DynamicObject()
        {
            var testData = new object[]
            {
                new DynamicObjectFallbackTestContainer
                {
                    MyProperty = 100,
                    MoreObject = new string[]{"a", "b", "c" }
                },

                new DynamicObjectFallbackTestContainer
                {
                    MyProperty = 300,
                    MoreObject = new SharedData.SimlpeStringKeyData
                    {
                        Prop1 = 10,
                        Prop2 = SharedData.ByteEnum.C,
                        Prop3 = 99999
                    }
                },
            };

            var resolver = new MyResolver();
            var data1 = JsonSerializer.Serialize(testData, resolver);

            var json = Encoding.UTF8.GetString(data1);

            var reference = JsonConvert.SerializeObject(testData);

            json.Is(reference);

        }
    }

    class MyResolver : IJsonFormatterResolver
    {
        public IJsonFormatter<T> GetFormatter<T>()
        {
            if (typeof(T).IsEnum)
            {
                return Resolvers.EnumResolver.UnderlyingValue.GetFormatter<T>();
            }
            else
            {
                return Resolvers.StandardResolver.Default.GetFormatter<T>();
            }
        }
    }
}
