using System;
using Xunit;

namespace Utf8Json.Tests
{
    public class FooException : Exception
    {
        public int Bar { get; set; }

        public FooException() : base("BCD")
        {

        }
    }

    public class ExceptionTest
    {
        [Fact]
        public void Root1()
        {
            {
                var ex = new Exception("ABC");
                var json = JsonSerializer.ToJsonString(ex);
                json.Is("{\"ClassName\":\"System.Exception\",\"Message\":\"ABC\",\"StackTrace\":null,\"Source\":null,\"InnerException\":null}");
            }
            {
                var ex = new Exception("ABC");
                var json = JsonSerializer.ToJsonString(ex, Utf8Json.Resolvers.StandardResolver.ExcludeNullSnakeCase);
                json.Is("{\"class_name\":\"System.Exception\",\"message\":\"ABC\"}");
            }
        }

        [Fact]
        public void Root2()
        {
            {
                FooException ex = new FooException { Bar = 100 };

                var json = JsonSerializer.ToJsonString(ex);
                json.Is("{\"ClassName\":\"Utf8Json.Tests.FooException\",\"Bar\":100,\"Message\":\"BCD\",\"StackTrace\":null,\"Source\":null,\"InnerException\":null}");
            }
            {
                Exception ex = new FooException { Bar = 100 };

                var json = JsonSerializer.ToJsonString(ex, Utf8Json.Resolvers.StandardResolver.ExcludeNullSnakeCase);
                json.Is("{\"class_name\":\"Utf8Json.Tests.FooException\",\"bar\":100,\"message\":\"BCD\"}");
            }
        }

        [Fact]
        public void Inner()
        {
            {
                var ex = new Exception("ABC", new FooException { Bar = 100 });

                var json = JsonSerializer.ToJsonString(ex);
                json.Is("{\"ClassName\":\"System.Exception\",\"Message\":\"ABC\",\"StackTrace\":null,\"Source\":null,\"InnerException\":{\"ClassName\":\"Utf8Json.Tests.FooException\",\"Bar\":100,\"Message\":\"BCD\",\"StackTrace\":null,\"Source\":null,\"InnerException\":null}}");
            }
            {
                var ex = new Exception("ABC", new FooException { Bar = 100 });

                var json = JsonSerializer.ToJsonString(ex, Utf8Json.Resolvers.StandardResolver.ExcludeNullSnakeCase);
                json.Is("{\"class_name\":\"System.Exception\",\"message\":\"ABC\",\"inner_exception\":{\"class_name\":\"Utf8Json.Tests.FooException\",\"bar\":100,\"message\":\"BCD\"}}");
            }
        }
    }
}
