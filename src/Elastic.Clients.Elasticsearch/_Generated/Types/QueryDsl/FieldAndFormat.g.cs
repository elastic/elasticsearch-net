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
	public partial class FieldAndFormat
	{
		[JsonInclude]
		[JsonPropertyName("field")]
		public Elastic.Clients.Elasticsearch.Field Field { get; set; }

		[JsonInclude]
		[JsonPropertyName("format")]
		public string? Format { get; set; }

		[JsonInclude]
		[JsonPropertyName("include_unmapped")]
		public bool? IncludeUnmapped { get; set; }
	}

	public sealed partial class FieldAndFormatDescriptor<TDocument> : DescriptorBase<FieldAndFormatDescriptor<TDocument>>
	{
		public FieldAndFormatDescriptor()
		{
		}

		internal FieldAndFormatDescriptor(Action<FieldAndFormatDescriptor<TDocument>> configure) => configure.Invoke(this);
		internal Elastic.Clients.Elasticsearch.Field FieldValue { get; private set; }

		internal string? FormatValue { get; private set; }

		internal bool? IncludeUnmappedValue { get; private set; }

		public FieldAndFormatDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field) => Assign(field, (a, v) => a.FieldValue = v);
		public FieldAndFormatDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field) => Assign(field, (a, v) => a.FieldValue = v);
		public FieldAndFormatDescriptor<TDocument> Format(string? format) => Assign(format, (a, v) => a.FormatValue = v);
		public FieldAndFormatDescriptor<TDocument> IncludeUnmapped(bool? includeUnmapped = true) => Assign(includeUnmapped, (a, v) => a.IncludeUnmappedValue = v);
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
			if (!string.IsNullOrEmpty(FormatValue))
			{
				writer.WritePropertyName("format");
				writer.WriteStringValue(FormatValue);
			}

			if (IncludeUnmappedValue.HasValue)
			{
				writer.WritePropertyName("include_unmapped");
				writer.WriteBooleanValue(IncludeUnmappedValue.Value);
			}

			writer.WriteEndObject();
		}
	}
}