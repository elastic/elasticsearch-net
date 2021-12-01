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
	internal sealed class TermsSetQueryConverter : FieldNameQueryConverterBase<TermsSetQuery>
	{
		internal override TermsSetQuery ReadInternal(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Unexpected JSON detected.");
			var variant = new TermsSetQuery();
			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					var property = reader.GetString();
					if (property == "minimum_should_match_field")
					{
						variant.MinimumShouldMatchField = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Field?>(ref reader, options);
						continue;
					}

					if (property == "minimum_should_match_script")
					{
						variant.MinimumShouldMatchScript = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Script?>(ref reader, options);
						continue;
					}

					if (property == "terms")
					{
						variant.Terms = JsonSerializer.Deserialize<IEnumerable<string>>(ref reader, options);
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

		internal override void WriteInternal(Utf8JsonWriter writer, TermsSetQuery value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			if (value.MinimumShouldMatchField is not null)
			{
				writer.WritePropertyName("minimum_should_match_field");
				JsonSerializer.Serialize(writer, value.MinimumShouldMatchField, options);
			}

			if (value.MinimumShouldMatchScript is not null)
			{
				writer.WritePropertyName("minimum_should_match_script");
				JsonSerializer.Serialize(writer, value.MinimumShouldMatchScript, options);
			}

			writer.WritePropertyName("terms");
			JsonSerializer.Serialize(writer, value.Terms, options);
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

	[JsonConverter(typeof(TermsSetQueryConverter))]
	public partial class TermsSetQuery : FieldNameQueryBase, IQueryContainerVariant
	{
		[JsonIgnore]
		string QueryDsl.IQueryContainerVariant.QueryContainerVariantName => "terms_set";
		[JsonInclude]
		[JsonPropertyName("minimum_should_match_field")]
		public Elastic.Clients.Elasticsearch.Field? MinimumShouldMatchField { get; set; }

		[JsonInclude]
		[JsonPropertyName("minimum_should_match_script")]
		public Elastic.Clients.Elasticsearch.Script? MinimumShouldMatchScript { get; set; }

		[JsonInclude]
		[JsonPropertyName("terms")]
		public IEnumerable<string> Terms { get; set; }
	}

	public sealed partial class TermsSetQueryDescriptor<T> : FieldNameQueryDescriptorBase<TermsSetQueryDescriptor<T>, T>
	{
		public TermsSetQueryDescriptor()
		{
		}

		internal TermsSetQueryDescriptor(Action<TermsSetQueryDescriptor<T>> configure) => configure.Invoke(this);
		internal Elastic.Clients.Elasticsearch.Field? MinimumShouldMatchFieldValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Script? MinimumShouldMatchScriptValue { get; private set; }

		internal IEnumerable<string> TermsValue { get; private set; }

		internal float? BoostValue { get; private set; }

		internal string? QueryNameValue { get; private set; }

		public TermsSetQueryDescriptor<T> MinimumShouldMatchField(Elastic.Clients.Elasticsearch.Field? minimumShouldMatchField) => Assign(minimumShouldMatchField, (a, v) => a.MinimumShouldMatchFieldValue = v);
		public TermsSetQueryDescriptor<T> MinimumShouldMatchField<TValue>(Expression<Func<T, TValue>> minimumShouldMatchField) => Assign(minimumShouldMatchField, (a, v) => a.MinimumShouldMatchFieldValue = v);
		public TermsSetQueryDescriptor<T> MinimumShouldMatchScript(Elastic.Clients.Elasticsearch.Script? minimumShouldMatchScript) => Assign(minimumShouldMatchScript, (a, v) => a.MinimumShouldMatchScriptValue = v);
		public TermsSetQueryDescriptor<T> Terms(IEnumerable<string> terms) => Assign(terms, (a, v) => a.TermsValue = v);
		public TermsSetQueryDescriptor<T> Boost(float? boost) => Assign(boost, (a, v) => a.BoostValue = v);
		public TermsSetQueryDescriptor<T> QueryName(string? queryName) => Assign(queryName, (a, v) => a.QueryNameValue = v);
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WritePropertyName(settings.Inferrer.Field(_field));
			writer.WriteStartObject();
			if (MinimumShouldMatchFieldValue is not null)
			{
				writer.WritePropertyName("minimum_should_match_field");
				JsonSerializer.Serialize(writer, MinimumShouldMatchFieldValue, options);
			}

			if (MinimumShouldMatchScriptValue is not null)
			{
				writer.WritePropertyName("minimum_should_match_script");
				JsonSerializer.Serialize(writer, MinimumShouldMatchScriptValue, options);
			}

			writer.WritePropertyName("terms");
			JsonSerializer.Serialize(writer, TermsValue, options);
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