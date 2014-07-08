using System;
using System.IO;
using Elasticsearch.Net;

namespace Nest
{
	public class ElasticsearchPathInfo<TParameters> 
		where TParameters : IRequestParameters, new()
	{
		public PathInfoHttpMethod HttpMethod { get; set; }
		public string Index { get; set; }
		public string Type { get; set; }
		public string Id { get; set; }
		public TParameters RequestParameters { get; set; }
		public string Name { get; set; }
		public string Field { get; set; }
		public string ScrollId { get; set; }
		public string NodeId { get; set; }
		public string Fields { get; set; }
		public string SearchGroups { get; set; }
		public string IndexingTypes { get; set; }
		public string Repository { get; set; }
		public string Snapshot { get; set; }

		public string Metric { get; set; }
		public string IndexMetric { get; set; }

		public ElasticsearchPathInfo()
		{
			this.RequestParameters = new TParameters();
		}

		public ElasticsearchPathInfo<TParameters> DeserializationState(Func<IElasticsearchResponse, Stream, object> customObjectCreation)
		{
			this.RequestParameters.DeserializationState = customObjectCreation;
			return this;
		}
	}
}