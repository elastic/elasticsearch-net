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

namespace Elastic.Clients.Elasticsearch.Security;

internal sealed partial class GetServiceCredentialsResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.GetServiceCredentialsResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropCount = System.Text.Json.JsonEncodedText.Encode("count");
	private static readonly System.Text.Json.JsonEncodedText PropNodesCredentials = System.Text.Json.JsonEncodedText.Encode("nodes_credentials");
	private static readonly System.Text.Json.JsonEncodedText PropServiceAccount = System.Text.Json.JsonEncodedText.Encode("service_account");
	private static readonly System.Text.Json.JsonEncodedText PropTokens = System.Text.Json.JsonEncodedText.Encode("tokens");

	public override Elastic.Clients.Elasticsearch.Security.GetServiceCredentialsResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int> propCount = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Security.NodesCredentials> propNodesCredentials = default;
		LocalJsonValue<string> propServiceAccount = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IReadOnlyDictionary<string, object>>> propTokens = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCount.TryReadProperty(ref reader, options, PropCount, null))
			{
				continue;
			}

			if (propNodesCredentials.TryReadProperty(ref reader, options, PropNodesCredentials, null))
			{
				continue;
			}

			if (propServiceAccount.TryReadProperty(ref reader, options, PropServiceAccount, null))
			{
				continue;
			}

			if (propTokens.TryReadProperty(ref reader, options, PropTokens, static System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IReadOnlyDictionary<string, object>> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, System.Collections.Generic.IReadOnlyDictionary<string, object>>(o, null, static System.Collections.Generic.IReadOnlyDictionary<string, object> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)!)!))
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
		return new Elastic.Clients.Elasticsearch.Security.GetServiceCredentialsResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Count = propCount.Value,
			NodesCredentials = propNodesCredentials.Value,
			ServiceAccount = propServiceAccount.Value,
			Tokens = propTokens.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.GetServiceCredentialsResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCount, value.Count, null, null);
		writer.WriteProperty(options, PropNodesCredentials, value.NodesCredentials, null, null);
		writer.WriteProperty(options, PropServiceAccount, value.ServiceAccount, null, null);
		writer.WriteProperty(options, PropTokens, value.Tokens, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IReadOnlyDictionary<string, object>> v) => w.WriteDictionaryValue<string, System.Collections.Generic.IReadOnlyDictionary<string, object>>(o, v, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, object> v) => w.WriteDictionaryValue<string, object>(o, v, null, null)));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.GetServiceCredentialsResponseConverter))]
public sealed partial class GetServiceCredentialsResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetServiceCredentialsResponse(int count, Elastic.Clients.Elasticsearch.Security.NodesCredentials nodesCredentials, string serviceAccount, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IReadOnlyDictionary<string, object>> tokens)
	{
		Count = count;
		NodesCredentials = nodesCredentials;
		ServiceAccount = serviceAccount;
		Tokens = tokens;
	}

	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetServiceCredentialsResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetServiceCredentialsResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
		required
#endif
		int Count { get; set; }

	/// <summary>
	/// <para>
	/// Service account credentials collected from all nodes of the cluster.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Security.NodesCredentials NodesCredentials { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string ServiceAccount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IReadOnlyDictionary<string, object>> Tokens { get; set; }
}