using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Connection.Configuration;

namespace Elasticsearch.Net
{
	public class RequestParameters : IRequestParameters
	{
		public IDictionary<string, object> QueryString { get; set; }
		public Func<IElasticsearchResponse, Stream, object> DeserializationState { get; set; }
		public IRequestConfiguration RequestConfiguration { get; set; }
	}
}