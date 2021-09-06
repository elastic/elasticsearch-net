using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch
{
	public class UnionConverter : JsonConverterFactory
	{
		public override bool CanConvert(Type typeToConvert) => typeToConvert.Name == typeof(Union<,>).Name;

		public override JsonConverter CreateConverter(
			Type type,
			JsonSerializerOptions options)
		{
			var itemOneType = type.GetGenericArguments()[0];
			var itemTwoType = type.GetGenericArguments()[1];

			var converter = (JsonConverter)Activator.CreateInstance(
				typeof(UnionConverterInner<,>).MakeGenericType(itemOneType, itemTwoType),
				BindingFlags.Instance | BindingFlags.Public,
				null,
				null,
				null);

			return converter;
		}

		private class UnionConverterInner<TItem1, TItem2> : JsonConverter<Union<TItem1, TItem2>>
		{
			// TODO - Implement properly as we need to figure out the possible types and handle accordingly
			public override Union<TItem1, TItem2>? Read(ref Utf8JsonReader reader, Type typeToConvert,
				JsonSerializerOptions options) => null;

			public override void Write(Utf8JsonWriter writer, Union<TItem1, TItem2> value,
				JsonSerializerOptions options)
			{
				if (value.Item1 is not null)
				{
					JsonSerializer.Serialize(writer, value.Item1, value.Item1.GetType(), options);
					return;
				}

				if (value.Item2 is not null)
				{
					JsonSerializer.Serialize(writer, value.Item2, value.Item2.GetType(), options);
					return;
				}

				throw new SerializationException("Invalid union type");
			}
		}
	}

	public class UnionConverter<TConcrete> : JsonConverter<TConcrete> where TConcrete : class
	{
		public override TConcrete Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var token = reader.TokenType;

			switch (token)
			{
				case JsonTokenType.String:
				{
					var value = reader.GetString();
					var result = (TConcrete)Activator.CreateInstance(typeof(TConcrete), value);
					return result;
				}
				case JsonTokenType.Number:
				{
					var value = reader.GetInt32();
					var result = (TConcrete)Activator.CreateInstance(typeof(TConcrete), value);
					return result;
				}
			}

			throw new SerializationException();
		}

		public override void Write(Utf8JsonWriter writer, TConcrete value, JsonSerializerOptions options) =>
			throw new NotImplementedException();
	}
}
