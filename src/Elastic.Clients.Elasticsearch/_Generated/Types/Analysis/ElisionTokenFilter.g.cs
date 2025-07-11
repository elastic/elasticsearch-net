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

internal sealed partial class ElisionTokenFilterConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Analysis.ElisionTokenFilter>
{
	private static readonly System.Text.Json.JsonEncodedText PropArticles = System.Text.Json.JsonEncodedText.Encode("articles");
	private static readonly System.Text.Json.JsonEncodedText PropArticlesCase = System.Text.Json.JsonEncodedText.Encode("articles_case");
	private static readonly System.Text.Json.JsonEncodedText PropArticlesPath = System.Text.Json.JsonEncodedText.Encode("articles_path");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.Analysis.ElisionTokenFilter Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.ICollection<string>?> propArticles = default;
		LocalJsonValue<bool?> propArticlesCase = default;
		LocalJsonValue<string?> propArticlesPath = default;
		LocalJsonValue<string?> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propArticles.TryReadProperty(ref reader, options, PropArticles, static System.Collections.Generic.ICollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propArticlesCase.TryReadProperty(ref reader, options, PropArticlesCase, static bool? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<bool>(o)))
			{
				continue;
			}

			if (propArticlesPath.TryReadProperty(ref reader, options, PropArticlesPath, null))
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
		return new Elastic.Clients.Elasticsearch.Analysis.ElisionTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Articles = propArticles.Value,
			ArticlesCase = propArticlesCase.Value,
			ArticlesPath = propArticlesPath.Value,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Analysis.ElisionTokenFilter value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropArticles, value.Articles, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropArticlesCase, value.ArticlesCase, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, bool? v) => w.WriteNullableValue<bool>(o, v));
		writer.WriteProperty(options, PropArticlesPath, value.ArticlesPath, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Analysis.ElisionTokenFilterConverter))]
public sealed partial class ElisionTokenFilter : Elastic.Clients.Elasticsearch.Analysis.ITokenFilter
{
#if NET7_0_OR_GREATER
	public ElisionTokenFilter()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public ElisionTokenFilter()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ElisionTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// List of elisions to remove.
	/// To be removed, the elision must be at the beginning of a token and be immediately followed by an apostrophe. Both the elision and apostrophe are removed.
	/// For custom <c>elision</c> filters, either this parameter or <c>articles_path</c> must be specified.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<string>? Articles { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c>, elision matching is case insensitive. If <c>false</c>, elision matching is case sensitive. Defaults to <c>false</c>.
	/// </para>
	/// </summary>
	public bool? ArticlesCase { get; set; }

	/// <summary>
	/// <para>
	/// Path to a file that contains a list of elisions to remove.
	/// This path must be absolute or relative to the <c>config</c> location, and the file must be UTF-8 encoded. Each elision in the file must be separated by a line break.
	/// To be removed, the elision must be at the beginning of a token and be immediately followed by an apostrophe. Both the elision and apostrophe are removed.
	/// For custom <c>elision</c> filters, either this parameter or <c>articles</c> must be specified.
	/// </para>
	/// </summary>
	public string? ArticlesPath { get; set; }

	public string Type => "elision";

	public string? Version { get; set; }
}

public readonly partial struct ElisionTokenFilterDescriptor
{
	internal Elastic.Clients.Elasticsearch.Analysis.ElisionTokenFilter Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ElisionTokenFilterDescriptor(Elastic.Clients.Elasticsearch.Analysis.ElisionTokenFilter instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ElisionTokenFilterDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Analysis.ElisionTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Analysis.ElisionTokenFilterDescriptor(Elastic.Clients.Elasticsearch.Analysis.ElisionTokenFilter instance) => new Elastic.Clients.Elasticsearch.Analysis.ElisionTokenFilterDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Analysis.ElisionTokenFilter(Elastic.Clients.Elasticsearch.Analysis.ElisionTokenFilterDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// List of elisions to remove.
	/// To be removed, the elision must be at the beginning of a token and be immediately followed by an apostrophe. Both the elision and apostrophe are removed.
	/// For custom <c>elision</c> filters, either this parameter or <c>articles_path</c> must be specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Analysis.ElisionTokenFilterDescriptor Articles(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.Articles = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// List of elisions to remove.
	/// To be removed, the elision must be at the beginning of a token and be immediately followed by an apostrophe. Both the elision and apostrophe are removed.
	/// For custom <c>elision</c> filters, either this parameter or <c>articles_path</c> must be specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Analysis.ElisionTokenFilterDescriptor Articles(params string[] values)
	{
		Instance.Articles = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, elision matching is case insensitive. If <c>false</c>, elision matching is case sensitive. Defaults to <c>false</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Analysis.ElisionTokenFilterDescriptor ArticlesCase(bool? value = true)
	{
		Instance.ArticlesCase = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Path to a file that contains a list of elisions to remove.
	/// This path must be absolute or relative to the <c>config</c> location, and the file must be UTF-8 encoded. Each elision in the file must be separated by a line break.
	/// To be removed, the elision must be at the beginning of a token and be immediately followed by an apostrophe. Both the elision and apostrophe are removed.
	/// For custom <c>elision</c> filters, either this parameter or <c>articles</c> must be specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Analysis.ElisionTokenFilterDescriptor ArticlesPath(string? value)
	{
		Instance.ArticlesPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.ElisionTokenFilterDescriptor Version(string? value)
	{
		Instance.Version = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Analysis.ElisionTokenFilter Build(System.Action<Elastic.Clients.Elasticsearch.Analysis.ElisionTokenFilterDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Analysis.ElisionTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Analysis.ElisionTokenFilterDescriptor(new Elastic.Clients.Elasticsearch.Analysis.ElisionTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}