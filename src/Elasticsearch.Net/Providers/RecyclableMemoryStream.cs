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
	///  MemoryStream implementation that deals with pooling and managing memory streams which use potentially large
	///  buffers.
	/// </summary>
	/// <remarks>
	///  This class works in tandem with the RecylableMemoryStreamManager to supply MemoryStream
	///  objects to callers, while avoiding these specific problems:
	///  1. LOH allocations - since all large buffers are pooled, they will never incur a Gen2 GC
	///  2. Memory waste - A standard memory stream doubles its size when it runs out of room. This
	///  leads to continual memory growth as each stream approaches the maximum allowed size.
	///  3. Memory copying - Each time a MemoryStream grows, all the bytes are copied into new buffers.
	///  This implementation only copies the bytes when GetBuffer is called.
	///  4. Memory fragmentation - By using homogeneous buffer sizes, it ensures that blocks of memory
	///  can be easily reused.
	///  The stream is implemented on top of a series of uniformly-sized blocks. As the stream's length grows,
	///  additional blocks are retrieved from the memory manager. It is these blocks that are pooled, not the stream
	///  object itself.
	///  The biggest wrinkle in this implementation is when GetBuffer() is called. This requires a single
	///  contiguous buffer. If only a single block is in use, then that block is returned. If multiple blocks
	///  are in use, we retrieve a larger buffer from the memory manager. These large buffers are also pooled,
	///  split by size--they are multiples of a chunk size (1 MB by default).
	///  Once a large buffer is assigned to the stream the blocks are NEVER again used for this stream. All operations take place on the
	///  large buffer. The large buffer can be replaced by a larger buffer from the pool as needed. All blocks and large buffers
	///  are maintained in the stream until the stream is disposed (unless AggressiveBufferReturn is enabled in the stream manager).
	/// </remarks>
	internal class RecyclableMemoryStream : MemoryStream
	{
		private const long MaxStreamLength = Int32.MaxValue;

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
		/// <remarks>
		/// If this field is non-null, it contains the concatenation of the bytes found in the individual
		/// blocks. Once it is created, this (or a larger) largeBuffer will be used for the life of the stream.
		/// </remarks>
		private byte[] _largeBuffer;

		private int length;

		private int position;

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
		/// <param name="initialLargeBuffer">
		/// An initial buffer to use. This buffer will be owned by the stream and returned to the memory manager upon
		/// Dispose.
		/// </param>
		internal RecyclableMemoryStream(RecyclableMemoryStreamManager memoryManager, string tag, int requestedSize,
			byte[] initialLargeBuffer
		)
			: base(EmptyArray)
		{
			_memoryManager = memoryManager;
			_id = Guid.NewGuid();
			_tag = tag;

			if (requestedSize < memoryManager.BlockSize) requestedSize = memoryManager.BlockSize;

			if (initialLargeBuffer == null)
				EnsureCapacity(requestedSize);
			else
				_largeBuffer = initialLargeBuffer;
		}

		/// <summary>
		/// Whether the stream can currently read
		/// </summary>
		public override bool CanRead => !Disposed;

		/// <summary>
		/// Whether the stream can currently seek
		/// </summary>
		public override bool CanSeek => !Disposed;

		/// <summary>
		/// Always false
		/// </summary>
		public override bool CanTimeout => false;

		/// <summary>
		/// Whether the stream can currently write
		/// </summary>
		public override bool CanWrite => !Disposed;

		/// <summary>
		/// Gets or sets the capacity
		/// </summary>
		/// <remarks>
		/// Capacity is always in multiples of the memory manager's block size, unless
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
				CheckDisposed();
				if (_largeBuffer != null) return _largeBuffer.Length;

				var size = (long)_blocks.Count * _memoryManager.BlockSize;
				return (int)Math.Min(int.MaxValue, size);
			}
			set
			{
				CheckDisposed();
				EnsureCapacity(value);
			}
		}

		/// <summary>
		/// Gets the number of bytes written to this stream.
		/// </summary>
		/// <exception cref="ObjectDisposedException">Object has been disposed</exception>
		public override long Length
		{
			get
			{
				CheckDisposed();
				return length;
			}
		}

		/// <summary>
		/// Gets the current position in the stream
		/// </summary>
		/// <exception cref="ObjectDisposedException">Object has been disposed</exception>
		public override long Position
		{
			get
			{
				CheckDisposed();
				return position;
			}
			set
			{
				CheckDisposed();
				if (value < 0) throw new ArgumentOutOfRangeException("value", "value must be non-negative");

				if (value > MaxStreamLength) throw new ArgumentOutOfRangeException("value", "value cannot be more than " + MaxStreamLength);

				position = (int)value;
			}
		}

		/// <summary>
		/// Unique identifier for this stream across it's entire lifetime
		/// </summary>
		/// <exception cref="ObjectDisposedException">Object has been disposed</exception>
		internal Guid Id
		{
			get
			{
				CheckDisposed();
				return _id;
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
				CheckDisposed();
				return _memoryManager;
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
				CheckDisposed();
				return _tag;
			}
		}

		private bool Disposed => Interlocked.Read(ref _disposedState) != 0;

		~RecyclableMemoryStream() => Dispose(false);

		/// <summary>
		/// Returns the memory used by this stream back to the pool.
		/// </summary>
		/// <param name="disposing">Whether we're disposing (true), or being called by the finalizer (false)</param>
		[SuppressMessage("Microsoft.Usage", "CA1816:CallGCSuppressFinalizeCorrectly",
			Justification = "We have different disposal semantics, so SuppressFinalize is in a different spot.")]
		protected override void Dispose(bool disposing)
		{
			if (Interlocked.CompareExchange(ref _disposedState, 1, 0) != 0) return;

			if (disposing)
				GC.SuppressFinalize(this);
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

			if (_largeBuffer != null) _memoryManager.ReturnLargeBuffer(_largeBuffer, _tag);

			if (_dirtyBuffers != null)
				foreach (var buffer in _dirtyBuffers)
					_memoryManager.ReturnLargeBuffer(buffer, _tag);

			_memoryManager.ReturnBlocks(_blocks, _tag);
			_blocks.Clear();

			base.Dispose(disposing);
		}

		/// <summary>
		/// Equivalent to Dispose
		/// </summary>
		public override void Close() => Dispose(true);

		/// <summary>
		/// Returns a single buffer containing the contents of the stream.
		/// The buffer may be longer than the stream length.
		/// </summary>
		/// <returns>A byte[] buffer</returns>
		/// <remarks>
		/// IMPORTANT: Doing a Write() after calling GetBuffer() invalidates the buffer. The old buffer is held onto
		/// until Dispose is called, but the next time GetBuffer() is called, a new buffer from the pool will be required.
		/// </remarks>
		/// <exception cref="ObjectDisposedException">Object has been disposed</exception>
		public override byte[] GetBuffer()
		{
			CheckDisposed();

			if (_largeBuffer != null) return _largeBuffer;

			if (_blocks.Count == 1) return _blocks[0];

			// Buffer needs to reflect the capacity, not the length, because
			// it's possible that people will manipulate the buffer directly
			// and set the length afterward. Capacity sets the expectation
			// for the size of the buffer.
			var newBuffer = _memoryManager.GetLargeBuffer(Capacity, _tag);

			// InternalRead will check for existence of largeBuffer, so make sure we
			// don't set it until after we've copied the data.
			InternalRead(newBuffer, 0, length, 0);
			_largeBuffer = newBuffer;

			if (_blocks.Count > 0 && _memoryManager.AggressiveBufferReturn)
			{
				_memoryManager.ReturnBlocks(_blocks, _tag);
				_blocks.Clear();
			}

			return _largeBuffer;
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
			CheckDisposed();
			var newBuffer = new byte[Length];

			InternalRead(newBuffer, 0, length, 0);
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
		public override int Read(byte[] buffer, int offset, int count) => SafeRead(buffer, offset, count, ref position);

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
			CheckDisposed();
			if (buffer == null) throw new ArgumentNullException(nameof(buffer));

			if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset), "offset cannot be negative");

			if (count < 0) throw new ArgumentOutOfRangeException(nameof(count), "count cannot be negative");

			if (offset + count > buffer.Length) throw new ArgumentException("buffer length must be at least offset + count");

			var amountRead = InternalRead(buffer, offset, count, streamPosition);
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
			CheckDisposed();
			if (buffer == null) throw new ArgumentNullException(nameof(buffer));

			if (offset < 0)
				throw new ArgumentOutOfRangeException(nameof(offset), offset,
					"Offset must be in the range of 0 - buffer.Length-1");

			if (count < 0) throw new ArgumentOutOfRangeException(nameof(count), count, "count must be non-negative");

			if (count + offset > buffer.Length) throw new ArgumentException("count must be greater than buffer.Length - offset");

			var blockSize = _memoryManager.BlockSize;
			var end = (long)position + count;
			// Check for overflow
			if (end > MaxStreamLength) throw new IOException("Maximum capacity exceeded");

			var requiredBuffers = (end + blockSize - 1) / blockSize;

			if (requiredBuffers * blockSize > MaxStreamLength) throw new IOException("Maximum capacity exceeded");

			EnsureCapacity((int)end);

			if (_largeBuffer == null)
			{
				var bytesRemaining = count;
				var bytesWritten = 0;
				var blockAndOffset = GetBlockAndRelativeOffset(position);

				while (bytesRemaining > 0)
				{
					var currentBlock = _blocks[blockAndOffset.Block];
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
				Buffer.BlockCopy(buffer, offset, _largeBuffer, position, count);
			position = (int)end;
			length = Math.Max(position, length);
		}

		/// <summary>
		/// Returns a useful string for debugging. This should not normally be called in actual production code.
		/// </summary>
		public override string ToString() => $"Id = {Id}, Tag = {Tag}, Length = {Length:N0} bytes";

		/// <summary>
		/// Writes a single byte to the current position in the stream.
		/// </summary>
		/// <param name="value">byte value to write</param>
		/// <exception cref="ObjectDisposedException">Object has been disposed</exception>
		public override void WriteByte(byte value)
		{
			CheckDisposed();
			var end = position + 1;

			// Check for overflow
			if (end > MaxStreamLength) throw new IOException("Maximum capacity exceeded");

			EnsureCapacity(end);
			if (_largeBuffer == null)
			{
				var blockSize = _memoryManager.BlockSize;
				var block = position / blockSize;
				var offset = position - block * blockSize;
				var currentBlock = _blocks[block];
				currentBlock[offset] = value;
			}
			else
				_largeBuffer[position] = value;

			position = end;
			length = Math.Max(position, length);
		}

		/// <summary>
		/// Reads a single byte from the current position in the stream.
		/// </summary>
		/// <returns>The byte at the current position, or -1 if the position is at the end of the stream.</returns>
		/// <exception cref="ObjectDisposedException">Object has been disposed</exception>
		public override int ReadByte() => SafeReadByte(ref position);

		/// <summary>
		/// Reads a single byte from the specified position in the stream.
		/// </summary>
		/// <param name="streamPosition">The position in the stream to read from</param>
		/// <returns>The byte at the current position, or -1 if the position is at the end of the stream.</returns>
		/// <exception cref="ObjectDisposedException">Object has been disposed</exception>
		public int SafeReadByte(ref int streamPosition)
		{
			CheckDisposed();
			if (streamPosition == length) return -1;

			byte value;
			if (_largeBuffer == null)
			{
				var blockAndOffset = GetBlockAndRelativeOffset(streamPosition);
				value = _blocks[blockAndOffset.Block][blockAndOffset.Offset];
			}
			else
				value = _largeBuffer[streamPosition];
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
			CheckDisposed();
			if (value < 0 || value > MaxStreamLength)
				throw new ArgumentOutOfRangeException(nameof(value),
					"value must be non-negative and at most " + MaxStreamLength);

			EnsureCapacity((int)value);

			length = (int)value;
			if (position > value) position = (int)value;
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
			CheckDisposed();
			if (offset > MaxStreamLength) throw new ArgumentOutOfRangeException(nameof(offset), "offset cannot be larger than " + MaxStreamLength);

			int newPosition;
			switch (loc)
			{
				case SeekOrigin.Begin:
					newPosition = (int)offset;
					break;
				case SeekOrigin.Current:
					newPosition = (int)offset + position;
					break;
				case SeekOrigin.End:
					newPosition = (int)offset + length;
					break;
				default:
					throw new ArgumentException("Invalid seek origin", nameof(loc));
			}
			if (newPosition < 0) throw new IOException("Seek before beginning");

			position = newPosition;
			return position;
		}

		/// <summary>
		/// Synchronously writes this stream's bytes to the parameter stream.
		/// </summary>
		/// <param name="stream">Destination stream</param>
		/// <remarks>Important: This does a synchronous write, which may not be desired in some situations</remarks>
		public override void WriteTo(Stream stream)
		{
			CheckDisposed();
			if (stream == null) throw new ArgumentNullException(nameof(stream));

			if (_largeBuffer == null)
			{
				var currentBlock = 0;
				var bytesRemaining = length;

				while (bytesRemaining > 0)
				{
					var amountToCopy = Math.Min(_blocks[currentBlock].Length, bytesRemaining);
					stream.Write(_blocks[currentBlock], 0, amountToCopy);

					bytesRemaining -= amountToCopy;

					++currentBlock;
				}
			}
			else
				stream.Write(_largeBuffer, 0, length);
		}

		private void CheckDisposed()
		{
			if (Disposed) throw new ObjectDisposedException($"The stream with Id {_id} and Tag {_tag} is disposed.");
		}

		private int InternalRead(byte[] buffer, int offset, int count, int fromPosition)
		{
			if (length - fromPosition <= 0) return 0;

			int amountToCopy;

			if (_largeBuffer == null)
			{
				var blockAndOffset = GetBlockAndRelativeOffset(fromPosition);
				var bytesWritten = 0;
				var bytesRemaining = Math.Min(count, length - fromPosition);

				while (bytesRemaining > 0)
				{
					amountToCopy = Math.Min(_blocks[blockAndOffset.Block].Length - blockAndOffset.Offset,
						bytesRemaining);

					Buffer.BlockCopy(_blocks[blockAndOffset.Block], blockAndOffset.Offset, buffer,
						bytesWritten + offset, amountToCopy);

					bytesWritten += amountToCopy;
					bytesRemaining -= amountToCopy;

					++blockAndOffset.Block;
					blockAndOffset.Offset = 0;
				}
				return bytesWritten;
			}
			amountToCopy = Math.Min(count, length - fromPosition);
			Buffer.BlockCopy(_largeBuffer, fromPosition, buffer, offset, amountToCopy);
			return amountToCopy;
		}

		private BlockAndOffset GetBlockAndRelativeOffset(int offset)
		{
			var blockSize = _memoryManager.BlockSize;
			return new BlockAndOffset(offset / blockSize, offset % blockSize);
		}

		private void EnsureCapacity(int newCapacity)
		{
			if (newCapacity > _memoryManager.MaximumStreamCapacity && _memoryManager.MaximumStreamCapacity > 0)
				throw new InvalidOperationException("Requested capacity is too large: " + newCapacity + ". Limit is " +
					_memoryManager.MaximumStreamCapacity);

			if (_largeBuffer != null)
			{
				if (newCapacity > _largeBuffer.Length)
				{
					var newBuffer = _memoryManager.GetLargeBuffer(newCapacity, _tag);
					InternalRead(newBuffer, 0, length, 0);
					ReleaseLargeBuffer();
					_largeBuffer = newBuffer;
				}
			}
			else
				while (Capacity < newCapacity)
					_blocks.Add(_memoryManager.GetBlock());
		}

		/// <summary>
		/// Release the large buffer (either stores it for eventual release or returns it immediately).
		/// </summary>
		private void ReleaseLargeBuffer()
		{
			if (_memoryManager.AggressiveBufferReturn)
				_memoryManager.ReturnLargeBuffer(_largeBuffer, _tag);
			else
			{
				if (_dirtyBuffers == null) _dirtyBuffers = new List<byte[]>(1);
				_dirtyBuffers.Add(_largeBuffer);
			}

			_largeBuffer = null;
		}

		private struct BlockAndOffset
		{
			public int Block;
			public int Offset;

			public BlockAndOffset(int block, int offset)
			{
				Block = block;
				Offset = offset;
			}
		}
	}
}
