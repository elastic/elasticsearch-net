using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Nest.Resolvers;

namespace Nest
{

	public partial class ElasticClient
	{
		/// <summary>
		/// Gets a document of T by id in the default index and the inferred typename for T
		/// </summary>
		/// <returns>an instance of T</returns>
		public T Get<T>(int id) where T : class
		{
			return this.Get<T>(id.ToString());
		}
		/// <summary>
		/// Gets a document of T by id in the default index and the inferred typename for T
		/// </summary>
		/// <returns>an instance of T</returns>
		public T Get<T>(string id) where T : class
		{
			var index = this.IndexNameResolver.GetIndexForType<T>();
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			var typeName = this.TypeNameResolver.GetTypeNameFor<T>();

			return this.Get<T>(id, this.PathResolver.CreateIndexTypePath(index, typeName));
		}
		/// <summary>
		/// Gets a document of T by id in the specified index and the specified typename
		/// </summary>
		/// <returns>an instance of T</returns>
		public T Get<T>(string index, string type, string id) where T : class
		{
			return this.Get<T>(id, index + "/" + type + "/");
		}
		/// <summary>
		/// Gets a document of T by id in the specified index and the specified typename
		/// </summary>
		/// <returns>an instance of T</returns>
		public T Get<T>(string index, string type, int id) where T : class
		{
			return this.Get<T>(id.ToString(), index + "/" + type + "/");
		}
		private T Get<T>(string id, string path) where T : class
		{
			return this._Get<T>(path + id);
		}

		public T Get<T>(Action<GetDescriptor<T>> getSelector) where T : class
		{
			getSelector.ThrowIfNull("getSelector");
			var d = new GetDescriptor<T>();
			getSelector(d);

			d._Id.ThrowIfNullOrEmpty("Id on getselector");

			var p = new PathResolver(this.Settings);
			var path = p.CreateGetPath<T>(d);
			return this._Get<T>(path);
		}

		private T _Get<T>(string path) where T : class
		{
			var response = this.Connection.GetSync(path);

			if (response.Result == null) //a 404 is hit when there is an attempt to grab a non existant document by id, this causes the 'result' to be null
				return null;

			var o = JObject.Parse(response.Result);
			var source = o["_source"];
			if (source != null)
			{
				return this.Deserialize<T>(source.ToString());
			}

			return null;
		}
		
	}
}
