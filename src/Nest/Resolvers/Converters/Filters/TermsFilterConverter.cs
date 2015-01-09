using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.Resolvers.Converters.Filters
{
	public class TermsFilterConverter : JsonConverter
	{
		public override bool CanRead { get { return true; } }
		public override bool CanWrite { get { return true; } }

		public override bool CanConvert(Type objectType)
		{
			return true; //only to be used with attribute or contract registration.
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var f = value as ITermsBaseFilter;
			if (f == null || (f.IsConditionless && !f.IsVerbatim)) return;
			
			var contract = serializer.ContractResolver as SettingsContractResolver;
			if (contract == null)
				return;
			
			var field = contract.Infer.PropertyPath(f.Field);
			if (field.IsNullOrEmpty())
				return;

			writer.WriteStartObject();
			{
				var lf = f as ITermsLookupFilter;
				if (lf != null)
				{
					writer.WritePropertyName(field);
					writer.WriteStartObject();
					{
						WriteProperty(writer, f, "id", lf.Id);
						SerializeProperty(writer, serializer, f, "type", lf.Type);
						SerializeProperty(writer, serializer, f, "index", lf.Index);
						SerializeProperty(writer, serializer, f, "path", lf.Path);
						WriteProperty(writer, f, "routing", lf.Routing);
						WriteProperty(writer, f, "cache", lf.CacheLookup);
					}
					writer.WriteEndObject();
				}
				var tf = f as ITermsFilter;
				if (tf != null)
				{
					writer.WritePropertyName(field);
					serializer.Serialize(writer, tf.Terms);
				}
				if (f.Execution.HasValue)
				{
					writer.WritePropertyName("execution");
					serializer.Serialize(writer, f.Execution.Value);
				}

				WriteProperty(writer, f, "_cache", f.Cache);
				WriteProperty(writer, f, "_cache_key", f.CacheKey);
				WriteProperty(writer, f, "_name", f.FilterName);
				
			}
			writer.WriteEndObject();		
		}

		private static void SerializeProperty(JsonWriter writer, JsonSerializer serializer , IFilter filter, string field, object value)
		{
			if ((field.IsNullOrEmpty() || value == null))
				return;
			writer.WritePropertyName(field);
			serializer.Serialize(writer, value);
		}
		private static void WriteProperty(JsonWriter writer, IFilter filter, string field, object value)
		{
			if ((field.IsNullOrEmpty() || value == null))
				return;
			writer.WritePropertyName(field);
			writer.WriteValue(value);
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var j = JObject.Load(reader);
			if (j == null || !j.HasValues)
				return null;

			string cacheKey = null, cacheName = null, field = null, execution = null;
			bool? cache = null;
			ITermsBaseFilter filter = null;
			foreach (var jv in j)
			{
				switch (jv.Key)
				{
					case "execution":
						execution = jv.Value.Value<string>();
						break;
					case "_cache":
						cache = jv.Value.Value<bool>();
						break;
					case "_cache_key":
						cacheKey = jv.Value.Value<string>();
						break;
					case "_name":
						cacheName = jv.Value.Value<string>();
						break;
					default:
						field = jv.Key;
						
						if (jv.Value.Type == JTokenType.Array)
						{
							ITermsFilter f = new TermsFilterDescriptor();
							f.Terms = jv.Value.Values<string>();
							filter = f;
						}
						else 
						{
							ITermsLookupFilter f = new TermsLookupFilterDescriptor();
							var id = jv.Value["id"];
							var index = jv.Value["index"];
							var type = jv.Value["type"];
							var path = jv.Value["path"];

							if (id != null) f.Id = id.Value<string>();
							if (index != null) f.Index = index.Value<string>();
							if (type != null) f.Type = type.Value<string>();
							if (path != null) f.Path = path.Value<string>();
							filter = f;
						}
						break;
				}
			}
			if (filter == null) return null;
			if (execution != null)
				filter.Execution = execution.ToEnum<TermsExecution>();
			filter.Field = field;
			filter.Cache = cache;
			filter.CacheKey = cacheKey;
			filter.FilterName = cacheName;
			return filter;

		}
	}
	
}
