using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Nest
{

	public partial class ElasticClient
	{
		public IEnumerable<T> MultiGet<T>(IEnumerable<int> ids)
			where T : class
		{
			return this.MultiGet<T>(ids.Select(i => Convert.ToString(i)));
		}

		/// <summary>
		/// Gets multiple documents of T by id in the default index and the inferred typename for T
		/// </summary>
		public IEnumerable<T> MultiGet<T>(IEnumerable<string> ids)
			where T : class
		{
			var index = this.IndexNameResolver.GetIndexForType<T>();
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			var typeName = this.TypeNameResolver.GetTypeNameFor<T>();

			return this.MultiGet<T>(ids, this.PathResolver.CreateIndexTypePath(index, typeName));
		}
		/// <summary>
		/// Gets multiple documents of T by id in the specified index and the specified typename for T
		/// </summary>
		public IEnumerable<T> MultiGet<T>(string index, string type, IEnumerable<int> ids)
			where T : class
		{
			return this.MultiGet<T>(index, type, ids.Select(i => Convert.ToString(i)));
		}
		/// <summary>
		/// Gets multiple documents of T by id in the specified index and the specified typename for T
		/// </summary>
		public IEnumerable<T> MultiGet<T>(string index, string type, IEnumerable<string> ids)
			where T : class
		{
			return this.MultiGet<T>(ids, this.PathResolver.CreateIndexTypePath(index, type));
		}
		public IEnumerable<object> MultiGet(Action<MultiGetDescriptor> multiGetSelector)
		{
			multiGetSelector.ThrowIfNull("multiGetSelector");
			var descriptor = new MultiGetDescriptor();
			multiGetSelector(descriptor);
			var data = @"{ ""docs"" : [";
			var objects = new List<string>();
			foreach (var g in descriptor._GetOperations)
			{
				string objectJson = "{";
				var properties = new List<string>();
				if (descriptor._FixedIndex.IsNullOrEmpty())
				{
					var index = string.IsNullOrEmpty(g._Index) ? this.IndexNameResolver.GetIndexForType(g._ClrType) : g._Index;
					properties.Add(@"""_index"" : " + this.Serialize(index));
				}

				if (descriptor._FixedType.IsNullOrEmpty())
				{
					var type = string.IsNullOrEmpty(g._Type) ? this.TypeNameResolver.GetTypeNameFor(g._ClrType) : g._Type;
					properties.Add(@"""_type"" : " + this.Serialize(type));
				}
				properties.Add(@"""_id"" : " + this.Serialize(g._Id));

				if (g._Fields.HasAny())
					properties.Add(@"""fields"" : " + this.Serialize(g._Fields));

				objectJson += string.Join(", ", properties);
				objectJson += "}";
				objects.Add(objectJson);
			}
			data += string.Join(", ", objects);
			data += "] }";

			var path = "";
			if (!descriptor._FixedIndex.IsNullOrEmpty())
			{
				path += descriptor._FixedIndex;
				if (!descriptor._FixedIndex.IsNullOrEmpty())
				{
					path += "/" + descriptor._FixedType;
				}
			}
			path += "/_mget";
			var response = this.Connection.PostSync(path, data);
			if (!response.Success)
				yield break;

			var jsonObject = JObject.Parse(response.Result);
			var docsJarray = (JArray)jsonObject["docs"];
			var withMeta = docsJarray.Zip(descriptor._GetOperations, (doc, desc) => new { Hit = doc, Descriptor = desc });
			foreach (var m in withMeta)
			{
				// non-existent ids come back as a hit without a "_source"

				if (m.Hit["_source"] != null)
					yield return JsonConvert.DeserializeObject(m.Hit["_source"].ToString(), m.Descriptor._ClrType, this.IndexSerializationSettings);
				else if (m.Hit["fields"] != null)
					yield return JsonConvert.DeserializeObject(m.Hit["fields"].ToString(), m.Descriptor._ClrType, this.IndexSerializationSettings);
			}			
		}
		private IEnumerable<T> MultiGet<T>(IEnumerable<string> ids, string path)
			where T : class
		{
			var data = @"{{ ""ids"": {0} }}".F(JsonConvert.SerializeObject(ids));
			var response = this.Connection.PostSync(path + "_mget", data);

			if (response.Result == null)
				return null;

			return this.Deserialize<MultiHit<T>>(response.Result)
				.Hits
				.Where(h => h.Source != null) // non-existent ids come back as a hit without a "_source"
				.Select(h => h.Source);
		}

	}
}
