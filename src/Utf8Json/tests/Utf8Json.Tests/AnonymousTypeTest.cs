using System;
using System.Collections.Generic;
using System.Text;
using Utf8Json.Resolvers;
using Xunit;

namespace Utf8Json.Tests
{
    public class AnonymousTypeTest
    {
        [Fact]
        public void SerializeAndToJson()
        {
            var testData = new { Hoge = 100, Huga = true, Yaki = new { Rec = 1, T = 10 }, Nano = "nanoanno" };
            var ok = @"{""Hoge"":100,""Huga"":true,""Yaki"":{""Rec"":1,""T"":10},""Nano"":""nanoanno""}";

            JsonSerializer.ToJsonString(testData, StandardResolver.Default).Is(ok);
            JsonSerializer.ToJsonString(testData, StandardResolver.AllowPrivate).Is(ok);
            JsonSerializer.ToJsonString(testData, StandardResolver.ExcludeNull).Is(ok);
        }
    }
}
