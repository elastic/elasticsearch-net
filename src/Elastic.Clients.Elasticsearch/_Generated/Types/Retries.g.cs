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

namespace Elastic.Clients.Elasticsearch;

internal sealed partial class RetriesConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Retries>
{
	private static readonly System.Text.Json.JsonEncodedText PropBulk = System.Text.Json.JsonEncodedText.Encode("bulk");
	private static readonly System.Text.Json.JsonEncodedText PropSearch = System.Text.Json.JsonEncodedText.Encode("search");

	public override Elastic.Clients.Elasticsearch.Retries Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long> propBulk = default;
		LocalJsonValue<long> propSearch = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBulk.TryReadProperty(ref reader, options, PropBulk, null))
			{
				continue;
			}

			if (propSearch.TryReadProperty(ref reader, options, PropSearch, null))
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
		return new Elastic.Clients.Elasticsearch.Retries(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Bulk = propBulk.Value,
			Search = propSearch.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Retries value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBulk, value.Bulk, null, null);
		writer.WriteProperty(options, PropSearch, value.Search, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.RetriesConverter))]
public sealed partial class Retries
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public Retries(long bulk, long search)
	{
		Bulk = bulk;
		Search = search;
	}
#if NET7_0_OR_GREATER
	public Retries()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public Retries()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Retries(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The number of bulk actions retried.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	long Bulk { get; set; }

	/// <summary>
	/// <para>
	/// The number of search actions retried.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	long Search { get; set; }
}