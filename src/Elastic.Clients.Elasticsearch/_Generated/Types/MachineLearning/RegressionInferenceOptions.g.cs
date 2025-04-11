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

internal sealed partial class RegressionInferenceOptionsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptions>
{
	private static readonly System.Text.Json.JsonEncodedText PropNumTopFeatureImportanceValues = System.Text.Json.JsonEncodedText.Encode("num_top_feature_importance_values");
	private static readonly System.Text.Json.JsonEncodedText PropResultsField = System.Text.Json.JsonEncodedText.Encode("results_field");

	public override Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptions Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int?> propNumTopFeatureImportanceValues = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field?> propResultsField = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propNumTopFeatureImportanceValues.TryReadProperty(ref reader, options, PropNumTopFeatureImportanceValues, null))
			{
				continue;
			}

			if (propResultsField.TryReadProperty(ref reader, options, PropResultsField, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			NumTopFeatureImportanceValues = propNumTopFeatureImportanceValues.Value,
			ResultsField = propResultsField.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptions value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropNumTopFeatureImportanceValues, value.NumTopFeatureImportanceValues, null, null);
		writer.WriteProperty(options, PropResultsField, value.ResultsField, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptionsConverter))]
public sealed partial class RegressionInferenceOptions
{
#if NET7_0_OR_GREATER
	public RegressionInferenceOptions()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public RegressionInferenceOptions()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal RegressionInferenceOptions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Specifies the maximum number of feature importance values per document.
	/// </para>
	/// </summary>
	public int? NumTopFeatureImportanceValues { get; set; }

	/// <summary>
	/// <para>
	/// The field that is added to incoming documents to contain the inference prediction. Defaults to predicted_value.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Field? ResultsField { get; set; }
}

public readonly partial struct RegressionInferenceOptionsDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptions Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RegressionInferenceOptionsDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptions instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RegressionInferenceOptionsDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptionsDescriptor<TDocument>(Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptions instance) => new Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptionsDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptions(Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptionsDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Specifies the maximum number of feature importance values per document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptionsDescriptor<TDocument> NumTopFeatureImportanceValues(int? value)
	{
		Instance.NumTopFeatureImportanceValues = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field that is added to incoming documents to contain the inference prediction. Defaults to predicted_value.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptionsDescriptor<TDocument> ResultsField(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.ResultsField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field that is added to incoming documents to contain the inference prediction. Defaults to predicted_value.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptionsDescriptor<TDocument> ResultsField(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.ResultsField = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptions Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptionsDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptionsDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct RegressionInferenceOptionsDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptions Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RegressionInferenceOptionsDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptions instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RegressionInferenceOptionsDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptionsDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptions instance) => new Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptionsDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptions(Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptionsDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Specifies the maximum number of feature importance values per document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptionsDescriptor NumTopFeatureImportanceValues(int? value)
	{
		Instance.NumTopFeatureImportanceValues = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field that is added to incoming documents to contain the inference prediction. Defaults to predicted_value.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptionsDescriptor ResultsField(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.ResultsField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field that is added to incoming documents to contain the inference prediction. Defaults to predicted_value.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptionsDescriptor ResultsField<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.ResultsField = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptions Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptionsDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptionsDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.RegressionInferenceOptions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}