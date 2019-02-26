using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Utf8Json.Tests
{
    public class SkipReadTest
    {
        static JsonReader CreateReader(string json)
        {
            return new JsonReader(Encoding.UTF8.GetBytes(json));
        }

        [Fact]
        public void Skip()
        {
            CreateReader("          true").ReadBoolean().IsTrue();
            CreateReader("   1").ReadByte().Is((byte)1);
            CreateReader("   1.21341").ReadDouble().Is(1.21341);
            CreateReader("        2").ReadInt16().Is((short)2);
            CreateReader("        2").ReadInt32().Is((int)2);
            CreateReader("        2").ReadInt64().Is((long)2);
            CreateReader("        [").ReadIsBeginArray().IsTrue();
            CreateReader("        {").ReadIsBeginObject().IsTrue();
            CreateReader("        ]").ReadIsEndArray().IsTrue();
            CreateReader("        }").ReadIsEndObject().IsTrue();
            CreateReader("        :").ReadIsNameSeparator().IsTrue();
            CreateReader("        null").ReadIsNull().IsTrue();
            CreateReader("        ,").ReadIsValueSeparator().IsTrue();
            CreateReader("        3").ReadSByte().Is((sbyte)3);
            CreateReader("        1.4").ReadSingle().Is((float)1.4);
            CreateReader("        \"foo\"").ReadString().Is("foo");
            CreateReader("        1").ReadUInt16().Is((UInt16)1);
            CreateReader("        1").ReadUInt32().Is((UInt32)1);
            CreateReader("        1").ReadUInt64().Is((UInt64)1);
        }
    }
}
