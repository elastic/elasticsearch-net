using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Internal;

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

		public override string ToString() => CausedBy == null
			? $"Type: {Type} Reason: \"{Reason}\""
			: $"Type: {Type} Reason: \"{Reason}\" CausedBy: \"{CausedBy}\"";
	}

	internal class ErrorFormatter : ErrorCauseFormatter<Error>
	{
		private static readonly AutomataDictionary Fields = new AutomataDictionary
		{
			{ "headers", 0 },
			{ "root_cause", 1 }
		};

		protected override void Serialize(ref JsonWriter writer, ref int count, Error value, IJsonFormatterResolver formatterResolver)
		{
			if (value.Headers.Any())
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName("headers");
				formatterResolver.GetFormatter<IReadOnlyDictionary<string, string>>()
					.Serialize(ref writer, value.Headers, formatterResolver);

				count++;
			}

			if (value.RootCause.Any())
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName("root_cause");
				formatterResolver.GetFormatter<IReadOnlyCollection<ErrorCause>>()
					.Serialize(ref writer, value.RootCause, formatterResolver);

				count++;
			}
		}

		protected override bool Deserialize(ref JsonReader reader, ref ArraySegment<byte> property, Error value, IJsonFormatterResolver formatterResolver)
		{
			if (Fields.TryGetValue(property, out var fieldValue))
			{
				switch (fieldValue)
				{
					case 0:
						value.Headers = formatterResolver.GetFormatter<Dictionary<string, string>>()
							.Deserialize(ref reader, formatterResolver);
						break;
					case 1:
						value.RootCause = formatterResolver.GetFormatter<List<ErrorCause>>()
							.Deserialize(ref reader, formatterResolver);
						break;
				}

				return true;
			}

			return false;
		}
	}
}
