using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Elasticsearch.Net;
using Utf8Json;

namespace Nest
{
	internal class ErrorCauseFormatter : ErrorCauseFormatter<ErrorCause> { }

	internal class ErrorCauseFormatter<TErrorCause> : IJsonFormatter<TErrorCause>
		where TErrorCause : ErrorCause, new()
	{
		public TErrorCause Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			ReadError<TErrorCause>(ref reader, formatterResolver, MaybeSkipProperty);

		public void Serialize(ref JsonWriter writer, TErrorCause value, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();

		private void MaybeSkipProperty(ref JsonReader reader, TErrorCause error, string propertyName, IJsonFormatterResolver formatterResolver)
		{
			if (!ReadProperty(ref reader, propertyName, error, formatterResolver))
				reader.ReadNextBlock();
		}

		protected virtual bool ReadProperty(ref JsonReader reader, string propertyName, TErrorCause error, IJsonFormatterResolver formatterResolver) =>
			false;

		private static void ReadNextBlock<TInnerError>(ref JsonReader reader, TInnerError error, string prop, IJsonFormatterResolver formatterResolver)
			where TInnerError : ErrorCause, new() => reader.ReadNextBlock();

		protected ErrorCause ReadCausedBy(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			ReadError<ErrorCause>(ref reader, formatterResolver, ReadNextBlock);

		private TInnerError ReadError<TInnerError>(ref JsonReader reader, IJsonFormatterResolver formatterResolver, ReadMore<TInnerError> readMore)
			where TInnerError : ErrorCause, new()
		{
			var token = reader.GetCurrentJsonToken();

			if (token == JsonToken.String)
			{
				var reason = reader.ReadString();
				return new TInnerError { Reason = reason };
			}
			if (token != JsonToken.BeginObject)
			{
				reader.ReadNextBlock();
				return null;
			}

			var error = new TInnerError { Metadata = new ErrorCause.ErrorCauseMetadata() };

			var count = 0;
			while (reader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyName();
				switch (propertyName)
				{
					case "type":
						error.Type = reader.ReadString();
						break;
					case "stack_trace":
						error.StackTrace = reader.ReadString();
						break;
					case "reason":
						error.Reason = reader.ReadString();
						break;
					case "caused_by":
						error.CausedBy = ReadCausedBy(ref reader, formatterResolver);
						break;
					default:
						if (!ExtractMetadata(ref reader, propertyName, error, formatterResolver))
							readMore(ref reader, error, propertyName, formatterResolver);
						break;
				}
			}

			return error;
		}

		protected static bool ExtractMetadata(ref JsonReader reader, string propertyName, ErrorCause error, IJsonFormatterResolver formatterResolver)
		{
			var m = error.Metadata;
			switch (propertyName)
			{
				case "license.expired.feature":
					m.LicensedExpiredFeature = reader.ReadString();
					break;
				case "index":
					m.Index = reader.ReadString();
					break;
				case "index_uuid":
					m.IndexUUID = reader.ReadString();
					break;
				case "resource.type":
					m.ResourceType = reader.ReadString();
					break;
				case "resource.id":
					m.ResourceId = ReadArray(ref reader, formatterResolver);
					break;
				case "shard":
					m.Shard = reader.GetCurrentJsonToken() == JsonToken.Number
						? reader.ReadInt32()
						: int.Parse(reader.ReadString());
					break;
				case "line":
					m.Line = reader.ReadInt32();
					break;
				case "col":
					m.Column = reader.ReadInt32();
					break;
				case "bytes_wanted":
					m.BytesWanted = reader.ReadInt64();
					break;
				case "bytes_limit":
					m.BytesLimit = reader.ReadInt64();
					break;
				case "phase":
					m.Phase = reader.ReadString();
					break;
				case "grouped":
					m.Grouped = reader.ReadBoolean();
					break;
				case "script_stack":
					m.ScriptStack = ReadArray(ref reader, formatterResolver);
					break;
				case "script":
					m.Script = reader.ReadString();
					break;
				case "lang":
					m.Language = reader.ReadString();
					break;
				case "failed_shards":
					m.FailedShards = ExtractFailedShards(ref reader, formatterResolver);
					break;
				default: return false;
			}
			return true;
		}

		protected static IReadOnlyCollection<ShardFailure> ExtractFailedShards(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			//reader.ReadNext();
			if (reader.GetCurrentJsonToken() != JsonToken.BeginArray)
				return EmptyReadOnly<ShardFailure>.Collection;

			var formatter = formatterResolver.GetFormatter<ReadOnlyCollection<ShardFailure>>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}

		private static IReadOnlyCollection<string> ReadArray(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var a = Array.Empty<string>();
			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
				case JsonToken.String:
					a = new[] { reader.ReadString() };
					break;
				case JsonToken.BeginArray:
					a = formatterResolver.GetFormatter<string[]>().Deserialize(ref reader, formatterResolver);
					break;
			}
			return new ReadOnlyCollection<string>(a);
		}

		private delegate void ReadMore<in TInnerError>(ref JsonReader reader, TInnerError e, string prop, IJsonFormatterResolver formatterResolver)
			where TInnerError : ErrorCause, new();
	}
}
