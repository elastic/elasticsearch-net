using UnityEngine;
using RuntimeUnitTestToolkit;
using System.Collections;

using System.Collections.Generic;
using System;
using Utf8Json;
using System.Text;
using System.Linq;

namespace Utf8Json.UnityClient.Tests
{
    public class BassPrivate
    {
        int x = 0;

        public int GetX()
        {
            return x;
        }
    }

    public class Inherit: BassPrivate
    {
        public int Y;
    }

    public class PrivateTest
    {
        public void CanSerialize()
        {
            var foo = JsonSerializer.Deserialize<Inherit>("{\"x\":10,\"Y\":99}", Utf8Json.Resolvers.StandardResolver.AllowPrivate);
            foo.Y.Is(99);
            foo.GetX().Is(10);
        }
    }
}