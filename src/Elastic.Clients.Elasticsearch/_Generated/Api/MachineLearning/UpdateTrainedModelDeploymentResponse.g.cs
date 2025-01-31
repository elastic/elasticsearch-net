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

internal sealed partial class UpdateTrainedModelDeploymentResponseConverter : System.Text.Json.Serialization.JsonConverter<UpdateTrainedModelDeploymentResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropAssignment = System.Text.Json.JsonEncodedText.Encode("assignment");

	public override UpdateTrainedModelDeploymentResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelAssignment> propAssignment = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAssignment.TryRead(ref reader, options, PropAssignment))
			{
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new UpdateTrainedModelDeploymentResponse
		{
			Assignment = propAssignment.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, UpdateTrainedModelDeploymentResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAssignment, value.Assignment);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(UpdateTrainedModelDeploymentResponseConverter))]
public sealed partial class UpdateTrainedModelDeploymentResponse : ElasticsearchResponse
{
	public Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelAssignment Assignment { get; init; }
}