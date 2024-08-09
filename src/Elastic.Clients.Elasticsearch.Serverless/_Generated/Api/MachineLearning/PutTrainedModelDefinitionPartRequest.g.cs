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
using Elastic.Clients.Elasticsearch.Serverless.Requests;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.MachineLearning;

public sealed partial class PutTrainedModelDefinitionPartRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Creates part of a trained model definition.
/// </para>
/// </summary>
public sealed partial class PutTrainedModelDefinitionPartRequest : PlainRequest<PutTrainedModelDefinitionPartRequestParameters>
{
	public PutTrainedModelDefinitionPartRequest(Elastic.Clients.Elasticsearch.Serverless.Id modelId, int part) : base(r => r.Required("model_id", modelId).Required("part", part))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningPutTrainedModelDefinitionPart;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.put_trained_model_definition_part";

	/// <summary>
	/// <para>
	/// The definition part for the model. Must be a base64 encoded string.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("definition")]
	public string Definition { get; set; }

	/// <summary>
	/// <para>
	/// The total uncompressed definition length in bytes. Not base64 encoded.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("total_definition_length")]
	public long TotalDefinitionLength { get; set; }

	/// <summary>
	/// <para>
	/// The total number of parts that will be uploaded. Must be greater than 0.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("total_parts")]
	public int TotalParts { get; set; }
}

/// <summary>
/// <para>
/// Creates part of a trained model definition.
/// </para>
/// </summary>
public sealed partial class PutTrainedModelDefinitionPartRequestDescriptor : RequestDescriptor<PutTrainedModelDefinitionPartRequestDescriptor, PutTrainedModelDefinitionPartRequestParameters>
{
	internal PutTrainedModelDefinitionPartRequestDescriptor(Action<PutTrainedModelDefinitionPartRequestDescriptor> configure) => configure.Invoke(this);

	public PutTrainedModelDefinitionPartRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.Id modelId, int part) : base(r => r.Required("model_id", modelId).Required("part", part))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningPutTrainedModelDefinitionPart;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.put_trained_model_definition_part";

	public PutTrainedModelDefinitionPartRequestDescriptor ModelId(Elastic.Clients.Elasticsearch.Serverless.Id modelId)
	{
		RouteValues.Required("model_id", modelId);
		return Self;
	}

	public PutTrainedModelDefinitionPartRequestDescriptor Part(int part)
	{
		RouteValues.Required("part", part);
		return Self;
	}

	private string DefinitionValue { get; set; }
	private long TotalDefinitionLengthValue { get; set; }
	private int TotalPartsValue { get; set; }

	/// <summary>
	/// <para>
	/// The definition part for the model. Must be a base64 encoded string.
	/// </para>
	/// </summary>
	public PutTrainedModelDefinitionPartRequestDescriptor Definition(string definition)
	{
		DefinitionValue = definition;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The total uncompressed definition length in bytes. Not base64 encoded.
	/// </para>
	/// </summary>
	public PutTrainedModelDefinitionPartRequestDescriptor TotalDefinitionLength(long totalDefinitionLength)
	{
		TotalDefinitionLengthValue = totalDefinitionLength;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The total number of parts that will be uploaded. Must be greater than 0.
	/// </para>
	/// </summary>
	public PutTrainedModelDefinitionPartRequestDescriptor TotalParts(int totalParts)
	{
		TotalPartsValue = totalParts;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("definition");
		writer.WriteStringValue(DefinitionValue);
		writer.WritePropertyName("total_definition_length");
		writer.WriteNumberValue(TotalDefinitionLengthValue);
		writer.WritePropertyName("total_parts");
		writer.WriteNumberValue(TotalPartsValue);
		writer.WriteEndObject();
	}
}