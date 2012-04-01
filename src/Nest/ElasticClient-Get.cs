using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Thrift;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Fasterflect;
using Newtonsoft.Json.Converters;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;

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
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			var typeName = this.InferTypeName<T>();
		
			return this.Get<T>(id, this.CreatePath(index, typeName));
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
			var response = this.Connection.GetSync(path + id);

			if (response.Result == null) //a 404 is hit when there is an attempt to grab a non existant document by id, this causes the 'result' to be null
				return null;

			var o = JObject.Parse(response.Result);
			var source = o["_source"];
			if (source != null)
			{
				return JsonConvert.DeserializeObject<T>(source.ToString());
			}

			return null;
		}

		public IEnumerable<T> Get<T>(IEnumerable<int> ids)
			where T : class
		{
			return this.Get<T>(ids.Select(i => Convert.ToString(i)));
		}


		/// <summary>
		/// Gets multiple documents of T by id in the default index and the inferred typename for T
		/// </summary>
		public IEnumerable<T> Get<T>(IEnumerable<string> ids)
			where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			var typeName = this.InferTypeName<T>();
		
			return this.Get<T>(ids, this.CreatePath(index, typeName));
		}
		/// <summary>
		/// Gets multiple documents of T by id in the specified index and the specified typename for T
		/// </summary>
		public IEnumerable<T> Get<T>(string index, string type, IEnumerable<int> ids)
			where T : class
		{
			return this.Get<T>(index, type, ids.Select(i => Convert.ToString(i)));
		}
		/// <summary>
		/// Gets multiple documents of T by id in the specified index and the specified typename for T
		/// </summary>
		public IEnumerable<T> Get<T>(string index, string type, IEnumerable<string> ids)
			where T : class
		{
			return this.Get<T>(ids, this.CreatePath(index, type));
		}

		private IEnumerable<T> Get<T>(IEnumerable<string> ids, string path)
			where T : class
		{
			var data = @"{{ ""ids"": {0} }}".F(JsonConvert.SerializeObject(ids));
			var response = this.Connection.PostSync(path + "_mget", data);

			if (response.Result == null)
				return null;

			return JsonConvert.DeserializeObject<MultiHit<T>>(response.Result)
				.Hits
				.Where(h => h.Source != null) // non-existent ids come back as a hit without a "_source"
				.Select(h => h.Source);
		}
		
	}
}
