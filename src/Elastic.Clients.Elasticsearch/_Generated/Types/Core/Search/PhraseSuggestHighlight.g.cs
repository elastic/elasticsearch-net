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

namespace Elastic.Clients.Elasticsearch.Core.Search;

internal sealed partial class PhraseSuggestHighlightConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlight>
{
	private static readonly System.Text.Json.JsonEncodedText PropPostTag = System.Text.Json.JsonEncodedText.Encode("post_tag");
	private static readonly System.Text.Json.JsonEncodedText PropPreTag = System.Text.Json.JsonEncodedText.Encode("pre_tag");

	public override Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlight Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propPostTag = default;
		LocalJsonValue<string> propPreTag = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propPostTag.TryReadProperty(ref reader, options, PropPostTag, null))
			{
				continue;
			}

			if (propPreTag.TryReadProperty(ref reader, options, PropPreTag, null))
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
		return new Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlight(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			PostTag = propPostTag.Value,
			PreTag = propPreTag.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlight value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropPostTag, value.PostTag, null, null);
		writer.WriteProperty(options, PropPreTag, value.PreTag, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlightConverter))]
public sealed partial class PhraseSuggestHighlight
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PhraseSuggestHighlight(string postTag, string preTag)
	{
		PostTag = postTag;
		PreTag = preTag;
	}
#if NET7_0_OR_GREATER
	public PhraseSuggestHighlight()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public PhraseSuggestHighlight()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PhraseSuggestHighlight(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Use in conjunction with <c>pre_tag</c> to define the HTML tags to use for the highlighted text.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string PostTag { get; set; }

	/// <summary>
	/// <para>
	/// Use in conjunction with <c>post_tag</c> to define the HTML tags to use for the highlighted text.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string PreTag { get; set; }
}

public readonly partial struct PhraseSuggestHighlightDescriptor
{
	internal Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlight Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PhraseSuggestHighlightDescriptor(Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlight instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PhraseSuggestHighlightDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlight(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlightDescriptor(Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlight instance) => new Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlightDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlight(Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlightDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Use in conjunction with <c>pre_tag</c> to define the HTML tags to use for the highlighted text.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlightDescriptor PostTag(string value)
	{
		Instance.PostTag = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Use in conjunction with <c>post_tag</c> to define the HTML tags to use for the highlighted text.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlightDescriptor PreTag(string value)
	{
		Instance.PreTag = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlight Build(System.Action<Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlightDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlightDescriptor(new Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlight(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}