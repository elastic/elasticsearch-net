using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Extensions;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Exceptions;
using Elasticsearch.Net.Serialization;

namespace Elasticsearch.Net
{
	/// <summary>
	/// Low level client that exposes all of elasticsearch API endpoints but leaves you in charge of building request and handling the response
	/// </summary>
	public partial class ElasticsearchClient : IElasticsearchClient
	{
		public IConnectionConfigurationValues Settings { get { return this.Transport.Settings; } }
		public IElasticsearchSerializer Serializer { get { return this.Transport.Serializer; } }
		
		protected IStringifier Stringifier { get; set; }
		protected ITransport Transport { get; set; }
		
		/// <summary>
		/// Instantiate a new low level elasticsearch client
		/// </summary>
		/// <param name="settings">Specify how and where the client connects to elasticsearch, defaults to a static single node connectionpool 
		/// to http://localhost:9200
		/// </param>
		/// <param name="connection">Provide an alternative connection handler</param>
		/// <param name="transport">Provide a custom transport implementation that coordinates between IConnectionPool, IConnection and ISerializer</param>
		/// <param name="serializer">Provide a custom serializer</param>
		/// <param name="stringifier">This interface is responsible for translating non string objects in the querystring to strings</param>
		public ElasticsearchClient(
			IConnectionConfigurationValues settings = null, 
			IConnection connection = null, 
			ITransport transport = null,
			IElasticsearchSerializer serializer = null,
			IStringifier stringifier = null
			)
		{
			settings = settings ?? new ConnectionConfiguration();
			this.Transport = transport ?? new Transport(settings, connection, serializer);
			this.Stringifier = stringifier ?? new Stringifier();
			
			//neccessary to pass the serializer to ElasticsearchResponse
			this.Settings.Serializer = this.Transport.Serializer;
		
		}

		protected void ToNameValueCollection(BaseRequestParameters requestParameters)
		{
			if (requestParameters == null)
				return;
			var dict = requestParameters._QueryStringDictionary;
			if (dict == null || dict.Count < 0)
				return;
			
			var nv = new NameValueCollection();
			foreach (var kv in dict.Where(kv => !kv.Key.IsNullOrEmpty()))
			{
				nv.Add(kv.Key, this.Stringifier.Stringify(kv.Value));
			}
			requestParameters._queryString = nv;
		}

		public string Encoded(object o)
		{
			return Uri.EscapeDataString(this.Stringifier.Stringify(o));
		}


		protected ElasticsearchResponse<T> DoRequest<T>(string method, string path, object data = null, BaseRequestParameters requestParameters = null)
		{
			return this.Transport.DoRequest<T>(method, path, data, requestParameters);
		}


		protected Task<ElasticsearchResponse<T>> DoRequestAsync<T>(string method, string path, object data = null, BaseRequestParameters requestParameters = null)
		{
			return this.Transport.DoRequestAsync<T>(method, path, data, requestParameters);
		}
	}
}
