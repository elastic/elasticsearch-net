using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nest
{
	public class StringAliasConverter<T> : JsonConverter<T>
	{
		public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var value = reader.GetString();

			var instance = (T)Activator.CreateInstance(
				typeof(T),
				BindingFlags.Instance | BindingFlags.Public,
				args: new object[] {value},
				binder: null,
				culture: null)!;

			return instance;
		}

		public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) =>
			throw new NotImplementedException();
	}
}
