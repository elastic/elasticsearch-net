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

public sealed partial class PutTrainedModelRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// If set to <c>true</c> and a <c>compressed_definition</c> is provided,
	/// the request defers definition decompression and skips relevant
	/// validations.
	/// </para>
	/// </summary>
	public bool? DeferDefinitionDecompression { get => Q<bool?>("defer_definition_decompression"); set => Q("defer_definition_decompression", value); }

	/// <summary>
	/// <para>
	/// Whether to wait for all child operations (e.g. model download)
	/// to complete.
	/// </para>
	/// </summary>
	public bool? WaitForCompletion { get => Q<bool?>("wait_for_completion"); set => Q("wait_for_completion", value); }
}

internal sealed partial class PutTrainedModelRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropCompressedDefinition = System.Text.Json.JsonEncodedText.Encode("compressed_definition");
	private static readonly System.Text.Json.JsonEncodedText PropDefinition = System.Text.Json.JsonEncodedText.Encode("definition");
	private static readonly System.Text.Json.JsonEncodedText PropDescription = System.Text.Json.JsonEncodedText.Encode("description");
	private static readonly System.Text.Json.JsonEncodedText PropInferenceConfig = System.Text.Json.JsonEncodedText.Encode("inference_config");
	private static readonly System.Text.Json.JsonEncodedText PropInput = System.Text.Json.JsonEncodedText.Encode("input");
	private static readonly System.Text.Json.JsonEncodedText PropMetadata = System.Text.Json.JsonEncodedText.Encode("metadata");
	private static readonly System.Text.Json.JsonEncodedText PropModelSizeBytes = System.Text.Json.JsonEncodedText.Encode("model_size_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropModelType = System.Text.Json.JsonEncodedText.Encode("model_type");
	private static readonly System.Text.Json.JsonEncodedText PropPlatformArchitecture = System.Text.Json.JsonEncodedText.Encode("platform_architecture");
	private static readonly System.Text.Json.JsonEncodedText PropPrefixStrings = System.Text.Json.JsonEncodedText.Encode("prefix_strings");
	private static readonly System.Text.Json.JsonEncodedText PropTags = System.Text.Json.JsonEncodedText.Encode("tags");

	public override Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propCompressedDefinition = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.Definition?> propDefinition = default;
		LocalJsonValue<string?> propDescription = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.InferenceConfigCreate?> propInferenceConfig = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.Input?> propInput = default;
		LocalJsonValue<object?> propMetadata = default;
		LocalJsonValue<long?> propModelSizeBytes = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelType?> propModelType = default;
		LocalJsonValue<string?> propPlatformArchitecture = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelPrefixStrings?> propPrefixStrings = default;
		LocalJsonValue<System.Collections.Generic.ICollection<string>?> propTags = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCompressedDefinition.TryReadProperty(ref reader, options, PropCompressedDefinition, null))
			{
				continue;
			}

			if (propDefinition.TryReadProperty(ref reader, options, PropDefinition, null))
			{
				continue;
			}

			if (propDescription.TryReadProperty(ref reader, options, PropDescription, null))
			{
				continue;
			}

			if (propInferenceConfig.TryReadProperty(ref reader, options, PropInferenceConfig, null))
			{
				continue;
			}

			if (propInput.TryReadProperty(ref reader, options, PropInput, null))
			{
				continue;
			}

			if (propMetadata.TryReadProperty(ref reader, options, PropMetadata, null))
			{
				continue;
			}

			if (propModelSizeBytes.TryReadProperty(ref reader, options, PropModelSizeBytes, static long? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<long>(o)))
			{
				continue;
			}

			if (propModelType.TryReadProperty(ref reader, options, PropModelType, static Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelType? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelType>(o)))
			{
				continue;
			}

			if (propPlatformArchitecture.TryReadProperty(ref reader, options, PropPlatformArchitecture, null))
			{
				continue;
			}

			if (propPrefixStrings.TryReadProperty(ref reader, options, PropPrefixStrings, null))
			{
				continue;
			}

			if (propTags.TryReadProperty(ref reader, options, PropTags, static System.Collections.Generic.ICollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			CompressedDefinition = propCompressedDefinition.Value,
			Definition = propDefinition.Value,
			Description = propDescription.Value,
			InferenceConfig = propInferenceConfig.Value,
			Input = propInput.Value,
			Metadata = propMetadata.Value,
			ModelSizeBytes = propModelSizeBytes.Value,
			ModelType = propModelType.Value,
			PlatformArchitecture = propPlatformArchitecture.Value,
			PrefixStrings = propPrefixStrings.Value,
			Tags = propTags.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCompressedDefinition, value.CompressedDefinition, null, null);
		writer.WriteProperty(options, PropDefinition, value.Definition, null, null);
		writer.WriteProperty(options, PropDescription, value.Description, null, null);
		writer.WriteProperty(options, PropInferenceConfig, value.InferenceConfig, null, null);
		writer.WriteProperty(options, PropInput, value.Input, null, null);
		writer.WriteProperty(options, PropMetadata, value.Metadata, null, null);
		writer.WriteProperty(options, PropModelSizeBytes, value.ModelSizeBytes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, long? v) => w.WriteNullableValue<long>(o, v));
		writer.WriteProperty(options, PropModelType, value.ModelType, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelType? v) => w.WriteNullableValue<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelType>(o, v));
		writer.WriteProperty(options, PropPlatformArchitecture, value.PlatformArchitecture, null, null);
		writer.WriteProperty(options, PropPrefixStrings, value.PrefixStrings, null, null);
		writer.WriteProperty(options, PropTags, value.Tags, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Create a trained model.
/// Enable you to supply a trained model that is not created by data frame analytics.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestConverter))]
public sealed partial class PutTrainedModelRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutTrainedModelRequest(Elastic.Clients.Elasticsearch.Id modelId) : base(r => r.Required("model_id", modelId))
	{
	}
#if NET7_0_OR_GREATER
	public PutTrainedModelRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PutTrainedModelRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.MachineLearningPutTrainedModel;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.put_trained_model";

	/// <summary>
	/// <para>
	/// The unique identifier of the trained model.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id ModelId { get => P<Elastic.Clients.Elasticsearch.Id>("model_id"); set => PR("model_id", value); }

	/// <summary>
	/// <para>
	/// If set to <c>true</c> and a <c>compressed_definition</c> is provided,
	/// the request defers definition decompression and skips relevant
	/// validations.
	/// </para>
	/// </summary>
	public bool? DeferDefinitionDecompression { get => Q<bool?>("defer_definition_decompression"); set => Q("defer_definition_decompression", value); }

	/// <summary>
	/// <para>
	/// Whether to wait for all child operations (e.g. model download)
	/// to complete.
	/// </para>
	/// </summary>
	public bool? WaitForCompletion { get => Q<bool?>("wait_for_completion"); set => Q("wait_for_completion", value); }

	/// <summary>
	/// <para>
	/// The compressed (GZipped and Base64 encoded) inference definition of the
	/// model. If compressed_definition is specified, then definition cannot be
	/// specified.
	/// </para>
	/// </summary>
	public string? CompressedDefinition { get; set; }

	/// <summary>
	/// <para>
	/// The inference definition for the model. If definition is specified, then
	/// compressed_definition cannot be specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.Definition? Definition { get; set; }

	/// <summary>
	/// <para>
	/// A human-readable description of the inference trained model.
	/// </para>
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// <para>
	/// The default configuration for inference. This can be either a regression
	/// or classification configuration. It must match the underlying
	/// definition.trained_model's target_type. For pre-packaged models such as
	/// ELSER the config is not required.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.InferenceConfigCreate? InferenceConfig { get; set; }

	/// <summary>
	/// <para>
	/// The input field names for the model definition.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.Input? Input { get; set; }

	/// <summary>
	/// <para>
	/// An object map that contains metadata about the model.
	/// </para>
	/// </summary>
	public object? Metadata { get; set; }

	/// <summary>
	/// <para>
	/// The estimated memory usage in bytes to keep the trained model in memory.
	/// This property is supported only if defer_definition_decompression is true
	/// or the model definition is not supplied.
	/// </para>
	/// </summary>
	public long? ModelSizeBytes { get; set; }

	/// <summary>
	/// <para>
	/// The model type.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelType? ModelType { get; set; }

	/// <summary>
	/// <para>
	/// The platform architecture (if applicable) of the trained mode. If the model
	/// only works on one platform, because it is heavily optimized for a particular
	/// processor architecture and OS combination, then this field specifies which.
	/// The format of the string must match the platform identifiers used by Elasticsearch,
	/// so one of, <c>linux-x86_64</c>, <c>linux-aarch64</c>, <c>darwin-x86_64</c>, <c>darwin-aarch64</c>,
	/// or <c>windows-x86_64</c>. For portable models (those that work independent of processor
	/// architecture or OS features), leave this field unset.
	/// </para>
	/// </summary>
	public string? PlatformArchitecture { get; set; }

	/// <summary>
	/// <para>
	/// Optional prefix strings applied at inference
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelPrefixStrings? PrefixStrings { get; set; }

	/// <summary>
	/// <para>
	/// An array of tags to organize the model.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<string>? Tags { get; set; }
}

/// <summary>
/// <para>
/// Create a trained model.
/// Enable you to supply a trained model that is not created by data frame analytics.
/// </para>
/// </summary>
public readonly partial struct PutTrainedModelRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutTrainedModelRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequest instance)
	{
		Instance = instance;
	}

	public PutTrainedModelRequestDescriptor(Elastic.Clients.Elasticsearch.Id modelId)
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequest(modelId);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public PutTrainedModelRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequest instance) => new Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequest(Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The unique identifier of the trained model.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor ModelId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.ModelId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If set to <c>true</c> and a <c>compressed_definition</c> is provided,
	/// the request defers definition decompression and skips relevant
	/// validations.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor DeferDefinitionDecompression(bool? value = true)
	{
		Instance.DeferDefinitionDecompression = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Whether to wait for all child operations (e.g. model download)
	/// to complete.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor WaitForCompletion(bool? value = true)
	{
		Instance.WaitForCompletion = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The compressed (GZipped and Base64 encoded) inference definition of the
	/// model. If compressed_definition is specified, then definition cannot be
	/// specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor CompressedDefinition(string? value)
	{
		Instance.CompressedDefinition = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The inference definition for the model. If definition is specified, then
	/// compressed_definition cannot be specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor Definition(Elastic.Clients.Elasticsearch.MachineLearning.Definition? value)
	{
		Instance.Definition = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The inference definition for the model. If definition is specified, then
	/// compressed_definition cannot be specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor Definition(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.DefinitionDescriptor> action)
	{
		Instance.Definition = Elastic.Clients.Elasticsearch.MachineLearning.DefinitionDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A human-readable description of the inference trained model.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The default configuration for inference. This can be either a regression
	/// or classification configuration. It must match the underlying
	/// definition.trained_model's target_type. For pre-packaged models such as
	/// ELSER the config is not required.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor InferenceConfig(Elastic.Clients.Elasticsearch.MachineLearning.InferenceConfigCreate? value)
	{
		Instance.InferenceConfig = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The default configuration for inference. This can be either a regression
	/// or classification configuration. It must match the underlying
	/// definition.trained_model's target_type. For pre-packaged models such as
	/// ELSER the config is not required.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor InferenceConfig(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.InferenceConfigCreateDescriptor> action)
	{
		Instance.InferenceConfig = Elastic.Clients.Elasticsearch.MachineLearning.InferenceConfigCreateDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The default configuration for inference. This can be either a regression
	/// or classification configuration. It must match the underlying
	/// definition.trained_model's target_type. For pre-packaged models such as
	/// ELSER the config is not required.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor InferenceConfig<T>(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.InferenceConfigCreateDescriptor<T>> action)
	{
		Instance.InferenceConfig = Elastic.Clients.Elasticsearch.MachineLearning.InferenceConfigCreateDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The input field names for the model definition.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor Input(Elastic.Clients.Elasticsearch.MachineLearning.Input? value)
	{
		Instance.Input = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The input field names for the model definition.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor Input(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.InputDescriptor> action)
	{
		Instance.Input = Elastic.Clients.Elasticsearch.MachineLearning.InputDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// An object map that contains metadata about the model.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor Metadata(object? value)
	{
		Instance.Metadata = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The estimated memory usage in bytes to keep the trained model in memory.
	/// This property is supported only if defer_definition_decompression is true
	/// or the model definition is not supplied.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor ModelSizeBytes(long? value)
	{
		Instance.ModelSizeBytes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The model type.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor ModelType(Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelType? value)
	{
		Instance.ModelType = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The platform architecture (if applicable) of the trained mode. If the model
	/// only works on one platform, because it is heavily optimized for a particular
	/// processor architecture and OS combination, then this field specifies which.
	/// The format of the string must match the platform identifiers used by Elasticsearch,
	/// so one of, <c>linux-x86_64</c>, <c>linux-aarch64</c>, <c>darwin-x86_64</c>, <c>darwin-aarch64</c>,
	/// or <c>windows-x86_64</c>. For portable models (those that work independent of processor
	/// architecture or OS features), leave this field unset.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor PlatformArchitecture(string? value)
	{
		Instance.PlatformArchitecture = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Optional prefix strings applied at inference
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor PrefixStrings(Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelPrefixStrings? value)
	{
		Instance.PrefixStrings = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Optional prefix strings applied at inference
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor PrefixStrings()
	{
		Instance.PrefixStrings = Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelPrefixStringsDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Optional prefix strings applied at inference
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor PrefixStrings(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelPrefixStringsDescriptor>? action)
	{
		Instance.PrefixStrings = Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelPrefixStringsDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// An array of tags to organize the model.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor Tags(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.Tags = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// An array of tags to organize the model.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor Tags(params string[] values)
	{
		Instance.Tags = [.. values];
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequest Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}

/// <summary>
/// <para>
/// Create a trained model.
/// Enable you to supply a trained model that is not created by data frame analytics.
/// </para>
/// </summary>
public readonly partial struct PutTrainedModelRequestDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutTrainedModelRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequest instance)
	{
		Instance = instance;
	}

	public PutTrainedModelRequestDescriptor(Elastic.Clients.Elasticsearch.Id modelId)
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequest(modelId);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public PutTrainedModelRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument>(Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequest instance) => new Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequest(Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The unique identifier of the trained model.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> ModelId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.ModelId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If set to <c>true</c> and a <c>compressed_definition</c> is provided,
	/// the request defers definition decompression and skips relevant
	/// validations.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> DeferDefinitionDecompression(bool? value = true)
	{
		Instance.DeferDefinitionDecompression = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Whether to wait for all child operations (e.g. model download)
	/// to complete.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> WaitForCompletion(bool? value = true)
	{
		Instance.WaitForCompletion = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The compressed (GZipped and Base64 encoded) inference definition of the
	/// model. If compressed_definition is specified, then definition cannot be
	/// specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> CompressedDefinition(string? value)
	{
		Instance.CompressedDefinition = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The inference definition for the model. If definition is specified, then
	/// compressed_definition cannot be specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> Definition(Elastic.Clients.Elasticsearch.MachineLearning.Definition? value)
	{
		Instance.Definition = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The inference definition for the model. If definition is specified, then
	/// compressed_definition cannot be specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> Definition(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.DefinitionDescriptor> action)
	{
		Instance.Definition = Elastic.Clients.Elasticsearch.MachineLearning.DefinitionDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A human-readable description of the inference trained model.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The default configuration for inference. This can be either a regression
	/// or classification configuration. It must match the underlying
	/// definition.trained_model's target_type. For pre-packaged models such as
	/// ELSER the config is not required.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> InferenceConfig(Elastic.Clients.Elasticsearch.MachineLearning.InferenceConfigCreate? value)
	{
		Instance.InferenceConfig = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The default configuration for inference. This can be either a regression
	/// or classification configuration. It must match the underlying
	/// definition.trained_model's target_type. For pre-packaged models such as
	/// ELSER the config is not required.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> InferenceConfig(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.InferenceConfigCreateDescriptor<TDocument>> action)
	{
		Instance.InferenceConfig = Elastic.Clients.Elasticsearch.MachineLearning.InferenceConfigCreateDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The input field names for the model definition.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> Input(Elastic.Clients.Elasticsearch.MachineLearning.Input? value)
	{
		Instance.Input = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The input field names for the model definition.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> Input(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.InputDescriptor> action)
	{
		Instance.Input = Elastic.Clients.Elasticsearch.MachineLearning.InputDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// An object map that contains metadata about the model.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> Metadata(object? value)
	{
		Instance.Metadata = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The estimated memory usage in bytes to keep the trained model in memory.
	/// This property is supported only if defer_definition_decompression is true
	/// or the model definition is not supplied.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> ModelSizeBytes(long? value)
	{
		Instance.ModelSizeBytes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The model type.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> ModelType(Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelType? value)
	{
		Instance.ModelType = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The platform architecture (if applicable) of the trained mode. If the model
	/// only works on one platform, because it is heavily optimized for a particular
	/// processor architecture and OS combination, then this field specifies which.
	/// The format of the string must match the platform identifiers used by Elasticsearch,
	/// so one of, <c>linux-x86_64</c>, <c>linux-aarch64</c>, <c>darwin-x86_64</c>, <c>darwin-aarch64</c>,
	/// or <c>windows-x86_64</c>. For portable models (those that work independent of processor
	/// architecture or OS features), leave this field unset.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> PlatformArchitecture(string? value)
	{
		Instance.PlatformArchitecture = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Optional prefix strings applied at inference
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> PrefixStrings(Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelPrefixStrings? value)
	{
		Instance.PrefixStrings = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Optional prefix strings applied at inference
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> PrefixStrings()
	{
		Instance.PrefixStrings = Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelPrefixStringsDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Optional prefix strings applied at inference
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> PrefixStrings(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelPrefixStringsDescriptor>? action)
	{
		Instance.PrefixStrings = Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelPrefixStringsDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// An array of tags to organize the model.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> Tags(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.Tags = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// An array of tags to organize the model.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> Tags(params string[] values)
	{
		Instance.Tags = [.. values];
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequest Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PutTrainedModelRequestDescriptor<TDocument> RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}