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
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.QueryDsl
{
	internal sealed class SpanTermQueryConverter : FieldNameQueryConverterBase<SpanTermQuery>
	{
		internal override SpanTermQuery ReadInternal(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
		internal override void WriteInternal(Utf8JsonWriter writer, SpanTermQuery value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("value");
			writer.WriteStringValue(value.Value);
			writer.WriteEndObject();
		}
	}

	[JsonConverter(typeof(SpanTermQueryConverter))]
	public partial class SpanTermQuery : FieldNameQueryBase, IQueryContainerVariant, ISpanQueryVariant
	{
		[JsonIgnore]
		string QueryDsl.IQueryContainerVariant.QueryContainerVariantName => "span_term";
		[JsonIgnore]
		string QueryDsl.ISpanQueryVariant.SpanQueryVariantName => "span_term";
		[JsonInclude]
		[JsonPropertyName("value")]
		public string Value { get; set; }
	}

	public sealed partial class SpanTermQueryDescriptor<T> : FieldNameQueryDescriptorBase<SpanTermQueryDescriptor<T>, T>
	{
		public SpanTermQueryDescriptor()
		{
		}

		internal SpanTermQueryDescriptor(Action<SpanTermQueryDescriptor<T>> configure) => configure.Invoke(this);
		internal string ValueValue { get; private set; }

		public SpanTermQueryDescriptor<T> Value(string value) => Assign(value, (a, v) => a.ValueValue = v);
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WritePropertyName(settings.Inferrer.Field(_field));
			writer.WriteStartObject();
			writer.WritePropertyName("value");
			writer.WriteStringValue(ValueValue);
			writer.WriteEndObject();
		}
	}
}