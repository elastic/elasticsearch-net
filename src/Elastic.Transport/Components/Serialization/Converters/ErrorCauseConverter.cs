// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Transport.Extensions;

namespace Elastic.Transport
{
	internal class ErrorCauseConverter : ErrorCauseConverter<ErrorCause> { }

	internal class ErrorConverter : ErrorCauseConverter<Error>
	{
		protected override bool ReadMore(ref Utf8JsonReader reader, JsonSerializerOptions options, string propertyName, Error errorCause)
		{
			void ReadAssign<T>(ref Utf8JsonReader r, Action<Error, T> set) =>
				set(errorCause, JsonSerializer.Deserialize<T>(ref r, options));
			switch (propertyName)
			{
				case "headers":
					ReadAssign<Dictionary<string, string>>(ref reader, (e, v) => e.Headers = v);
					return true;

				case "root_cause":
					ReadAssign<IReadOnlyCollection<ErrorCause>>(ref reader, (e, v) => e.RootCause = v);
					return true;
				default:
					return false;

			}
		}
	}

	internal class ErrorCauseConverter<TErrorCause> : JsonConverter<TErrorCause> where TErrorCause : ErrorCause, new()
	{
		public override TErrorCause Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				return reader.TokenType == JsonTokenType.String
					? new TErrorCause { Reason = reader.GetString() }
					: null;

			var errorCause = new TErrorCause();
			var additionalProperties = new Dictionary<string, object>();
			errorCause.AdditionalProperties = additionalProperties;

			void ReadAssign<T>(ref Utf8JsonReader r, Action<ErrorCause, T> set) =>
				set(errorCause, JsonSerializer.Deserialize<T>(ref r, options));
			void ReadAny(ref Utf8JsonReader r, string property, Action<ErrorCause, string, object> set) =>
				set(errorCause, property, JsonSerializer.Deserialize<object>(ref r, options));

			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndObject) return errorCause;

				if (reader.TokenType != JsonTokenType.PropertyName) throw new JsonException();

				var propertyName = reader.GetString();
				switch (propertyName)
				{
					case "bytes_limit":
						ReadAssign<int?>(ref reader, (e, v) => e.BytesLimit = v);
						break;
					case "bytes_wanted":
						ReadAssign<int?>(ref reader, (e, v) => e.BytesWanted = v);
						break;
					case "caused_by":
						ReadAssign<ErrorCause>(ref reader, (e, v) => e.CausedBy = v);
						break;
					case "col":
						ReadAssign<int?>(ref reader, (e, v) => e.Column = v);
						break;
					case "failed_shards":
						ReadAssign<IReadOnlyCollection<ShardFailure>>(ref reader, (e, v) => e.FailedShards = v);
						break;
					case "grouped":
						ReadAssign<bool?>(ref reader, (e, v) => e.Grouped = v);
						break;
					case "index":
						ReadAssign<string>(ref reader, (e, v) => e.Index = v);
						break;
					case "index_uuid":
						ReadAssign<string>(ref reader, (e, v) => e.IndexUUID = v);
						break;
					case "lang":
						ReadAssign<string>(ref reader, (e, v) => e.Language = v);
						break;

					case "license.expired.feature":
						ReadAssign<string>(ref reader, (e, v) => e.LicensedExpiredFeature = v);
						break;
					case "line":
						ReadAssign<int?>(ref reader, (e, v) => e.Line = v);
						break;
					case "phase":
						ReadAssign<string>(ref reader, (e, v) => e.Phase = v);
						break;
					case "reason":
						ReadAssign<string>(ref reader, (e, v) => e.Reason = v);
						break;
					case "resource.id":
						errorCause.ResourceId = ReadSingleOrCollection(ref reader, options);
						break;
					case "resource.type":
						ReadAssign<string>(ref reader, (e, v) => e.ResourceType = v);
						break;
					case "script":
						ReadAssign<string>(ref reader, (e, v) => e.Script = v);
						break;
					case "script_stack":
						errorCause.ScriptStack = ReadSingleOrCollection(ref reader, options);
						break;
					case "shard":
						errorCause.Shard = ReadIntFromString(ref reader, options);
						break;
					case "stack_trace":
						ReadAssign<string>(ref reader, (e, v) => e.StackTrace = v);
						break;
					case "type":
						ReadAssign<string>(ref reader, (e, v) => e.Type = v);
						break;
					default:
						if (ReadMore(ref reader, options, propertyName, errorCause)) break;
						else
						{
							ReadAny(ref reader, propertyName, (e, p, v) => additionalProperties.Add(p, v));
							break;
						}
				}
			}
			return errorCause;
		}


		private static int? ReadIntFromString(ref Utf8JsonReader reader, JsonSerializerOptions options)
		{
			reader.Read();
			switch (reader.TokenType)
			{
				case JsonTokenType.Null: return null;
				case JsonTokenType.Number:
					return JsonSerializer.Deserialize<int?>(ref reader, options);
				case JsonTokenType.String:
					var s = JsonSerializer.Deserialize<string>(ref reader, options);
					if (int.TryParse(s, out var i)) return i;
					return null;
				default:
					reader.TrySkip();
					return null;
			}
		}

		private static IReadOnlyCollection<string> ReadSingleOrCollection(ref Utf8JsonReader reader, JsonSerializerOptions options)
		{
			reader.Read();
			switch (reader.TokenType)
			{
				case JsonTokenType.Null: return EmptyReadOnly<string>.Collection;
				case JsonTokenType.StartArray:
					var list = new List<string>();
					while (reader.Read())
					{
						if (reader.TokenType == JsonTokenType.EndArray)
							break;
						list.Add(JsonSerializer.Deserialize<string>(ref reader, options));
					}
					return new ReadOnlyCollection<string>(list);
				default:
					var v = JsonSerializer.Deserialize<string>(ref reader, options);
					return new ReadOnlyCollection<string>(new List<string>(1) { v});
			}
		}

		protected virtual bool ReadMore(ref Utf8JsonReader reader, JsonSerializerOptions options, string propertyName, TErrorCause errorCause) => false;

		public override void Write(Utf8JsonWriter writer, TErrorCause value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();

			static void Serialize<T>(Utf8JsonWriter writer, JsonSerializerOptions options, string name, T value)
			{
				if (value == null) return;

				writer.WritePropertyName(name);
				JsonSerializer.Serialize(writer, value, options);
			}

			Serialize(writer, options, "bytes_limit", value.BytesLimit);
			Serialize(writer, options, "bytes_wanted", value.BytesWanted);
			Serialize(writer, options, "caused_by", value.CausedBy);
			Serialize(writer, options, "col", value.Column);
			Serialize(writer, options, "failed_shards", value.FailedShards);
			Serialize(writer, options, "grouped", value.Grouped);
			Serialize(writer, options, "index", value.Index);
			Serialize(writer, options, "index_uuid", value.IndexUUID);
			Serialize(writer, options, "lang", value.Language);
			Serialize(writer, options, "license.expired.feature", value.LicensedExpiredFeature);
			Serialize(writer, options, "line", value.Line);
			Serialize(writer, options, "phase", value.Phase);
			Serialize(writer, options, "reason", value.Reason);
			Serialize(writer, options, "resource.id", value.ResourceId);
			Serialize(writer, options, "resource.type", value.ResourceType);
			Serialize(writer, options, "script", value.Script);
			Serialize(writer, options, "script_stack", value.ScriptStack);
			Serialize(writer, options, "shard", value.Shard);
			Serialize(writer, options, "stack_trace", value.StackTrace);
			Serialize(writer, options, "type", value.Type);

			foreach (var kv in value.AdditionalProperties)
				Serialize(writer, options, kv.Key, kv.Value);
			writer.WriteEndObject();
		}
	}
}
