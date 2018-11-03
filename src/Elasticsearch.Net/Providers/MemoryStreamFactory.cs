using System.IO;

namespace Elasticsearch.Net
{
	/// <summary>
	/// A factory for creating memory streams using instances of <see cref="MemoryStream" />
	/// </summary>
	public class MemoryStreamFactory : IMemoryStreamFactory
	{
		/// <summary>
		/// Creates a memory stream using <see cref="MemoryStream" />
		/// </summary>
		public MemoryStream Create() => new MemoryStream();

		/// <summary>
		/// Creates a memory stream using <see cref="MemoryStream" /> with the bytes written to the stream
		/// </summary>
		public MemoryStream Create(byte[] bytes) => new MemoryStream(bytes);
	}
}
