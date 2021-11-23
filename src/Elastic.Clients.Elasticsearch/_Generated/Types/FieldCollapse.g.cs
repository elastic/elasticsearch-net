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
namespace Elastic.Clients.Elasticsearch
{
	public partial class FieldCollapse
	{
		[JsonInclude]
		[JsonPropertyName("field")]
		public string Field { get; set; }

		[JsonInclude]
		[JsonPropertyName("inner_hits")]
		public Elastic.Clients.Elasticsearch.InnerHits? InnerHits { get; set; }

		[JsonInclude]
		[JsonPropertyName("max_concurrent_group_searches")]
		public int? MaxConcurrentGroupSearches { get; set; }
	}

	public sealed partial class FieldCollapseDescriptor<T> : DescriptorBase<FieldCollapseDescriptor<T>>
	{
		public FieldCollapseDescriptor()
		{
		}

		internal FieldCollapseDescriptor(Action<FieldCollapseDescriptor<T>> configure) => configure.Invoke(this);
		internal string FieldValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.InnerHits? InnerHitsValue { get; private set; }

		internal int? MaxConcurrentGroupSearchesValue { get; private set; }

		internal InnerHitsDescriptor<T> InnerHitsDescriptor { get; private set; }

		internal Action<InnerHitsDescriptor<T>> InnerHitsDescriptorAction { get; private set; }

		public FieldCollapseDescriptor<T> Field(string field) => Assign(field, (a, v) => a.FieldValue = v);
		public FieldCollapseDescriptor<T> InnerHits(Elastic.Clients.Elasticsearch.InnerHits? innerHits)
		{
			InnerHitsDescriptor = null;
			InnerHitsDescriptorAction = null;
			return Assign(innerHits, (a, v) => a.InnerHitsValue = v);
		}

		public FieldCollapseDescriptor<T> InnerHits(Elastic.Clients.Elasticsearch.InnerHitsDescriptor<T> descriptor)
		{
			InnerHitsValue = null;
			InnerHitsDescriptorAction = null;
			return Assign(descriptor, (a, v) => a.InnerHitsDescriptor = v);
		}

		public FieldCollapseDescriptor<T> InnerHits(Action<Elastic.Clients.Elasticsearch.InnerHitsDescriptor<T>> configure)
		{
			InnerHitsValue = null;
			InnerHitsDescriptorAction = null;
			return Assign(configure, (a, v) => a.InnerHitsDescriptorAction = v);
		}

		public FieldCollapseDescriptor<T> MaxConcurrentGroupSearches(int? maxConcurrentGroupSearches) => Assign(maxConcurrentGroupSearches, (a, v) => a.MaxConcurrentGroupSearchesValue = v);
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
			if (InnerHitsDescriptor is not null)
			{
				writer.WritePropertyName("inner_hits");
				JsonSerializer.Serialize(writer, InnerHitsDescriptor, options);
			}
			else if (InnerHitsDescriptorAction is not null)
			{
				writer.WritePropertyName("inner_hits");
				JsonSerializer.Serialize(writer, new InnerHitsDescriptor<T>(InnerHitsDescriptorAction), options);
			}
			else if (InnerHitsValue is not null)
			{
				writer.WritePropertyName("inner_hits");
				JsonSerializer.Serialize(writer, InnerHitsValue, options);
			}

			if (MaxConcurrentGroupSearchesValue.HasValue)
			{
				writer.WritePropertyName("max_concurrent_group_searches");
				writer.WriteNumberValue(MaxConcurrentGroupSearchesValue.Value);
			}

			writer.WriteEndObject();
		}
	}
}