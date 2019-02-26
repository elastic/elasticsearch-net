using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Utf8Json.Tests
{
    public class StreamNonMemoryStraemDeserializationTest
    {
        [Fact]
        public void ConcurrencyCheck()
        {
            {
                var ms = new NonMemoryStream(new MemoryStream(JsonSerializer.Serialize(123)));
                var d = JsonSerializer.Deserialize<int>(ms);
                d.Is(123);
            }
            {
                var ms = new NonMemoryStream(new MemoryStream(JsonSerializer.Serialize(9)));
                var d = JsonSerializer.Deserialize<int>(ms);
                d.Is(9);
            }
            {
                var ms = new MemoryStream(JsonSerializer.Serialize(123), 0, 3, true, true);
                var d = JsonSerializer.Deserialize<int>(ms);
                d.Is(123);
                ms.Position = 0;
                ms.SetLength(1);
                JsonSerializer.Serialize(ms, 1);
                var d2 = JsonSerializer.Deserialize<int>(ms);
                d2.Is(1);
            }
        }
    }

    class NonMemoryStream : Stream
    {
        MemoryStream ms;

        public NonMemoryStream(MemoryStream ms)
        {
            this.ms = ms;
        }

        public override bool CanRead => ms.CanRead;

        public override bool CanSeek => ms.CanSeek;

        public override bool CanWrite => ms.CanWrite;

        public override long Length => ms.Length;

        public override long Position { get => ms.Position; set => ms.Position = value; }

        public override void Flush()
        {
            ms.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return ms.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return ms.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            ms.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            ms.Write(buffer, offset, count);
        }
    }
}
