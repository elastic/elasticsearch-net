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

internal sealed partial class QuestionAnsweringInferenceOptionsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.QuestionAnsweringInferenceOptions>
{
	private static readonly System.Text.Json.JsonEncodedText PropMaxAnswerLength = System.Text.Json.JsonEncodedText.Encode("max_answer_length");
	private static readonly System.Text.Json.JsonEncodedText PropNumTopClasses = System.Text.Json.JsonEncodedText.Encode("num_top_classes");
	private static readonly System.Text.Json.JsonEncodedText PropResultsField = System.Text.Json.JsonEncodedText.Encode("results_field");
	private static readonly System.Text.Json.JsonEncodedText PropTokenization = System.Text.Json.JsonEncodedText.Encode("tokenization");

	public override Elastic.Clients.Elasticsearch.MachineLearning.QuestionAnsweringInferenceOptions Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int?> propMaxAnswerLength = default;
		LocalJsonValue<int?> propNumTopClasses = default;
		LocalJsonValue<string?> propResultsField = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.TokenizationConfig?> propTokenization = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propMaxAnswerLength.TryReadProperty(ref reader, options, PropMaxAnswerLength, null))
			{
				continue;
			}

			if (propNumTopClasses.TryReadProperty(ref reader, options, PropNumTopClasses, null))
			{
				continue;
			}

			if (propResultsField.TryReadProperty(ref reader, options, PropResultsField, null))
			{
				continue;
			}

			if (propTokenization.TryReadProperty(ref reader, options, PropTokenization, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.QuestionAnsweringInferenceOptions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			MaxAnswerLength = propMaxAnswerLength.Value,
			NumTopClasses = propNumTopClasses.Value,
			ResultsField = propResultsField.Value,
			Tokenization = propTokenization.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.QuestionAnsweringInferenceOptions value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropMaxAnswerLength, value.MaxAnswerLength, null, null);
		writer.WriteProperty(options, PropNumTopClasses, value.NumTopClasses, null, null);
		writer.WriteProperty(options, PropResultsField, value.ResultsField, null, null);
		writer.WriteProperty(options, PropTokenization, value.Tokenization, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Question answering inference options
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.QuestionAnsweringInferenceOptionsConverter))]
public sealed partial class QuestionAnsweringInferenceOptions
{
#if NET7_0_OR_GREATER
	public QuestionAnsweringInferenceOptions()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public QuestionAnsweringInferenceOptions()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal QuestionAnsweringInferenceOptions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The maximum answer length to consider
	/// </para>
	/// </summary>
	public int? MaxAnswerLength { get; set; }

	/// <summary>
	/// <para>
	/// Specifies the number of top class predictions to return. Defaults to 0.
	/// </para>
	/// </summary>
	public int? NumTopClasses { get; set; }

	/// <summary>
	/// <para>
	/// The field that is added to incoming documents to contain the inference prediction. Defaults to predicted_value.
	/// </para>
	/// </summary>
	public string? ResultsField { get; set; }

	/// <summary>
	/// <para>
	/// The tokenization options to update when inferring
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.TokenizationConfig? Tokenization { get; set; }
}

/// <summary>
/// <para>
/// Question answering inference options
/// </para>
/// </summary>
public readonly partial struct QuestionAnsweringInferenceOptionsDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.QuestionAnsweringInferenceOptions Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public QuestionAnsweringInferenceOptionsDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.QuestionAnsweringInferenceOptions instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public QuestionAnsweringInferenceOptionsDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.QuestionAnsweringInferenceOptions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.QuestionAnsweringInferenceOptionsDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.QuestionAnsweringInferenceOptions instance) => new Elastic.Clients.Elasticsearch.MachineLearning.QuestionAnsweringInferenceOptionsDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.QuestionAnsweringInferenceOptions(Elastic.Clients.Elasticsearch.MachineLearning.QuestionAnsweringInferenceOptionsDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The maximum answer length to consider
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.QuestionAnsweringInferenceOptionsDescriptor MaxAnswerLength(int? value)
	{
		Instance.MaxAnswerLength = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies the number of top class predictions to return. Defaults to 0.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.QuestionAnsweringInferenceOptionsDescriptor NumTopClasses(int? value)
	{
		Instance.NumTopClasses = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field that is added to incoming documents to contain the inference prediction. Defaults to predicted_value.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.QuestionAnsweringInferenceOptionsDescriptor ResultsField(string? value)
	{
		Instance.ResultsField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The tokenization options to update when inferring
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.QuestionAnsweringInferenceOptionsDescriptor Tokenization(Elastic.Clients.Elasticsearch.MachineLearning.TokenizationConfig? value)
	{
		Instance.Tokenization = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The tokenization options to update when inferring
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.QuestionAnsweringInferenceOptionsDescriptor Tokenization(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.TokenizationConfigDescriptor> action)
	{
		Instance.Tokenization = Elastic.Clients.Elasticsearch.MachineLearning.TokenizationConfigDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.QuestionAnsweringInferenceOptions Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.QuestionAnsweringInferenceOptionsDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.MachineLearning.QuestionAnsweringInferenceOptions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.QuestionAnsweringInferenceOptionsDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.QuestionAnsweringInferenceOptions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}