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
		/// Clears all caches of all indices
		/// </summary>
		public IndicesResponse ClearCache()
		{
			return this.ClearCache(null, ClearCacheOptions.All);
		}
		/// <summary>
		/// Clears the entire cache for the default index set in the client settings
		/// </summary>
		public IndicesResponse ClearCache<T>() where T : class
		{
			return this.ClearCache(new List<string> { this.Settings.DefaultIndex }, ClearCacheOptions.All);
		}

		/// <summary>
		/// Clears the specified caches for the default index set in the client settings
		/// </summary>
		public IndicesResponse ClearCache<T>(ClearCacheOptions options) where T : class
		{
			return this.ClearCache(new List<string> { this.Settings.DefaultIndex }, options);
		}
		/// <summary>
		/// Clears the specified caches for all indices
		/// </summary>
		public IndicesResponse ClearCache(ClearCacheOptions options)
		{
			return this.ClearCache(null, options);
		}
		/// <summary>
		/// Clears the specified caches for only the indices passed under indices
		/// </summary>
		public IndicesResponse ClearCache(List<string> indices, ClearCacheOptions options)
		{
			string path = "/_cache/clear";
			if (indices != null && indices.Any(s => !string.IsNullOrEmpty(s)))
			{
				path = "/" + string.Join(",", indices.Where(s => !string.IsNullOrEmpty(s)).ToArray()) + path;
			}
			if (options != ClearCacheOptions.All)
			{
				var caches = new List<string>();
				if ((options & ClearCacheOptions.Id) == ClearCacheOptions.Id)
				{
					caches.Add("id=true");
				}
				if ((options & ClearCacheOptions.Filter) == ClearCacheOptions.Filter)
				{
					caches.Add("filter=true");
				}
				if ((options & ClearCacheOptions.FieldData) == ClearCacheOptions.FieldData)
				{
					caches.Add("field_data=true");
				}
				if ((options & ClearCacheOptions.Bloom) == ClearCacheOptions.Bloom)
				{
					caches.Add("bloom=true");
				}

				path += "?" + string.Join("&", caches.ToArray());
			}

			ConnectionStatus status = this.Connection.PostSync(path, string.Empty);
			var response = new IndicesResponse();
			try
			{
				response = JsonConvert.DeserializeObject<IndicesResponse>(status.Result);
				response.IsValid = true;
			}
			catch
			{
			}
			response.ConnectionStatus = status;
			return response;
		}
	}
}
