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
	public partial class ElasticsearchClient : IElasticsearchClient
	{
		public IConnectionConfigurationValues Settings { get { return this.Transport.Settings; } }
		public IElasticsearchSerializer Serializer { get { return this.Transport.Serializer; } }
		
		protected IStringifier Stringifier { get; set; }
		protected ITransport Transport { get; set; }

		public ElasticsearchClient(
			IConnectionConfigurationValues settings, 
			IConnection connection = null, 
			ITransport transport = null,
			IElasticsearchSerializer serializer = null,
			IStringifier stringifier = null
			)
		{
			if (settings == null)
				throw new ArgumentNullException("settings");

			this.Transport = transport ?? new Transport(settings, connection, serializer);
			this.Stringifier = stringifier ?? new Stringifier();
			
			//neccessary to pass the serializer to ElasticsearchResponse
			this.Settings.Serializer = this.Transport.Serializer;
		}

		protected NameValueCollection ToNameValueCollection<TQueryString>(FluentQueryString<TQueryString> qs)
			where TQueryString : FluentQueryString<TQueryString>
		{
			if (qs == null)
				return null;
			var dict = qs._QueryStringDictionary;
			if (dict == null || dict.Count < 0)
				return null;

			var nv = new NameValueCollection();
			foreach (var kv in dict.Where(kv => !kv.Key.IsNullOrEmpty()))
			{
				nv.Add(kv.Key, this.Stringifier.Stringify(kv.Value));
			}
			return nv;
		}

		public string Encoded(object o)
		{
			return Uri.EscapeDataString(this.Stringifier.Stringify(o));
		}


		protected ElasticsearchResponse DoRequest(string method, string path, object data = null, NameValueCollection queryString = null)
		{
			return this.Transport.DoRequest(method, path, data, queryString);
		}


		protected Task<ElasticsearchResponse> DoRequestAsync(string method, string path, object data = null, NameValueCollection queryString = null)
		{
			return this.Transport.DoRequestAsync(method, path, data, queryString);
		}
	}
}
