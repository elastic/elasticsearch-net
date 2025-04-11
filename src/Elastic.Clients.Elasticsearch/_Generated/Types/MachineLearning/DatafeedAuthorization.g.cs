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

internal sealed partial class DatafeedAuthorizationConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.DatafeedAuthorization>
{
	private static readonly System.Text.Json.JsonEncodedText PropApiKey = System.Text.Json.JsonEncodedText.Encode("api_key");
	private static readonly System.Text.Json.JsonEncodedText PropRoles = System.Text.Json.JsonEncodedText.Encode("roles");
	private static readonly System.Text.Json.JsonEncodedText PropServiceAccount = System.Text.Json.JsonEncodedText.Encode("service_account");

	public override Elastic.Clients.Elasticsearch.MachineLearning.DatafeedAuthorization Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.ApiKeyAuthorization?> propApiKey = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<string>?> propRoles = default;
		LocalJsonValue<string?> propServiceAccount = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propApiKey.TryReadProperty(ref reader, options, PropApiKey, null))
			{
				continue;
			}

			if (propRoles.TryReadProperty(ref reader, options, PropRoles, static System.Collections.Generic.IReadOnlyCollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propServiceAccount.TryReadProperty(ref reader, options, PropServiceAccount, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.DatafeedAuthorization(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ApiKey = propApiKey.Value,
			Roles = propRoles.Value,
			ServiceAccount = propServiceAccount.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.DatafeedAuthorization value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropApiKey, value.ApiKey, null, null);
		writer.WriteProperty(options, PropRoles, value.Roles, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropServiceAccount, value.ServiceAccount, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.DatafeedAuthorizationConverter))]
public sealed partial class DatafeedAuthorization
{
#if NET7_0_OR_GREATER
	public DatafeedAuthorization()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public DatafeedAuthorization()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DatafeedAuthorization(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// If an API key was used for the most recent update to the datafeed, its name and identifier are listed in the response.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.ApiKeyAuthorization? ApiKey { get; set; }

	/// <summary>
	/// <para>
	/// If a user ID was used for the most recent update to the datafeed, its roles at the time of the update are listed in the response.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IReadOnlyCollection<string>? Roles { get; set; }

	/// <summary>
	/// <para>
	/// If a service account was used for the most recent update to the datafeed, the account name is listed in the response.
	/// </para>
	/// </summary>
	public string? ServiceAccount { get; set; }
}