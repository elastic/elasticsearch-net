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

internal sealed partial class SynonymTokenFilterConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilter>
{
	private static readonly System.Text.Json.JsonEncodedText PropExpand = System.Text.Json.JsonEncodedText.Encode("expand");
	private static readonly System.Text.Json.JsonEncodedText PropFormat = System.Text.Json.JsonEncodedText.Encode("format");
	private static readonly System.Text.Json.JsonEncodedText PropLenient = System.Text.Json.JsonEncodedText.Encode("lenient");
	private static readonly System.Text.Json.JsonEncodedText PropSynonyms = System.Text.Json.JsonEncodedText.Encode("synonyms");
	private static readonly System.Text.Json.JsonEncodedText PropSynonymsPath = System.Text.Json.JsonEncodedText.Encode("synonyms_path");
	private static readonly System.Text.Json.JsonEncodedText PropSynonymsSet = System.Text.Json.JsonEncodedText.Encode("synonyms_set");
	private static readonly System.Text.Json.JsonEncodedText PropTokenizer = System.Text.Json.JsonEncodedText.Encode("tokenizer");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");
	private static readonly System.Text.Json.JsonEncodedText PropUpdateable = System.Text.Json.JsonEncodedText.Encode("updateable");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilter Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool?> propExpand = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Analysis.SynonymFormat?> propFormat = default;
		LocalJsonValue<bool?> propLenient = default;
		LocalJsonValue<System.Collections.Generic.ICollection<string>?> propSynonyms = default;
		LocalJsonValue<string?> propSynonymsPath = default;
		LocalJsonValue<string?> propSynonymsSet = default;
		LocalJsonValue<string?> propTokenizer = default;
		LocalJsonValue<bool?> propUpdateable = default;
		LocalJsonValue<string?> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propExpand.TryReadProperty(ref reader, options, PropExpand, null))
			{
				continue;
			}

			if (propFormat.TryReadProperty(ref reader, options, PropFormat, null))
			{
				continue;
			}

			if (propLenient.TryReadProperty(ref reader, options, PropLenient, null))
			{
				continue;
			}

			if (propSynonyms.TryReadProperty(ref reader, options, PropSynonyms, static System.Collections.Generic.ICollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propSynonymsPath.TryReadProperty(ref reader, options, PropSynonymsPath, null))
			{
				continue;
			}

			if (propSynonymsSet.TryReadProperty(ref reader, options, PropSynonymsSet, null))
			{
				continue;
			}

			if (propTokenizer.TryReadProperty(ref reader, options, PropTokenizer, null))
			{
				continue;
			}

			if (reader.ValueTextEquals(PropType))
			{
				reader.Skip();
				continue;
			}

			if (propUpdateable.TryReadProperty(ref reader, options, PropUpdateable, null))
			{
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
		return new Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Expand = propExpand.Value,
			Format = propFormat.Value,
			Lenient = propLenient.Value,
			Synonyms = propSynonyms.Value,
			SynonymsPath = propSynonymsPath.Value,
			SynonymsSet = propSynonymsSet.Value,
			Tokenizer = propTokenizer.Value,
			Updateable = propUpdateable.Value,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilter value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropExpand, value.Expand, null, null);
		writer.WriteProperty(options, PropFormat, value.Format, null, null);
		writer.WriteProperty(options, PropLenient, value.Lenient, null, null);
		writer.WriteProperty(options, PropSynonyms, value.Synonyms, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropSynonymsPath, value.SynonymsPath, null, null);
		writer.WriteProperty(options, PropSynonymsSet, value.SynonymsSet, null, null);
		writer.WriteProperty(options, PropTokenizer, value.Tokenizer, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteProperty(options, PropUpdateable, value.Updateable, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilterConverter))]
public sealed partial class SynonymTokenFilter : Elastic.Clients.Elasticsearch.Analysis.ITokenFilter
{
#if NET7_0_OR_GREATER
	public SynonymTokenFilter()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public SynonymTokenFilter()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal SynonymTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public bool? Expand { get; set; }
	public Elastic.Clients.Elasticsearch.Analysis.SynonymFormat? Format { get; set; }
	public bool? Lenient { get; set; }
	public System.Collections.Generic.ICollection<string>? Synonyms { get; set; }
	public string? SynonymsPath { get; set; }
	public string? SynonymsSet { get; set; }
	public string? Tokenizer { get; set; }

	public string Type => "synonym";

	public bool? Updateable { get; set; }
	public string? Version { get; set; }
}

public readonly partial struct SynonymTokenFilterDescriptor
{
	internal Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilter Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SynonymTokenFilterDescriptor(Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilter instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SynonymTokenFilterDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilterDescriptor(Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilter instance) => new Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilterDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilter(Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilterDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilterDescriptor Expand(bool? value = true)
	{
		Instance.Expand = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilterDescriptor Format(Elastic.Clients.Elasticsearch.Analysis.SynonymFormat? value)
	{
		Instance.Format = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilterDescriptor Lenient(bool? value = true)
	{
		Instance.Lenient = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilterDescriptor Synonyms(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.Synonyms = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilterDescriptor Synonyms()
	{
		Instance.Synonyms = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilterDescriptor Synonyms(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString>? action)
	{
		Instance.Synonyms = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilterDescriptor Synonyms(params string[] values)
	{
		Instance.Synonyms = [.. values];
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilterDescriptor SynonymsPath(string? value)
	{
		Instance.SynonymsPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilterDescriptor SynonymsSet(string? value)
	{
		Instance.SynonymsSet = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilterDescriptor Tokenizer(string? value)
	{
		Instance.Tokenizer = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilterDescriptor Updateable(bool? value = true)
	{
		Instance.Updateable = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilterDescriptor Version(string? value)
	{
		Instance.Version = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilter Build(System.Action<Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilterDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilterDescriptor(new Elastic.Clients.Elasticsearch.Analysis.SynonymTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}