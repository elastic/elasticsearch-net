namespace Nest
{
	internal class ElasticSearchPathInfo<T> where T : FluentQueryString<T>, new()
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
		public MetricOptions Metric { get; set; }
		public string Fields { get; set; }
		public MetricFamilyOptions MetricFamily { get; set; }
		public string SearchGroups { get; set; }
		public string IndexingTypes { get; set; }

		public ElasticSearchPathInfo()
		{
			this.QueryString = new T();
		}
	}
}