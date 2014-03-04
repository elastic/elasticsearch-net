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

		public ElasticsearchClient(
			IConnectionConfigurationValues settings, 
			IConnection connection = null, 
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


		protected ElasticsearchResponse DoRequest(string method, string path, object data = null, NameValueCollection queryString = null, int retried = 0)
		{
			if (queryString != null)
				path += queryString.ToQueryString();

			var maxRetries = this.GetMaximumRetries();
			var postData = PostData(data);
			ElasticsearchResponse response = null;
			var exceptionMessage = "Unable to perform request: '{0} {1}' on any of the nodes after retrying {2} times.".F(
				method, path, retried);
			try
			{
				response = DoSyncRequest(method, path, postData);
				if (response != null && response.SuccessOrKnownError)
					return response;
			}
			catch (Exception e)
			{
				if (retried < maxRetries)
					return this.DoRequest(method, path, data, queryString, ++retried);
				else
					throw new OutOfNodesException(exceptionMessage, e);
			}
			if (retried < maxRetries)
				return this.DoRequest(method, path, data, queryString, ++retried);
			
			throw new OutOfNodesException(exceptionMessage);
		}

		/// <summary>
		/// Returns either the fixed maximum set on the connection configuration settings or the number of nodes
		/// </summary>
		private int GetMaximumRetries()
		{
			return this.Settings.MaxRetries.GetValueOrDefault(this.Settings.ConnectionPool.MaxRetries);
		}


		private ElasticsearchResponse DoSyncRequest(string method, string path, byte[] postData)
		{
			switch (method.ToLowerInvariant())
			{
				case "post":
					return this.Connection.PostSync(path, postData);
				case "put":
					return this.Connection.PutSync(path, postData);
				case "delete":
					return postData == null || postData.Length == 0
						? this.Connection.DeleteSync(path)
						: this.Connection.DeleteSync(path, postData);
				case "head":
					return this.Connection.HeadSync(path);
				case "get":
					return this.Connection.GetSync(path);
			}
			return null;
		}

		protected Task<ElasticsearchResponse> DoRequestAsync(string method, string path, object data = null, NameValueCollection queryString = null)
		{
			if (queryString != null)
				path += queryString.ToQueryString();

			var postData = PostData(data);

			switch (method.ToLowerInvariant())
			{
				case "post": return this.Connection.Post(path, postData);
				case "put": return this.Connection.Put(path, postData);
				case "delete":
					return postData == null || postData.Length == 0
						? this.Connection.Delete(path)
						: this.Connection.Delete(path, postData);
				case "head": return this.Connection.Head(path);
				case "get": return this.Connection.Get(path);
			}
			throw new Exception("Unknown HTTP method " + method);
		}

		private byte[] PostData(object data)
		{
			var bytes = data as byte[];
			if (bytes != null)
				return bytes;

			var s = data as string;
			if (s != null)
				return s.Utf8Bytes();
			if (data == null) return null;
			var ss = data as IEnumerable<string>;
			if (ss != null)
				return (string.Join("\n", ss) + "\n").Utf8Bytes();
			
			var so = data as IEnumerable<object>;
			if (so == null)
				return this.Serializer.Serialize(data);
			var joined = string.Join("\n", so
				.Select(soo => this.Serializer.Serialize(soo, SerializationFormatting.None).Utf8String())) + "\n";
			return joined.Utf8Bytes();
		}
	}
}
