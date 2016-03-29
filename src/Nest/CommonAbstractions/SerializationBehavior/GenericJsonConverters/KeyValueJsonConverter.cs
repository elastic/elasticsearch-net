using System;
using System.Collections.Concurrent;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Nest
{

	internal class KeyValueConversion
	{
		private static readonly ConcurrentDictionary<Type, KeyValueConversion> KnownTypes = new ConcurrentDictionary<Type, KeyValueConversion>();

		public static KeyValueConversion Create<TContainer, TValue>() where TContainer : class, new()
		{
			var t = typeof(TContainer);
			KeyValueConversion conversion;
			if (KnownTypes.TryGetValue(t, out conversion)) return conversion;

			var properties = t.GetCachedObjectProperties(MemberSerialization.OptOut);
			var keyProp = properties.FirstOrDefault(p => p.PropertyType != typeof(TValue));
			var valueProp = properties.FirstOrDefault(p => p.PropertyType == typeof(TValue));
			if (keyProp == null) throw new Exception($"No key property found on type {t.Name}");
			if (valueProp == null) throw new Exception($"No value property found on type {t.Name}");
			conversion = new KeyValueConversion { KeyProperty = keyProp, ValueProperty = valueProp};
			KnownTypes.TryAdd(t, conversion);
			return conversion;
		}

		public JsonProperty KeyProperty { get; set; }
		public JsonProperty ValueProperty { get; set; }
	}


	//TODO this serializer feels to generalized since it relies on reflection and caching in KnownTypes :/
	internal class KeyValueJsonConverter<TContainer, TValue> : JsonConverter
		where TContainer : class, new()
	{

		public override bool CanConvert(Type objectType) => true;
		public override bool CanRead => true;
		public override bool CanWrite => true;

		public override void WriteJson(JsonWriter writer, object v, JsonSerializer serializer)
		{
			var conversion = KeyValueConversion.Create<TContainer, TValue>();
			if (conversion == null)
			{
				writer.WriteNull();
				return;
			};

			var key = conversion.KeyProperty.ValueProvider.GetValue(v).ToString();
			var value = conversion.ValueProperty.ValueProvider.GetValue(v);
			if (key.IsNullOrEmpty() || value == null)
			{
				writer.WriteNull();
				return;
			};

			writer.WriteStartObject();
			writer.WritePropertyName(key);
			serializer.Serialize(writer, value);

			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.StartObject) return null;
			var depth = reader.Depth;

			reader.Read(); //property name
			var key = reader.Value as string;
			reader.Read(); //{
			var value = serializer.Deserialize<TValue>(reader);

			if (reader.Depth > depth)
			{
				do
				{
					reader.Read();
				} while (reader.Depth >= depth && reader.TokenType != JsonToken.EndObject);
			}

			var conversion = KeyValueConversion.Create<TContainer, TValue>();
			if (conversion == null) return null;

			var o = new TContainer();
			conversion.KeyProperty.ValueProvider.SetValue(o, key);
			conversion.ValueProperty.ValueProvider.SetValue(o, value);
			return o;
		}

	}
}