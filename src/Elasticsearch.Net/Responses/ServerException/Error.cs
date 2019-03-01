using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;

namespace Elasticsearch.Net
{
	[JsonFormatter(typeof(ErrorFormatter))]
	public class Error : ErrorCause
	{
		private static readonly IReadOnlyDictionary<string, string> DefaultHeaders =
			new ReadOnlyDictionary<string, string>(new Dictionary<string, string>(0));

		[DataMember(Name = "headers")]
		public IReadOnlyDictionary<string, string> Headers { get; set; } = DefaultHeaders;

		[DataMember(Name = "root_cause")]
		public IReadOnlyCollection<ErrorCause> RootCause { get; set; }
	}

	internal class ErrorFormatter : IJsonFormatter<Error>
	{
		public void Serialize(ref JsonWriter writer, Error value, IJsonFormatterResolver formatterResolver) { }

		public Error Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			switch (reader.GetCurrentJsonToken())
			{
				case JsonToken.String:
				{
					var error = new Error { Reason = reader.ReadString() };
					return error;
				}
				case JsonToken.BeginObject:
				{
					var formatter = formatterResolver.GetFormatter<Dictionary<string, object>>();
					var dict = formatter.Deserialize(ref reader, formatterResolver);

					var error = new Error();
					error.FillValues(dict);

					if (dict.TryGetValue("caused_by", out var causedBy))
						error.CausedBy = formatterResolver.ReserializeAndDeserialize<ErrorCause>(causedBy);

					if (dict.TryGetValue("headers", out var headers))
					{
						var d = formatterResolver.ReserializeAndDeserialize<Dictionary<string, string>>(headers);
						if (d != null) error.Headers = new ReadOnlyDictionary<string, string>(d);
					}

					error.Metadata = ErrorCause.ErrorCauseMetadata.CreateCauseMetadata(dict, formatterResolver);

					return ReadRootCause(dict, formatterResolver, error);
				}
				default:
					reader.ReadNextBlock();
					return null;
			}
		}

		private static Error ReadRootCause(IDictionary<string, object> dict, IJsonFormatterResolver formatterResolver, Error error)
		{
			if (!dict.TryGetValue("root_cause", out var rootCause)) return error;

			if (!(rootCause is List<object> os)) return error;

			error.RootCause = os.Select(formatterResolver.ReserializeAndDeserialize<ErrorCause>).ToList().AsReadOnly();
			return error;
		}
	}
}
