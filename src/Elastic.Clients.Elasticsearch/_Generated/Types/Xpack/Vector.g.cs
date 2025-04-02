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

namespace Elastic.Clients.Elasticsearch.Xpack;

internal sealed partial class VectorConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Xpack.Vector>
{
	private static readonly System.Text.Json.JsonEncodedText PropAvailable = System.Text.Json.JsonEncodedText.Encode("available");
	private static readonly System.Text.Json.JsonEncodedText PropDenseVectorDimsAvgCount = System.Text.Json.JsonEncodedText.Encode("dense_vector_dims_avg_count");
	private static readonly System.Text.Json.JsonEncodedText PropDenseVectorFieldsCount = System.Text.Json.JsonEncodedText.Encode("dense_vector_fields_count");
	private static readonly System.Text.Json.JsonEncodedText PropEnabled = System.Text.Json.JsonEncodedText.Encode("enabled");
	private static readonly System.Text.Json.JsonEncodedText PropSparseVectorFieldsCount = System.Text.Json.JsonEncodedText.Encode("sparse_vector_fields_count");

	public override Elastic.Clients.Elasticsearch.Xpack.Vector Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool> propAvailable = default;
		LocalJsonValue<int> propDenseVectorDimsAvgCount = default;
		LocalJsonValue<int> propDenseVectorFieldsCount = default;
		LocalJsonValue<bool> propEnabled = default;
		LocalJsonValue<int?> propSparseVectorFieldsCount = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAvailable.TryReadProperty(ref reader, options, PropAvailable, null))
			{
				continue;
			}

			if (propDenseVectorDimsAvgCount.TryReadProperty(ref reader, options, PropDenseVectorDimsAvgCount, null))
			{
				continue;
			}

			if (propDenseVectorFieldsCount.TryReadProperty(ref reader, options, PropDenseVectorFieldsCount, null))
			{
				continue;
			}

			if (propEnabled.TryReadProperty(ref reader, options, PropEnabled, null))
			{
				continue;
			}

			if (propSparseVectorFieldsCount.TryReadProperty(ref reader, options, PropSparseVectorFieldsCount, null))
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
		return new Elastic.Clients.Elasticsearch.Xpack.Vector(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Available = propAvailable.Value,
			DenseVectorDimsAvgCount = propDenseVectorDimsAvgCount.Value,
			DenseVectorFieldsCount = propDenseVectorFieldsCount.Value,
			Enabled = propEnabled.Value,
			SparseVectorFieldsCount = propSparseVectorFieldsCount.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Xpack.Vector value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAvailable, value.Available, null, null);
		writer.WriteProperty(options, PropDenseVectorDimsAvgCount, value.DenseVectorDimsAvgCount, null, null);
		writer.WriteProperty(options, PropDenseVectorFieldsCount, value.DenseVectorFieldsCount, null, null);
		writer.WriteProperty(options, PropEnabled, value.Enabled, null, null);
		writer.WriteProperty(options, PropSparseVectorFieldsCount, value.SparseVectorFieldsCount, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Xpack.VectorConverter))]
public sealed partial class Vector
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public Vector(bool available, int denseVectorDimsAvgCount, int denseVectorFieldsCount, bool enabled)
	{
		Available = available;
		DenseVectorDimsAvgCount = denseVectorDimsAvgCount;
		DenseVectorFieldsCount = denseVectorFieldsCount;
		Enabled = enabled;
	}
#if NET7_0_OR_GREATER
	public Vector()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public Vector()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Vector(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	bool Available { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int DenseVectorDimsAvgCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int DenseVectorFieldsCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool Enabled { get; set; }
	public int? SparseVectorFieldsCount { get; set; }
}