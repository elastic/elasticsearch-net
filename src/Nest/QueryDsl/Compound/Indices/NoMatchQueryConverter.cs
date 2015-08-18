using System;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	public class NoMatchQueryConverter : CompositeJsonConverter<ReadAsTypeJsonConverter<QueryContainerDescriptor<object>>, CustomJsonConverter>
	{
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.String)
				return base.ReadJson(reader, objectType, existingValue, serializer);

			var en = serializer.Deserialize<NoMatchShortcut>(reader);
			return new NoMatchQueryContainer {Shortcut = en};
		}
	}
}