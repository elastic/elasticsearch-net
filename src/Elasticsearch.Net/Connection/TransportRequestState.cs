using System;
using System.IO;
using Elasticsearch.Net.Connection.Configuration;
using System.Collections.Specialized;

namespace Elasticsearch.Net.Connection
{
	class TransportRequestState<T>
	{
		public IConnectionConfigurationValues ClientSettings { get; private set; }
		
		public IRequestParameters RequestParameters { get; private set;}

		public IRequestConfiguration RequestConfiguration
		{
			get
			{
				return this.RequestParameters == null ? null : this.RequestParameters.RequestConfiguration;
			}
		}

		public string Method { get; private set; }
		
		public string Path { get; private set; }
		
		public byte[] PostData { get; private set; }
		
		public ElasticsearchResponseTracer<T> Tracer { get; private set; }

		public int? Seed { get; set; }

		public Func<IElasticsearchResponse, Stream, object> DeserializationState { get; private set; }

		internal bool SniffedOnConnectionFailure { get; set; }

		public TransportRequestState(
			IConnectionConfigurationValues globalSettings,
			IRequestParameters requestSettings, 
			ElasticsearchResponseTracer<T> tracer, 
			string method, string path, byte[] postData = null)
		{
			this.ClientSettings = globalSettings;
			this.RequestParameters = requestSettings;
			this.Method = method;
			this.Path = path;
			this.PostData = postData;
			if (this.RequestParameters != null)
			{
				if (this.RequestParameters.QueryString != null) 
					this.Path += RequestParameters.QueryString.ToNameValueCollection(ClientSettings.Serializer).ToQueryString();
				this.DeserializationState = this.RequestParameters.DeserializationState;
			}
			this.Tracer = tracer;
		}

	}
}