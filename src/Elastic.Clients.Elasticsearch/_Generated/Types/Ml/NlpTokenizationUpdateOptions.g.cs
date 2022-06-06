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
namespace Elastic.Clients.Elasticsearch.Ml
{
	public partial class NlpTokenizationUpdateOptions
	{
		[JsonInclude]
		[JsonPropertyName("span")]
		public int? Span { get; set; }

		[JsonInclude]
		[JsonPropertyName("truncate")]
		public Elastic.Clients.Elasticsearch.Ml.TokenizationTruncate? Truncate { get; set; }
	}

	public sealed partial class NlpTokenizationUpdateOptionsDescriptor : SerializableDescriptorBase<NlpTokenizationUpdateOptionsDescriptor>
	{
		internal NlpTokenizationUpdateOptionsDescriptor(Action<NlpTokenizationUpdateOptionsDescriptor> configure) => configure.Invoke(this);
		public NlpTokenizationUpdateOptionsDescriptor() : base()
		{
		}

		private int? SpanValue { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.TokenizationTruncate? TruncateValue { get; set; }

		public NlpTokenizationUpdateOptionsDescriptor Span(int? span)
		{
			SpanValue = span;
			return Self;
		}

		public NlpTokenizationUpdateOptionsDescriptor Truncate(Elastic.Clients.Elasticsearch.Ml.TokenizationTruncate? truncate)
		{
			TruncateValue = truncate;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (SpanValue.HasValue)
			{
				writer.WritePropertyName("span");
				writer.WriteNumberValue(SpanValue.Value);
			}

			if (TruncateValue is not null)
			{
				writer.WritePropertyName("truncate");
				JsonSerializer.Serialize(writer, TruncateValue, options);
			}

			writer.WriteEndObject();
		}
	}
}