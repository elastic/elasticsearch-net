// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;

namespace Elasticsearch.Net
{
	/// <summary>
	/// A factory for creating memory streams using a recyclable pool of <see cref="MemoryStream" /> instances
	/// </summary>
	public class RecyclableMemoryStreamFactory : IMemoryStreamFactory
	{
		private const string TagSource = "Elasticsearch.Net";
		private readonly RecyclableMemoryStreamManager _manager;

		public static RecyclableMemoryStreamFactory Default { get; } = new RecyclableMemoryStreamFactory();

		public RecyclableMemoryStreamFactory() => _manager = CreateManager(experimental: false);

		private static RecyclableMemoryStreamManager CreateManager(bool experimental)
		{
			if (!experimental) return new RecyclableMemoryStreamManager() { AggressiveBufferReturn = true };

			const int blockSize = 1024;
			const int largeBufferMultiple = 1024 * 1024;
			const int maxBufferSize = 16 * largeBufferMultiple;
			return new RecyclableMemoryStreamManager(blockSize, largeBufferMultiple, maxBufferSize)
			{
				AggressiveBufferReturn = true, MaximumFreeLargePoolBytes = maxBufferSize * 4, MaximumFreeSmallPoolBytes = 100 * blockSize
			};

		}

		public MemoryStream Create() => _manager.GetStream(Guid.Empty, TagSource);

		public MemoryStream Create(byte[] bytes) => _manager.GetStream(bytes);

		public MemoryStream Create(byte[] bytes, int index, int count) => _manager.GetStream(Guid.Empty, TagSource, bytes, index, count);
	}
}
