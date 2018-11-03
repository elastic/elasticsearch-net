// ---------------------------------------------------------------------
// Copyright (c) 2015-2016 Microsoft
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// ---------------------------------------------------------------------
//
// https://github.com/Microsoft/Microsoft.IO.RecyclableMemoryStream/blob/master/src/RecyclableMemoryStreamManager.cs
// MIT license: https://github.com/Microsoft/Microsoft.IO.RecyclableMemoryStream/blob/master/LICENSE

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;

namespace Elasticsearch.Net
{
	internal class RecyclableMemoryStreamManager
	{
		public const int DefaultBlockSize = 128 * 1024;
		public const int DefaultLargeBufferMultiple = 1024 * 1024;
		public const int DefaultMaximumBufferSize = 128 * 1024 * 1024;

		private readonly long[] _largeBufferFreeSize;
		private readonly long[] _largeBufferInUseSize;

		/// <summary>
		/// pools[0] = 1x largeBufferMultiple buffers
		/// pools[1] = 2x largeBufferMultiple buffers
		/// etc., up to maximumBufferSize
		/// </summary>
		private readonly ConcurrentStack<byte[]>[] largePools;

		private readonly ConcurrentStack<byte[]> smallPool;

		private long _smallPoolFreeSize;
		private long _smallPoolInUseSize;

		/// <summary>
		/// Initializes the memory manager with the default block/buffer specifications.
		/// </summary>
		public RecyclableMemoryStreamManager()
			: this(DefaultBlockSize, DefaultLargeBufferMultiple, DefaultMaximumBufferSize) { }

		/// <summary>
		/// Initializes the memory manager with the given block requiredSize.
		/// </summary>
		/// <param name="blockSize">Size of each block that is pooled. Must be > 0.</param>
		/// <param name="largeBufferMultiple">Each large buffer will be a multiple of this value.</param>
		/// <param name="maximumBufferSize">Buffers larger than this are not pooled</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// blockSize is not a positive number, or largeBufferMultiple is not a positive number, or
		/// maximumBufferSize is less than blockSize.
		/// </exception>
		/// <exception cref="ArgumentException">maximumBufferSize is not a multiple of largeBufferMultiple</exception>
		public RecyclableMemoryStreamManager(int blockSize, int largeBufferMultiple, int maximumBufferSize)
		{
			if (blockSize <= 0) throw new ArgumentOutOfRangeException(nameof(blockSize), blockSize, "blockSize must be a positive number");

			if (largeBufferMultiple <= 0)
				throw new ArgumentOutOfRangeException(nameof(largeBufferMultiple),
					"largeBufferMultiple must be a positive number");

			if (maximumBufferSize < blockSize)
				throw new ArgumentOutOfRangeException(nameof(maximumBufferSize),
					"maximumBufferSize must be at least blockSize");

			BlockSize = blockSize;
			LargeBufferMultiple = largeBufferMultiple;
			MaximumBufferSize = maximumBufferSize;

			if (!IsLargeBufferMultiple(maximumBufferSize))
				throw new ArgumentException("maximumBufferSize is not a multiple of largeBufferMultiple",
					nameof(maximumBufferSize));

			smallPool = new ConcurrentStack<byte[]>();
			var numLargePools = maximumBufferSize / largeBufferMultiple;

			// +1 to store size of bytes in use that are too large to be pooled
			_largeBufferInUseSize = new long[numLargePools + 1];
			_largeBufferFreeSize = new long[numLargePools];

			largePools = new ConcurrentStack<byte[]>[numLargePools];

			for (var i = 0; i < largePools.Length; ++i) largePools[i] = new ConcurrentStack<byte[]>();
		}

		/// <summary>
		/// Whether dirty buffers can be immediately returned to the buffer pool. E.g. when GetBuffer() is called on
		/// a stream and creates a single large buffer, if this setting is enabled, the other blocks will be returned
		/// to the buffer pool immediately.
		/// Note when enabling this setting that the user is responsible for ensuring that any buffer previously
		/// retrieved from a stream which is subsequently modified is not used after modification (as it may no longer
		/// be valid).
		/// </summary>
		public bool AggressiveBufferReturn { get; set; }

		/// <summary>
		/// The size of each block. It must be set at creation and cannot be changed.
		/// </summary>
		public int BlockSize { get; }

		/// <summary>
		/// All buffers are multiples of this number. It must be set at creation and cannot be changed.
		/// </summary>
		public int LargeBufferMultiple { get; }

		/// <summary>
		/// How many buffers are in the large pool
		/// </summary>
		public long LargeBuffersFree
		{
			get
			{
				long free = 0;
				foreach (var pool in largePools) free += pool.Count;
				return free;
			}
		}

		/// <summary>
		/// Number of bytes in large pool not currently in use
		/// </summary>
		public long LargePoolFreeSize
		{
			get
			{
				long sum = 0;
				for (var index = 0; index < _largeBufferFreeSize.Length; index++)
				{
					var freeSize = _largeBufferFreeSize[index];
					sum += freeSize;
				}

				return sum;
			}
		}

		/// <summary>
		/// Number of bytes currently in use by streams from the large pool
		/// </summary>
		public long LargePoolInUseSize
		{
			get
			{
				long sum = 0;
				for (var index = 0; index < _largeBufferInUseSize.Length; index++)
				{
					var inUseSize = _largeBufferInUseSize[index];
					sum += inUseSize;
				}

				return sum;
			}
		}

		/// <summary>
		/// Gets or sets the maximum buffer size.
		/// </summary>
		/// <remarks>
		/// Any buffer that is returned to the pool that is larger than this will be
		/// discarded and garbage collected.
		/// </remarks>
		public int MaximumBufferSize { get; }

		/// <summary>
		/// How many bytes of large free buffers to allow before we start dropping
		/// those returned to us.
		/// </summary>
		public long MaximumFreeLargePoolBytes { get; set; }

		/// <summary>
		/// How many bytes of small free blocks to allow before we start dropping
		/// those returned to us.
		/// </summary>
		public long MaximumFreeSmallPoolBytes { get; set; }

		/// <summary>
		/// Maximum stream capacity in bytes. Attempts to set a larger capacity will
		/// result in an exception.
		/// </summary>
		/// <remarks>A value of 0 indicates no limit.</remarks>
		public long MaximumStreamCapacity { get; set; }

		/// <summary>
		/// How many blocks are in the small pool
		/// </summary>
		public long SmallBlocksFree => smallPool.Count;

		/// <summary>
		/// Number of bytes in small pool not currently in use
		/// </summary>
		public long SmallPoolFreeSize => _smallPoolFreeSize;

		/// <summary>
		/// Number of bytes currently in use by stream from the small pool
		/// </summary>
		public long SmallPoolInUseSize => _smallPoolInUseSize;

		/// <summary>
		/// Removes and returns a single block from the pool.
		/// </summary>
		/// <returns>A byte[] array</returns>
		internal byte[] GetBlock()
		{
			if (!smallPool.TryPop(out var block))
				block = new byte[BlockSize];
			else
				Interlocked.Add(ref _smallPoolFreeSize, -BlockSize);

			Interlocked.Add(ref _smallPoolInUseSize, BlockSize);
			return block;
		}

		/// <summary>
		/// Returns a buffer of arbitrary size from the large buffer pool. This buffer
		/// will be at least the requiredSize and always be a multiple of largeBufferMultiple.
		/// </summary>
		/// <param name="requiredSize">The minimum length of the buffer</param>
		/// <param name="tag">The tag of the stream returning this buffer, for logging if necessary.</param>
		/// <returns>A buffer of at least the required size.</returns>
		internal byte[] GetLargeBuffer(int requiredSize, string tag)
		{
			requiredSize = RoundToLargeBufferMultiple(requiredSize);

			var poolIndex = requiredSize / LargeBufferMultiple - 1;

			byte[] buffer;
			if (poolIndex < largePools.Length)
			{
				if (!largePools[poolIndex].TryPop(out buffer))
					buffer = new byte[requiredSize];
				else
					Interlocked.Add(ref _largeBufferFreeSize[poolIndex], -buffer.Length);
			}
			else
			{
				// Buffer is too large to pool. They get a new buffer.

				// We still want to track the size, though, and we've reserved a slot
				// in the end of the inuse array for nonpooled bytes in use.
				poolIndex = _largeBufferInUseSize.Length - 1;

				// We still want to round up to reduce heap fragmentation.
				buffer = new byte[requiredSize];
			}

			Interlocked.Add(ref _largeBufferInUseSize[poolIndex], buffer.Length);

			return buffer;
		}

		private int RoundToLargeBufferMultiple(int requiredSize) =>
			(requiredSize + LargeBufferMultiple - 1) / LargeBufferMultiple * LargeBufferMultiple;

		private bool IsLargeBufferMultiple(int value) => value != 0 && value % LargeBufferMultiple == 0;

		/// <summary>
		/// Returns the buffer to the large pool
		/// </summary>
		/// <param name="buffer">The buffer to return.</param>
		/// <param name="tag">The tag of the stream returning this buffer, for logging if necessary.</param>
		/// <exception cref="ArgumentNullException">buffer is null</exception>
		/// <exception cref="ArgumentException">buffer.Length is not a multiple of LargeBufferMultiple (it did not originate from this pool)</exception>
		internal void ReturnLargeBuffer(byte[] buffer, string tag)
		{
			if (buffer == null) throw new ArgumentNullException(nameof(buffer));

			if (!IsLargeBufferMultiple(buffer.Length))
				throw new ArgumentException(
					"buffer did not originate from this memory manager. The size is not a multiple of " +
					LargeBufferMultiple);

			var poolIndex = buffer.Length / LargeBufferMultiple - 1;

			if (poolIndex < largePools.Length)
			{
				if ((largePools[poolIndex].Count + 1) * buffer.Length <= MaximumFreeLargePoolBytes ||
					MaximumFreeLargePoolBytes == 0)
				{
					largePools[poolIndex].Push(buffer);
					Interlocked.Add(ref _largeBufferFreeSize[poolIndex], buffer.Length);
				}
			}
			else
				poolIndex = _largeBufferInUseSize.Length - 1;

			Interlocked.Add(ref _largeBufferInUseSize[poolIndex], -buffer.Length);
		}

		/// <summary>
		/// Returns the blocks to the pool
		/// </summary>
		/// <param name="blocks">Collection of blocks to return to the pool</param>
		/// <param name="tag">The tag of the stream returning these blocks, for logging if necessary.</param>
		/// <exception cref="ArgumentNullException">blocks is null</exception>
		/// <exception cref="ArgumentException">blocks contains buffers that are the wrong size (or null) for this memory manager</exception>
		internal void ReturnBlocks(ICollection<byte[]> blocks, string tag)
		{
			if (blocks == null) throw new ArgumentNullException(nameof(blocks));

			var bytesToReturn = blocks.Count * BlockSize;
			Interlocked.Add(ref _smallPoolInUseSize, -bytesToReturn);

			foreach (var block in blocks)
			{
				if (block == null || block.Length != BlockSize)
					throw new ArgumentException("blocks contains buffers that are not BlockSize in length");
			}

			foreach (var block in blocks)
			{
				if (MaximumFreeSmallPoolBytes == 0 || SmallPoolFreeSize < MaximumFreeSmallPoolBytes)
				{
					Interlocked.Add(ref _smallPoolFreeSize, BlockSize);
					smallPool.Push(block);
				}
				else
					break;
			}
		}


		/// <summary>
		/// Retrieve a new MemoryStream object with no tag and a default initial capacity.
		/// </summary>
		/// <returns>A MemoryStream.</returns>
		public MemoryStream GetStream() => new RecyclableMemoryStream(this);

		/// <summary>
		/// Retrieve a new MemoryStream object with the given tag and a default initial capacity.
		/// </summary>
		/// <param name="tag">A tag which can be used to track the source of the stream.</param>
		/// <returns>A MemoryStream.</returns>
		public MemoryStream GetStream(string tag) => new RecyclableMemoryStream(this, tag);

		/// <summary>
		/// Retrieve a new MemoryStream object with the given tag and at least the given capacity.
		/// </summary>
		/// <param name="tag">A tag which can be used to track the source of the stream.</param>
		/// <param name="requiredSize">The minimum desired capacity for the stream.</param>
		/// <returns>A MemoryStream.</returns>
		public MemoryStream GetStream(string tag, int requiredSize) => new RecyclableMemoryStream(this, tag, requiredSize);

		/// <summary>
		/// Retrieve a new MemoryStream object with the given tag and at least the given capacity, possibly using
		/// a single continugous underlying buffer.
		/// </summary>
		/// <remarks>
		/// Retrieving a MemoryStream which provides a single contiguous buffer can be useful in situations
		/// where the initial size is known and it is desirable to avoid copying data between the smaller underlying
		/// buffers to a single large one. This is most helpful when you know that you will always call GetBuffer
		/// on the underlying stream.
		/// </remarks>
		/// <param name="tag">A tag which can be used to track the source of the stream.</param>
		/// <param name="requiredSize">The minimum desired capacity for the stream.</param>
		/// <param name="asContiguousBuffer">Whether to attempt to use a single contiguous buffer.</param>
		/// <returns>A MemoryStream.</returns>
		public MemoryStream GetStream(string tag, int requiredSize, bool asContiguousBuffer)
		{
			if (!asContiguousBuffer || requiredSize <= BlockSize) return GetStream(tag, requiredSize);

			return new RecyclableMemoryStream(this, tag, requiredSize, GetLargeBuffer(requiredSize, tag));
		}

		/// <summary>
		/// Retrieve a new MemoryStream object with the given tag and with contents copied from the provided
		/// buffer. The provided buffer is not wrapped or used after construction.
		/// </summary>
		/// <remarks>The new stream's position is set to the beginning of the stream when returned.</remarks>
		/// <param name="tag">A tag which can be used to track the source of the stream.</param>
		/// <param name="buffer">The byte buffer to copy data from.</param>
		/// <param name="offset">The offset from the start of the buffer to copy from.</param>
		/// <param name="count">The number of bytes to copy from the buffer.</param>
		/// <returns>A MemoryStream.</returns>
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public MemoryStream GetStream(string tag, byte[] buffer, int offset, int count)
		{
			var stream = new RecyclableMemoryStream(this, tag, count);
			stream.Write(buffer, offset, count);
			stream.Position = 0;
			return stream;
		}
	}
}
