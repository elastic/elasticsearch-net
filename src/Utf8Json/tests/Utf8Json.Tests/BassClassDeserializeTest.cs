using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Utf8Json.Tests
{
    public abstract class Base1
    {
        public int B1 { get { return _b1; } }
        private int _b1 = -1;
    }

    public abstract class Base2 : Base1
    {
        public int B2 { get { return _b2; } }
        private int _b2 = -2;
    }

    public class Impl1_2 : Base2
    {
        public string Name { get { return name; } }
        private string name = "none";
    }

    public class BassClassDeserializeTest
    {
        [Fact]
        public void Deserialize()
        {
            var json = "{\"_b1\":10,\"_b2\":99,\"name\":\"foobar\"}";

            var r = JsonSerializer.Deserialize<Impl1_2>(json, Utf8Json.Resolvers.StandardResolver.AllowPrivate);

            r.B1.Is(10);
            r.B2.Is(99);
            r.Name.Is("foobar");
        }
    }
}
