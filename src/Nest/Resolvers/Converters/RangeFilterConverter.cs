using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class RangeFilterConverter<T> : JsonConverter
		where T : class, new()
	{
		private readonly ReadAsTypeConverter<T> _reader;

		public RangeFilterConverter()
		{
			this._reader = new ReadAsTypeConverter<T>();
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof (IRangeFilter).IsAssignableFrom(objectType);
		}

		public override bool CanRead
		{
			get { return true; }
		}

		public override bool CanWrite
		{
			get { return true; }
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var depth = reader.Depth;
			if (reader.TokenType != JsonToken.StartObject)
				return null;

			var r = JObject.Load(reader);
			var dict = r.Properties().ToDictionary(p => p.Name);

			var name = GetValue<string>("_name", dict);
			var cache = GetValue<bool?>("_cache", dict);
			var cacheKey = GetValue<string>("_cacheKey", dict) ?? GetValue<string>("_cache_key", dict);
			var executionStr = GetValue<string>("execution", dict);
			FilterRangeExecutionType execution;
			var foundExecution = Enum.TryParse(executionStr, true, out execution);

			if (dict.Count == 0) return null;

			var remainingProperty = dict.First();

			var filter = this._reader.ReadJson(remainingProperty.Value.First().CreateReader(), objectType, existingValue,
				serializer);
			var setter = filter as IRangeFilter;
			if (setter != null)
			{
				setter.Field = remainingProperty.Key;
				if (foundExecution)
					setter.ExecutionType = execution;
			}
			var f = filter as IFilter;
			if (f == null) return filter;
			f.Cache = cache;
			f.FilterName = name;
			f.CacheKey = cacheKey;
			return filter;
		}

		private static TValue GetValue<TValue>(string name, Dictionary<string, JProperty> dict)
		{
			JProperty prop;
			if (dict.TryGetValue(name, out prop))
			{
				dict.Remove(name);
				return prop.Value.Value<TValue>();
			}
			return default(TValue);
		}

		private static void WriteProperty(JsonWriter writer, IFilter filter, string field, object value)
		{
			if ((field.IsNullOrEmpty() || value == null))
				return;
			writer.WritePropertyName(field);
			writer.WriteValue(value);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = value as IRangeFilter;
			if (v == null) return;

			var fieldName = v.Field;
			if (fieldName == null)
				return;

			var contract = serializer.ContractResolver as SettingsContractResolver;
			if (contract == null)
				return;

			var field = contract.Infer.PropertyPath(fieldName);
			if (field.IsNullOrEmpty())
				return;

			var cache = v.Cache;
			var cacheKey = v.CacheKey;
			var name = v.FilterName;

			v.Cache = null;
			v.CacheKey = null;
			v.FilterName = null;

			writer.WriteStartObject();
			{
				writer.WritePropertyName(field);
				serializer.Serialize(writer, value);
				if (v.ExecutionType.HasValue)
					WriteProperty(writer, v, "execution", v.ExecutionType.Value.GetStringValue());
				WriteProperty(writer, v, "_cache", cache);
				WriteProperty(writer, v, "_cache_key", cacheKey);
				WriteProperty(writer, v, "_name", name);
			}
			writer.WriteEndObject();
		}
	}
}