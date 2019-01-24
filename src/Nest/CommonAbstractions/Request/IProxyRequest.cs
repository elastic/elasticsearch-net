using System.IO;
using Elasticsearch.Net;

namespace Nest
{
	//TODO: since this only deals with source serializer rename this to ISourceProxyRequest
	/// <summary> A request that that does not necessarily (de)serializes itself </summary>
	public interface IProxyRequest : IRequest
	{
		void WriteJson(IElasticsearchSerializer sourceSerializer, Stream s, SerializationFormatting serializationFormatting);
	}
}
