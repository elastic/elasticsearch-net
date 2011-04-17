using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElasticSearch.Client.Thrift;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Fasterflect;
using ElasticSearch;
using Newtonsoft.Json.Converters;
using ElasticSearch.Client.DSL;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;

namespace ElasticSearch.Client
{
	public partial class ElasticClient
	{

		public IndicesResponse ClearCache()
		{
			return this.ClearCache(null, ClearCacheOptions.All);
		}
		public IndicesResponse ClearCache<T>() where T : class
		{
			return this.ClearCache(new List<string> { this.Settings.DefaultIndex } , ClearCacheOptions.All);
		}
		public IndicesResponse ClearCache<T>(ClearCacheOptions options) where T : class
		{
			return this.ClearCache(new List<string> { this.Settings.DefaultIndex }, options);
		}
		public IndicesResponse ClearCache(ClearCacheOptions options)
		{
			return this.ClearCache(null, options);
		}
		public IndicesResponse ClearCache(List<string> indices, ClearCacheOptions options)
		{
			var path = "/_cache/clear";
			if (indices != null && indices.Any(s=> !string.IsNullOrEmpty(s)))
			{
				path = "/" + string.Join(",", indices.Where(s => !string.IsNullOrEmpty(s)).ToArray()) + path;
			}
			if (options != null && options != ClearCacheOptions.All)
			{
				var caches = new List<string>();
				if ((options & ClearCacheOptions.Id) == ClearCacheOptions.Id)
					caches.Add("id=true");
				if ((options & ClearCacheOptions.Filter) == ClearCacheOptions.Filter)
					caches.Add("filter=true");
				if ((options & ClearCacheOptions.FieldData) == ClearCacheOptions.FieldData)
					caches.Add("field_data=true");
				if ((options & ClearCacheOptions.Bloom) == ClearCacheOptions.Bloom)
					caches.Add("bloom=true");

				path += "?" + string.Join("&",caches.ToArray());
			}

			var status = this.Connection.PostSync(path, string.Empty);
			var response = new IndicesResponse();
			try
			{
				response = JsonConvert.DeserializeObject<IndicesResponse>(status.Result);
			}
			catch {}
			response.ConnectionStatus = status;
			return response;
		}
	}
	[Flags]
	public enum ClearCacheOptions
	{
		Id = 0x1,
		Filter = 0x2,
		FieldData = 0x4,
		Bloom = 0x8,
		All = Id | Filter | FieldData | Bloom
	}
	
}
