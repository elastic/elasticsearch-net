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
	public sealed partial class RankFeatureQuery : IQueryVariant
	{
		[JsonInclude]
		[JsonPropertyName("_name")]
		public string? QueryName { get; set; }

		[JsonInclude]
		[JsonPropertyName("boost")]
		public float? Boost { get; set; }

		[JsonInclude]
		[JsonPropertyName("field")]
		public Elastic.Clients.Elasticsearch.Field Field { get; set; }

		[JsonInclude]
		[JsonPropertyName("linear")]
		public Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLinear? Linear { get; set; }

		[JsonInclude]
		[JsonPropertyName("log")]
		public Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLogarithm? Log { get; set; }

		[JsonInclude]
		[JsonPropertyName("saturation")]
		public Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionSaturation? Saturation { get; set; }

		[JsonInclude]
		[JsonPropertyName("sigmoid")]
		public Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionSigmoid? Sigmoid { get; set; }
	}

	public sealed partial class RankFeatureQueryDescriptor<TDocument> : SerializableDescriptorBase<RankFeatureQueryDescriptor<TDocument>>
	{
		internal RankFeatureQueryDescriptor(Action<RankFeatureQueryDescriptor<TDocument>> configure) => configure.Invoke(this);
		public RankFeatureQueryDescriptor() : base()
		{
		}

		private string? QueryNameValue { get; set; }

		private float? BoostValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

		private Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLinear? LinearValue { get; set; }

		private RankFeatureFunctionLinearDescriptor LinearDescriptor { get; set; }

		private Action<RankFeatureFunctionLinearDescriptor> LinearDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLogarithm? LogValue { get; set; }

		private RankFeatureFunctionLogarithmDescriptor LogDescriptor { get; set; }

		private Action<RankFeatureFunctionLogarithmDescriptor> LogDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionSaturation? SaturationValue { get; set; }

		private RankFeatureFunctionSaturationDescriptor SaturationDescriptor { get; set; }

		private Action<RankFeatureFunctionSaturationDescriptor> SaturationDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionSigmoid? SigmoidValue { get; set; }

		private RankFeatureFunctionSigmoidDescriptor SigmoidDescriptor { get; set; }

		private Action<RankFeatureFunctionSigmoidDescriptor> SigmoidDescriptorAction { get; set; }

		public RankFeatureQueryDescriptor<TDocument> QueryName(string? queryName)
		{
			QueryNameValue = queryName;
			return Self;
		}

		public RankFeatureQueryDescriptor<TDocument> Boost(float? boost)
		{
			BoostValue = boost;
			return Self;
		}

		public RankFeatureQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
		{
			FieldValue = field;
			return Self;
		}

		public RankFeatureQueryDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public RankFeatureQueryDescriptor<TDocument> Linear(Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLinear? linear)
		{
			LinearDescriptor = null;
			LinearDescriptorAction = null;
			LinearValue = linear;
			return Self;
		}

		public RankFeatureQueryDescriptor<TDocument> Linear(RankFeatureFunctionLinearDescriptor descriptor)
		{
			LinearValue = null;
			LinearDescriptorAction = null;
			LinearDescriptor = descriptor;
			return Self;
		}

		public RankFeatureQueryDescriptor<TDocument> Linear(Action<RankFeatureFunctionLinearDescriptor> configure)
		{
			LinearValue = null;
			LinearDescriptor = null;
			LinearDescriptorAction = configure;
			return Self;
		}

		public RankFeatureQueryDescriptor<TDocument> Log(Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLogarithm? log)
		{
			LogDescriptor = null;
			LogDescriptorAction = null;
			LogValue = log;
			return Self;
		}

		public RankFeatureQueryDescriptor<TDocument> Log(RankFeatureFunctionLogarithmDescriptor descriptor)
		{
			LogValue = null;
			LogDescriptorAction = null;
			LogDescriptor = descriptor;
			return Self;
		}

		public RankFeatureQueryDescriptor<TDocument> Log(Action<RankFeatureFunctionLogarithmDescriptor> configure)
		{
			LogValue = null;
			LogDescriptor = null;
			LogDescriptorAction = configure;
			return Self;
		}

		public RankFeatureQueryDescriptor<TDocument> Saturation(Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionSaturation? saturation)
		{
			SaturationDescriptor = null;
			SaturationDescriptorAction = null;
			SaturationValue = saturation;
			return Self;
		}

		public RankFeatureQueryDescriptor<TDocument> Saturation(RankFeatureFunctionSaturationDescriptor descriptor)
		{
			SaturationValue = null;
			SaturationDescriptorAction = null;
			SaturationDescriptor = descriptor;
			return Self;
		}

		public RankFeatureQueryDescriptor<TDocument> Saturation(Action<RankFeatureFunctionSaturationDescriptor> configure)
		{
			SaturationValue = null;
			SaturationDescriptor = null;
			SaturationDescriptorAction = configure;
			return Self;
		}

		public RankFeatureQueryDescriptor<TDocument> Sigmoid(Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionSigmoid? sigmoid)
		{
			SigmoidDescriptor = null;
			SigmoidDescriptorAction = null;
			SigmoidValue = sigmoid;
			return Self;
		}

		public RankFeatureQueryDescriptor<TDocument> Sigmoid(RankFeatureFunctionSigmoidDescriptor descriptor)
		{
			SigmoidValue = null;
			SigmoidDescriptorAction = null;
			SigmoidDescriptor = descriptor;
			return Self;
		}

		public RankFeatureQueryDescriptor<TDocument> Sigmoid(Action<RankFeatureFunctionSigmoidDescriptor> configure)
		{
			SigmoidValue = null;
			SigmoidDescriptor = null;
			SigmoidDescriptorAction = configure;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
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

			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
			if (LinearDescriptor is not null)
			{
				writer.WritePropertyName("linear");
				JsonSerializer.Serialize(writer, LinearDescriptor, options);
			}
			else if (LinearDescriptorAction is not null)
			{
				writer.WritePropertyName("linear");
				JsonSerializer.Serialize(writer, new RankFeatureFunctionLinearDescriptor(LinearDescriptorAction), options);
			}
			else if (LinearValue is not null)
			{
				writer.WritePropertyName("linear");
				JsonSerializer.Serialize(writer, LinearValue, options);
			}

			if (LogDescriptor is not null)
			{
				writer.WritePropertyName("log");
				JsonSerializer.Serialize(writer, LogDescriptor, options);
			}
			else if (LogDescriptorAction is not null)
			{
				writer.WritePropertyName("log");
				JsonSerializer.Serialize(writer, new RankFeatureFunctionLogarithmDescriptor(LogDescriptorAction), options);
			}
			else if (LogValue is not null)
			{
				writer.WritePropertyName("log");
				JsonSerializer.Serialize(writer, LogValue, options);
			}

			if (SaturationDescriptor is not null)
			{
				writer.WritePropertyName("saturation");
				JsonSerializer.Serialize(writer, SaturationDescriptor, options);
			}
			else if (SaturationDescriptorAction is not null)
			{
				writer.WritePropertyName("saturation");
				JsonSerializer.Serialize(writer, new RankFeatureFunctionSaturationDescriptor(SaturationDescriptorAction), options);
			}
			else if (SaturationValue is not null)
			{
				writer.WritePropertyName("saturation");
				JsonSerializer.Serialize(writer, SaturationValue, options);
			}

			if (SigmoidDescriptor is not null)
			{
				writer.WritePropertyName("sigmoid");
				JsonSerializer.Serialize(writer, SigmoidDescriptor, options);
			}
			else if (SigmoidDescriptorAction is not null)
			{
				writer.WritePropertyName("sigmoid");
				JsonSerializer.Serialize(writer, new RankFeatureFunctionSigmoidDescriptor(SigmoidDescriptorAction), options);
			}
			else if (SigmoidValue is not null)
			{
				writer.WritePropertyName("sigmoid");
				JsonSerializer.Serialize(writer, SigmoidValue, options);
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class RankFeatureQueryDescriptor : SerializableDescriptorBase<RankFeatureQueryDescriptor>
	{
		internal RankFeatureQueryDescriptor(Action<RankFeatureQueryDescriptor> configure) => configure.Invoke(this);
		public RankFeatureQueryDescriptor() : base()
		{
		}

		private string? QueryNameValue { get; set; }

		private float? BoostValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

		private Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLinear? LinearValue { get; set; }

		private RankFeatureFunctionLinearDescriptor LinearDescriptor { get; set; }

		private Action<RankFeatureFunctionLinearDescriptor> LinearDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLogarithm? LogValue { get; set; }

		private RankFeatureFunctionLogarithmDescriptor LogDescriptor { get; set; }

		private Action<RankFeatureFunctionLogarithmDescriptor> LogDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionSaturation? SaturationValue { get; set; }

		private RankFeatureFunctionSaturationDescriptor SaturationDescriptor { get; set; }

		private Action<RankFeatureFunctionSaturationDescriptor> SaturationDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionSigmoid? SigmoidValue { get; set; }

		private RankFeatureFunctionSigmoidDescriptor SigmoidDescriptor { get; set; }

		private Action<RankFeatureFunctionSigmoidDescriptor> SigmoidDescriptorAction { get; set; }

		public RankFeatureQueryDescriptor QueryName(string? queryName)
		{
			QueryNameValue = queryName;
			return Self;
		}

		public RankFeatureQueryDescriptor Boost(float? boost)
		{
			BoostValue = boost;
			return Self;
		}

		public RankFeatureQueryDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
		{
			FieldValue = field;
			return Self;
		}

		public RankFeatureQueryDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public RankFeatureQueryDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
		{
			FieldValue = field;
			return Self;
		}

		public RankFeatureQueryDescriptor Linear(Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLinear? linear)
		{
			LinearDescriptor = null;
			LinearDescriptorAction = null;
			LinearValue = linear;
			return Self;
		}

		public RankFeatureQueryDescriptor Linear(RankFeatureFunctionLinearDescriptor descriptor)
		{
			LinearValue = null;
			LinearDescriptorAction = null;
			LinearDescriptor = descriptor;
			return Self;
		}

		public RankFeatureQueryDescriptor Linear(Action<RankFeatureFunctionLinearDescriptor> configure)
		{
			LinearValue = null;
			LinearDescriptor = null;
			LinearDescriptorAction = configure;
			return Self;
		}

		public RankFeatureQueryDescriptor Log(Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLogarithm? log)
		{
			LogDescriptor = null;
			LogDescriptorAction = null;
			LogValue = log;
			return Self;
		}

		public RankFeatureQueryDescriptor Log(RankFeatureFunctionLogarithmDescriptor descriptor)
		{
			LogValue = null;
			LogDescriptorAction = null;
			LogDescriptor = descriptor;
			return Self;
		}

		public RankFeatureQueryDescriptor Log(Action<RankFeatureFunctionLogarithmDescriptor> configure)
		{
			LogValue = null;
			LogDescriptor = null;
			LogDescriptorAction = configure;
			return Self;
		}

		public RankFeatureQueryDescriptor Saturation(Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionSaturation? saturation)
		{
			SaturationDescriptor = null;
			SaturationDescriptorAction = null;
			SaturationValue = saturation;
			return Self;
		}

		public RankFeatureQueryDescriptor Saturation(RankFeatureFunctionSaturationDescriptor descriptor)
		{
			SaturationValue = null;
			SaturationDescriptorAction = null;
			SaturationDescriptor = descriptor;
			return Self;
		}

		public RankFeatureQueryDescriptor Saturation(Action<RankFeatureFunctionSaturationDescriptor> configure)
		{
			SaturationValue = null;
			SaturationDescriptor = null;
			SaturationDescriptorAction = configure;
			return Self;
		}

		public RankFeatureQueryDescriptor Sigmoid(Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionSigmoid? sigmoid)
		{
			SigmoidDescriptor = null;
			SigmoidDescriptorAction = null;
			SigmoidValue = sigmoid;
			return Self;
		}

		public RankFeatureQueryDescriptor Sigmoid(RankFeatureFunctionSigmoidDescriptor descriptor)
		{
			SigmoidValue = null;
			SigmoidDescriptorAction = null;
			SigmoidDescriptor = descriptor;
			return Self;
		}

		public RankFeatureQueryDescriptor Sigmoid(Action<RankFeatureFunctionSigmoidDescriptor> configure)
		{
			SigmoidValue = null;
			SigmoidDescriptor = null;
			SigmoidDescriptorAction = configure;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
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

			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
			if (LinearDescriptor is not null)
			{
				writer.WritePropertyName("linear");
				JsonSerializer.Serialize(writer, LinearDescriptor, options);
			}
			else if (LinearDescriptorAction is not null)
			{
				writer.WritePropertyName("linear");
				JsonSerializer.Serialize(writer, new RankFeatureFunctionLinearDescriptor(LinearDescriptorAction), options);
			}
			else if (LinearValue is not null)
			{
				writer.WritePropertyName("linear");
				JsonSerializer.Serialize(writer, LinearValue, options);
			}

			if (LogDescriptor is not null)
			{
				writer.WritePropertyName("log");
				JsonSerializer.Serialize(writer, LogDescriptor, options);
			}
			else if (LogDescriptorAction is not null)
			{
				writer.WritePropertyName("log");
				JsonSerializer.Serialize(writer, new RankFeatureFunctionLogarithmDescriptor(LogDescriptorAction), options);
			}
			else if (LogValue is not null)
			{
				writer.WritePropertyName("log");
				JsonSerializer.Serialize(writer, LogValue, options);
			}

			if (SaturationDescriptor is not null)
			{
				writer.WritePropertyName("saturation");
				JsonSerializer.Serialize(writer, SaturationDescriptor, options);
			}
			else if (SaturationDescriptorAction is not null)
			{
				writer.WritePropertyName("saturation");
				JsonSerializer.Serialize(writer, new RankFeatureFunctionSaturationDescriptor(SaturationDescriptorAction), options);
			}
			else if (SaturationValue is not null)
			{
				writer.WritePropertyName("saturation");
				JsonSerializer.Serialize(writer, SaturationValue, options);
			}

			if (SigmoidDescriptor is not null)
			{
				writer.WritePropertyName("sigmoid");
				JsonSerializer.Serialize(writer, SigmoidDescriptor, options);
			}
			else if (SigmoidDescriptorAction is not null)
			{
				writer.WritePropertyName("sigmoid");
				JsonSerializer.Serialize(writer, new RankFeatureFunctionSigmoidDescriptor(SigmoidDescriptorAction), options);
			}
			else if (SigmoidValue is not null)
			{
				writer.WritePropertyName("sigmoid");
				JsonSerializer.Serialize(writer, SigmoidValue, options);
			}

			writer.WriteEndObject();
		}
	}
}