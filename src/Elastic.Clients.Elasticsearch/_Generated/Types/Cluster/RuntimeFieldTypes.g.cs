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

namespace Elastic.Clients.Elasticsearch.Cluster;

internal sealed partial class RuntimeFieldTypesConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Cluster.RuntimeFieldTypes>
{
	private static readonly System.Text.Json.JsonEncodedText PropCharsMax = System.Text.Json.JsonEncodedText.Encode("chars_max");
	private static readonly System.Text.Json.JsonEncodedText PropCharsTotal = System.Text.Json.JsonEncodedText.Encode("chars_total");
	private static readonly System.Text.Json.JsonEncodedText PropCount = System.Text.Json.JsonEncodedText.Encode("count");
	private static readonly System.Text.Json.JsonEncodedText PropDocMax = System.Text.Json.JsonEncodedText.Encode("doc_max");
	private static readonly System.Text.Json.JsonEncodedText PropDocTotal = System.Text.Json.JsonEncodedText.Encode("doc_total");
	private static readonly System.Text.Json.JsonEncodedText PropIndexCount = System.Text.Json.JsonEncodedText.Encode("index_count");
	private static readonly System.Text.Json.JsonEncodedText PropLang = System.Text.Json.JsonEncodedText.Encode("lang");
	private static readonly System.Text.Json.JsonEncodedText PropLinesMax = System.Text.Json.JsonEncodedText.Encode("lines_max");
	private static readonly System.Text.Json.JsonEncodedText PropLinesTotal = System.Text.Json.JsonEncodedText.Encode("lines_total");
	private static readonly System.Text.Json.JsonEncodedText PropName = System.Text.Json.JsonEncodedText.Encode("name");
	private static readonly System.Text.Json.JsonEncodedText PropScriptlessCount = System.Text.Json.JsonEncodedText.Encode("scriptless_count");
	private static readonly System.Text.Json.JsonEncodedText PropShadowedCount = System.Text.Json.JsonEncodedText.Encode("shadowed_count");
	private static readonly System.Text.Json.JsonEncodedText PropSourceMax = System.Text.Json.JsonEncodedText.Encode("source_max");
	private static readonly System.Text.Json.JsonEncodedText PropSourceTotal = System.Text.Json.JsonEncodedText.Encode("source_total");

	public override Elastic.Clients.Elasticsearch.Cluster.RuntimeFieldTypes Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int> propCharsMax = default;
		LocalJsonValue<int> propCharsTotal = default;
		LocalJsonValue<int> propCount = default;
		LocalJsonValue<int> propDocMax = default;
		LocalJsonValue<int> propDocTotal = default;
		LocalJsonValue<int> propIndexCount = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<string>> propLang = default;
		LocalJsonValue<int> propLinesMax = default;
		LocalJsonValue<int> propLinesTotal = default;
		LocalJsonValue<string> propName = default;
		LocalJsonValue<int> propScriptlessCount = default;
		LocalJsonValue<int> propShadowedCount = default;
		LocalJsonValue<int> propSourceMax = default;
		LocalJsonValue<int> propSourceTotal = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCharsMax.TryReadProperty(ref reader, options, PropCharsMax, null))
			{
				continue;
			}

			if (propCharsTotal.TryReadProperty(ref reader, options, PropCharsTotal, null))
			{
				continue;
			}

			if (propCount.TryReadProperty(ref reader, options, PropCount, null))
			{
				continue;
			}

			if (propDocMax.TryReadProperty(ref reader, options, PropDocMax, null))
			{
				continue;
			}

			if (propDocTotal.TryReadProperty(ref reader, options, PropDocTotal, null))
			{
				continue;
			}

			if (propIndexCount.TryReadProperty(ref reader, options, PropIndexCount, null))
			{
				continue;
			}

			if (propLang.TryReadProperty(ref reader, options, PropLang, static System.Collections.Generic.IReadOnlyCollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)!))
			{
				continue;
			}

			if (propLinesMax.TryReadProperty(ref reader, options, PropLinesMax, null))
			{
				continue;
			}

			if (propLinesTotal.TryReadProperty(ref reader, options, PropLinesTotal, null))
			{
				continue;
			}

			if (propName.TryReadProperty(ref reader, options, PropName, null))
			{
				continue;
			}

			if (propScriptlessCount.TryReadProperty(ref reader, options, PropScriptlessCount, null))
			{
				continue;
			}

			if (propShadowedCount.TryReadProperty(ref reader, options, PropShadowedCount, null))
			{
				continue;
			}

			if (propSourceMax.TryReadProperty(ref reader, options, PropSourceMax, null))
			{
				continue;
			}

			if (propSourceTotal.TryReadProperty(ref reader, options, PropSourceTotal, null))
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
		return new Elastic.Clients.Elasticsearch.Cluster.RuntimeFieldTypes(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			CharsMax = propCharsMax.Value,
			CharsTotal = propCharsTotal.Value,
			Count = propCount.Value,
			DocMax = propDocMax.Value,
			DocTotal = propDocTotal.Value,
			IndexCount = propIndexCount.Value,
			Lang = propLang.Value,
			LinesMax = propLinesMax.Value,
			LinesTotal = propLinesTotal.Value,
			Name = propName.Value,
			ScriptlessCount = propScriptlessCount.Value,
			ShadowedCount = propShadowedCount.Value,
			SourceMax = propSourceMax.Value,
			SourceTotal = propSourceTotal.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Cluster.RuntimeFieldTypes value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCharsMax, value.CharsMax, null, null);
		writer.WriteProperty(options, PropCharsTotal, value.CharsTotal, null, null);
		writer.WriteProperty(options, PropCount, value.Count, null, null);
		writer.WriteProperty(options, PropDocMax, value.DocMax, null, null);
		writer.WriteProperty(options, PropDocTotal, value.DocTotal, null, null);
		writer.WriteProperty(options, PropIndexCount, value.IndexCount, null, null);
		writer.WriteProperty(options, PropLang, value.Lang, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<string> v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropLinesMax, value.LinesMax, null, null);
		writer.WriteProperty(options, PropLinesTotal, value.LinesTotal, null, null);
		writer.WriteProperty(options, PropName, value.Name, null, null);
		writer.WriteProperty(options, PropScriptlessCount, value.ScriptlessCount, null, null);
		writer.WriteProperty(options, PropShadowedCount, value.ShadowedCount, null, null);
		writer.WriteProperty(options, PropSourceMax, value.SourceMax, null, null);
		writer.WriteProperty(options, PropSourceTotal, value.SourceTotal, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Cluster.RuntimeFieldTypesConverter))]
public sealed partial class RuntimeFieldTypes
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RuntimeFieldTypes(int charsMax, int charsTotal, int count, int docMax, int docTotal, int indexCount, System.Collections.Generic.IReadOnlyCollection<string> lang, int linesMax, int linesTotal, string name, int scriptlessCount, int shadowedCount, int sourceMax, int sourceTotal)
	{
		CharsMax = charsMax;
		CharsTotal = charsTotal;
		Count = count;
		DocMax = docMax;
		DocTotal = docTotal;
		IndexCount = indexCount;
		Lang = lang;
		LinesMax = linesMax;
		LinesTotal = linesTotal;
		Name = name;
		ScriptlessCount = scriptlessCount;
		ShadowedCount = shadowedCount;
		SourceMax = sourceMax;
		SourceTotal = sourceTotal;
	}
#if NET7_0_OR_GREATER
	public RuntimeFieldTypes()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public RuntimeFieldTypes()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal RuntimeFieldTypes(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Maximum number of characters for a single runtime field script.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int CharsMax { get; set; }

	/// <summary>
	/// <para>
	/// Total number of characters for the scripts that define the current runtime field data type.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int CharsTotal { get; set; }

	/// <summary>
	/// <para>
	/// Number of runtime fields mapped to the field data type in selected nodes.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int Count { get; set; }

	/// <summary>
	/// <para>
	/// Maximum number of accesses to doc_values for a single runtime field script
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int DocMax { get; set; }

	/// <summary>
	/// <para>
	/// Total number of accesses to doc_values for the scripts that define the current runtime field data type.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int DocTotal { get; set; }

	/// <summary>
	/// <para>
	/// Number of indices containing a mapping of the runtime field data type in selected nodes.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int IndexCount { get; set; }

	/// <summary>
	/// <para>
	/// Script languages used for the runtime fields scripts.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<string> Lang { get; set; }

	/// <summary>
	/// <para>
	/// Maximum number of lines for a single runtime field script.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int LinesMax { get; set; }

	/// <summary>
	/// <para>
	/// Total number of lines for the scripts that define the current runtime field data type.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int LinesTotal { get; set; }

	/// <summary>
	/// <para>
	/// Field data type used in selected nodes.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Name { get; set; }

	/// <summary>
	/// <para>
	/// Number of runtime fields that don’t declare a script.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int ScriptlessCount { get; set; }

	/// <summary>
	/// <para>
	/// Number of runtime fields that shadow an indexed field.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int ShadowedCount { get; set; }

	/// <summary>
	/// <para>
	/// Maximum number of accesses to _source for a single runtime field script.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int SourceMax { get; set; }

	/// <summary>
	/// <para>
	/// Total number of accesses to _source for the scripts that define the current runtime field data type.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int SourceTotal { get; set; }
}