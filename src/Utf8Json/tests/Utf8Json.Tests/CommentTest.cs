using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Utf8Json.Tests
{
    public class CommentTest
    {
        public class MyClass
        {
            public int Foo { get; set; }
            public bool Bar { get; set; }
            public double Baz { get; set; }
        }

        [Fact]
        public void SingleLine()
        {
            var x = JsonSerializer.Deserialize<MyClass>(@"
{
    // foo bar baz
    ""Foo"":100,
    // bar
    ""Bar"":true
}");

            x.Foo.Is(100);
            x.Bar.Is(true);
        }

        [Fact]
        public void MultiLine()
        {
            var x = JsonSerializer.Deserialize<MyClass>(@"
{
    /*
       yarou
       yarou
       yaruo
    */
    ""Foo"":100,
    /* oooooo */
    ""Bar"":true
}");

            x.Foo.Is(100);
            x.Bar.Is(true);
        }

        [Fact]
        public void LastLine()
        {
            var x = JsonSerializer.Deserialize<MyClass>(@"
{
    ""Foo"":100, // this is foo
    ""Bar"":true, /* this is bar */
    ""Baz"":10.9
}");

            x.Foo.Is(100);
            x.Bar.Is(true);
            x.Baz.Is(10.9);
        }
    }
}
