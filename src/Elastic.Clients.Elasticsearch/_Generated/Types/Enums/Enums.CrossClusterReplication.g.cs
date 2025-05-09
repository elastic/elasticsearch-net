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

namespace Elastic.Clients.Elasticsearch.CrossClusterReplication;

internal sealed partial class FollowerIndexStatusConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndexStatus>
{
	private static readonly System.Text.Json.JsonEncodedText MemberActive = System.Text.Json.JsonEncodedText.Encode("active");
	private static readonly System.Text.Json.JsonEncodedText MemberPaused = System.Text.Json.JsonEncodedText.Encode("paused");

	public override Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndexStatus Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		if (reader.ValueTextEquals(MemberActive))
		{
			return Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndexStatus.Active;
		}

		if (reader.ValueTextEquals(MemberPaused))
		{
			return Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndexStatus.Paused;
		}

		var value = reader.GetString()!;
		if (string.Equals(value, MemberActive.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndexStatus.Active;
		}

		if (string.Equals(value, MemberPaused.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndexStatus.Paused;
		}

		throw new System.Text.Json.JsonException($"Unknown member '{value}' for enum '{nameof(Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndexStatus)}'.");
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndexStatus value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value)
		{
			case Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndexStatus.Active:
				writer.WriteStringValue(MemberActive);
				break;
			case Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndexStatus.Paused:
				writer.WriteStringValue(MemberPaused);
				break;
			default:
				throw new System.Text.Json.JsonException($"Invalid value '{value}' for enum '{nameof(Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndexStatus)}'.");
		}
	}

	public override Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndexStatus ReadAsPropertyName(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return Read(ref reader, typeToConvert, options);
	}

	public override void WriteAsPropertyName(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndexStatus value, System.Text.Json.JsonSerializerOptions options)
	{
		Write(writer, value, options);
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndexStatusConverter))]
public enum FollowerIndexStatus
{
	[System.Runtime.Serialization.EnumMember(Value = "active")]
	Active,
	[System.Runtime.Serialization.EnumMember(Value = "paused")]
	Paused
}