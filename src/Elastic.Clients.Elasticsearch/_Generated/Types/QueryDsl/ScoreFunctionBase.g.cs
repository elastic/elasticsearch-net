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
	public abstract partial class ScoreFunctionBase
	{
		[JsonInclude]
		[JsonPropertyName("filter")]
		public Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? Filter { get; set; }

		[JsonInclude]
		[JsonPropertyName("weight")]
		public double? Weight { get; set; }
	}

	public sealed partial class ScoreFunctionBaseDescriptor<TDocument> : DescriptorBase<ScoreFunctionBaseDescriptor<TDocument>>
	{
		public ScoreFunctionBaseDescriptor()
		{
		}

		internal ScoreFunctionBaseDescriptor(Action<ScoreFunctionBaseDescriptor<TDocument>> configure) => configure.Invoke(this);
		internal Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? FilterValue { get; private set; }

		internal double? WeightValue { get; private set; }

		internal QueryContainerDescriptor<TDocument> FilterDescriptor { get; private set; }

		internal Action<QueryContainerDescriptor<TDocument>> FilterDescriptorAction { get; private set; }

		public ScoreFunctionBaseDescriptor<TDocument> Filter(Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? filter)
		{
			FilterDescriptor = null;
			FilterDescriptorAction = null;
			return Assign(filter, (a, v) => a.FilterValue = v);
		}

		public ScoreFunctionBaseDescriptor<TDocument> Filter(QueryDsl.QueryContainerDescriptor<TDocument> descriptor)
		{
			FilterValue = null;
			FilterDescriptorAction = null;
			return Assign(descriptor, (a, v) => a.FilterDescriptor = v);
		}

		public ScoreFunctionBaseDescriptor<TDocument> Filter(Action<QueryDsl.QueryContainerDescriptor<TDocument>> configure)
		{
			FilterValue = null;
			FilterDescriptorAction = null;
			return Assign(configure, (a, v) => a.FilterDescriptorAction = v);
		}

		public ScoreFunctionBaseDescriptor<TDocument> Weight(double? weight) => Assign(weight, (a, v) => a.WeightValue = v);
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (FilterDescriptor is not null)
			{
				writer.WritePropertyName("filter");
				JsonSerializer.Serialize(writer, FilterDescriptor, options);
			}
			else if (FilterDescriptorAction is not null)
			{
				writer.WritePropertyName("filter");
				JsonSerializer.Serialize(writer, new QueryDsl.QueryContainerDescriptor<TDocument>(FilterDescriptorAction), options);
			}
			else if (FilterValue is not null)
			{
				writer.WritePropertyName("filter");
				JsonSerializer.Serialize(writer, FilterValue, options);
			}

			if (WeightValue.HasValue)
			{
				writer.WritePropertyName("weight");
				writer.WriteNumberValue(WeightValue.Value);
			}

			writer.WriteEndObject();
		}
	}
}