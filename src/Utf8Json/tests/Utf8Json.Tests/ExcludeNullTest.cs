using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Utf8Json.Tests
{
    public class ExcludeNullTest
    {
        [Fact]
        public void Foo()
        {
            var answer1 = "{\"Age\":0,\"Name\":null}";
            var answer2 = "{\"Age\":0}";
            JsonSerializer.ToJsonString(new SimplePerson(), Utf8Json.Resolvers.StandardResolver.Default).Is(answer1);
            JsonSerializer.ToJsonString(new SimplePerson(), Utf8Json.Resolvers.StandardResolver.ExcludeNull).Is(answer2);
            JsonSerializer.ToJsonString(new SimplePerson(), Utf8Json.Resolvers.StandardResolver.AllowPrivate).Is(answer1);
            JsonSerializer.ToJsonString(new SimplePerson(), Utf8Json.Resolvers.StandardResolver.AllowPrivateExcludeNull).Is(answer2);

            var answer3 = "{\"Age\":null,\"Name\":null}";
            var answer4 = "{}";
            JsonSerializer.ToJsonString(new IncludeNullablePerson(), Utf8Json.Resolvers.StandardResolver.Default).Is(answer3);
            JsonSerializer.ToJsonString(new IncludeNullablePerson(), Utf8Json.Resolvers.StandardResolver.ExcludeNull).Is(answer4);
            JsonSerializer.ToJsonString(new IncludeNullablePerson(), Utf8Json.Resolvers.StandardResolver.AllowPrivate).Is(answer3);
            JsonSerializer.ToJsonString(new IncludeNullablePerson(), Utf8Json.Resolvers.StandardResolver.AllowPrivateExcludeNull).Is(answer4);
        }

        public class SimplePerson
        {
            public int Age { get; set; }
            public string Name { get; set; }
        }

        public class IncludeNullablePerson
        {
            public int? Age { get; set; }
            public string Name { get; set; }
        }
    }
}
