// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch.Failures;

namespace Nest.Utf8Json
{
	internal class ErrorCauseFormatter : ErrorCauseFormatter<ErrorCause> {}

	internal class ErrorCauseFormatter<TErrorCause> : IJsonFormatter<TErrorCause>
		where TErrorCause : ErrorCause, new()
	{
		protected virtual bool Deserialize(ref JsonReader reader, ref ArraySegment<byte> property, TErrorCause value,
			IJsonFormatterResolver formatterResolver
		) => false;

		protected virtual void Serialize(ref JsonWriter writer, ref int count, TErrorCause value, IJsonFormatterResolver formatterResolver) { }

		public TErrorCause Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
				case JsonToken.String:
					return new TErrorCause { Reason = reader.ReadString() };
				case JsonToken.BeginObject:
					var count = 0;
					var errorCause = new TErrorCause();
					var additionalProperties = new Dictionary<string, object>();
					errorCause.AdditionalProperties = additionalProperties;
					var formatter = formatterResolver.GetFormatter<object>();
					while (reader.ReadIsInObject(ref count))
					{
						var property = reader.ReadPropertyNameSegmentRaw();
						if (ErrorCauseFormatterStatics.Fields.TryGetValue(property, out var value))
						{
							switch (value)
							{
								case 0:
									errorCause.BytesLimit = reader.ReadInt64();
									break;
								case 1:
									errorCause.BytesWanted = reader.ReadInt64();
									break;
								case 2:
									errorCause.CausedBy =
										ErrorCauseFormatterStatics.ErrorCausePropertyFormatter.Deserialize(ref reader, formatterResolver);
									break;
								case 3:
									errorCause.Column = reader.ReadInt32();
									break;
								case 4:
									errorCause.FailedShards = formatterResolver.GetFormatter<List<ShardFailure>>()
										.Deserialize(ref reader, formatterResolver);
									break;
								case 5:
									errorCause.Grouped = reader.ReadBoolean();
									break;
								case 6:
									errorCause.Index = reader.ReadString();
									break;
								case 7:
									errorCause.IndexUUID = reader.ReadString();
									break;
								case 8:
									errorCause.Language = reader.ReadString();
									break;
								case 9:
									errorCause.LicensedExpiredFeature = reader.ReadString();
									break;
								case 10:
									errorCause.Line = reader.ReadInt32();
									break;
								case 11:
									errorCause.Phase = reader.ReadString();
									break;
								case 12:
									errorCause.Reason = reader.ReadString();
									break;
								case 13:
									errorCause.ResourceId =
										ErrorCauseFormatterStatics.SingleOrEnumerableFormatter.Deserialize(ref reader, formatterResolver);
									break;
								case 14:
									errorCause.ResourceType = reader.ReadString();
									break;
								case 15:
									errorCause.Script = reader.ReadString();
									break;
								case 16:
									errorCause.ScriptStack =
										ErrorCauseFormatterStatics.SingleOrEnumerableFormatter.Deserialize(ref reader, formatterResolver);
									break;
								case 17:
									errorCause.Shard = ErrorCauseFormatterStatics.ShardFormatter.Deserialize(ref reader, formatterResolver);
									break;
								case 18:
									errorCause.StackTrace = reader.ReadString();
									break;
								case 19:
									errorCause.Type = reader.ReadString();
									break;
							}
						}
						else
						{
							if (!Deserialize(ref reader, ref property, errorCause, formatterResolver))
								additionalProperties.Add(property.Utf8String(), formatter.Deserialize(ref reader, formatterResolver));
						}
					}

					return errorCause;
				default:
					reader.ReadNextBlock();
					return null;
			}
		}

		public void Serialize(ref JsonWriter writer, TErrorCause value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBeginObject();
			var count = 0;

			if (value.BytesLimit.HasValue)
			{
				writer.WritePropertyName("bytes_limit");
				writer.WriteInt64(value.BytesLimit.Value);
				count++;
			}

			if (value.BytesWanted.HasValue)
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName("bytes_wanted");
				writer.WriteInt64(value.BytesWanted.Value);
				count++;
			}

			if (value.CausedBy != null)
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName("caused_by");
				ErrorCauseFormatterStatics.ErrorCausePropertyFormatter.Serialize(ref writer, value.CausedBy, formatterResolver);
				count++;
			}

			if (value.Column.HasValue)
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName("col");
				writer.WriteInt32(value.Column.Value);
				count++;
			}

			if (value.FailedShards.Any())
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName("failed_shards");
				formatterResolver.GetFormatter<IReadOnlyCollection<ShardFailure>>()
					.Serialize(ref writer, value.FailedShards, formatterResolver);
				count++;
			}

			if (value.Grouped.HasValue)
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName("grouped");
				writer.WriteBoolean(value.Grouped.Value);
				count++;
			}

			if (value.Index != null)
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName("index");
				writer.WriteString(value.Index);
				count++;
			}

			if (value.IndexUUID != null)
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName("index_uuid");
				writer.WriteString(value.IndexUUID);
				count++;
			}

			if (value.Language != null)
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName("lang");
				writer.WriteString(value.Language);
				count++;
			}

			if (value.LicensedExpiredFeature != null)
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName("license.expired.feature");
				writer.WriteString(value.LicensedExpiredFeature);
				count++;
			}

			if (value.Line.HasValue)
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName("line");
				writer.WriteInt32(value.Line.Value);
				count++;
			}

			if (value.Phase != null)
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName("phase");
				writer.WriteString(value.Phase);
				count++;
			}

			if (value.Reason != null)
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName("reason");
				writer.WriteString(value.Reason);
				count++;
			}

			if (value.ResourceId.Any())
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName("resource.id");
				ErrorCauseFormatterStatics.SingleOrEnumerableFormatter.Serialize(ref writer, value.ResourceId, formatterResolver);
				count++;
			}

			if (value.ResourceType != null)
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName("resource.type");
				writer.WriteString(value.ResourceType);
				count++;
			}

			if (value.Script != null)
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName("script");
				writer.WriteString(value.Script);
				count++;
			}

			if (value.ScriptStack.Any())
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName("script_stack");
				ErrorCauseFormatterStatics.SingleOrEnumerableFormatter.Serialize(ref writer, value.ScriptStack, formatterResolver);
				count++;
			}

			if (value.Shard.HasValue)
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName("shard");
				writer.WriteInt32(value.Shard.Value);
				count++;
			}

			if (value.StackTrace != null)
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName("stack_trace");
				writer.WriteString(value.StackTrace);
				count++;
			}

			if (value.Type != null)
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName("type");
				writer.WriteString(value.Type);
				count++;
			}

			Serialize(ref writer, ref count, value, formatterResolver);

			if (value.AdditionalProperties.Any())
			{
				var formatter = formatterResolver.GetFormatter<object>();
				foreach (var additionalProperty in value.AdditionalProperties)
				{
					if (count > 0)
						writer.WriteValueSeparator();

					writer.WritePropertyName(additionalProperty.Key);
					formatter.Serialize(ref writer, additionalProperty.Value, formatterResolver);
					count++;
				}
			}

			writer.WriteEndObject();
		}
	}
}
