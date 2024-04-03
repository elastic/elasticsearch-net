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

#nullable restore

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.QueryDsl;

public sealed partial class RankFeatureQuery
{
	/// <summary>
	/// <para>Floating point number used to decrease or increase the relevance scores of the query.<br/>Boost values are relative to the default value of 1.0.<br/>A boost value between 0 and 1.0 decreases the relevance score.<br/>A value greater than 1.0 increases the relevance score.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("boost")]
	public float? Boost { get; set; }

	/// <summary>
	/// <para>`rank_feature` or `rank_features` field used to boost relevance scores.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("field")]
	public Elastic.Clients.Elasticsearch.Serverless.Field Field { get; set; }

	/// <summary>
	/// <para>Linear function used to boost relevance scores based on the value of the rank feature `field`.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("linear")]
	public Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLinear? Linear { get; set; }

	/// <summary>
	/// <para>Logarithmic function used to boost relevance scores based on the value of the rank feature `field`.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("log")]
	public Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLogarithm? Log { get; set; }
	[JsonInclude, JsonPropertyName("_name")]
	public string? QueryName { get; set; }

	/// <summary>
	/// <para>Saturation function used to boost relevance scores based on the value of the rank feature `field`.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("saturation")]
	public Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSaturation? Saturation { get; set; }

	/// <summary>
	/// <para>Sigmoid function used to boost relevance scores based on the value of the rank feature `field`.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("sigmoid")]
	public Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSigmoid? Sigmoid { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query(RankFeatureQuery rankFeatureQuery) => Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query.RankFeature(rankFeatureQuery);
}

public sealed partial class RankFeatureQueryDescriptor<TDocument> : SerializableDescriptor<RankFeatureQueryDescriptor<TDocument>>
{
	internal RankFeatureQueryDescriptor(Action<RankFeatureQueryDescriptor<TDocument>> configure) => configure.Invoke(this);

	public RankFeatureQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Field FieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLinear? LinearValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLinearDescriptor LinearDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLinearDescriptor> LinearDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLogarithm? LogValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLogarithmDescriptor LogDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLogarithmDescriptor> LogDescriptorAction { get; set; }
	private string? QueryNameValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSaturation? SaturationValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSaturationDescriptor SaturationDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSaturationDescriptor> SaturationDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSigmoid? SigmoidValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSigmoidDescriptor SigmoidDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSigmoidDescriptor> SigmoidDescriptorAction { get; set; }

	/// <summary>
	/// <para>Floating point number used to decrease or increase the relevance scores of the query.<br/>Boost values are relative to the default value of 1.0.<br/>A boost value between 0 and 1.0 decreases the relevance score.<br/>A value greater than 1.0 increases the relevance score.</para>
	/// </summary>
	public RankFeatureQueryDescriptor<TDocument> Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>`rank_feature` or `rank_features` field used to boost relevance scores.</para>
	/// </summary>
	public RankFeatureQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Serverless.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>`rank_feature` or `rank_features` field used to boost relevance scores.</para>
	/// </summary>
	public RankFeatureQueryDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>`rank_feature` or `rank_features` field used to boost relevance scores.</para>
	/// </summary>
	public RankFeatureQueryDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>Linear function used to boost relevance scores based on the value of the rank feature `field`.</para>
	/// </summary>
	public RankFeatureQueryDescriptor<TDocument> Linear(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLinear? linear)
	{
		LinearDescriptor = null;
		LinearDescriptorAction = null;
		LinearValue = linear;
		return Self;
	}

	public RankFeatureQueryDescriptor<TDocument> Linear(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLinearDescriptor descriptor)
	{
		LinearValue = null;
		LinearDescriptorAction = null;
		LinearDescriptor = descriptor;
		return Self;
	}

	public RankFeatureQueryDescriptor<TDocument> Linear(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLinearDescriptor> configure)
	{
		LinearValue = null;
		LinearDescriptor = null;
		LinearDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>Logarithmic function used to boost relevance scores based on the value of the rank feature `field`.</para>
	/// </summary>
	public RankFeatureQueryDescriptor<TDocument> Log(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLogarithm? log)
	{
		LogDescriptor = null;
		LogDescriptorAction = null;
		LogValue = log;
		return Self;
	}

	public RankFeatureQueryDescriptor<TDocument> Log(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLogarithmDescriptor descriptor)
	{
		LogValue = null;
		LogDescriptorAction = null;
		LogDescriptor = descriptor;
		return Self;
	}

	public RankFeatureQueryDescriptor<TDocument> Log(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLogarithmDescriptor> configure)
	{
		LogValue = null;
		LogDescriptor = null;
		LogDescriptorAction = configure;
		return Self;
	}

	public RankFeatureQueryDescriptor<TDocument> QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	/// <summary>
	/// <para>Saturation function used to boost relevance scores based on the value of the rank feature `field`.</para>
	/// </summary>
	public RankFeatureQueryDescriptor<TDocument> Saturation(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSaturation? saturation)
	{
		SaturationDescriptor = null;
		SaturationDescriptorAction = null;
		SaturationValue = saturation;
		return Self;
	}

	public RankFeatureQueryDescriptor<TDocument> Saturation(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSaturationDescriptor descriptor)
	{
		SaturationValue = null;
		SaturationDescriptorAction = null;
		SaturationDescriptor = descriptor;
		return Self;
	}

	public RankFeatureQueryDescriptor<TDocument> Saturation(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSaturationDescriptor> configure)
	{
		SaturationValue = null;
		SaturationDescriptor = null;
		SaturationDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>Sigmoid function used to boost relevance scores based on the value of the rank feature `field`.</para>
	/// </summary>
	public RankFeatureQueryDescriptor<TDocument> Sigmoid(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSigmoid? sigmoid)
	{
		SigmoidDescriptor = null;
		SigmoidDescriptorAction = null;
		SigmoidValue = sigmoid;
		return Self;
	}

	public RankFeatureQueryDescriptor<TDocument> Sigmoid(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSigmoidDescriptor descriptor)
	{
		SigmoidValue = null;
		SigmoidDescriptorAction = null;
		SigmoidDescriptor = descriptor;
		return Self;
	}

	public RankFeatureQueryDescriptor<TDocument> Sigmoid(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSigmoidDescriptor> configure)
	{
		SigmoidValue = null;
		SigmoidDescriptor = null;
		SigmoidDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLinearDescriptor(LinearDescriptorAction), options);
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLogarithmDescriptor(LogDescriptorAction), options);
		}
		else if (LogValue is not null)
		{
			writer.WritePropertyName("log");
			JsonSerializer.Serialize(writer, LogValue, options);
		}

		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		if (SaturationDescriptor is not null)
		{
			writer.WritePropertyName("saturation");
			JsonSerializer.Serialize(writer, SaturationDescriptor, options);
		}
		else if (SaturationDescriptorAction is not null)
		{
			writer.WritePropertyName("saturation");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSaturationDescriptor(SaturationDescriptorAction), options);
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSigmoidDescriptor(SigmoidDescriptorAction), options);
		}
		else if (SigmoidValue is not null)
		{
			writer.WritePropertyName("sigmoid");
			JsonSerializer.Serialize(writer, SigmoidValue, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class RankFeatureQueryDescriptor : SerializableDescriptor<RankFeatureQueryDescriptor>
{
	internal RankFeatureQueryDescriptor(Action<RankFeatureQueryDescriptor> configure) => configure.Invoke(this);

	public RankFeatureQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Field FieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLinear? LinearValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLinearDescriptor LinearDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLinearDescriptor> LinearDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLogarithm? LogValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLogarithmDescriptor LogDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLogarithmDescriptor> LogDescriptorAction { get; set; }
	private string? QueryNameValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSaturation? SaturationValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSaturationDescriptor SaturationDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSaturationDescriptor> SaturationDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSigmoid? SigmoidValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSigmoidDescriptor SigmoidDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSigmoidDescriptor> SigmoidDescriptorAction { get; set; }

	/// <summary>
	/// <para>Floating point number used to decrease or increase the relevance scores of the query.<br/>Boost values are relative to the default value of 1.0.<br/>A boost value between 0 and 1.0 decreases the relevance score.<br/>A value greater than 1.0 increases the relevance score.</para>
	/// </summary>
	public RankFeatureQueryDescriptor Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>`rank_feature` or `rank_features` field used to boost relevance scores.</para>
	/// </summary>
	public RankFeatureQueryDescriptor Field(Elastic.Clients.Elasticsearch.Serverless.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>`rank_feature` or `rank_features` field used to boost relevance scores.</para>
	/// </summary>
	public RankFeatureQueryDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>`rank_feature` or `rank_features` field used to boost relevance scores.</para>
	/// </summary>
	public RankFeatureQueryDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>Linear function used to boost relevance scores based on the value of the rank feature `field`.</para>
	/// </summary>
	public RankFeatureQueryDescriptor Linear(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLinear? linear)
	{
		LinearDescriptor = null;
		LinearDescriptorAction = null;
		LinearValue = linear;
		return Self;
	}

	public RankFeatureQueryDescriptor Linear(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLinearDescriptor descriptor)
	{
		LinearValue = null;
		LinearDescriptorAction = null;
		LinearDescriptor = descriptor;
		return Self;
	}

	public RankFeatureQueryDescriptor Linear(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLinearDescriptor> configure)
	{
		LinearValue = null;
		LinearDescriptor = null;
		LinearDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>Logarithmic function used to boost relevance scores based on the value of the rank feature `field`.</para>
	/// </summary>
	public RankFeatureQueryDescriptor Log(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLogarithm? log)
	{
		LogDescriptor = null;
		LogDescriptorAction = null;
		LogValue = log;
		return Self;
	}

	public RankFeatureQueryDescriptor Log(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLogarithmDescriptor descriptor)
	{
		LogValue = null;
		LogDescriptorAction = null;
		LogDescriptor = descriptor;
		return Self;
	}

	public RankFeatureQueryDescriptor Log(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLogarithmDescriptor> configure)
	{
		LogValue = null;
		LogDescriptor = null;
		LogDescriptorAction = configure;
		return Self;
	}

	public RankFeatureQueryDescriptor QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	/// <summary>
	/// <para>Saturation function used to boost relevance scores based on the value of the rank feature `field`.</para>
	/// </summary>
	public RankFeatureQueryDescriptor Saturation(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSaturation? saturation)
	{
		SaturationDescriptor = null;
		SaturationDescriptorAction = null;
		SaturationValue = saturation;
		return Self;
	}

	public RankFeatureQueryDescriptor Saturation(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSaturationDescriptor descriptor)
	{
		SaturationValue = null;
		SaturationDescriptorAction = null;
		SaturationDescriptor = descriptor;
		return Self;
	}

	public RankFeatureQueryDescriptor Saturation(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSaturationDescriptor> configure)
	{
		SaturationValue = null;
		SaturationDescriptor = null;
		SaturationDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>Sigmoid function used to boost relevance scores based on the value of the rank feature `field`.</para>
	/// </summary>
	public RankFeatureQueryDescriptor Sigmoid(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSigmoid? sigmoid)
	{
		SigmoidDescriptor = null;
		SigmoidDescriptorAction = null;
		SigmoidValue = sigmoid;
		return Self;
	}

	public RankFeatureQueryDescriptor Sigmoid(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSigmoidDescriptor descriptor)
	{
		SigmoidValue = null;
		SigmoidDescriptorAction = null;
		SigmoidDescriptor = descriptor;
		return Self;
	}

	public RankFeatureQueryDescriptor Sigmoid(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSigmoidDescriptor> configure)
	{
		SigmoidValue = null;
		SigmoidDescriptor = null;
		SigmoidDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLinearDescriptor(LinearDescriptorAction), options);
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionLogarithmDescriptor(LogDescriptorAction), options);
		}
		else if (LogValue is not null)
		{
			writer.WritePropertyName("log");
			JsonSerializer.Serialize(writer, LogValue, options);
		}

		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		if (SaturationDescriptor is not null)
		{
			writer.WritePropertyName("saturation");
			JsonSerializer.Serialize(writer, SaturationDescriptor, options);
		}
		else if (SaturationDescriptorAction is not null)
		{
			writer.WritePropertyName("saturation");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSaturationDescriptor(SaturationDescriptorAction), options);
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.QueryDsl.RankFeatureFunctionSigmoidDescriptor(SigmoidDescriptorAction), options);
		}
		else if (SigmoidValue is not null)
		{
			writer.WritePropertyName("sigmoid");
			JsonSerializer.Serialize(writer, SigmoidValue, options);
		}

		writer.WriteEndObject();
	}
}