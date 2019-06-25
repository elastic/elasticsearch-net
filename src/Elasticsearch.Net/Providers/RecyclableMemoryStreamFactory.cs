using System.IO;

namespace Elasticsearch.Net
{
	/// <summary>
	/// A factory for creating memory streams using a recyclable pool of <see cref="MemoryStream" /> instances
	/// </summary>
	public class RecyclableMemoryStreamFactory : IMemoryStreamFactory
	{
		private readonly RecyclableMemoryStreamManager _manager;

		public static RecyclableMemoryStreamFactory Default { get; } = new RecyclableMemoryStreamFactory();

		public RecyclableMemoryStreamFactory() =>
			_manager = new RecyclableMemoryStreamManager { AggressiveBufferReturn = true };

		public MemoryStream Create() => _manager.GetStream();

		public MemoryStream Create(byte[] bytes) => new MemoryStream(bytes);

		public MemoryStream Create(byte[] bytes, int index, int count) => _manager.GetStream(string.Empty, bytes, index, count);
	}
}
