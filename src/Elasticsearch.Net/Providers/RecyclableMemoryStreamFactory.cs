using System.IO;
using System.Linq;

namespace Elasticsearch.Net
{
	/// <summary>
	/// A factory for creating memory streams using a recyclable pool of <see cref="MemoryStream"/> instances
	/// http://www.philosophicalgeek.com/2015/02/06/announcing-microsoft-io-recycablememorystream/
	/// </summary>
	//TODO SHOULD THIS be public?
	public class RecyclableMemoryStreamFactory : IMemoryStreamFactory
	{
		public static RecyclableMemoryStreamFactory Default { get; } = new RecyclableMemoryStreamFactory();

		private readonly RecyclableMemoryStreamManager _manager;

		public RecyclableMemoryStreamFactory() => _manager = new RecyclableMemoryStreamManager { AggressiveBufferReturn = true };

		public MemoryStream Create() => _manager.GetStream();

		public MemoryStream Create(byte[] bytes) => _manager.GetStream(string.Empty, bytes, 0, bytes.Length);
	}
}
