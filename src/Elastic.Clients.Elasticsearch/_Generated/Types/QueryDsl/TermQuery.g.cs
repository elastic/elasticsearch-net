// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.QueryDsl
{
	internal sealed class TermQueryConverter : FieldNameQueryConverterBase<TermQuery>
	{
		internal override TermQuery ReadInternal(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Unexpected JSON detected.");
			var variant = new TermQuery();
			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					var property = reader.GetString();
					if (property == "value")
					{
						variant.Value = JsonSerializer.Deserialize<object>(ref reader, options);
						continue;
					}

					if (property == "case_insensitive")
					{
						variant.CaseInsensitive = JsonSerializer.Deserialize<bool?>(ref reader, options);
						continue;
					}

					if (property == "boost")
					{
						variant.Boost = JsonSerializer.Deserialize<float?>(ref reader, options);
						continue;
					}

					if (property == "_name")
					{
						variant.QueryName = JsonSerializer.Deserialize<string?>(ref reader, options);
						continue;
					}
				}
			}

			return variant;
		}

		internal override void WriteInternal(Utf8JsonWriter writer, TermQuery value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("value");
			JsonSerializer.Serialize(writer, value.Value, options);
			if (value.CaseInsensitive.HasValue)
			{
				writer.WritePropertyName("case_insensitive");
				writer.WriteBooleanValue(value.CaseInsensitive.Value);
			}

			if (value.Boost.HasValue)
			{
				writer.WritePropertyName("boost");
				writer.WriteNumberValue(value.Boost.Value);
			}

			if (!string.IsNullOrEmpty(value.QueryName))
			{
				writer.WritePropertyName("_name");
				writer.WriteStringValue(value.QueryName);
			}

			writer.WriteEndObject();
		}
	}

	[JsonConverter(typeof(TermQueryConverter))]
	public partial class TermQuery : FieldNameQueryBase, IQueryContainerVariant
	{
		[JsonIgnore]
		string QueryDsl.IQueryContainerVariant.QueryContainerVariantName => "term";
		[JsonInclude]
		[JsonPropertyName("value")]
		public object Value { get; set; }

		[JsonInclude]
		[JsonPropertyName("case_insensitive")]
		public bool? CaseInsensitive { get; set; }
	}

	public sealed partial class TermQueryDescriptor<T> : FieldNameQueryDescriptorBase<TermQueryDescriptor<T>, T>
	{
		public TermQueryDescriptor()
		{
		}

		internal TermQueryDescriptor(Action<TermQueryDescriptor<T>> configure) => configure.Invoke(this);
		internal object ValueValue { get; private set; }

		internal bool? CaseInsensitiveValue { get; private set; }

		internal float? BoostValue { get; private set; }

		internal string? QueryNameValue { get; private set; }

		public TermQueryDescriptor<T> Value(object value) => Assign(value, (a, v) => a.ValueValue = v);
		public TermQueryDescriptor<T> CaseInsensitive(bool? caseInsensitive = true) => Assign(caseInsensitive, (a, v) => a.CaseInsensitiveValue = v);
		public TermQueryDescriptor<T> Boost(float? boost) => Assign(boost, (a, v) => a.BoostValue = v);
		public TermQueryDescriptor<T> QueryName(string? queryName) => Assign(queryName, (a, v) => a.QueryNameValue = v);
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WritePropertyName(settings.Inferrer.Field(_field));
			writer.WriteStartObject();
			writer.WritePropertyName("value");
			JsonSerializer.Serialize(writer, ValueValue, options);
			if (CaseInsensitiveValue.HasValue)
			{
				writer.WritePropertyName("case_insensitive");
				writer.WriteBooleanValue(CaseInsensitiveValue.Value);
			}

			if (BoostValue.HasValue)
			{
				writer.WritePropertyName("boost");
				writer.WriteNumberValue(BoostValue.Value);
			}

			if (!string.IsNullOrEmpty(QueryNameValue))
			{
				writer.WritePropertyName("_name");
				writer.WriteStringValue(QueryNameValue);
			}

			writer.WriteEndObject();
		}
	}
}