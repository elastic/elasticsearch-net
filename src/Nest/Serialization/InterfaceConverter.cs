using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nest
{
	public class InterfaceConverter<TInterface, TConcrete> : JsonConverter<TInterface>
		where TConcrete : class, TInterface
	{
		public override TInterface Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
			JsonSerializer.Deserialize<TConcrete>(ref reader, options);

		public override void Write(Utf8JsonWriter writer, TInterface value, JsonSerializerOptions options) =>
			JsonSerializer.Serialize<TConcrete>(writer, value as TConcrete, options);
	}
}