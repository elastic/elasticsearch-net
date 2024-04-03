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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.MachineLearning;

public sealed partial class RuleCondition
{
	/// <summary>
	/// <para>Specifies the result property to which the condition applies. If your detector uses `lat_long`, `metric`, `rare`, or `freq_rare` functions, you can only specify conditions that apply to time.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("applies_to")]
	public Elastic.Clients.Elasticsearch.MachineLearning.AppliesTo AppliesTo { get; set; }

	/// <summary>
	/// <para>Specifies the condition operator. The available options are greater than, greater than or equals, less than, and less than or equals.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("operator")]
	public Elastic.Clients.Elasticsearch.MachineLearning.ConditionOperator Operator { get; set; }

	/// <summary>
	/// <para>The value that is compared against the `applies_to` field using the operator.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("value")]
	public double Value { get; set; }
}

public sealed partial class RuleConditionDescriptor : SerializableDescriptor<RuleConditionDescriptor>
{
	internal RuleConditionDescriptor(Action<RuleConditionDescriptor> configure) => configure.Invoke(this);

	public RuleConditionDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.MachineLearning.AppliesTo AppliesToValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.ConditionOperator OperatorValue { get; set; }
	private double ValueValue { get; set; }

	/// <summary>
	/// <para>Specifies the result property to which the condition applies. If your detector uses `lat_long`, `metric`, `rare`, or `freq_rare` functions, you can only specify conditions that apply to time.</para>
	/// </summary>
	public RuleConditionDescriptor AppliesTo(Elastic.Clients.Elasticsearch.MachineLearning.AppliesTo appliesTo)
	{
		AppliesToValue = appliesTo;
		return Self;
	}

	/// <summary>
	/// <para>Specifies the condition operator. The available options are greater than, greater than or equals, less than, and less than or equals.</para>
	/// </summary>
	public RuleConditionDescriptor Operator(Elastic.Clients.Elasticsearch.MachineLearning.ConditionOperator value)
	{
		OperatorValue = value;
		return Self;
	}

	/// <summary>
	/// <para>The value that is compared against the `applies_to` field using the operator.</para>
	/// </summary>
	public RuleConditionDescriptor Value(double value)
	{
		ValueValue = value;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("applies_to");
		JsonSerializer.Serialize(writer, AppliesToValue, options);
		writer.WritePropertyName("operator");
		JsonSerializer.Serialize(writer, OperatorValue, options);
		writer.WritePropertyName("value");
		writer.WriteNumberValue(ValueValue);
		writer.WriteEndObject();
	}
}