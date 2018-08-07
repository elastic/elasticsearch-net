// The MIT License (MIT)
//
// Copyright (c) 2015-2016 Microsoft
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
// https://github.com/Microsoft/Microsoft.IO.RecyclableMemoryStream/blob/master/src/RecyclableMemoryStream.cs
// MIT license: https://github.com/Microsoft/Microsoft.IO.RecyclableMemoryStream/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;

namespace Elasticsearch.Net
{
	/// <summary>
	/// MemoryStream implementation that deals with pooling and managing memory streams which use potentially large
	/// buffers.
	/// </summary>
	/// <remarks>
	/// This class works in tandem with the RecylableMemoryStreamManager to supply MemoryStream
	/// objects to callers, while avoiding these specific problems:
	/// 1. LOH allocations - since all large buffers are pooled, they will never incur a Gen2 GC
	/// 2. Memory waste - A standard memory stream doubles its size when it runs out of room. This
	/// leads to continual memory growth as each stream approaches the maximum allowed size.
	/// 3. Memory copying - Each time a MemoryStream grows, all the bytes are copied into new buffers.
	/// This implementation only copies the bytes when GetBuffer is called.
	/// 4. Memory fragmentation - By using homogeneous buffer sizes, it ensures that blocks of memory
	/// can be easily reused.
	///
	/// The stream is implemented on top of a series of uniformly-sized blocks. As the stream's length grows,
	/// additional blocks are retrieved from the memory manager. It is these blocks that are pooled, not the stream
	/// object itself.
	///
	/// The biggest wrinkle in this implementation is when GetBuffer() is called. This requires a single
	/// contiguous buffer. If only a single block is in use, then that block is returned. If multiple blocks
	/// are in use, we retrieve a larger buffer from the memory manager. These large buffers are also pooled,
	/// split by size--they are multiples of a chunk size (1 MB by default).
	///
	/// Once a large buffer is assigned to the stream the blocks are NEVER again used for this stream. All operations take place on the
	/// large buffer. The large buffer can be replaced by a larger buffer from the pool as needed. All blocks and large buffers
	/// are maintained in the stream until the stream is disposed (unless AggressiveBufferReturn is enabled in the stream manager).
	///
	/// </remarks>
	internal class RecyclableMemoryStream : MemoryStream
	{
		private const long MaxStreamLength = int.MaxValue;

		private static readonly byte[] EmptyArray = new byte[0];

		/// <summary>
		/// All of these blocks must be the same size
		/// </summary>
		private readonly List<byte[]> _blocks = new List<byte[]>(1);

		private readonly Guid _id;

		private readonly RecyclableMemoryStreamManager _memoryManager;

		private readonly string _tag;

		/// <summary>
		/// This list is used to store buffers once they're replaced by something larger.
		/// This is for the cases where you have users of this class that may hold onto the buffers longer
		/// than they should and you want to prevent race conditions which could corrupt the data.
		/// </summary>
		private List<byte[]> _dirtyBuffers;

		// long to allow Interlocked.Read (for .NET Standard 1.4 compat)
		private long _disposedState;

		/// <summary>
		/// This is only set by GetBuffer() if the necessary buffer is larger than a single block size, or on
		/// construction if the caller immediately requests a single large buffer.
		/// </summary>
		/// <remarks>If this field is non-null, it contains the concatenation of the bytes found in the individual
		/// blocks. Once it is created, this (or a larger) largeBuffer will be used for the life of the stream.
		/// </remarks>
		private byte[] _largeBuffer;

		/// <summary>
		/// Unique identifier for this stream across it's entire lifetime
		/// </summary>
		/// <exception cref="ObjectDisposedException">Object has been disposed</exception>
		internal Guid Id
		{
			get
			{
				this.CheckDisposed();
				return this._id;
			}
		}

		/// <summary>
		/// A temporary identifier for the current usage of this stream.
		/// </summary>
		/// <exception cref="ObjectDisposedException">Object has been disposed</exception>
		internal string Tag
		{
			get
			{
				this.CheckDisposed();
				return this._tag;
			}
		}

		/// <summary>
		/// Gets the memory manager being used by this stream.
		/// </summary>
		/// <exception cref="ObjectDisposedException">Object has been disposed</exception>
		internal RecyclableMemoryStreamManager MemoryManager
		{
			get
			{
				this.CheckDisposed();
				return this._memoryManager;
			}
		}

		#region Constructors
		/// <summary>
		/// Allocate a new RecyclableMemoryStream object.
		/// </summary>
		/// <param name="memoryManager">The memory manager</param>
		public RecyclableMemoryStream(RecyclableMemoryStreamManager memoryManager)
			: this(memoryManager, null, 0, null) { }

		/// <summary>
		/// Allocate a new RecyclableMemoryStream object
		/// </summary>
		/// <param name="memoryManager">The memory manager</param>
		/// <param name="tag">A string identifying this stream for logging and debugging purposes</param>
		public RecyclableMemoryStream(RecyclableMemoryStreamManager memoryManager, string tag)
			: this(memoryManager, tag, 0, null) { }

		/// <summary>
		/// Allocate a new RecyclableMemoryStream object
		/// </summary>
		/// <param name="memoryManager">The memory manager</param>
		/// <param name="tag">A string identifying this stream for logging and debugging purposes</param>
		/// <param name="requestedSize">The initial requested size to prevent future allocations</param>
		public RecyclableMemoryStream(RecyclableMemoryStreamManager memoryManager, string tag, int requestedSize)
			: this(memoryManager, tag, requestedSize, null) { }

		/// <summary>
		/// Allocate a new RecyclableMemoryStream object
		/// </summary>
		/// <param name="memoryManager">The memory manager</param>
		/// <param name="tag">A string identifying this stream for logging and debugging purposes</param>
		/// <param name="requestedSize">The initial requested size to prevent future allocations</param>
		/// <param name="initialLargeBuffer">An initial buffer to use. This buffer will be owned by the stream and returned to the memory manager upon Dispose.</param>
		internal RecyclableMemoryStream(RecyclableMemoryStreamManager memoryManager, string tag, int requestedSize,
			byte[] initialLargeBuffer)
			: base(EmptyArray)
		{
			this._memoryManager = memoryManager;
			this._id = Guid.NewGuid();
			this._tag = tag;

			if (requestedSize < memoryManager.BlockSize)
			{
				requestedSize = memoryManager.BlockSize;
			}

			if (initialLargeBuffer == null)
			{
				this.EnsureCapacity(requestedSize);
			}
			else
			{
				this._largeBuffer = initialLargeBuffer;
			}
		}
		#endregion

		#region Dispose and Finalize
		~RecyclableMemoryStream()
		{
			this.Dispose(false);
		}

		/// <summary>
		/// Returns the memory used by this stream back to the pool.
		/// </summary>
		/// <param name="disposing">Whether we're disposing (true), or being called by the finalizer (false)</param>
		[SuppressMessage("Microsoft.Usage", "CA1816:CallGCSuppressFinalizeCorrectly",
			Justification = "We have different disposal semantics, so SuppressFinalize is in a different spot.")]
		protected override void Dispose(bool disposing)
		{
			if (Interlocked.CompareExchange(ref this._disposedState, 1, 0) != 0)
			{
				return;
			}

			if (disposing)
			{
				GC.SuppressFinalize(this);
			}
			else
			{
#if !DOTNETCORE
				if (AppDomain.CurrentDomain.IsFinalizingForUnload())
				{
					// If we're being finalized because of a shutdown, don't go any further.
					// We have no idea what's already been cleaned up. Triggering events may cause
					// a crash.
					base.Dispose(disposing);
					return;
				}
#endif
			}

			if (this._largeBuffer != null)
			{
				this._memoryManager.ReturnLargeBuffer(this._largeBuffer, this._tag);
			}

			if (this._dirtyBuffers != null)
			{
				foreach (var buffer in this._dirtyBuffers)
				{
					this._memoryManager.ReturnLargeBuffer(buffer, this._tag);
				}
			}

			this._memoryManager.ReturnBlocks(this._blocks, this._tag);
			this._blocks.Clear();

			base.Dispose(disposing);
		}

		/// <summary>
		/// Equivalent to Dispose
		/// </summary>
        public override void Close()
		{
			this.Dispose(true);
		}
		#endregion

		#region MemoryStream overrides
		/// <summary>
		/// Gets or sets the capacity
		/// </summary>
		/// <remarks>Capacity is always in multiples of the memory manager's block size, unless
		/// the large buffer is in use.  Capacity never decreases during a stream's lifetime.
		/// Explicitly setting the capacity to a lower value than the current value will have no effect.
		/// This is because the buffers are all pooled by chunks and there's little reason to
		/// allow stream truncation.
		/// </remarks>
		/// <exception cref="ObjectDisposedException">Object has been disposed</exception>
		public override int Capacity
		{
			get
			{
				this.CheckDisposed();
				if (this._largeBuffer != null)
				{
					return this._largeBuffer.Length;
				}

				var size = (long)this._blocks.Count * this._memoryManager.BlockSize;
				return (int)Math.Min(int.MaxValue, size);
			}
			set
			{
				this.CheckDisposed();
				this.EnsureCapacity(value);
			}
		}

		private int length;

		/// <summary>
		/// Gets the number of bytes written to this stream.
		/// </summary>
		/// <exception cref="ObjectDisposedException">Object has been disposed</exception>
		public override long Length
		{
			get
			{
				this.CheckDisposed();
				return this.length;
			}
		}

		private int position;

		/// <summary>
		/// Gets the current position in the stream
		/// </summary>
		/// <exception cref="ObjectDisposedException">Object has been disposed</exception>
		public override long Position
		{
			get
			{
				this.CheckDisposed();
				return this.position;
			}
			set
			{
				this.CheckDisposed();
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "value must be non-negative");
				}

				if (value > MaxStreamLength)
				{
					throw new ArgumentOutOfRangeException("value", "value cannot be more than " + MaxStreamLength);
				}

				this.position = (int)value;
			}
		}

		/// <summary>
		/// Whether the stream can currently read
		/// </summary>
		public override bool CanRead => !this.Disposed;

		/// <summary>
		/// Whether the stream can currently seek
		/// </summary>
		public override bool CanSeek => !this.Disposed;

		/// <summary>
		/// Always false
		/// </summary>
		public override bool CanTimeout => false;

		/// <summary>
		/// Whether the stream can currently write
		/// </summary>
		public override bool CanWrite => !this.Disposed;

		/// <summary>
		/// Returns a single buffer containing the contents of the stream.
		/// The buffer may be longer than the stream length.
		/// </summary>
		/// <returns>A byte[] buffer</returns>
		/// <remarks>IMPORTANT: Doing a Write() after calling GetBuffer() invalidates the buffer. The old buffer is held onto
		/// until Dispose is called, but the next time GetBuffer() is called, a new buffer from the pool will be required.</remarks>
		/// <exception cref="ObjectDisposedException">Object has been disposed</exception>
        public override byte[] GetBuffer()
		{
			this.CheckDisposed();

			if (this._largeBuffer != null)
			{
				return this._largeBuffer;
			}

			if (this._blocks.Count == 1)
			{
				return this._blocks[0];
			}

			// Buffer needs to reflect the capacity, not the length, because
			// it's possible that people will manipulate the buffer directly
			// and set the length afterward. Capacity sets the expectation
			// for the size of the buffer.
			var newBuffer = this._memoryManager.GetLargeBuffer(this.Capacity, this._tag);

			// InternalRead will check for existence of largeBuffer, so make sure we
			// don't set it until after we've copied the data.
			this.InternalRead(newBuffer, 0, this.length, 0);
			this._largeBuffer = newBuffer;

			if (this._blocks.Count > 0 && this._memoryManager.AggressiveBufferReturn)
			{
				this._memoryManager.ReturnBlocks(this._blocks, this._tag);
				this._blocks.Clear();
			}

			return this._largeBuffer;
		}

		/// <summary>
		/// Returns a new array with a copy of the buffer's contents. You should almost certainly be using GetBuffer combined with the Length to
		/// access the bytes in this stream. Calling ToArray will destroy the benefits of pooled buffers, but it is included
		/// for the sake of completeness.
		/// </summary>
		/// <exception cref="ObjectDisposedException">Object has been disposed</exception>
#pragma warning disable CS0809
		[Obsolete("This method has degraded performance vs. GetBuffer and should be avoided.")]
		public override byte[] ToArray()
		{
			this.CheckDisposed();
			var newBuffer = new byte[this.Length];

			this.InternalRead(newBuffer, 0, this.length, 0);
			return newBuffer;
		}
#pragma warning restore CS0809

		/// <summary>
		/// Reads from the current position into the provided buffer
		/// </summary>
		/// <param name="buffer">Destination buffer</param>
		/// <param name="offset">Offset into buffer at which to start placing the read bytes.</param>
		/// <param name="count">Number of bytes to read.</param>
		/// <returns>The number of bytes read</returns>
		/// <exception cref="ArgumentNullException">buffer is null</exception>
		/// <exception cref="ArgumentOutOfRangeException">offset or count is less than 0</exception>
		/// <exception cref="ArgumentException">offset subtracted from the buffer length is less than count</exception>
		/// <exception cref="ObjectDisposedException">Object has been disposed</exception>
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.SafeRead(buffer, offset, count, ref this.position);
		}

		/// <summary>
		/// Reads from the specified position into the provided buffer
		/// </summary>
		/// <param name="buffer">Destination buffer</param>
		/// <param name="offset">Offset into buffer at which to start placing the read bytes.</param>
		/// <param name="count">Number of bytes to read.</param>
		/// <param name="streamPosition">Position in the stream to start reading from</param>
		/// <returns>The number of bytes read</returns>
		/// <exception cref="ArgumentNullException">buffer is null</exception>
		/// <exception cref="ArgumentOutOfRangeException">offset or count is less than 0</exception>
		/// <exception cref="ArgumentException">offset subtracted from the buffer length is less than count</exception>
		/// <exception cref="ObjectDisposedException">Object has been disposed</exception>
		public int SafeRead(byte[] buffer, int offset, int count, ref int streamPosition)
		{
			this.CheckDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException(nameof(buffer));
			}

			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(offset), "offset cannot be negative");
			}

			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), "count cannot be negative");
			}

			if (offset + count > buffer.Length)
			{
				throw new ArgumentException("buffer length must be at least offset + count");
			}

			var amountRead = this.InternalRead(buffer, offset, count, streamPosition);
			streamPosition += amountRead;
			return amountRead;
		}

		/// <summary>
		/// Writes the buffer to the stream
		/// </summary>
		/// <param name="buffer">Source buffer</param>
		/// <param name="offset">Start position</param>
		/// <param name="count">Number of bytes to write</param>
		/// <exception cref="ArgumentNullException">buffer is null</exception>
		/// <exception cref="ArgumentOutOfRangeException">offset or count is negative</exception>
		/// <exception cref="ArgumentException">buffer.Length - offset is not less than count</exception>
		/// <exception cref="ObjectDisposedException">Object has been disposed</exception>
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException(nameof(buffer));
			}

			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(offset), offset,
					"Offset must be in the range of 0 - buffer.Length-1");
			}

			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), count, "count must be non-negative");
			}

			if (count + offset > buffer.Length)
			{
				throw new ArgumentException("count must be greater than buffer.Length - offset");
			}

			var blockSize = this._memoryManager.BlockSize;
			var end = (long)this.position + count;
			// Check for overflow
			if (end > MaxStreamLength)
			{
				throw new IOException("Maximum capacity exceeded");
			}

			var requiredBuffers = (end + blockSize - 1) / blockSize;

			if (requiredBuffers * blockSize > MaxStreamLength)
			{
				throw new IOException("Maximum capacity exceeded");
			}

			this.EnsureCapacity((int)end);

			if (this._largeBuffer == null)
			{
				var bytesRemaining = count;
				var bytesWritten = 0;
				var blockAndOffset = this.GetBlockAndRelativeOffset(this.position);

				while (bytesRemaining > 0)
				{
					var currentBlock = this._blocks[blockAndOffset.Block];
					var remainingInBlock = blockSize - blockAndOffset.Offset;
					var amountToWriteInBlock = Math.Min(remainingInBlock, bytesRemaining);

					Buffer.BlockCopy(buffer, offset + bytesWritten, currentBlock, blockAndOffset.Offset,
						amountToWriteInBlock);

					bytesRemaining -= amountToWriteInBlock;
					bytesWritten += amountToWriteInBlock;

					++blockAndOffset.Block;
					blockAndOffset.Offset = 0;
				}
			}
			else
			{
				Buffer.BlockCopy(buffer, offset, this._largeBuffer, this.position, count);
			}
			this.position = (int)end;
			this.length = Math.Max(this.position, this.length);
		}

		/// <summary>
		/// Returns a useful string for debugging. This should not normally be called in actual production code.
		/// </summary>
		public override string ToString()
		{
			return $"Id = {this.Id}, Tag = {this.Tag}, Length = {this.Length:N0} bytes";
		}

		/// <summary>
		/// Writes a single byte to the current position in the stream.
		/// </summary>
		/// <param name="value">byte value to write</param>
		/// <exception cref="ObjectDisposedException">Object has been disposed</exception>
		public override void WriteByte(byte value)
		{
			this.CheckDisposed();
			var end = this.position + 1;

			// Check for overflow
			if (end > MaxStreamLength)
			{
				throw new IOException("Maximum capacity exceeded");
			}

			this.EnsureCapacity(end);
			if (this._largeBuffer == null)
			{
				var blockSize = this._memoryManager.BlockSize;
				var block = this.position / blockSize;
				var offset = this.position - block * blockSize;
				var currentBlock = this._blocks[block];
				currentBlock[offset] = value;
			}
			else
			{
				this._largeBuffer[this.position] = value;
			}

			this.position = end;
			this.length = Math.Max(this.position, this.length);
		}

		/// <summary>
		/// Reads a single byte from the current position in the stream.
		/// </summary>
		/// <returns>The byte at the current position, or -1 if the position is at the end of the stream.</returns>
		/// <exception cref="ObjectDisposedException">Object has been disposed</exception>
		public override int ReadByte()
		{
			return this.SafeReadByte(ref this.position);
		}

		/// <summary>
		/// Reads a single byte from the specified position in the stream.
		/// </summary>
		/// <param name="streamPosition">The position in the stream to read from</param>
		/// <returns>The byte at the current position, or -1 if the position is at the end of the stream.</returns>
		/// <exception cref="ObjectDisposedException">Object has been disposed</exception>
		public int SafeReadByte(ref int streamPosition)
		{
			this.CheckDisposed();
			if (streamPosition == this.length)
			{
				return -1;
			}
			byte value;
			if (this._largeBuffer == null)
			{
				var blockAndOffset = this.GetBlockAndRelativeOffset(streamPosition);
				value = this._blocks[blockAndOffset.Block][blockAndOffset.Offset];
			}
			else
			{
				value = this._largeBuffer[streamPosition];
			}
			streamPosition++;
			return value;
		}

		/// <summary>
		/// Sets the length of the stream
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">value is negative or larger than MaxStreamLength</exception>
		/// <exception cref="ObjectDisposedException">Object has been disposed</exception>
		public override void SetLength(long value)
		{
			this.CheckDisposed();
			if (value < 0 || value > MaxStreamLength)
			{
				throw new ArgumentOutOfRangeException(nameof(value),
					"value must be non-negative and at most " + MaxStreamLength);
			}

			this.EnsureCapacity((int)value);

			this.length = (int)value;
			if (this.position > value)
			{
				this.position = (int)value;
			}
		}

		/// <summary>
		/// Sets the position to the offset from the seek location
		/// </summary>
		/// <param name="offset">How many bytes to move</param>
		/// <param name="loc">From where</param>
		/// <returns>The new position</returns>
		/// <exception cref="ObjectDisposedException">Object has been disposed</exception>
		/// <exception cref="ArgumentOutOfRangeException">offset is larger than MaxStreamLength</exception>
		/// <exception cref="ArgumentException">Invalid seek origin</exception>
		/// <exception cref="IOException">Attempt to set negative position</exception>
		public override long Seek(long offset, SeekOrigin loc)
		{
			this.CheckDisposed();
			if (offset > MaxStreamLength)
			{
				throw new ArgumentOutOfRangeException(nameof(offset), "offset cannot be larger than " + MaxStreamLength);
			}

			int newPosition;
			switch (loc)
			{
				case SeekOrigin.Begin:
					newPosition = (int)offset;
					break;
				case SeekOrigin.Current:
					newPosition = (int)offset + this.position;
					break;
				case SeekOrigin.End:
					newPosition = (int)offset + this.length;
					break;
				default:
					throw new ArgumentException("Invalid seek origin", nameof(loc));
			}
			if (newPosition < 0)
			{
				throw new IOException("Seek before beginning");
			}
			this.position = newPosition;
			return this.position;
		}

		/// <summary>
		/// Synchronously writes this stream's bytes to the parameter stream.
		/// </summary>
		/// <param name="stream">Destination stream</param>
		/// <remarks>Important: This does a synchronous write, which may not be desired in some situations</remarks>
		public override void WriteTo(Stream stream)
		{
			this.CheckDisposed();
			if (stream == null)
			{
				throw new ArgumentNullException(nameof(stream));
			}

			if (this._largeBuffer == null)
			{
				var currentBlock = 0;
				var bytesRemaining = this.length;

				while (bytesRemaining > 0)
				{
					var amountToCopy = Math.Min(this._blocks[currentBlock].Length, bytesRemaining);
					stream.Write(this._blocks[currentBlock], 0, amountToCopy);

					bytesRemaining -= amountToCopy;

					++currentBlock;
				}
			}
			else
			{
				stream.Write(this._largeBuffer, 0, this.length);
			}
		}
		#endregion

		#region Helper Methods
		private bool Disposed => Interlocked.Read(ref this._disposedState) != 0;

		private void CheckDisposed()
		{
			if (this.Disposed)
			{
				throw new ObjectDisposedException($"The stream with Id {this._id} and Tag {this._tag} is disposed.");
			}
		}

		private int InternalRead(byte[] buffer, int offset, int count, int fromPosition)
		{
			if (this.length - fromPosition <= 0)
			{
				return 0;
			}

			int amountToCopy;

			if (this._largeBuffer == null)
			{
				var blockAndOffset = this.GetBlockAndRelativeOffset(fromPosition);
				var bytesWritten = 0;
				var bytesRemaining = Math.Min(count, this.length - fromPosition);

				while (bytesRemaining > 0)
				{
					amountToCopy = Math.Min(this._blocks[blockAndOffset.Block].Length - blockAndOffset.Offset,
						bytesRemaining);

					Buffer.BlockCopy(this._blocks[blockAndOffset.Block], blockAndOffset.Offset, buffer,
						bytesWritten + offset, amountToCopy);

					bytesWritten += amountToCopy;
					bytesRemaining -= amountToCopy;

					++blockAndOffset.Block;
					blockAndOffset.Offset = 0;
				}
				return bytesWritten;
			}
			amountToCopy = Math.Min(count, this.length - fromPosition);
			Buffer.BlockCopy(this._largeBuffer, fromPosition, buffer, offset, amountToCopy);
			return amountToCopy;
		}

		private struct BlockAndOffset
		{
			public int Block;
			public int Offset;

			public BlockAndOffset(int block, int offset)
			{
				this.Block = block;
				this.Offset = offset;
			}
		}

		private BlockAndOffset GetBlockAndRelativeOffset(int offset)
		{
			var blockSize = this._memoryManager.BlockSize;
			return new BlockAndOffset(offset / blockSize, offset % blockSize);
		}

		private void EnsureCapacity(int newCapacity)
		{
			if (newCapacity > this._memoryManager.MaximumStreamCapacity && this._memoryManager.MaximumStreamCapacity > 0)
			{
				throw new InvalidOperationException("Requested capacity is too large: " + newCapacity + ". Limit is " +
				                                    this._memoryManager.MaximumStreamCapacity);
			}

			if (this._largeBuffer != null)
			{
				if (newCapacity > this._largeBuffer.Length)
				{
					var newBuffer = this._memoryManager.GetLargeBuffer(newCapacity, this._tag);
					this.InternalRead(newBuffer, 0, this.length, 0);
					this.ReleaseLargeBuffer();
					this._largeBuffer = newBuffer;
				}
			}
			else
			{
				while (this.Capacity < newCapacity)
				{
					_blocks.Add((this._memoryManager.GetBlock()));
				}
			}
		}

		/// <summary>
		/// Release the large buffer (either stores it for eventual release or returns it immediately).
		/// </summary>
		private void ReleaseLargeBuffer()
		{
			if (this._memoryManager.AggressiveBufferReturn)
			{
				this._memoryManager.ReturnLargeBuffer(this._largeBuffer, this._tag);
			}
			else
			{
				if (this._dirtyBuffers == null)
				{
					// We most likely will only ever need space for one
					this._dirtyBuffers = new List<byte[]>(1);
				}
				this._dirtyBuffers.Add(this._largeBuffer);
			}

			this._largeBuffer = null;
		}
		#endregion
	}
}
