using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class ErrorCauseJsonConverter : ErrorCauseJsonConverter<ErrorCause> { }

	internal class ErrorCauseJsonConverter<TErrorCause> : JsonConverter
		where TErrorCause : ErrorCause, new()
	{
		public override bool CanRead => true;
		public override bool CanWrite => false;
		public override bool CanConvert(Type objectType) => objectType == typeof(Error);

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) { }

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
			ReadError<TErrorCause>(reader, serializer, (e, prop) =>
			{
				if (!this.ReadProperty(e, prop, reader, serializer))
					reader.Skip();
			});

		protected virtual bool ReadProperty(TErrorCause error, string propertyName, JsonReader reader, JsonSerializer serializer) => false;

		protected ErrorCause ReadCausedBy(JsonReader reader, JsonSerializer serializer) =>
			ReadError<ErrorCause>(reader, serializer, (e, prop) => { reader.Skip(); });

		private TInnerError ReadError<TInnerError>(JsonReader reader, JsonSerializer serializer, Action<TInnerError, string> readMore)
			where TInnerError : ErrorCause, new()
		{
			if (reader.TokenType == JsonToken.String)
			{
				var reason = (string) reader.Value;
				return new TInnerError { Reason = reason };
			}
			if (reader.TokenType != JsonToken.StartObject)
			{
				reader.Skip();
				return null;
			}

			var error = new TInnerError { Metadata = new ErrorCause.ErrorCauseMetadata() };
			var depth = reader.Depth;
			reader.Read();
			do
			{
				var propertyName = (string) reader.Value;
				switch (propertyName)
				{
					case "type":
						error.Type = reader.ReadAsString();
						break;
					case "stack_trace":
						error.StackTrace = reader.ReadAsString();
						break;
					case "reason":
						error.Reason = reader.ReadAsString();
						break;
					case "caused_by":
						reader.Read();
						error.CausedBy = ReadCausedBy(reader, serializer);
						break;
					default:
						if (!ExtractMetadata(propertyName, error, reader, serializer))
							readMore(error, propertyName);
						break;
				}
				reader.Read();
			} while (reader.Depth >= depth && reader.TokenType != JsonToken.EndObject);
			return error;
		}

		protected static bool ExtractMetadata(string propertyName, ErrorCause error, JsonReader reader, JsonSerializer serializer)
		{
			var m = error.Metadata;
			switch (propertyName)
			{
				case "license.expired.feature":
					m.LicensedExpiredFeature = reader.ReadAsString();
					break;
				case "index":
					m.Index = reader.ReadAsString();
					break;
				case "index_uuid":
					m.IndexUUID = reader.ReadAsString();
					break;
				case "resource.type":
					m.ResourceType = reader.ReadAsString();
					break;
				case "resource.id":
					m.ResourceId = ReadArray(reader);
					break;
				case "shard":
					m.Shard = reader.ReadAsInt32();
					break;
				case "line":
					m.Line = reader.ReadAsInt32();
					break;
				case "col":
					m.Column = reader.ReadAsInt32();
					break;
				case "bytes_wanted":
					reader.Read();
					m.BytesWanted = reader.Value as long?;
					break;
				case "bytes_limit":
					reader.Read();
					m.BytesLimit = reader.Value as long?;
					break;
				case "phase":
					m.Phase = reader.ReadAsString();
					break;
				case "grouped":
					m.Grouped = reader.ReadAsBoolean();
					break;
				case "script_stack":
					m.ScriptStack = ReadArray(reader);
					break;
				case "script":
					m.Script = reader.ReadAsString();
					break;
				case "lang":
					m.Language = reader.ReadAsString();
					break;
				case "failed_shards":
					m.FailedShards = ExtractFailedShards(reader, serializer);
					break;
				default: return false;
			}
			return true;
		}

		protected static IReadOnlyCollection<ShardFailure> ExtractFailedShards(JsonReader reader, JsonSerializer serializer)
		{
			reader.Read();
			if (reader.TokenType != JsonToken.StartArray) return EmptyReadOnly<ShardFailure>.Collection;
			var shardFailures = serializer.Deserialize<List<ShardFailure>>(reader);
			return new ReadOnlyCollection<ShardFailure>(shardFailures);
		}

		private static IReadOnlyCollection<string> ReadArray(JsonReader reader)
		{
			var a = new string[0] { };
			reader.Read();
			if (reader.TokenType == JsonToken.String)
				a = new[] {(reader.Value as string)};
			else if (reader.TokenType == JsonToken.StartArray)
				a = JArray.Load(reader).ToObject<List<string>>().ToArray();
			return new ReadOnlyCollection<string>(a);
		}
	}
}
