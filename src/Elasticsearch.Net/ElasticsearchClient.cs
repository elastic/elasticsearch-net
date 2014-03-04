using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Exceptions;

namespace Elasticsearch.Net
{
	public partial class ElasticsearchClient : IElasticsearchClient
	{
		public IConnection Connection { get; protected set; }
		public IConnectionConfigurationValues Settings { get; protected set; }
		public IElasticsearchSerializer Serializer { get; protected set; }
		protected IStringifier Stringifier { get; set; }
		protected IHttpTransport Transport { get; set; }

		public ElasticsearchClient(
			IConnectionConfigurationValues settings, 
			IConnection connection = null, 
			IHttpTransport transport = null,
			IElasticsearchSerializer serializer = null,
			IStringifier stringifier = null
			)
		{
			if (settings == null)
				throw new ArgumentNullException("settings");

			this.Settings = settings;
			this.Connection = connection ?? new HttpConnection(settings);
			this.Serializer = serializer ?? new ElasticsearchDefaultSerializer();
			((IConnectionConfigurationValues) this.Settings).Serializer = this.Serializer;
			this.Transport = transport ?? new HttpTransport(settings, this.Connection, this.Serializer);
			this.Stringifier = stringifier ?? new Stringifier();
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
