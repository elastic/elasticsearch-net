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

internal sealed partial class SslConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Xpack.Ssl>
{
	private static readonly System.Text.Json.JsonEncodedText PropHttp = System.Text.Json.JsonEncodedText.Encode("http");
	private static readonly System.Text.Json.JsonEncodedText PropTransport = System.Text.Json.JsonEncodedText.Encode("transport");

	public override Elastic.Clients.Elasticsearch.Xpack.Ssl Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.FeatureToggle> propHttp = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.FeatureToggle> propTransport = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propHttp.TryReadProperty(ref reader, options, PropHttp, null))
			{
				continue;
			}

			if (propTransport.TryReadProperty(ref reader, options, PropTransport, null))
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
		return new Elastic.Clients.Elasticsearch.Xpack.Ssl(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Http = propHttp.Value,
			Transport = propTransport.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Xpack.Ssl value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropHttp, value.Http, null, null);
		writer.WriteProperty(options, PropTransport, value.Transport, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Xpack.SslConverter))]
public sealed partial class Ssl
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public Ssl(Elastic.Clients.Elasticsearch.Xpack.FeatureToggle http, Elastic.Clients.Elasticsearch.Xpack.FeatureToggle transport)
	{
		Http = http;
		Transport = transport;
	}
#if NET7_0_OR_GREATER
	public Ssl()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public Ssl()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Ssl(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.FeatureToggle Http { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.FeatureToggle Transport { get; set; }
}