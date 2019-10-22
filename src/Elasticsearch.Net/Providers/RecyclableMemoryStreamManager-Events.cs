// ---------------------------------------------------------------------
// Copyright (c) 2015 Microsoft
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

using System;
using System.Diagnostics.Tracing;
using System.Threading;

namespace Elasticsearch.Net
{
#if !DOTNETCORE
	/// <summary>
	/// Stub for System.Diagnostics.Tracing.EventCounter which is not available on .NET 4.6.1
	/// </summary>
	internal class EventCounter
	{
		public EventCounter(string blocks, object eventsWriter) { }

		public void WriteMetric(long v) { }
	}
#endif

	internal sealed partial class RecyclableMemoryStreamManager
	{
		public static readonly Events EventsWriter = new Events();
		public static readonly Counters Counter = new Counters();

		public sealed class Counters
		{
			public Counters()
			{
				BlocksCounter = new EventCounter("blocks", EventsWriter);
				BufferCounter = new EventCounter("large-buffers", EventsWriter);
				MemoryStreamCounter = new EventCounter("memorystreams", EventsWriter);
			}

			private EventCounter BlocksCounter { get; }
			private EventCounter BufferCounter { get; }
			private EventCounter MemoryStreamCounter { get; }

			private long _blocks = 0;
			internal void ReportBlockCreated()
			{
				var count = Interlocked.Increment(ref _blocks);
				BlocksCounter.WriteMetric(count);

			}

			internal void ReportBlockDiscarded()
			{
				var count = Interlocked.Decrement(ref _blocks);
				BlocksCounter.WriteMetric(count);

			}

			private long _largeBuffers = 0;
			internal void ReportLargeBufferCreated()
			{
				var count = Interlocked.Increment(ref _largeBuffers);
				BufferCounter.WriteMetric(count);
			}

			internal void ReportLargeBufferDiscarded()
			{
				var count = Interlocked.Decrement(ref _largeBuffers);
				BufferCounter.WriteMetric(count);
			}

			private long _memoryStreams = 0;
			internal void ReportStreamCreated()
			{
				var count = Interlocked.Decrement(ref _memoryStreams);
				MemoryStreamCounter.WriteMetric(count);
			}

			internal void ReportStreamDisposed()
			{
				var count = Interlocked.Decrement(ref _memoryStreams);
				MemoryStreamCounter.WriteMetric(count);
			}

		}

		[EventSource(Name = "Elasticsearch-Net-RecyclableMemoryStream", Guid = "{AD44FDAC-D3FC-460A-9EBE-E55A3569A8F6}")]
		public sealed class Events : EventSource
		{

			public enum MemoryStreamBufferType
			{
				Small,
				Large
			}

			public enum MemoryStreamDiscardReason
			{
				TooLarge,
				EnoughFree
			}

			[Event(1, Level = EventLevel.Verbose)]
			public void MemoryStreamCreated(Guid guid, string tag, int requestedSize)
			{
				if (IsEnabled(EventLevel.Verbose, EventKeywords.None))
					WriteEvent(1, guid, tag ?? string.Empty, requestedSize);
			}

			[Event(2, Level = EventLevel.Verbose)]
			public void MemoryStreamDisposed(Guid guid, string tag)
			{
				if (IsEnabled(EventLevel.Verbose, EventKeywords.None))
					WriteEvent(2, guid, tag ?? string.Empty);
			}

			[Event(3, Level = EventLevel.Critical)]
			public void MemoryStreamDoubleDispose(Guid guid, string tag, string allocationStack, string disposeStack1, string disposeStack2)
			{
				if (IsEnabled())
					WriteEvent(3, guid, tag ?? string.Empty, allocationStack ?? string.Empty,
						disposeStack1 ?? string.Empty, disposeStack2 ?? string.Empty);
			}

			[Event(4, Level = EventLevel.Error)]
			public void MemoryStreamFinalized(Guid guid, string tag, string allocationStack)
			{
				if (IsEnabled())
					WriteEvent(4, guid, tag ?? string.Empty, allocationStack ?? string.Empty);
			}

			[Event(5, Level = EventLevel.Verbose)]
			public void MemoryStreamToArray(Guid guid, string tag, string stack, int size)
			{
				if (IsEnabled(EventLevel.Verbose, EventKeywords.None))
					WriteEvent(5, guid, tag ?? string.Empty, stack ?? string.Empty, size);
			}

			[Event(6, Level = EventLevel.Informational)]
			public void MemoryStreamManagerInitialized(int blockSize, int largeBufferMultiple, int maximumBufferSize)
			{
				if (IsEnabled())
					WriteEvent(6, blockSize, largeBufferMultiple, maximumBufferSize);
			}

			[Event(7, Level = EventLevel.Verbose)]
			public void MemoryStreamNewBlockCreated(long smallPoolInUseBytes)
			{
				if (IsEnabled(EventLevel.Verbose, EventKeywords.None))
					WriteEvent(7, smallPoolInUseBytes);
			}

			[Event(8, Level = EventLevel.Verbose)]
			public void MemoryStreamNewLargeBufferCreated(int requiredSize, long largePoolInUseBytes)
			{
				if (IsEnabled(EventLevel.Verbose, EventKeywords.None))
					WriteEvent(8, requiredSize, largePoolInUseBytes);
			}

			[Event(9, Level = EventLevel.Verbose)]
			public void MemoryStreamNonPooledLargeBufferCreated(int requiredSize, string tag, string allocationStack)
			{
				if (IsEnabled(EventLevel.Verbose, EventKeywords.None))
					WriteEvent(9, requiredSize, tag ?? string.Empty, allocationStack ?? string.Empty);
			}

			[Event(10, Level = EventLevel.Warning)]
			public void MemoryStreamDiscardBuffer(MemoryStreamBufferType bufferType, string tag, MemoryStreamDiscardReason reason)
			{
				if (IsEnabled())
					WriteEvent(10, bufferType, tag ?? string.Empty, reason);
			}

			[Event(11, Level = EventLevel.Error)]
			public void MemoryStreamOverCapacity(int requestedCapacity, long maxCapacity, string tag, string allocationStack)
			{
				if (IsEnabled())
					WriteEvent(11, requestedCapacity, maxCapacity, tag ?? string.Empty, allocationStack ?? string.Empty);
			}
		}
	}
}
