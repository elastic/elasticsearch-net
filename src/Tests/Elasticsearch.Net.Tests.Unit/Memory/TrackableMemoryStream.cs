using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Providers;

namespace Elasticsearch.Net.Tests.Unit.Memory
{
	public class TrackableMemoryStreamProvider : IMemoryStreamProvider
	{
		public MemoryStream New()
		{
			return new TrackableMemoryStream();
		}
	}


	public class TrackableMemoryStream : MemoryStream, IDisposable
	{
		private readonly MemoryStream _memoryStream;

		private bool _isClosed = false;
		private bool _isDisposed = false;

		public TrackableMemoryStream()
		{
			this._memoryStream = new MemoryStream();
		}

		public TrackableMemoryStream(byte[] data)
		{
			this._memoryStream = new MemoryStream(data);
		} 

		public bool IsClosedOrDisposed { get { return _isClosed || _isDisposed; } }

		public override void Flush()
		{
			this._memoryStream.Flush();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._memoryStream.Seek(offset, origin);
		}

		public override void SetLength(long value)
		{
			this._memoryStream.SetLength(value);
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			return this._memoryStream.Read(buffer, offset, count);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			this._memoryStream.Write(buffer, offset, count);
		}

		public override bool CanRead
		{
			get { return this._memoryStream.CanRead; }
		}

		public override bool CanSeek
		{
			get { return this._memoryStream.CanSeek; }
		}

		public override bool CanWrite
		{
			get { return this._memoryStream.CanWrite; }
		}

		public override long Length
		{
			get { return this._memoryStream.Length; }
		}

		public override long Position
		{
			get { return this._memoryStream.Position; }
			set { this._memoryStream.Position = value; }
		}

		public override void Close()
		{
			this._isClosed = true;
			this._memoryStream.Close();
		}

		public override byte[] ToArray()
		{
			return this._memoryStream.ToArray();
		}

		protected override void Dispose(bool disposing)
		{
			this._isDisposed = true;
			this._memoryStream.Dispose();
			base.Dispose(disposing);
		}
	}
}
