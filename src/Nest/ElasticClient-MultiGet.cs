using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Domain;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;

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
			var index = this.Infer.IndexName<T>();
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			var typeName = this.Infer.TypeName<T>();

			return this.MultiGet<T>(ids, this.PathResolver.CreateIndexTypePath(index, typeName));
		}
		/// <summary>
		/// Gets multiple documents of T by id in the specified index
		/// </summary>
		public IEnumerable<T> MultiGet<T>(string index, IEnumerable<int> ids)
			where T : class
		{
			return this.MultiGet<T>(index, ids.Select(i => Convert.ToString(i)));
		}
		/// <summary>
		/// Gets multiple documents of T by id in the specified index
		/// </summary>
		public IEnumerable<T> MultiGet<T>(string index, IEnumerable<string> ids)
			where T : class
		{
			var typeName = this.Infer.TypeName<T>();
			
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
		
		public MultiGetResponse MultiGetFull(Action<MultiGetDescriptor> multiGetSelector)
		{
			MultiGetDescriptor descriptor;
			var response = _multiGetUsingDescriptor(multiGetSelector, out descriptor);

			var multiGetHitConverter = new MultiGetHitConverter(descriptor);
			var multiGetResponse = this.Serializer.DeserializeInternal<MultiGetResponse>(
				response, 
				extraConverters: new List<JsonConverter> { multiGetHitConverter });

			return multiGetResponse;
		}

		private ConnectionStatus _multiGetUsingDescriptor(Action<MultiGetDescriptor> multiGetSelector, out MultiGetDescriptor descriptor)
		{
			multiGetSelector.ThrowIfNull("multiGetSelector");
			descriptor = new MultiGetDescriptor();
			multiGetSelector(descriptor);
			var data = @"{ ""docs"" : [";
			var objects = new List<string>();
			foreach (var g in descriptor._GetOperations)
			{
				string objectJson = "{";
				var properties = new List<string>();
				if (descriptor._FixedIndex.IsNullOrEmpty())
				{
					var index = string.IsNullOrEmpty(g._Index) ? this.Infer.IndexName(g._ClrType) : g._Index;
					properties.Add(@"""_index"" : " + this.Serialize(index));
				}
				if (descriptor._FixedType.IsNullOrEmpty())
				{
					var type = this.ResolveTypeName(g._Type, this.Infer.TypeName(g._ClrType));
					properties.Add(@"""_type"" : " + this.Serialize(type));
				}
				properties.Add(@"""_id"" : " + this.Serialize(g._Id));
				if (!g._Routing.IsNullOrEmpty())
					properties.Add(@"""_routing"" : " + this.Serialize(g._Routing));
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
			return response;
		}

		private IEnumerable<T> MultiGet<T>(IEnumerable<string> ids, string path)
			where T : class
		{
			var data = @"{{ ""ids"": {0} }}".F(this.Serialize(ids));
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
