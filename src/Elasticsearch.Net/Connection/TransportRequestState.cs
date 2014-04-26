namespace Elasticsearch.Net.Connection
{
	class TransportRequestState<T>
	{
		public string Method { get; private set; }
		public string Path { get; private set; }
		public byte[] PostData { get; private set; }
		public ElasticsearchResponseTracer<T> Tracer { get; private set; }

		public int? Seed { get; set; }

		public IRequestConfiguration RequestConfiguration { get; set; }
		public object DeserializationState { get; private set; }

		public TransportRequestState(ElasticsearchResponseTracer<T> tracer, string method, string path, byte[] postData = null, IRequestParameters requestParameters = null)
		{
			this.Method = method;
			this.Path = path;
			this.PostData = postData;
			if (requestParameters != null)
			{
				if (requestParameters.QueryString != null) this.Path += requestParameters.QueryString.ToQueryString();
				this.DeserializationState = requestParameters.DeserializationState;
				this.RequestConfiguration = requestParameters.RequestConfiguration;
			}
			this.Tracer = tracer;
		}

	}
}