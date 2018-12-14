using System.Collections.Generic;
using System.Collections.ObjectModel;
using Elasticsearch.Net;
using Utf8Json;

namespace Nest
{
	internal class ErrorFormatter : ErrorCauseFormatter<Error>
	{
		protected override bool ReadProperty(ref JsonReader reader, string propertyName, Error error, IJsonFormatterResolver formatterResolver)
		{
			if (propertyName == "root_cause")
				return ExtractRootCauses(ref reader, error, formatterResolver);

			if (propertyName == "headers")
				return ExtractHeaders(ref reader, error, formatterResolver);

			return ExtractMetadata(ref reader, propertyName, error, formatterResolver);
		}

		private static bool ExtractHeaders(ref JsonReader reader, Error error, IJsonFormatterResolver formatterResolver)
		{
			// reader.ReadNext();
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
				return false;

			var headers = formatterResolver.GetFormatter<ReadOnlyDictionary<string, string>>()
				.Deserialize(ref reader, formatterResolver);

			if (headers == null)
				return false;

			error.Headers = headers;
			return true;
		}

		private bool ExtractRootCauses(ref JsonReader reader, Error error, IJsonFormatterResolver formatterResolver)
		{
			// reader.ReadNext();
			if (reader.GetCurrentJsonToken() != JsonToken.BeginArray)
				return false;

			var count = 0;
			var rootCauses = new List<ErrorCause>();
			while (reader.ReadIsInArray(ref count))
			{
				var rootCause = ReadCausedBy(ref reader, formatterResolver);
				if (rootCause != null)
					rootCauses.Add(rootCause);
			}

			error.RootCause = rootCauses;
			return true;
		}
	}
}
