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
		public Func<IApiCallDetails, Stream, object> DeserializationState { get; set; }
		public IRequestConfiguration RequestConfiguration { get; set; }
		
		public RequestParameters()
		{
			this.QueryString = new Dictionary<string, object>();
		}

		public TOut GetQueryStringValue<TOut>(string name)
		{
			if (!this.QueryString.ContainsKey(name))
				return default(TOut);
			var value = this.QueryString[name];
			if (value == null)
				return default(TOut);
			return (TOut)value;
		}

		public void AddQueryStringValue(string name, object value)
		{
			if (value == null) return;
			this.QueryString[name] = value;
		}
	}
}