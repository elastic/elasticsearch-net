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

namespace Elastic.Clients.Elasticsearch.LicenseManagement;

internal sealed partial class PostResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.LicenseManagement.PostResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropAcknowledge = System.Text.Json.JsonEncodedText.Encode("acknowledge");
	private static readonly System.Text.Json.JsonEncodedText PropAcknowledged = System.Text.Json.JsonEncodedText.Encode("acknowledged");
	private static readonly System.Text.Json.JsonEncodedText PropLicenseStatus = System.Text.Json.JsonEncodedText.Encode("license_status");

	public override Elastic.Clients.Elasticsearch.LicenseManagement.PostResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.LicenseManagement.Acknowledgement?> propAcknowledge = default;
		LocalJsonValue<bool> propAcknowledged = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.LicenseManagement.LicenseStatus> propLicenseStatus = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAcknowledge.TryReadProperty(ref reader, options, PropAcknowledge, null))
			{
				continue;
			}

			if (propAcknowledged.TryReadProperty(ref reader, options, PropAcknowledged, null))
			{
				continue;
			}

			if (propLicenseStatus.TryReadProperty(ref reader, options, PropLicenseStatus, null))
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
		return new Elastic.Clients.Elasticsearch.LicenseManagement.PostResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Acknowledge = propAcknowledge.Value,
			Acknowledged = propAcknowledged.Value,
			LicenseStatus = propLicenseStatus.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.LicenseManagement.PostResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAcknowledge, value.Acknowledge, null, null);
		writer.WriteProperty(options, PropAcknowledged, value.Acknowledged, null, null);
		writer.WriteProperty(options, PropLicenseStatus, value.LicenseStatus, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.LicenseManagement.PostResponseConverter))]
public sealed partial class PostResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PostResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PostResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public Elastic.Clients.Elasticsearch.LicenseManagement.Acknowledgement? Acknowledge { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		bool Acknowledged { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		Elastic.Clients.Elasticsearch.LicenseManagement.LicenseStatus LicenseStatus { get; set; }
}