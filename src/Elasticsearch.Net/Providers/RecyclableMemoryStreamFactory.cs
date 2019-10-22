using System;
using System.IO;
using System.Threading.Tasks;

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

		public RecyclableMemoryStreamFactory()
		{
			var blockSize = 1024;
			var largeBufferMultiple = 1024 * 1024;
			var maxBufferSize = 16 * largeBufferMultiple;
			_manager = new RecyclableMemoryStreamManager(blockSize, largeBufferMultiple, maxBufferSize)
			{
				//AggressiveBufferReturn = true,
				MaximumFreeLargePoolBytes = maxBufferSize * 4,
				MaximumFreeSmallPoolBytes = 100 * blockSize
			};
		}

		public MemoryStream Create() => _manager.GetStream(Guid.Empty, TagSource);

		public MemoryStream Create(byte[] bytes) => _manager.GetStream(bytes);

		public MemoryStream Create(byte[] bytes, int index, int count) => _manager.GetStream(Guid.Empty, TagSource, bytes, index, count);
	}
}
