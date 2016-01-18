using System;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient 
	{
		IConnectionSettingsValues ConnectionSettings { get; }
		IElasticsearchSerializer Serializer { get; }
		IElasticsearchClient Raw { get; }
		Inferrer Infer { get; }
	}
}
