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
using Elastic.Transport.Products.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.MachineLearning;

internal sealed partial class PutTrainedModelResponseConverter : System.Text.Json.Serialization.JsonConverter<PutTrainedModelResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropCompressedDefinition = System.Text.Json.JsonEncodedText.Encode("compressed_definition");
	private static readonly System.Text.Json.JsonEncodedText PropCreatedBy = System.Text.Json.JsonEncodedText.Encode("created_by");
	private static readonly System.Text.Json.JsonEncodedText PropCreateTime = System.Text.Json.JsonEncodedText.Encode("create_time");
	private static readonly System.Text.Json.JsonEncodedText PropDefaultFieldMap = System.Text.Json.JsonEncodedText.Encode("default_field_map");
	private static readonly System.Text.Json.JsonEncodedText PropDescription = System.Text.Json.JsonEncodedText.Encode("description");
	private static readonly System.Text.Json.JsonEncodedText PropEstimatedHeapMemoryUsageBytes = System.Text.Json.JsonEncodedText.Encode("estimated_heap_memory_usage_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropEstimatedOperations = System.Text.Json.JsonEncodedText.Encode("estimated_operations");
	private static readonly System.Text.Json.JsonEncodedText PropFullyDefined = System.Text.Json.JsonEncodedText.Encode("fully_defined");
	private static readonly System.Text.Json.JsonEncodedText PropInferenceConfig = System.Text.Json.JsonEncodedText.Encode("inference_config");
	private static readonly System.Text.Json.JsonEncodedText PropInput = System.Text.Json.JsonEncodedText.Encode("input");
	private static readonly System.Text.Json.JsonEncodedText PropLicenseLevel = System.Text.Json.JsonEncodedText.Encode("license_level");
	private static readonly System.Text.Json.JsonEncodedText PropLocation = System.Text.Json.JsonEncodedText.Encode("location");
	private static readonly System.Text.Json.JsonEncodedText PropMetadata = System.Text.Json.JsonEncodedText.Encode("metadata");
	private static readonly System.Text.Json.JsonEncodedText PropModelId = System.Text.Json.JsonEncodedText.Encode("model_id");
	private static readonly System.Text.Json.JsonEncodedText PropModelPackage = System.Text.Json.JsonEncodedText.Encode("model_package");
	private static readonly System.Text.Json.JsonEncodedText PropModelSizeBytes = System.Text.Json.JsonEncodedText.Encode("model_size_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropModelType = System.Text.Json.JsonEncodedText.Encode("model_type");
	private static readonly System.Text.Json.JsonEncodedText PropPrefixStrings = System.Text.Json.JsonEncodedText.Encode("prefix_strings");
	private static readonly System.Text.Json.JsonEncodedText PropTags = System.Text.Json.JsonEncodedText.Encode("tags");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override PutTrainedModelResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propCompressedDefinition = default;
		LocalJsonValue<string?> propCreatedBy = default;
		LocalJsonValue<DateTimeOffset?> propCreateTime = default;
		LocalJsonValue<IReadOnlyDictionary<string, string>?> propDefaultFieldMap = default;
		LocalJsonValue<string?> propDescription = default;
		LocalJsonValue<int?> propEstimatedHeapMemoryUsageBytes = default;
		LocalJsonValue<int?> propEstimatedOperations = default;
		LocalJsonValue<bool?> propFullyDefined = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.InferenceConfigCreate?> propInferenceConfig = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelConfigInput> propInput = default;
		LocalJsonValue<string?> propLicenseLevel = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelLocation?> propLocation = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelConfigMetadata?> propMetadata = default;
		LocalJsonValue<string> propModelId = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.ModelPackageConfig?> propModelPackage = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propModelSizeBytes = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelType?> propModelType = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelPrefixStrings?> propPrefixStrings = default;
		LocalJsonValue<IReadOnlyCollection<string>> propTags = default;
		LocalJsonValue<string?> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCompressedDefinition.TryReadProperty(ref reader, options, PropCompressedDefinition, null))
			{
				continue;
			}

			if (propCreatedBy.TryReadProperty(ref reader, options, PropCreatedBy, null))
			{
				continue;
			}

			if (propCreateTime.TryReadProperty(ref reader, options, PropCreateTime, null))
			{
				continue;
			}

			if (propDefaultFieldMap.TryReadProperty(ref reader, options, PropDefaultFieldMap, static IReadOnlyDictionary<string, string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, string>(o, null, null)))
			{
				continue;
			}

			if (propDescription.TryReadProperty(ref reader, options, PropDescription, null))
			{
				continue;
			}

			if (propEstimatedHeapMemoryUsageBytes.TryReadProperty(ref reader, options, PropEstimatedHeapMemoryUsageBytes, null))
			{
				continue;
			}

			if (propEstimatedOperations.TryReadProperty(ref reader, options, PropEstimatedOperations, null))
			{
				continue;
			}

			if (propFullyDefined.TryReadProperty(ref reader, options, PropFullyDefined, null))
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

			if (propLicenseLevel.TryReadProperty(ref reader, options, PropLicenseLevel, null))
			{
				continue;
			}

			if (propLocation.TryReadProperty(ref reader, options, PropLocation, null))
			{
				continue;
			}

			if (propMetadata.TryReadProperty(ref reader, options, PropMetadata, null))
			{
				continue;
			}

			if (propModelId.TryReadProperty(ref reader, options, PropModelId, null))
			{
				continue;
			}

			if (propModelPackage.TryReadProperty(ref reader, options, PropModelPackage, null))
			{
				continue;
			}

			if (propModelSizeBytes.TryReadProperty(ref reader, options, PropModelSizeBytes, null))
			{
				continue;
			}

			if (propModelType.TryReadProperty(ref reader, options, PropModelType, null))
			{
				continue;
			}

			if (propPrefixStrings.TryReadProperty(ref reader, options, PropPrefixStrings, null))
			{
				continue;
			}

			if (propTags.TryReadProperty(ref reader, options, PropTags, static IReadOnlyCollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)!))
			{
				continue;
			}

			if (propVersion.TryReadProperty(ref reader, options, PropVersion, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new PutTrainedModelResponse
		{
			CompressedDefinition = propCompressedDefinition.Value
,
			CreatedBy = propCreatedBy.Value
,
			CreateTime = propCreateTime.Value
,
			DefaultFieldMap = propDefaultFieldMap.Value
,
			Description = propDescription.Value
,
			EstimatedHeapMemoryUsageBytes = propEstimatedHeapMemoryUsageBytes.Value
,
			EstimatedOperations = propEstimatedOperations.Value
,
			FullyDefined = propFullyDefined.Value
,
			InferenceConfig = propInferenceConfig.Value
,
			Input = propInput.Value
,
			LicenseLevel = propLicenseLevel.Value
,
			Location = propLocation.Value
,
			Metadata = propMetadata.Value
,
			ModelId = propModelId.Value
,
			ModelPackage = propModelPackage.Value
,
			ModelSizeBytes = propModelSizeBytes.Value
,
			ModelType = propModelType.Value
,
			PrefixStrings = propPrefixStrings.Value
,
			Tags = propTags.Value
,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, PutTrainedModelResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCompressedDefinition, value.CompressedDefinition, null, null);
		writer.WriteProperty(options, PropCreatedBy, value.CreatedBy, null, null);
		writer.WriteProperty(options, PropCreateTime, value.CreateTime, null, null);
		writer.WriteProperty(options, PropDefaultFieldMap, value.DefaultFieldMap, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, IReadOnlyDictionary<string, string>? v) => w.WriteDictionaryValue<string, string>(o, v, null, null));
		writer.WriteProperty(options, PropDescription, value.Description, null, null);
		writer.WriteProperty(options, PropEstimatedHeapMemoryUsageBytes, value.EstimatedHeapMemoryUsageBytes, null, null);
		writer.WriteProperty(options, PropEstimatedOperations, value.EstimatedOperations, null, null);
		writer.WriteProperty(options, PropFullyDefined, value.FullyDefined, null, null);
		writer.WriteProperty(options, PropInferenceConfig, value.InferenceConfig, null, null);
		writer.WriteProperty(options, PropInput, value.Input, null, null);
		writer.WriteProperty(options, PropLicenseLevel, value.LicenseLevel, null, null);
		writer.WriteProperty(options, PropLocation, value.Location, null, null);
		writer.WriteProperty(options, PropMetadata, value.Metadata, null, null);
		writer.WriteProperty(options, PropModelId, value.ModelId, null, null);
		writer.WriteProperty(options, PropModelPackage, value.ModelPackage, null, null);
		writer.WriteProperty(options, PropModelSizeBytes, value.ModelSizeBytes, null, null);
		writer.WriteProperty(options, PropModelType, value.ModelType, null, null);
		writer.WriteProperty(options, PropPrefixStrings, value.PrefixStrings, null, null);
		writer.WriteProperty(options, PropTags, value.Tags, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, IReadOnlyCollection<string> v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(PutTrainedModelResponseConverter))]
public sealed partial class PutTrainedModelResponse : ElasticsearchResponse
{
	public string? CompressedDefinition { get; init; }

	/// <summary>
	/// <para>
	/// Information on the creator of the trained model.
	/// </para>
	/// </summary>
	public string? CreatedBy { get; init; }

	/// <summary>
	/// <para>
	/// The time when the trained model was created.
	/// </para>
	/// </summary>
	public DateTimeOffset? CreateTime { get; init; }

	/// <summary>
	/// <para>
	/// Any field map described in the inference configuration takes precedence.
	/// </para>
	/// </summary>
	public IReadOnlyDictionary<string, string>? DefaultFieldMap { get; init; }

	/// <summary>
	/// <para>
	/// The free-text description of the trained model.
	/// </para>
	/// </summary>
	public string? Description { get; init; }

	/// <summary>
	/// <para>
	/// The estimated heap usage in bytes to keep the trained model in memory.
	/// </para>
	/// </summary>
	public int? EstimatedHeapMemoryUsageBytes { get; init; }

	/// <summary>
	/// <para>
	/// The estimated number of operations to use the trained model.
	/// </para>
	/// </summary>
	public int? EstimatedOperations { get; init; }

	/// <summary>
	/// <para>
	/// True if the full model definition is present.
	/// </para>
	/// </summary>
	public bool? FullyDefined { get; init; }

	/// <summary>
	/// <para>
	/// The default configuration for inference. This can be either a regression, classification, or one of the many NLP focused configurations. It must match the underlying definition.trained_model's target_type. For pre-packaged models such as ELSER the config is not required.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.InferenceConfigCreate? InferenceConfig { get; init; }

	/// <summary>
	/// <para>
	/// The input field names for the model definition.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelConfigInput Input { get; init; }

	/// <summary>
	/// <para>
	/// The license level of the trained model.
	/// </para>
	/// </summary>
	public string? LicenseLevel { get; init; }
	public Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelLocation? Location { get; init; }

	/// <summary>
	/// <para>
	/// An object containing metadata about the trained model. For example, models created by data frame analytics contain analysis_config and input objects.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelConfigMetadata? Metadata { get; init; }

	/// <summary>
	/// <para>
	/// Identifier for the trained model.
	/// </para>
	/// </summary>
	public string ModelId { get; init; }
	public Elastic.Clients.Elasticsearch.MachineLearning.ModelPackageConfig? ModelPackage { get; init; }
	public Elastic.Clients.Elasticsearch.ByteSize? ModelSizeBytes { get; init; }

	/// <summary>
	/// <para>
	/// The model type
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelType? ModelType { get; init; }
	public Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelPrefixStrings? PrefixStrings { get; init; }

	/// <summary>
	/// <para>
	/// A comma delimited string of tags. A trained model can have many tags, or none.
	/// </para>
	/// </summary>
	public IReadOnlyCollection<string> Tags { get; init; }

	/// <summary>
	/// <para>
	/// The Elasticsearch version number in which the trained model was created.
	/// </para>
	/// </summary>
	public string? Version { get; init; }
}