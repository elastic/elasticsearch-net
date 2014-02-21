using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public partial class Elasticsearch : IElasticsearch
	{
		public IConnection Connection { get; protected set; }
		public IConnectionSettings2 Settings { get; protected set; }
		public IElasticsearchSerializer Serializer { get; protected set; }
		protected IStringifier Stringifier { get; set; }

		public Elasticsearch(
			IConnectionSettings2 settings, 
			IConnection connection = null, 
			IElasticsearchSerializer serializer = null,
			IStringifier stringifier = null
			)
		{
			if (settings == null)
				throw new ArgumentNullException("settings");

			this.Settings = settings;
			this.Connection = connection ?? new Connection(settings);
			this.Serializer = serializer;// ?? new Elasticsear(settings);
			((IConnectionSettings2) this.Settings).Serializer = this.Serializer;
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
			if (queryString != null)
				path += queryString.ToQueryString();

			var postData = PostData(data);

			switch (method.ToLowerInvariant())
			{
				case "post": return this.Connection.PostSync(path, postData);
				case "put": return this.Connection.PutSync(path, postData);
				case "delete":
					return postData == null || postData.Length == 0
						? this.Connection.DeleteSync(path)
						: this.Connection.DeleteSync(path, postData);
				case "head": return this.Connection.HeadSync(path);
				case "get": return this.Connection.GetSync(path);
			}

			throw new ConnectionException("Unknown HTTP method " + method);
		}

		private static byte[] _enter = Encoding.UTF8.GetBytes("\n");
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
			var sb = data as IEnumerable<byte[]>;
			
			var so = data as IEnumerable<object>;
			if (so == null)
				return this.Serializer.Serialize(data);
			var joined = string.Join("\n", so
				.Select(soo => this.Serializer.Serialize(soo, SerializationFormatting.None).Utf8String())) + "\n";
			return joined.Utf8Bytes();
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
			throw new ConnectionException("Unknown HTTP method " + method);
		}
	}
}
