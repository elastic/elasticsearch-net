using System;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nest
{
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