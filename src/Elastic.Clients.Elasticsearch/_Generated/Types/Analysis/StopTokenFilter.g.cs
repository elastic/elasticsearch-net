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

namespace Elastic.Clients.Elasticsearch.Analysis;

internal sealed partial class StopTokenFilterConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Analysis.StopTokenFilter>
{
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreCase = System.Text.Json.JsonEncodedText.Encode("ignore_case");
	private static readonly System.Text.Json.JsonEncodedText PropRemoveTrailing = System.Text.Json.JsonEncodedText.Encode("remove_trailing");
	private static readonly System.Text.Json.JsonEncodedText PropStopwords = System.Text.Json.JsonEncodedText.Encode("stopwords");
	private static readonly System.Text.Json.JsonEncodedText PropStopwordsPath = System.Text.Json.JsonEncodedText.Encode("stopwords_path");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.Analysis.StopTokenFilter Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool?> propIgnoreCase = default;
		LocalJsonValue<bool?> propRemoveTrailing = default;
		LocalJsonValue<System.Collections.Generic.ICollection<string>?> propStopwords = default;
		LocalJsonValue<string?> propStopwordsPath = default;
		LocalJsonValue<string?> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propIgnoreCase.TryReadProperty(ref reader, options, PropIgnoreCase, null))
			{
				continue;
			}

			if (propRemoveTrailing.TryReadProperty(ref reader, options, PropRemoveTrailing, null))
			{
				continue;
			}

			if (propStopwords.TryReadProperty(ref reader, options, PropStopwords, static System.Collections.Generic.ICollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadSingleOrManyCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propStopwordsPath.TryReadProperty(ref reader, options, PropStopwordsPath, null))
			{
				continue;
			}

			if (reader.ValueTextEquals(PropType))
			{
				reader.Skip();
				continue;
			}

			if (propVersion.TryReadProperty(ref reader, options, PropVersion, null))
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
		return new Elastic.Clients.Elasticsearch.Analysis.StopTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			IgnoreCase = propIgnoreCase.Value,
			RemoveTrailing = propRemoveTrailing.Value,
			Stopwords = propStopwords.Value,
			StopwordsPath = propStopwordsPath.Value,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Analysis.StopTokenFilter value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropIgnoreCase, value.IgnoreCase, null, null);
		writer.WriteProperty(options, PropRemoveTrailing, value.RemoveTrailing, null, null);
		writer.WriteProperty(options, PropStopwords, value.Stopwords, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string>? v) => w.WriteSingleOrManyCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropStopwordsPath, value.StopwordsPath, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Analysis.StopTokenFilterConverter))]
public sealed partial class StopTokenFilter : Elastic.Clients.Elasticsearch.Analysis.ITokenFilter
{
#if NET7_0_OR_GREATER
	public StopTokenFilter()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public StopTokenFilter()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal StopTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public bool? IgnoreCase { get; set; }
	public bool? RemoveTrailing { get; set; }
	public System.Collections.Generic.ICollection<string>? Stopwords { get; set; }
	public string? StopwordsPath { get; set; }

	public string Type => "stop";

	public string? Version { get; set; }
}

public readonly partial struct StopTokenFilterDescriptor
{
	internal Elastic.Clients.Elasticsearch.Analysis.StopTokenFilter Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public StopTokenFilterDescriptor(Elastic.Clients.Elasticsearch.Analysis.StopTokenFilter instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public StopTokenFilterDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Analysis.StopTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Analysis.StopTokenFilterDescriptor(Elastic.Clients.Elasticsearch.Analysis.StopTokenFilter instance) => new Elastic.Clients.Elasticsearch.Analysis.StopTokenFilterDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Analysis.StopTokenFilter(Elastic.Clients.Elasticsearch.Analysis.StopTokenFilterDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Analysis.StopTokenFilterDescriptor IgnoreCase(bool? value = true)
	{
		Instance.IgnoreCase = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.StopTokenFilterDescriptor RemoveTrailing(bool? value = true)
	{
		Instance.RemoveTrailing = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.StopTokenFilterDescriptor Stopwords(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.Stopwords = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.StopTokenFilterDescriptor Stopwords(params string[] values)
	{
		Instance.Stopwords = [.. values];
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.StopTokenFilterDescriptor StopwordsPath(string? value)
	{
		Instance.StopwordsPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.StopTokenFilterDescriptor Version(string? value)
	{
		Instance.Version = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Analysis.StopTokenFilter Build(System.Action<Elastic.Clients.Elasticsearch.Analysis.StopTokenFilterDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Analysis.StopTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Analysis.StopTokenFilterDescriptor(new Elastic.Clients.Elasticsearch.Analysis.StopTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}