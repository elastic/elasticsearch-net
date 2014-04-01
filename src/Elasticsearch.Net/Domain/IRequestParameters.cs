using System.Collections.Specialized;
using Elasticsearch.Net.Connection;

namespace Elasticsearch.Net
{
	public interface IRequestParameters
	{
		NameValueCollection QueryString { get; }
		object DeserializationState { get; }
		IRequestConfiguration RequestConfiguration { get; }

	}
}