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
	public partial class ScriptScoreFunction : ScoreFunctionBase, IFunctionScoreContainerVariant
	{
		[JsonIgnore]
		string IFunctionScoreContainerVariant.FunctionScoreContainerVariantName => "script_score";
		[JsonInclude]
		[JsonPropertyName("script")]
		public ScriptBase Script { get; set; }
	}

	public sealed partial class ScriptScoreFunctionDescriptor<TDocument> : SerializableDescriptorBase<ScriptScoreFunctionDescriptor<TDocument>>
	{
		internal ScriptScoreFunctionDescriptor(Action<ScriptScoreFunctionDescriptor<TDocument>> configure) => configure.Invoke(this);
		public ScriptScoreFunctionDescriptor() : base()
		{
		}

		private Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? FilterValue { get; set; }

		private QueryContainerDescriptor<TDocument> FilterDescriptor { get; set; }

		private Action<QueryContainerDescriptor<TDocument>> FilterDescriptorAction { get; set; }

		private ScriptBase ScriptValue { get; set; }

		private ScriptDescriptor ScriptDescriptor { get; set; }

		private Action<ScriptDescriptor> ScriptDescriptorAction { get; set; }

		private double? WeightValue { get; set; }

		public ScriptScoreFunctionDescriptor<TDocument> Filter(Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? filter)
		{
			FilterDescriptor = null;
			FilterDescriptorAction = null;
			FilterValue = filter;
			return Self;
		}

		public ScriptScoreFunctionDescriptor<TDocument> Filter(QueryContainerDescriptor<TDocument> descriptor)
		{
			FilterValue = null;
			FilterDescriptorAction = null;
			FilterDescriptor = descriptor;
			return Self;
		}

		public ScriptScoreFunctionDescriptor<TDocument> Filter(Action<QueryContainerDescriptor<TDocument>> configure)
		{
			FilterValue = null;
			FilterDescriptor = null;
			FilterDescriptorAction = configure;
			return Self;
		}

		public ScriptScoreFunctionDescriptor<TDocument> Script(ScriptBase script)
		{
			ScriptDescriptor = null;
			ScriptDescriptorAction = null;
			ScriptValue = script;
			return Self;
		}

		public ScriptScoreFunctionDescriptor<TDocument> Script(ScriptDescriptor descriptor)
		{
			ScriptValue = null;
			ScriptDescriptorAction = null;
			ScriptDescriptor = descriptor;
			return Self;
		}

		public ScriptScoreFunctionDescriptor<TDocument> Script(Action<ScriptDescriptor> configure)
		{
			ScriptValue = null;
			ScriptDescriptor = null;
			ScriptDescriptorAction = configure;
			return Self;
		}

		public ScriptScoreFunctionDescriptor<TDocument> Weight(double? weight)
		{
			WeightValue = weight;
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
				JsonSerializer.Serialize(writer, new QueryContainerDescriptor<TDocument>(FilterDescriptorAction), options);
			}
			else if (FilterValue is not null)
			{
				writer.WritePropertyName("filter");
				JsonSerializer.Serialize(writer, FilterValue, options);
			}

			if (ScriptDescriptor is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, ScriptDescriptor, options);
			}
			else if (ScriptDescriptorAction is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, new ScriptDescriptor(ScriptDescriptorAction), options);
			}
			else
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, ScriptValue, options);
			}

			if (WeightValue.HasValue)
			{
				writer.WritePropertyName("weight");
				writer.WriteNumberValue(WeightValue.Value);
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class ScriptScoreFunctionDescriptor : SerializableDescriptorBase<ScriptScoreFunctionDescriptor>
	{
		internal ScriptScoreFunctionDescriptor(Action<ScriptScoreFunctionDescriptor> configure) => configure.Invoke(this);
		public ScriptScoreFunctionDescriptor() : base()
		{
		}

		private Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? FilterValue { get; set; }

		private QueryContainerDescriptor FilterDescriptor { get; set; }

		private Action<QueryContainerDescriptor> FilterDescriptorAction { get; set; }

		private ScriptBase ScriptValue { get; set; }

		private ScriptDescriptor ScriptDescriptor { get; set; }

		private Action<ScriptDescriptor> ScriptDescriptorAction { get; set; }

		private double? WeightValue { get; set; }

		public ScriptScoreFunctionDescriptor Filter(Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? filter)
		{
			FilterDescriptor = null;
			FilterDescriptorAction = null;
			FilterValue = filter;
			return Self;
		}

		public ScriptScoreFunctionDescriptor Filter(QueryContainerDescriptor descriptor)
		{
			FilterValue = null;
			FilterDescriptorAction = null;
			FilterDescriptor = descriptor;
			return Self;
		}

		public ScriptScoreFunctionDescriptor Filter(Action<QueryContainerDescriptor> configure)
		{
			FilterValue = null;
			FilterDescriptor = null;
			FilterDescriptorAction = configure;
			return Self;
		}

		public ScriptScoreFunctionDescriptor Script(ScriptBase script)
		{
			ScriptDescriptor = null;
			ScriptDescriptorAction = null;
			ScriptValue = script;
			return Self;
		}

		public ScriptScoreFunctionDescriptor Script(ScriptDescriptor descriptor)
		{
			ScriptValue = null;
			ScriptDescriptorAction = null;
			ScriptDescriptor = descriptor;
			return Self;
		}

		public ScriptScoreFunctionDescriptor Script(Action<ScriptDescriptor> configure)
		{
			ScriptValue = null;
			ScriptDescriptor = null;
			ScriptDescriptorAction = configure;
			return Self;
		}

		public ScriptScoreFunctionDescriptor Weight(double? weight)
		{
			WeightValue = weight;
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
				JsonSerializer.Serialize(writer, new QueryContainerDescriptor(FilterDescriptorAction), options);
			}
			else if (FilterValue is not null)
			{
				writer.WritePropertyName("filter");
				JsonSerializer.Serialize(writer, FilterValue, options);
			}

			if (ScriptDescriptor is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, ScriptDescriptor, options);
			}
			else if (ScriptDescriptorAction is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, new ScriptDescriptor(ScriptDescriptorAction), options);
			}
			else
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, ScriptValue, options);
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