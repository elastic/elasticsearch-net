using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Utf8Json.Tests
{
    public class AsyncTest
    {
        [Fact]
        public async Task Generic()
        {
            var ms = new MemoryStream();
            await Utf8Json.JsonSerializer.SerializeAsync(ms, 100);
            ms.Position = 0;
            var i = await Utf8Json.JsonSerializer.DeserializeAsync<int>(ms);
            i.Is(100);
        }

        [Fact]
        public async Task NonGeneric()
        {
            var ms = new MemoryStream();
            await Utf8Json.JsonSerializer.NonGeneric.SerializeAsync(ms, 100);
            ms.Position = 0;
            var i = await Utf8Json.JsonSerializer.NonGeneric.DeserializeAsync(typeof(int), ms);
            ((int)i).Is(100);
        }
    }
}
