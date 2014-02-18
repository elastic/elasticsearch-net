using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Nest.Resolvers.Converters;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Nest.Resolvers;

namespace Nest
{
	public partial class RawElasticClient : IRawElasticClient
	{
		public IConnection Connection { get; protected set; }
		public IConnectionSettings Settings { get; protected set; }
		public IElasticsearchSerializer Serializer { get; protected set; }
		public ElasticInferrer Infer { get; protected set; }



		public RawElasticClient(IConnectionSettings settings, IConnection connection = null, IElasticsearchSerializer serializer = null)
		{
			if (settings == null)
				throw new ArgumentNullException("settings");

			this.Settings = settings;
			this.Connection = connection ?? new Connection(settings);
			this.Serializer = serializer ?? new NestSerializer(settings);
			this.Serializer = new NestSerializer(this.Settings);
			this.Infer = new ElasticInferrer(this.Settings);
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
				nv.Add(kv.Key, Stringify(kv.Value));
			}
			return nv;
		}

		public string Encoded(object o)
		{
			return Uri.EscapeDataString(Stringify(o));
		}

		public string Stringify(object o)
		{
			var s = o as string;
			if (s != null)
				return s;
			var ss = o as string[];
			if (ss != null)
				return string.Join(",", ss);

			var pn = o as PropertyPathMarker;
			if (pn != null)
				return this.Infer.PropertyPath(pn);

			var pns = o as IEnumerable<PropertyPathMarker>;
			if (pns != null)
				return string.Join(",", pns.Select(p => this.Infer.PropertyPath(p)));

			//TODO: is this neccessarys
			var e = o as Enum;
			if (e != null) return GetEnumMemberValue(e);
			if (o is bool)
				return ((bool) o) ? "true" : "false";
			//var serialized = this.Serializer.Serialize(o);
			return o.ToString();
		}

		public static string GetEnumMemberValue(Enum enumValue)
		{
			var type = enumValue.GetType();
			var info = type.GetField(enumValue.ToString());
			var da = (EnumMemberAttribute[])(info.GetCustomAttributes(typeof(EnumMemberAttribute), false));

			if (da.Length > 0)
				return da[0].Value;
			else
				return string.Empty;
		}
		protected ConnectionStatus DoRequest(string method, string path, object data = null, NameValueCollection queryString = null)
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



			throw new DslException("Unknown HTTP method " + method);
		}

		private static byte[] _enter = Encoding.UTF8.GetBytes("\n");
		private byte[] PostData(object data)
		{
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

		protected Task<ConnectionStatus> DoRequestAsync(string method, string path, object data = null, NameValueCollection queryString = null)
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
			throw new DslException("Unknown HTTP method " + method);
		}
	}
}
