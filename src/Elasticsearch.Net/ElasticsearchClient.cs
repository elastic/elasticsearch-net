using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		public ElasticsearchClient(
			IConnectionConfigurationValues settings = null, 
			IConnection connection = null, 
			ITransport transport = null,
			IElasticsearchSerializer serializer = null
			)
		{
			settings = settings ?? new ConnectionConfiguration();
			this.Transport = transport ?? new Transport(settings, connection, serializer);
			//neccessary to pass the serializer to ElasticsearchResponse
			this.Settings.Serializer = this.Transport.Serializer;
		
		}

		

		public string Encoded(object o)
		{
			return Uri.EscapeDataString(this.Serializer.Stringify(o));
		}


		protected ElasticsearchResponse<T> DoRequest<T>(string method, string path, object data = null, IRequestParameters requestParameters = null)
		{
			return this.Transport.DoRequest<T>(method, path, data, requestParameters);
		}


		protected Task<ElasticsearchResponse<T>> DoRequestAsync<T>(string method, string path, object data = null, IRequestParameters requestParameters = null)
		{
			return this.Transport.DoRequestAsync<T>(method, path, data, requestParameters);
		}
	}
}
