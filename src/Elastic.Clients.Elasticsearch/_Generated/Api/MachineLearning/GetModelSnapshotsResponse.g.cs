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

internal sealed partial class GetModelSnapshotsResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.GetModelSnapshotsResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropCount = System.Text.Json.JsonEncodedText.Encode("count");
	private static readonly System.Text.Json.JsonEncodedText PropModelSnapshots = System.Text.Json.JsonEncodedText.Encode("model_snapshots");

	public override Elastic.Clients.Elasticsearch.MachineLearning.GetModelSnapshotsResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long> propCount = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.ModelSnapshot>> propModelSnapshots = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCount.TryReadProperty(ref reader, options, PropCount, null))
			{
				continue;
			}

			if (propModelSnapshots.TryReadProperty(ref reader, options, PropModelSnapshots, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.ModelSnapshot> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.ModelSnapshot>(o, null)!))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.GetModelSnapshotsResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Count = propCount.Value,
			ModelSnapshots = propModelSnapshots.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.GetModelSnapshotsResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCount, value.Count, null, null);
		writer.WriteProperty(options, PropModelSnapshots, value.ModelSnapshots, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.ModelSnapshot> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.ModelSnapshot>(o, v, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.GetModelSnapshotsResponseConverter))]
public sealed partial class GetModelSnapshotsResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetModelSnapshotsResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetModelSnapshotsResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
		required
#endif
		long Count { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.ModelSnapshot> ModelSnapshots { get; set; }
}