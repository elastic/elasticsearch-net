using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Xunit;

namespace Utf8Json.Tests
{
    public class ExpandoObjectTest
    {
        [Fact]
        public void Hoge()
        {
            dynamic expando = new ExpandoObject();
            expando.Hoge = "hogehoge";
            expando.Huga = "huga-";

            var j = Utf8Json.JsonSerializer.Serialize(expando);

            dynamic exp = Utf8Json.JsonSerializer.Deserialize<ExpandoObject>(j);
            (exp.Hoge as string).Is("hogehoge");
            (exp.Huga as string).Is("huga-");
        }
    }
}
