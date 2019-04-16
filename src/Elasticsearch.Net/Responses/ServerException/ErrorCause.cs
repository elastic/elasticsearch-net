using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Elasticsearch.Net
{
	[JsonFormatter(typeof(ErrorCauseFormatter))]
	[DataContract]
	public class ErrorCause
	{
		private static readonly IReadOnlyCollection<string> DefaultCollection =
			new ReadOnlyCollection<string>(new string[0]);

		private static readonly IReadOnlyCollection<ShardFailure> DefaultFailedShards =
			new ReadOnlyCollection<ShardFailure>(new ShardFailure[0]);

		[DataMember(Name = "bytes_limit")]
		public long? BytesLimit { get; set; }

		[DataMember(Name = "bytes_wanted")]
		public long? BytesWanted { get; set; }

		[DataMember(Name = "caused_by")]
		public ErrorCause CausedBy { get; set; }

		[DataMember(Name = "col")]
		public int? Column { get; set; }

		[DataMember(Name = "failed_shards")]
		public IReadOnlyCollection<ShardFailure> FailedShards { get; set; } = DefaultFailedShards;

		[DataMember(Name = "grouped")]
		public bool? Grouped { get; set; }

		[DataMember(Name = "index")]
		public string Index { get; set; }

		[DataMember(Name = "index_uuid")]
		public string IndexUUID { get; set; }

		[DataMember(Name = "lang")]
		public string Language { get; set; }

		[DataMember(Name = "license.expired.feature")]
		public string LicensedExpiredFeature { get; set; }

		[DataMember(Name = "line")]
		public int? Line { get; set; }

		[DataMember(Name = "phase")]
		public string Phase { get; set; }

		[DataMember(Name = "reason")]
		public string Reason { get; set; }

		[DataMember(Name = "resource.id")]
		[JsonFormatter(typeof(InterfaceReadOnlyCollectionSingleOrEnumerableFormatter<string>))]
		public IReadOnlyCollection<string> ResourceId { get; set; } = DefaultCollection;

		[DataMember(Name = "resource.type")]
		public string ResourceType { get; set; }

		[DataMember(Name = "script")]
		public string Script { get; set; }

		[DataMember(Name = "script_stack")]
		[JsonFormatter(typeof(InterfaceReadOnlyCollectionSingleOrEnumerableFormatter<string>))]
		public IReadOnlyCollection<string> ScriptStack { get; set; } = DefaultCollection;

		[DataMember(Name = "shard")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		public int? Shard { get; set; }

		[DataMember(Name = "stack_trace")]
		public string StackTrace { get; set; }

		[DataMember(Name = "type")]
		public string Type { get; set; }

		public override string ToString() => CausedBy == null
			? $"Type: {Type} Reason: \"{Reason}\""
			: $"Type: {Type} Reason: \"{Reason}\" CausedBy: \"{CausedBy}\"";
	}

	internal class ErrorCauseFormatter : IJsonFormatter<ErrorCause>
	{
		private static readonly IJsonFormatter<ErrorCause> Formatter =
			DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<ErrorCause>();

		public ErrorCause Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
				case JsonToken.String:
					return new ErrorCause { Reason = reader.ReadString() };
				case JsonToken.BeginObject:
					return Formatter.Deserialize(ref reader, formatterResolver);
				default:
					reader.ReadNextBlock();
					return null;
			}
		}

		public void Serialize(ref JsonWriter writer, ErrorCause value, IJsonFormatterResolver formatterResolver) =>
			Formatter.Serialize(ref writer, value, formatterResolver);
	}
}
