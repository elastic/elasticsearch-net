using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using ElasticSearch.Client.Mapping;

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

        public IndicesResponse DeleteMapping<T>() where T : class
        {
			var type = this.InferTypeName<T>();
			return this.DeleteMapping<T>(this.Settings.DefaultIndex, type);
        }
		public IndicesResponse DeleteMapping<T>(string index) where T : class
		{
			var type = this.InferTypeName<T>();
			return this.DeleteMapping<T>(index, type);
		}
		public IndicesResponse DeleteMapping<T>(string index, string type) where T : class
		{
			var path = this.CreatePath(index, type);
			var status = this.Connection.DeleteSync(path);

			var response = new IndicesResponse();
			try
			{
				response = JsonConvert.DeserializeObject<IndicesResponse>(status.Result);
			}
			catch { }

			response.ConnectionStatus = status;
			return response;
		}


		public IndicesResponse Map<T>() where T : class
		{
			var type = this.InferTypeName<T>();
			return this.Map<T>(this.Settings.DefaultIndex, type);
		}
		public IndicesResponse Map<T>(string index) where T : class
		{
			var type = this.InferTypeName<T>();
			return this.Map<T>(index, type);
		}
		public IndicesResponse Map<T>(string index, string type) where T : class
		{
			var path = this.CreatePath(index, type) + "_mapping";
			var map = this.CreateMapFor<T>();
			
			
			var status = this.Connection.PutSync(path, map);

			var response = new IndicesResponse();
			try
			{
				response = JsonConvert.DeserializeObject<IndicesResponse>(status.Result);
			}
			catch { }

			response.ConnectionStatus = status;
			return response;
		}
		private string CreateMapFor<T>() where T : class
		{
			StringBuilder sb = new StringBuilder();
			StringWriter sw = new StringWriter(sb);

			using (JsonWriter jsonWriter = new JsonTextWriter(sw))
			{
				jsonWriter.Formatting = Formatting.Indented;

				jsonWriter.WriteStartObject();
				{
					var typeName = this.InferTypeName<T>();
					jsonWriter.WritePropertyName(typeName);	
					jsonWriter.WriteStartObject();
					{
						jsonWriter.WritePropertyName("properties");
						jsonWriter.WriteStartObject();
						{
							var properties = typeof(T).GetProperties();
							foreach (var p in properties)
							{
								var att = this.PropertyNameResolver.GetElasticProperty(p);
								if (att != null && att.OptOut)
									continue;

								var propertyName = this.PropertyNameResolver.Resolve(p);
								var type = this.GetElasticSearchTypeFromType(p.PropertyType);
								if (att != null && att.Type != null && att.Type != FieldType.none)
									type = Enum.GetName(typeof(FieldType), att.Type);

								if (type == null)
									continue;

								jsonWriter.WritePropertyName(propertyName);
								jsonWriter.WriteStartObject();
								{
									jsonWriter.WritePropertyName("type");
									jsonWriter.WriteValue(type);

									//jsonWriter.WritePropertyName(store);
								}
								jsonWriter.WriteEnd();
							}
						}
						jsonWriter.WriteEnd();
					}
					jsonWriter.WriteEnd();
				}
				jsonWriter.WriteEndObject();

				return sw.ToString();
			}
		}

		private string GetElasticSearchTypeFromType(Type t)
		{
			if (t == typeof(string))
				return "string";
			if (t.IsValueType)
			{
				switch (t.Name)
				{
					case "Int32" :
						return "integer";
					case "Int64":
						return "long";
					case "Single":
						return "float";
					case "Double":
						return "double";
					case "DateTime":
						return "date";				
				}
			}
			return null;
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
