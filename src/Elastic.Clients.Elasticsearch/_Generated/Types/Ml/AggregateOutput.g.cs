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
	public sealed partial class AggregateOutput
	{
		[JsonInclude]
		[JsonPropertyName("exponent")]
		public Elastic.Clients.Elasticsearch.Ml.Weights? Exponent { get; set; }

		[JsonInclude]
		[JsonPropertyName("logistic_regression")]
		public Elastic.Clients.Elasticsearch.Ml.Weights? LogisticRegression { get; set; }

		[JsonInclude]
		[JsonPropertyName("weighted_mode")]
		public Elastic.Clients.Elasticsearch.Ml.Weights? WeightedMode { get; set; }

		[JsonInclude]
		[JsonPropertyName("weighted_sum")]
		public Elastic.Clients.Elasticsearch.Ml.Weights? WeightedSum { get; set; }
	}

	public sealed partial class AggregateOutputDescriptor : SerializableDescriptorBase<AggregateOutputDescriptor>
	{
		internal AggregateOutputDescriptor(Action<AggregateOutputDescriptor> configure) => configure.Invoke(this);
		public AggregateOutputDescriptor() : base()
		{
		}

		private Elastic.Clients.Elasticsearch.Ml.Weights? ExponentValue { get; set; }

		private WeightsDescriptor ExponentDescriptor { get; set; }

		private Action<WeightsDescriptor> ExponentDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.Weights? LogisticRegressionValue { get; set; }

		private WeightsDescriptor LogisticRegressionDescriptor { get; set; }

		private Action<WeightsDescriptor> LogisticRegressionDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.Weights? WeightedModeValue { get; set; }

		private WeightsDescriptor WeightedModeDescriptor { get; set; }

		private Action<WeightsDescriptor> WeightedModeDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.Weights? WeightedSumValue { get; set; }

		private WeightsDescriptor WeightedSumDescriptor { get; set; }

		private Action<WeightsDescriptor> WeightedSumDescriptorAction { get; set; }

		public AggregateOutputDescriptor Exponent(Elastic.Clients.Elasticsearch.Ml.Weights? exponent)
		{
			ExponentDescriptor = null;
			ExponentDescriptorAction = null;
			ExponentValue = exponent;
			return Self;
		}

		public AggregateOutputDescriptor Exponent(WeightsDescriptor descriptor)
		{
			ExponentValue = null;
			ExponentDescriptorAction = null;
			ExponentDescriptor = descriptor;
			return Self;
		}

		public AggregateOutputDescriptor Exponent(Action<WeightsDescriptor> configure)
		{
			ExponentValue = null;
			ExponentDescriptor = null;
			ExponentDescriptorAction = configure;
			return Self;
		}

		public AggregateOutputDescriptor LogisticRegression(Elastic.Clients.Elasticsearch.Ml.Weights? logisticRegression)
		{
			LogisticRegressionDescriptor = null;
			LogisticRegressionDescriptorAction = null;
			LogisticRegressionValue = logisticRegression;
			return Self;
		}

		public AggregateOutputDescriptor LogisticRegression(WeightsDescriptor descriptor)
		{
			LogisticRegressionValue = null;
			LogisticRegressionDescriptorAction = null;
			LogisticRegressionDescriptor = descriptor;
			return Self;
		}

		public AggregateOutputDescriptor LogisticRegression(Action<WeightsDescriptor> configure)
		{
			LogisticRegressionValue = null;
			LogisticRegressionDescriptor = null;
			LogisticRegressionDescriptorAction = configure;
			return Self;
		}

		public AggregateOutputDescriptor WeightedMode(Elastic.Clients.Elasticsearch.Ml.Weights? weightedMode)
		{
			WeightedModeDescriptor = null;
			WeightedModeDescriptorAction = null;
			WeightedModeValue = weightedMode;
			return Self;
		}

		public AggregateOutputDescriptor WeightedMode(WeightsDescriptor descriptor)
		{
			WeightedModeValue = null;
			WeightedModeDescriptorAction = null;
			WeightedModeDescriptor = descriptor;
			return Self;
		}

		public AggregateOutputDescriptor WeightedMode(Action<WeightsDescriptor> configure)
		{
			WeightedModeValue = null;
			WeightedModeDescriptor = null;
			WeightedModeDescriptorAction = configure;
			return Self;
		}

		public AggregateOutputDescriptor WeightedSum(Elastic.Clients.Elasticsearch.Ml.Weights? weightedSum)
		{
			WeightedSumDescriptor = null;
			WeightedSumDescriptorAction = null;
			WeightedSumValue = weightedSum;
			return Self;
		}

		public AggregateOutputDescriptor WeightedSum(WeightsDescriptor descriptor)
		{
			WeightedSumValue = null;
			WeightedSumDescriptorAction = null;
			WeightedSumDescriptor = descriptor;
			return Self;
		}

		public AggregateOutputDescriptor WeightedSum(Action<WeightsDescriptor> configure)
		{
			WeightedSumValue = null;
			WeightedSumDescriptor = null;
			WeightedSumDescriptorAction = configure;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (ExponentDescriptor is not null)
			{
				writer.WritePropertyName("exponent");
				JsonSerializer.Serialize(writer, ExponentDescriptor, options);
			}
			else if (ExponentDescriptorAction is not null)
			{
				writer.WritePropertyName("exponent");
				JsonSerializer.Serialize(writer, new WeightsDescriptor(ExponentDescriptorAction), options);
			}
			else if (ExponentValue is not null)
			{
				writer.WritePropertyName("exponent");
				JsonSerializer.Serialize(writer, ExponentValue, options);
			}

			if (LogisticRegressionDescriptor is not null)
			{
				writer.WritePropertyName("logistic_regression");
				JsonSerializer.Serialize(writer, LogisticRegressionDescriptor, options);
			}
			else if (LogisticRegressionDescriptorAction is not null)
			{
				writer.WritePropertyName("logistic_regression");
				JsonSerializer.Serialize(writer, new WeightsDescriptor(LogisticRegressionDescriptorAction), options);
			}
			else if (LogisticRegressionValue is not null)
			{
				writer.WritePropertyName("logistic_regression");
				JsonSerializer.Serialize(writer, LogisticRegressionValue, options);
			}

			if (WeightedModeDescriptor is not null)
			{
				writer.WritePropertyName("weighted_mode");
				JsonSerializer.Serialize(writer, WeightedModeDescriptor, options);
			}
			else if (WeightedModeDescriptorAction is not null)
			{
				writer.WritePropertyName("weighted_mode");
				JsonSerializer.Serialize(writer, new WeightsDescriptor(WeightedModeDescriptorAction), options);
			}
			else if (WeightedModeValue is not null)
			{
				writer.WritePropertyName("weighted_mode");
				JsonSerializer.Serialize(writer, WeightedModeValue, options);
			}

			if (WeightedSumDescriptor is not null)
			{
				writer.WritePropertyName("weighted_sum");
				JsonSerializer.Serialize(writer, WeightedSumDescriptor, options);
			}
			else if (WeightedSumDescriptorAction is not null)
			{
				writer.WritePropertyName("weighted_sum");
				JsonSerializer.Serialize(writer, new WeightsDescriptor(WeightedSumDescriptorAction), options);
			}
			else if (WeightedSumValue is not null)
			{
				writer.WritePropertyName("weighted_sum");
				JsonSerializer.Serialize(writer, WeightedSumValue, options);
			}

			writer.WriteEndObject();
		}
	}
}