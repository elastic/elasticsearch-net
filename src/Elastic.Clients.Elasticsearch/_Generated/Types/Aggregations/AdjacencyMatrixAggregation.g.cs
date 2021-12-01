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
namespace Elastic.Clients.Elasticsearch.Aggregations
{
	public partial class AdjacencyMatrixAggregation : Aggregations.BucketAggregationBase, IAggregationContainerVariant
	{
		public AdjacencyMatrixAggregation(string name) : base(name)
		{
		}

		[JsonIgnore]
		string Aggregations.IAggregationContainerVariant.AggregationContainerVariantName => "adjacency_matrix";
		[JsonInclude]
		[JsonPropertyName("filters")]
		public Dictionary<string, Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer>? Filters { get; set; }
	}

	public sealed partial class AdjacencyMatrixAggregationDescriptor : DescriptorBase<AdjacencyMatrixAggregationDescriptor>
	{
		public AdjacencyMatrixAggregationDescriptor()
		{
		}

		internal AdjacencyMatrixAggregationDescriptor(Action<AdjacencyMatrixAggregationDescriptor> configure) => configure.Invoke(this);
		internal Dictionary<string, Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer>? FiltersValue { get; private set; }

		public AdjacencyMatrixAggregationDescriptor Filters(Func<FluentDictionary<string?, Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer?>, FluentDictionary<string?, Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer?>> selector) => Assign(selector, (a, v) => a.FiltersValue = v?.Invoke(new FluentDictionary<string?, Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer?>()));
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (FiltersValue is not null)
			{
				writer.WritePropertyName("filters");
				JsonSerializer.Serialize(writer, FiltersValue, options);
			}

			writer.WriteEndObject();
		}
	}
}