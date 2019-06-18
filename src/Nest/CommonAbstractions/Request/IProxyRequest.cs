using System.IO;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary> A request that that does not necessarily (de)serializes itself </summary>
	public interface IProxyRequest : IRequest
	{
		void WriteJson(IElasticsearchSerializer sourceSerializer, Stream s, SerializationFormatting serializationFormatting);
	}


	/// <summary>
	/// Describes a request that serializes the document passed to <see cref="DocumentPath{T}"/> when calling the fluent API.
	/// </summary>
	public interface IDocumentRequest { }
}
