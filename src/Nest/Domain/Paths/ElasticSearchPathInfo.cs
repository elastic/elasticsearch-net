using Elasticsearch.Net;

namespace Nest
{
	internal class ElasticsearchPathInfo<T> where T : FluentRequestParameters<T>, new()
	{
		public PathInfoHttpMethod HttpMethod { get; set; }
		public string Index { get; set; }
		public string Type { get; set; }
		public string Id { get; set; }
		public T QueryString { get; set; }
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
			this.QueryString = new T();
		}
	}
}