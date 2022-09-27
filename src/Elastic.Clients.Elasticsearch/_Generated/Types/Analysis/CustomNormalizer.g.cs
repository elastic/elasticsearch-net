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
namespace Elastic.Clients.Elasticsearch.Analysis
{
	public sealed partial class CustomNormalizer
	{
		[JsonInclude]
		[JsonPropertyName("char_filter")]
		public IEnumerable<string>? CharFilter { get; set; }

		[JsonInclude]
		[JsonPropertyName("filter")]
		public IEnumerable<string>? Filter { get; set; }

		[JsonInclude]
		[JsonPropertyName("type")]
		public string Type => "custom";
	}

	public sealed partial class CustomNormalizerDescriptor : SerializableDescriptorBase<CustomNormalizerDescriptor>, IBuildableDescriptor<CustomNormalizer>
	{
		internal CustomNormalizerDescriptor(Action<CustomNormalizerDescriptor> configure) => configure.Invoke(this);
		public CustomNormalizerDescriptor() : base()
		{
		}

		private IEnumerable<string>? CharFilterValue { get; set; }

		private IEnumerable<string>? FilterValue { get; set; }

		public CustomNormalizerDescriptor CharFilter(IEnumerable<string>? charFilter)
		{
			CharFilterValue = charFilter;
			return Self;
		}

		public CustomNormalizerDescriptor Filter(IEnumerable<string>? filter)
		{
			FilterValue = filter;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (CharFilterValue is not null)
			{
				writer.WritePropertyName("char_filter");
				JsonSerializer.Serialize(writer, CharFilterValue, options);
			}

			if (FilterValue is not null)
			{
				writer.WritePropertyName("filter");
				JsonSerializer.Serialize(writer, FilterValue, options);
			}

			writer.WritePropertyName("type");
			writer.WriteStringValue("custom");
			writer.WriteEndObject();
		}

		CustomNormalizer IBuildableDescriptor<CustomNormalizer>.Build() => new()
		{ CharFilter = CharFilterValue, Filter = FilterValue };
	}
}