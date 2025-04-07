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

namespace Elastic.Clients.Elasticsearch.TransformManagement;

internal sealed partial class TransformHealthIssueConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.TransformManagement.TransformHealthIssue>
{
	private static readonly System.Text.Json.JsonEncodedText PropCount = System.Text.Json.JsonEncodedText.Encode("count");
	private static readonly System.Text.Json.JsonEncodedText PropDetails = System.Text.Json.JsonEncodedText.Encode("details");
	private static readonly System.Text.Json.JsonEncodedText PropFirstOccurenceString = System.Text.Json.JsonEncodedText.Encode("first_occurence_string");
	private static readonly System.Text.Json.JsonEncodedText PropFirstOccurrence = System.Text.Json.JsonEncodedText.Encode("first_occurrence");
	private static readonly System.Text.Json.JsonEncodedText PropIssue = System.Text.Json.JsonEncodedText.Encode("issue");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");

	public override Elastic.Clients.Elasticsearch.TransformManagement.TransformHealthIssue Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int> propCount = default;
		LocalJsonValue<string?> propDetails = default;
		LocalJsonValue<System.DateTime?> propFirstOccurenceString = default;
		LocalJsonValue<System.DateTime?> propFirstOccurrence = default;
		LocalJsonValue<string> propIssue = default;
		LocalJsonValue<string> propType = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCount.TryReadProperty(ref reader, options, PropCount, null))
			{
				continue;
			}

			if (propDetails.TryReadProperty(ref reader, options, PropDetails, null))
			{
				continue;
			}

			if (propFirstOccurenceString.TryReadProperty(ref reader, options, PropFirstOccurenceString, static System.DateTime? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTime>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker))))
			{
				continue;
			}

			if (propFirstOccurrence.TryReadProperty(ref reader, options, PropFirstOccurrence, static System.DateTime? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTime>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker))))
			{
				continue;
			}

			if (propIssue.TryReadProperty(ref reader, options, PropIssue, null))
			{
				continue;
			}

			if (propType.TryReadProperty(ref reader, options, PropType, null))
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
		return new Elastic.Clients.Elasticsearch.TransformManagement.TransformHealthIssue(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Count = propCount.Value,
			Details = propDetails.Value,
			FirstOccurenceString = propFirstOccurenceString.Value,
			FirstOccurrence = propFirstOccurrence.Value,
			Issue = propIssue.Value,
			Type = propType.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.TransformManagement.TransformHealthIssue value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCount, value.Count, null, null);
		writer.WriteProperty(options, PropDetails, value.Details, null, null);
		writer.WriteProperty(options, PropFirstOccurenceString, value.FirstOccurenceString, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTime? v) => w.WriteValueEx<System.DateTime?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker)));
		writer.WriteProperty(options, PropFirstOccurrence, value.FirstOccurrence, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTime? v) => w.WriteValueEx<System.DateTime?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker)));
		writer.WriteProperty(options, PropIssue, value.Issue, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.TransformManagement.TransformHealthIssueConverter))]
public sealed partial class TransformHealthIssue
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TransformHealthIssue(int count, string issue, string type)
	{
		Count = count;
		Issue = issue;
		Type = type;
	}
#if NET7_0_OR_GREATER
	public TransformHealthIssue()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public TransformHealthIssue()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal TransformHealthIssue(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Number of times this issue has occurred since it started
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int Count { get; set; }

	/// <summary>
	/// <para>
	/// Details about the issue
	/// </para>
	/// </summary>
	public string? Details { get; set; }
	public System.DateTime? FirstOccurenceString { get; set; }

	/// <summary>
	/// <para>
	/// The timestamp this issue occurred for for the first time
	/// </para>
	/// </summary>
	public System.DateTime? FirstOccurrence { get; set; }

	/// <summary>
	/// <para>
	/// A description of the issue
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Issue { get; set; }

	/// <summary>
	/// <para>
	/// The type of the issue
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Type { get; set; }
}