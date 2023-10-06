// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless;
#else
namespace Elastic.Clients.Elasticsearch;
#endif

public partial class GetSourceRequestDescriptor
{
	/// <summary>
	/// A shortcut into calling Index(typeof(TOther)).
	/// </summary>
	public GetSourceRequestDescriptor Index<TOther>()
	{
		RouteValues.Required("index", (IndexName)typeof(TOther));
		return Self;
	}
}
