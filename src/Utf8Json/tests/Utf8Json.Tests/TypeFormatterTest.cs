using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Utf8Json.Tests
{
    public class TypeFormatterTest
    {
        [Fact]
        public void TypeTest()
        {
            var bin = JsonSerializer.Serialize(typeof(int));
            var t = JsonSerializer.Deserialize<Type>(bin);
            (t == typeof(int)).IsTrue();
        }
    }
}
