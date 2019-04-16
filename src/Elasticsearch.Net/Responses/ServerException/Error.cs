using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;

namespace Elasticsearch.Net
{
	[DataContract]
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
		private static readonly IJsonFormatter<Error> Formatter =
			DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<Error>();

		public void Serialize(ref JsonWriter writer, Error value, IJsonFormatterResolver formatterResolver) =>
			Formatter.Serialize(ref writer, value, formatterResolver);

		public Error Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
				case JsonToken.String:
					return new Error { Reason = reader.ReadString() };
				case JsonToken.BeginObject:
					return Formatter.Deserialize(ref reader, formatterResolver);
				default:
					reader.ReadNextBlock();
					return null;
			}
		}
	}
}
