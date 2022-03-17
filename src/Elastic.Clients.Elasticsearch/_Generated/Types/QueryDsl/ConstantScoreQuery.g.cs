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
	public partial class ConstantScoreQuery : QueryDsl.QueryBase, IQueryContainerVariant
	{
		[JsonIgnore]
		string QueryDsl.IQueryContainerVariant.QueryContainerVariantName => "constant_score";
		[JsonInclude]
		[JsonPropertyName("filter")]
		public Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer Filter { get; set; }
	}

	public sealed partial class ConstantScoreQueryDescriptor<TDocument> : DescriptorBase<ConstantScoreQueryDescriptor<TDocument>>
	{
		internal ConstantScoreQueryDescriptor(Action<ConstantScoreQueryDescriptor<TDocument>> configure) => configure.Invoke(this);
		public ConstantScoreQueryDescriptor() : base()
		{
		}

		private Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer FilterValue { get; set; }

		private QueryContainerDescriptor<TDocument> FilterDescriptor { get; set; }

		private Action<QueryContainerDescriptor<TDocument>> FilterDescriptorAction { get; set; }

		private string? QueryNameValue { get; set; }

		private float? BoostValue { get; set; }

		public ConstantScoreQueryDescriptor<TDocument> Filter(Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer filter)
		{
			FilterDescriptor = null;
			FilterDescriptorAction = null;
			FilterValue = filter;
			return Self;
		}

		public ConstantScoreQueryDescriptor<TDocument> Filter(QueryDsl.QueryContainerDescriptor<TDocument> descriptor)
		{
			FilterValue = null;
			FilterDescriptorAction = null;
			FilterDescriptor = descriptor;
			return Self;
		}

		public ConstantScoreQueryDescriptor<TDocument> Filter(Action<QueryDsl.QueryContainerDescriptor<TDocument>> configure)
		{
			FilterValue = null;
			FilterDescriptorAction = null;
			FilterDescriptorAction = configure;
			return Self;
		}

		public ConstantScoreQueryDescriptor<TDocument> QueryName(string? queryName)
		{
			QueryNameValue = queryName;
			return Self;
		}

		public ConstantScoreQueryDescriptor<TDocument> Boost(float? boost)
		{
			BoostValue = boost;
			return Self;
		}

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
			else
			{
				writer.WritePropertyName("filter");
				JsonSerializer.Serialize(writer, FilterValue, options);
			}

			if (!string.IsNullOrEmpty(QueryNameValue))
			{
				writer.WritePropertyName("_name");
				writer.WriteStringValue(QueryNameValue);
			}

			if (BoostValue.HasValue)
			{
				writer.WritePropertyName("boost");
				writer.WriteNumberValue(BoostValue.Value);
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class ConstantScoreQueryDescriptor : DescriptorBase<ConstantScoreQueryDescriptor>
	{
		internal ConstantScoreQueryDescriptor(Action<ConstantScoreQueryDescriptor> configure) => configure.Invoke(this);
		public ConstantScoreQueryDescriptor() : base()
		{
		}

		private Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer FilterValue { get; set; }

		private QueryContainerDescriptor FilterDescriptor { get; set; }

		private Action<QueryContainerDescriptor> FilterDescriptorAction { get; set; }

		private string? QueryNameValue { get; set; }

		private float? BoostValue { get; set; }

		public ConstantScoreQueryDescriptor Filter(Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer filter)
		{
			FilterDescriptor = null;
			FilterDescriptorAction = null;
			FilterValue = filter;
			return Self;
		}

		public ConstantScoreQueryDescriptor Filter(QueryDsl.QueryContainerDescriptor descriptor)
		{
			FilterValue = null;
			FilterDescriptorAction = null;
			FilterDescriptor = descriptor;
			return Self;
		}

		public ConstantScoreQueryDescriptor Filter(Action<QueryDsl.QueryContainerDescriptor> configure)
		{
			FilterValue = null;
			FilterDescriptorAction = null;
			FilterDescriptorAction = configure;
			return Self;
		}

		public ConstantScoreQueryDescriptor QueryName(string? queryName)
		{
			QueryNameValue = queryName;
			return Self;
		}

		public ConstantScoreQueryDescriptor Boost(float? boost)
		{
			BoostValue = boost;
			return Self;
		}

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
				JsonSerializer.Serialize(writer, new QueryDsl.QueryContainerDescriptor(FilterDescriptorAction), options);
			}
			else
			{
				writer.WritePropertyName("filter");
				JsonSerializer.Serialize(writer, FilterValue, options);
			}

			if (!string.IsNullOrEmpty(QueryNameValue))
			{
				writer.WritePropertyName("_name");
				writer.WriteStringValue(QueryNameValue);
			}

			if (BoostValue.HasValue)
			{
				writer.WritePropertyName("boost");
				writer.WriteNumberValue(BoostValue.Value);
			}

			writer.WriteEndObject();
		}
	}
}