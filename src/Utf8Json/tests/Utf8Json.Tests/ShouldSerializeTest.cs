using System;
using System.Collections.Generic;
using System.Text;
using Utf8Json.Resolvers;
using Xunit;

namespace Utf8Json.Tests
{
    public class ShouldSerializeTest
    {
        public class SingleStruct
        {
            public int MyProperty { get; set; }

            public bool ShouldSerializeMyProperty()
            {
                return MyProperty != 100;
            }
        }

        public class SingleClass
        {
            public string MyProperty { get; set; }

            public bool ShouldSerializeMyProperty()
            {
                return MyProperty != "Foo";
            }
        }

        public class Multi
        {
            public int MyProperty0 { get; set; }
            public string MyProperty1 { get; set; }
            public string MyProperty2 { get; set; }
            public int MyProperty4 { get; set; }

            public bool ShouldSerializeMyProperty1()
            {
                return MyProperty1 != "Foo";
            }

            public bool ShouldSerializeMyProperty4()
            {
                return MyProperty4 != 100;
            }
        }

        [Fact]
        public void ShoudSerializeDynamicAssembly()
        {
            var ignore1 = new SingleStruct { MyProperty = 100 };
            var success1 = new SingleStruct { MyProperty = 190 };
            var ignore2 = new SingleClass { MyProperty = "Foo" };
            var success2 = new SingleClass { MyProperty = "Bar" };

            var ignore3 = new Multi
            {
                MyProperty0 = 99,
                MyProperty1 = "Foo",
                MyProperty2 = null,
                MyProperty4 = 100
            };


            var success3 = new Multi
            {
                MyProperty0 = 99,
                MyProperty1 = "FooFoo",
                MyProperty2 = null,
                MyProperty4 = 10000
            };

            {
                var f1 = JsonSerializer.ToJsonString(ignore1, StandardResolver.Default);
                var s1 = JsonSerializer.ToJsonString(success1, StandardResolver.Default);
                var f2 = JsonSerializer.ToJsonString(ignore2, StandardResolver.Default);
                var s2 = JsonSerializer.ToJsonString(success2, StandardResolver.Default);
                var f3 = JsonSerializer.ToJsonString(ignore3, StandardResolver.Default);
                var s3 = JsonSerializer.ToJsonString(success3, StandardResolver.Default);

                f1.Is("{}");
                s1.Is("{\"MyProperty\":190}");
                f2.Is("{}");
                s2.Is("{\"MyProperty\":\"Bar\"}");
                f3.Is("{\"MyProperty0\":99,\"MyProperty2\":null}");
                s3.Is("{\"MyProperty0\":99,\"MyProperty1\":\"FooFoo\",\"MyProperty2\":null,\"MyProperty4\":10000}");
            }
            {
                var f1 = JsonSerializer.ToJsonString(ignore1, StandardResolver.ExcludeNull);
                var s1 = JsonSerializer.ToJsonString(success1, StandardResolver.ExcludeNull);
                var f2 = JsonSerializer.ToJsonString(ignore2, StandardResolver.ExcludeNull);
                var s2 = JsonSerializer.ToJsonString(success2, StandardResolver.ExcludeNull);
                var f3 = JsonSerializer.ToJsonString(ignore3, StandardResolver.ExcludeNull);
                var s3 = JsonSerializer.ToJsonString(success3, StandardResolver.ExcludeNull);

                f1.Is("{}");
                s1.Is("{\"MyProperty\":190}");
                f2.Is("{}");
                s2.Is("{\"MyProperty\":\"Bar\"}");
                f3.Is("{\"MyProperty0\":99}");
                s3.Is("{\"MyProperty0\":99,\"MyProperty1\":\"FooFoo\",\"MyProperty4\":10000}");
            }
        }

        [Fact]
        public void ShoudSerializeDynamicMethod()
        {
            var ignore1 = new SingleStruct { MyProperty = 100 };
            var success1 = new SingleStruct { MyProperty = 190 };
            var ignore2 = new SingleClass { MyProperty = "Foo" };
            var success2 = new SingleClass { MyProperty = "Bar" };

            var ignore3 = new Multi
            {
                MyProperty0 = 99,
                MyProperty1 = "Foo",
                MyProperty2 = null,
                MyProperty4 = 100
            };


            var success3 = new Multi
            {
                MyProperty0 = 99,
                MyProperty1 = "FooFoo",
                MyProperty2 = null,
                MyProperty4 = 10000
            };

            {
                var f1 = JsonSerializer.ToJsonString(ignore1, StandardResolver.AllowPrivateCamelCase);
                var s1 = JsonSerializer.ToJsonString(success1, StandardResolver.AllowPrivateCamelCase);
                var f2 = JsonSerializer.ToJsonString(ignore2, StandardResolver.AllowPrivateCamelCase);
                var s2 = JsonSerializer.ToJsonString(success2, StandardResolver.AllowPrivateCamelCase);
                var f3 = JsonSerializer.ToJsonString(ignore3, StandardResolver.AllowPrivateCamelCase);
                var s3 = JsonSerializer.ToJsonString(success3, StandardResolver.AllowPrivateCamelCase);

                f1.Is("{}");
                s1.Is("{\"myProperty\":190}");
                f2.Is("{}");
                s2.Is("{\"myProperty\":\"Bar\"}");
                f3.Is("{\"myProperty0\":99,\"myProperty2\":null}");
                s3.Is("{\"myProperty0\":99,\"myProperty1\":\"FooFoo\",\"myProperty2\":null,\"myProperty4\":10000}");
            }
            {
                var f1 = JsonSerializer.ToJsonString(ignore1, StandardResolver.AllowPrivateExcludeNullCamelCase);
                var s1 = JsonSerializer.ToJsonString(success1, StandardResolver.AllowPrivateExcludeNullCamelCase);
                var f2 = JsonSerializer.ToJsonString(ignore2, StandardResolver.AllowPrivateExcludeNullCamelCase);
                var s2 = JsonSerializer.ToJsonString(success2, StandardResolver.AllowPrivateExcludeNullCamelCase);
                var f3 = JsonSerializer.ToJsonString(ignore3, StandardResolver.AllowPrivateExcludeNullCamelCase);
                var s3 = JsonSerializer.ToJsonString(success3, StandardResolver.AllowPrivateExcludeNullCamelCase);

                f1.Is("{}");
                s1.Is("{\"myProperty\":190}");
                f2.Is("{}");
                s2.Is("{\"myProperty\":\"Bar\"}");
                f3.Is("{\"myProperty0\":99}");
                s3.Is("{\"myProperty0\":99,\"myProperty1\":\"FooFoo\",\"myProperty4\":10000}");
            }
        }
    }
}
