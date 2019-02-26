using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Xunit;

namespace Utf8Json.Tests
{
    public enum AsString
    {
        Foo = 0,
        Bar = 1,
        Baz = 2,
        FooBar = 3,
        FooBaz = 4,
        BarBaz = 5,
        FooBarBaz = 6
    }

    [Flags]
    public enum AsStringFlag
    {
        Foo = 0,
        Bar = 1,
        Baz = 2,
        FooBar = 4,
        FooBaz = 8,
        BarBaz = 16,
        FooBarBaz = 32
    }

    [Flags]
    public enum DataMemberFlag
    {
        [DataMember(Name = "F")]
        Foo = 0,
        Bar = 1,
        Baz = 2,
        [DataMember(Name = "FB")]
        FooBar = 4,
        FooBaz = 8,
        BarBaz = 16,
        [DataMember(Name = "FBB")]
        FooBarBaz = 32,

        [DataMember(Name = "")]
        Empty = 64
    }

    [Flags]
    public enum EnumMemberFlag
    {
        [EnumMember(Value = "F")]
        [DataMember(Name = "Boo")]
        Foo = 0,
        Bar = 1,
        Baz = 2,
        [EnumMember(Value = "FB")]
        FooBar = 4,
        FooBaz = 8,
        BarBaz = 16,
        [EnumMember(Value = "FBB")]
        FooBarBaz = 32
    }

    public class EnumAsStringTest
    {
        public static object enumData = new object[]
        {
            // simple
            new object[] { AsString.Foo, null, "Foo", "null" },
            new object[] { AsString.Bar, AsString.Baz , "Bar", "Baz"},
            new object[] { AsString.FooBar, AsString.FooBaz, "FooBar", "FooBaz" },
            new object[] { AsString.BarBaz, AsString.FooBarBaz, "BarBaz", "FooBarBaz" },
            new object[] { (AsString)10, (AsString)999, "10", "999" },
            // flags
            new object[] { AsStringFlag.Foo, null, "Foo", "null" },
            new object[] { AsStringFlag.Bar, AsStringFlag.Baz , "Bar", "Baz"},
            new object[] { AsStringFlag.FooBar, AsStringFlag.FooBaz, "FooBar", "FooBaz" },
            new object[] { AsStringFlag.BarBaz, AsStringFlag.FooBarBaz, "BarBaz", "FooBarBaz" },
            new object[] { AsStringFlag.Bar | AsStringFlag.FooBaz, AsStringFlag.BarBaz | AsStringFlag.FooBarBaz, "Bar, FooBaz", "BarBaz, FooBarBaz" },
            new object[] { (AsStringFlag)10, (AsStringFlag)999, "Baz, FooBaz", "999" },
        };

        [Theory]
        [MemberData(nameof(enumData))]
        public void EnumTest<T>(T x, T? y, string xName, string yName)
            where T : struct
        {
            var bin = JsonSerializer.Serialize(x);
            Encoding.UTF8.GetString(bin).Trim('\"').Is(xName);
            JsonSerializer.Deserialize<T>(bin).Is(x);

            var bin2 = JsonSerializer.Serialize(y);
            Encoding.UTF8.GetString(bin2).Trim('\"').Is(yName);
            JsonSerializer.Deserialize<T?>(bin2).Is(y);
        }

        [Fact]
        public void DataMemberTest()
        {
            var xs = new[] {
                (DataMemberFlag.Foo, "F"),
                (DataMemberFlag.Bar, "Bar"),
                (DataMemberFlag.Baz, "Baz"),
                (DataMemberFlag.FooBar, "FB"),
                (DataMemberFlag.FooBaz, "FooBaz"),
                (DataMemberFlag.BarBaz, "BarBaz"),
                (DataMemberFlag.FooBarBaz, "FBB"),
            };
            foreach (var item in xs)
            {
                var v = JsonSerializer.ToJsonString(item.Item1);
                v.Trim('\"').Is(item.Item2);
                JsonSerializer.Deserialize<DataMemberFlag>(v).Is(item.Item1);
            }
        }

        [Fact]
        public void EmptyTest()
        {
            var xs = new[] { (DataMemberFlag.Empty, "") };

            foreach (var item in xs)
            {
                var v = JsonSerializer.ToJsonString(item.Item1);
                v.Trim('\"').Is(item.Item2);
                JsonSerializer.Deserialize<DataMemberFlag>(v).Is(item.Item1);
            }
        }

        [Fact]
        public void EnumMemberTest()
        {
            var xs = new[] {
                (EnumMemberFlag.Foo, "F"),
                (EnumMemberFlag.Bar, "Bar"),
                (EnumMemberFlag.Baz, "Baz"),
                (EnumMemberFlag.FooBar, "FB"),
                (EnumMemberFlag.FooBaz, "FooBaz"),
                (EnumMemberFlag.BarBaz, "BarBaz"),
                (EnumMemberFlag.FooBarBaz, "FBB"),

            };
            foreach (var item in xs)
            {
                var v = JsonSerializer.ToJsonString(item.Item1);
                v.Trim('\"').Is(item.Item2);
                JsonSerializer.Deserialize<EnumMemberFlag>(v).Is(item.Item1);
            }
        }
    }
}
