using System.IO;

namespace Elasticsearch.Net
{
	// TODO we use this in some places but its no longer clear to me why need to circumvent RecyclableMemoryStream in some cases
	/// <summary>
	/// A factory for creating memory streams using instances of <see cref="MemoryStream" />
	/// </summary>
	public class MemoryStreamFactory : IMemoryStreamFactory
	{
		/// <inheritdoc />
		public MemoryStream Create() => new MemoryStream();

		/// <inheritdoc />
		public MemoryStream Create(byte[] bytes) => new MemoryStream(bytes);

		/// <inheritdoc />
		public MemoryStream Create(byte[] bytes, int index, int count) => new MemoryStream(bytes, index, count);
	}
}
