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

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.MachineLearning;

internal sealed partial class DataframeEvaluationRegressionMetricsMsleConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegressionMetricsMsle>
{
	private static readonly System.Text.Json.JsonEncodedText PropOffset = System.Text.Json.JsonEncodedText.Encode("offset");

	public override Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegressionMetricsMsle Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<double?> propOffset = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propOffset.TryReadProperty(ref reader, options, PropOffset, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegressionMetricsMsle(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Offset = propOffset.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegressionMetricsMsle value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropOffset, value.Offset, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegressionMetricsMsleConverter))]
public sealed partial class DataframeEvaluationRegressionMetricsMsle
{
#if NET7_0_OR_GREATER
	public DataframeEvaluationRegressionMetricsMsle()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public DataframeEvaluationRegressionMetricsMsle()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DataframeEvaluationRegressionMetricsMsle(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Defines the transition point at which you switch from minimizing quadratic error to minimizing quadratic log error. Defaults to 1.
	/// </para>
	/// </summary>
	public double? Offset { get; set; }
}

public readonly partial struct DataframeEvaluationRegressionMetricsMsleDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegressionMetricsMsle Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataframeEvaluationRegressionMetricsMsleDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegressionMetricsMsle instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataframeEvaluationRegressionMetricsMsleDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegressionMetricsMsle(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegressionMetricsMsleDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegressionMetricsMsle instance) => new Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegressionMetricsMsleDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegressionMetricsMsle(Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegressionMetricsMsleDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Defines the transition point at which you switch from minimizing quadratic error to minimizing quadratic log error. Defaults to 1.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegressionMetricsMsleDescriptor Offset(double? value)
	{
		Instance.Offset = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegressionMetricsMsle Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegressionMetricsMsleDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegressionMetricsMsle(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegressionMetricsMsleDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationRegressionMetricsMsle(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}