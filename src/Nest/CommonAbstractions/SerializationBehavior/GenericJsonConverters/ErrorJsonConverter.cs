using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class ErrorJsonConverter : ErrorJsonConverter<Error>
	{
		protected override bool ReadProperty(Error error, string propertyName, JsonReader reader, JsonSerializer serializer)
		{
			if (propertyName != "root_cause") return false;

			reader.Read();
			if (reader.TokenType != JsonToken.StartArray)
				return false;

			var rootCauses = new List<ErrorCause>();
			var depth = reader.Depth;
			do
			{
				var rootCause = ReadCausedBy(reader, serializer);
				rootCauses.Add(rootCause);
			} while (reader.Depth >= depth && reader.TokenType != JsonToken.EndArray);
			error.RootCause = rootCauses;
			return true;
		}
	}

	internal class BulkErrorJsonConverter : ErrorJsonConverter<BulkError>
	{
		protected override bool ReadProperty(BulkError error, string propertyName, JsonReader reader, JsonSerializer serializer)
		{
			switch (propertyName)
			{
				case "index":
					error.Index = reader.ReadAsString();
					return true;
				case "shard":
					error.Shard = reader.ReadAsInt32().GetValueOrDefault();
					return true;
				default:
					return false;
			}
		}
	}

	internal class ErrorJsonConverter<TError> : JsonConverter
		where TError : ErrorCause, new()
	{
		public override bool CanRead => true;
		public override bool CanWrite => false;
		public override bool CanConvert(Type objectType) => objectType == typeof(Error);

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return ReadError<TError>(reader, serializer, (e, prop) =>
			{
				if (!this.ReadProperty(e, prop, reader, serializer))
					reader.Skip();
			});
		}

		protected virtual bool ReadProperty(TError error, string propertyName, JsonReader reader, JsonSerializer serializer)
		{
			return false;
		}

		protected ErrorCause ReadCausedBy(JsonReader reader, JsonSerializer serializer)
		{
			return ReadError<ErrorCause>(reader, serializer, (e, prop) =>
			{
				reader.Skip();
			});
		}

		private TInnerError ReadError<TInnerError>(JsonReader reader, JsonSerializer serializer, Action<TInnerError, string> readMore)
			where TInnerError : ErrorCause, new()
		{
			if (reader.TokenType != JsonToken.StartObject)
			{
				reader.Skip();
				return null;
			}

			var error = new TInnerError();
			var depth = reader.Depth;
			reader.Read();
			do
			{
				var propertyName = (string)reader.Value;
				switch (propertyName)
				{
					case "type":
						error.Type = reader.ReadAsString();
						break;
					case "reason":
						error.Reason = reader.ReadAsString();
						break;
					case "caused_by":
						reader.Read();
						error.CausedBy = ReadCausedBy(reader, serializer);
						break;
					default:
						readMore(error, propertyName);
						break;
				}
				reader.Read();
			} while (reader.Depth >= depth && reader.TokenType != JsonToken.EndObject);
			return error;
		}

}
}
