using System;
using Elasticsearch.Net_5_2_0;

namespace Nest_5_2_0
{
	public partial interface IElasticClient 
	{
		IConnectionSettingsValues ConnectionSettings { get; }
		IElasticsearchSerializer Serializer { get; }
		IElasticLowLevelClient LowLevel { get; }
		Inferrer Infer { get; }
	}
}
