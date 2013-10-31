using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
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
    public ElasticSerializer Serializer { get; protected set; }

		public RawElasticClient(IConnectionSettings settings)
			: this(settings, new Connection(settings))
		{

		}

		public RawElasticClient(IConnectionSettings settings, IConnection connection)
		{
			if (settings == null)
				throw new ArgumentNullException("settings");

			this.Settings = settings;
			this.Connection = connection;
      this.Serializer = new ElasticSerializer(this.Settings);
		}

		public string Serialize(object @object)
		{
      return this.Serializer.Serialize(@object);
		}

		protected ConnectionStatus DoRequest(string method, string path, object data = null,  NameValueCollection queryString = null)
		{
			if (queryString != null)
				path += queryString.ToQueryString();

			var postData = string.Empty;
			var s = data as string;
			if (s != null)
				postData = s;
			else if (data != null)
				postData = this.Serialize(data);

			switch (method.ToLowerInvariant())
			{
				case "post": return this.Connection.PostSync(path, postData);
				case "put": return this.Connection.PutSync(path, postData);
				case "delete": 
					return string.IsNullOrWhiteSpace(postData) 
						? this.Connection.DeleteSync(path) 
						: this.Connection.DeleteSync(path, postData);
				case "head": return this.Connection.HeadSync(path);
				case "get": return this.Connection.GetSync(path);
			}

			

			throw new DslException("Unknown HTTP method " + method);
		}

		protected Task<ConnectionStatus> DoRequestAsync(string method, string path, object data = null, NameValueCollection queryString = null)
		{
			if (queryString != null)
				path += queryString.ToQueryString(); 

			var postData = string.Empty;
			var s = data as string;
			if (s != null)
				postData = s;
			else if (data != null)
				postData = this.Serialize(data);

			switch (method.ToLowerInvariant())
			{
				case "post": return this.Connection.Post(path, postData);
				case "put": return this.Connection.Put(path, postData);
				case "delete":
					return string.IsNullOrWhiteSpace(postData)
						? this.Connection.Delete(path)
						: this.Connection.Delete(path, postData);
				case "head": return this.Connection.Head(path);
				case "get": return this.Connection.Get(path);
			}
			throw new DslException("Unknown HTTP method " + method);
		}
	}
}
