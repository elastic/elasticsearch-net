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

internal sealed partial class DefinitionConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.Definition>
{
	private static readonly System.Text.Json.JsonEncodedText PropPreprocessors = System.Text.Json.JsonEncodedText.Encode("preprocessors");
	private static readonly System.Text.Json.JsonEncodedText PropTrainedModel = System.Text.Json.JsonEncodedText.Encode("trained_model");

	public override Elastic.Clients.Elasticsearch.MachineLearning.Definition Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.Preprocessor>?> propPreprocessors = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModel> propTrainedModel = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propPreprocessors.TryReadProperty(ref reader, options, PropPreprocessors, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.Preprocessor>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.Preprocessor>(o, null)))
			{
				continue;
			}

			if (propTrainedModel.TryReadProperty(ref reader, options, PropTrainedModel, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.Definition(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Preprocessors = propPreprocessors.Value,
			TrainedModel = propTrainedModel.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.Definition value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropPreprocessors, value.Preprocessors, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.Preprocessor>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.Preprocessor>(o, v, null));
		writer.WriteProperty(options, PropTrainedModel, value.TrainedModel, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.DefinitionConverter))]
public sealed partial class Definition
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public Definition(Elastic.Clients.Elasticsearch.MachineLearning.TrainedModel trainedModel)
	{
		TrainedModel = trainedModel;
	}
#if NET7_0_OR_GREATER
	public Definition()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public Definition()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Definition(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Collection of preprocessors
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.Preprocessor>? Preprocessors { get; set; }

	/// <summary>
	/// <para>
	/// The definition of the trained model.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.MachineLearning.TrainedModel TrainedModel { get; set; }
}

public readonly partial struct DefinitionDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.Definition Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DefinitionDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.Definition instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DefinitionDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.Definition(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.DefinitionDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.Definition instance) => new Elastic.Clients.Elasticsearch.MachineLearning.DefinitionDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.Definition(Elastic.Clients.Elasticsearch.MachineLearning.DefinitionDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Collection of preprocessors
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DefinitionDescriptor Preprocessors(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.Preprocessor>? value)
	{
		Instance.Preprocessors = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Collection of preprocessors
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DefinitionDescriptor Preprocessors()
	{
		Instance.Preprocessors = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfPreprocessor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Collection of preprocessors
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DefinitionDescriptor Preprocessors(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfPreprocessor>? action)
	{
		Instance.Preprocessors = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfPreprocessor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Collection of preprocessors
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DefinitionDescriptor Preprocessors(params Elastic.Clients.Elasticsearch.MachineLearning.Preprocessor[] values)
	{
		Instance.Preprocessors = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Collection of preprocessors
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DefinitionDescriptor Preprocessors(params System.Action<Elastic.Clients.Elasticsearch.MachineLearning.PreprocessorDescriptor>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.MachineLearning.Preprocessor>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.MachineLearning.PreprocessorDescriptor.Build(action));
		}

		Instance.Preprocessors = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// The definition of the trained model.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DefinitionDescriptor TrainedModel(Elastic.Clients.Elasticsearch.MachineLearning.TrainedModel value)
	{
		Instance.TrainedModel = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The definition of the trained model.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DefinitionDescriptor TrainedModel()
	{
		Instance.TrainedModel = Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The definition of the trained model.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DefinitionDescriptor TrainedModel(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelDescriptor>? action)
	{
		Instance.TrainedModel = Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.Definition Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.DefinitionDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.DefinitionDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.Definition(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}