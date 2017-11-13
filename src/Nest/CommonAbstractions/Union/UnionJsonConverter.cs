using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class UnionJsonConverter : JsonConverter
	{
		private static readonly ConcurrentDictionary<Type, UnionJsonConverterBase> KnownTypes = new ConcurrentDictionary<Type, UnionJsonConverterBase>();

		public override bool CanConvert(Type objectType) => true;

		public static UnionJsonConverterBase CreateConverter(Type t)
		{
			UnionJsonConverterBase conversion;
			if (KnownTypes.TryGetValue(t, out conversion))
				return conversion;

			var genericArguments = t.GetTypeInfo().GenericTypeArguments;
			switch (genericArguments.Length)
			{
				case 2:
					conversion = typeof (UnionJsonConverter<,>).CreateGenericInstance(genericArguments) as UnionJsonConverterBase;
					break;
				default:
					throw new Exception($"No union converter registered that takes {genericArguments.Length} type arguments for {t.Name}");

			}
			KnownTypes.TryAdd(t, conversion);
			return conversion;
		}

		public override void WriteJson(JsonWriter writer, object v, JsonSerializer serializer)
		{
			var converter = CreateConverter(v.GetType());
			converter?.WriteJson(writer, v, serializer);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var converter = CreateConverter(objectType);
			return converter?.ReadJson(reader, objectType, existingValue, serializer);
		}
	}

	internal abstract class UnionJsonConverterBase
	{
		public bool TryRead<T>(JsonReader reader, JsonSerializer serializer, out T v)
		{
			try
			{
				v = serializer.Deserialize<T>(reader);
				return true;
			}
			catch {}
			v= default(T);
			return false;
		}

		public abstract void WriteJson(JsonWriter writer, object v, JsonSerializer serializer);
		public abstract object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer);
	}

	internal class UnionJsonConverter<TFirst, TSecond> : UnionJsonConverterBase
	{
		public override void WriteJson(JsonWriter writer, object v, JsonSerializer serializer)
		{
			if (!(v is Union<TFirst, TSecond> union))
			{
				writer.WriteNull();
				return;
			}

			union.Match(
				first => serializer.Serialize(writer, first),
				second => serializer.Serialize(writer, second)
			);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			Union<TFirst, TSecond> u = null;
			using (var r = JToken.Load(reader).CreateReader())
			{
				if (this.TryRead(r, serializer, out TFirst first)) u = first;
				else if (this.TryRead(r, serializer, out TSecond second)) u = second;
			}
			return u;
		}

	}
}
