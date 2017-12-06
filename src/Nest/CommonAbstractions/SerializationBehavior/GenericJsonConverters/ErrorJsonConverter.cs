using System.Collections.Generic;
using System.Collections.ObjectModel;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	internal class ErrorJsonConverter : ErrorCauseJsonConverter<Error>
	{
		protected override bool ReadProperty(Error error, string propertyName, JsonReader reader, JsonSerializer serializer)
		{
			if (propertyName == "root_cause")
				return ExtractRootCauses(error, reader, serializer);

			if (propertyName == "headers")
				return ExtractHeaders(error, reader, serializer);

			return ExtractMetadata(propertyName, error, reader, serializer);
		}

		private static bool ExtractHeaders(Error error, JsonReader reader, JsonSerializer serializer)
		{
			reader.Read();
			if (reader.TokenType != JsonToken.StartObject)
				return false;
			var dict = serializer.Deserialize<Dictionary<string, string>>(reader);
			if (dict == null) return false;
			error.Headers = new ReadOnlyDictionary<string, string>(dict);
			return true;
		}

		private bool ExtractRootCauses(Error error, JsonReader reader, JsonSerializer serializer)
		{
			reader.Read();
			if (reader.TokenType != JsonToken.StartArray)
				return false;

			var depth = reader.Depth;
			var rootCauses = new List<ErrorCause>();
			do
			{
				reader.Read();
				var rootCause = ReadCausedBy(reader, serializer);
				if (rootCause != null)
					rootCauses.Add(rootCause);
			} while (reader.Depth >= depth && reader.TokenType != JsonToken.EndArray);
			error.RootCause = rootCauses;
			return true;
		}
	}
}
