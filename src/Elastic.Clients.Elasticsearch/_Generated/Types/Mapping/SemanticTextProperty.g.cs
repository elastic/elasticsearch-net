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

namespace Elastic.Clients.Elasticsearch.Mapping;

internal sealed partial class SemanticTextPropertyConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Mapping.SemanticTextProperty>
{
	private static readonly System.Text.Json.JsonEncodedText PropInferenceId = System.Text.Json.JsonEncodedText.Encode("inference_id");
	private static readonly System.Text.Json.JsonEncodedText PropMeta = System.Text.Json.JsonEncodedText.Encode("meta");
	private static readonly System.Text.Json.JsonEncodedText PropSearchInferenceId = System.Text.Json.JsonEncodedText.Encode("search_inference_id");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");

	public override Elastic.Clients.Elasticsearch.Mapping.SemanticTextProperty Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Id?> propInferenceId = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<string, string>?> propMeta = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Id?> propSearchInferenceId = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propInferenceId.TryReadProperty(ref reader, options, PropInferenceId, null))
			{
				continue;
			}

			if (propMeta.TryReadProperty(ref reader, options, PropMeta, static System.Collections.Generic.IDictionary<string, string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, string>(o, null, null)))
			{
				continue;
			}

			if (propSearchInferenceId.TryReadProperty(ref reader, options, PropSearchInferenceId, null))
			{
				continue;
			}

			if (reader.ValueTextEquals(PropType))
			{
				reader.Skip();
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
		return new Elastic.Clients.Elasticsearch.Mapping.SemanticTextProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			InferenceId = propInferenceId.Value,
			Meta = propMeta.Value,
			SearchInferenceId = propSearchInferenceId.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Mapping.SemanticTextProperty value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropInferenceId, value.InferenceId, null, null);
		writer.WriteProperty(options, PropMeta, value.Meta, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, string>? v) => w.WriteDictionaryValue<string, string>(o, v, null, null));
		writer.WriteProperty(options, PropSearchInferenceId, value.SearchInferenceId, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Mapping.SemanticTextPropertyConverter))]
public sealed partial class SemanticTextProperty : Elastic.Clients.Elasticsearch.Mapping.IProperty
{
#if NET7_0_OR_GREATER
	public SemanticTextProperty()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public SemanticTextProperty()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal SemanticTextProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Inference endpoint that will be used to generate embeddings for the field.
	/// This parameter cannot be updated. Use the Create inference API to create the endpoint.
	/// If <c>search_inference_id</c> is specified, the inference endpoint will only be used at index time.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Id? InferenceId { get; set; }
	public System.Collections.Generic.IDictionary<string, string>? Meta { get; set; }

	/// <summary>
	/// <para>
	/// Inference endpoint that will be used to generate embeddings at query time.
	/// You can update this parameter by using the Update mapping API. Use the Create inference API to create the endpoint.
	/// If not specified, the inference endpoint defined by inference_id will be used at both index and query time.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Id? SearchInferenceId { get; set; }

	public string Type => "semantic_text";
}

public readonly partial struct SemanticTextPropertyDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Mapping.SemanticTextProperty Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SemanticTextPropertyDescriptor(Elastic.Clients.Elasticsearch.Mapping.SemanticTextProperty instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SemanticTextPropertyDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Mapping.SemanticTextProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Mapping.SemanticTextPropertyDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Mapping.SemanticTextProperty instance) => new Elastic.Clients.Elasticsearch.Mapping.SemanticTextPropertyDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Mapping.SemanticTextProperty(Elastic.Clients.Elasticsearch.Mapping.SemanticTextPropertyDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Inference endpoint that will be used to generate embeddings for the field.
	/// This parameter cannot be updated. Use the Create inference API to create the endpoint.
	/// If <c>search_inference_id</c> is specified, the inference endpoint will only be used at index time.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.SemanticTextPropertyDescriptor<TDocument> InferenceId(Elastic.Clients.Elasticsearch.Id? value)
	{
		Instance.InferenceId = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.SemanticTextPropertyDescriptor<TDocument> Meta(System.Collections.Generic.IDictionary<string, string>? value)
	{
		Instance.Meta = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.SemanticTextPropertyDescriptor<TDocument> Meta()
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringString.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.SemanticTextPropertyDescriptor<TDocument> Meta(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringString>? action)
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.SemanticTextPropertyDescriptor<TDocument> AddMeta(string key, string value)
	{
		Instance.Meta ??= new System.Collections.Generic.Dictionary<string, string>();
		Instance.Meta.Add(key, value);
		return this;
	}

	/// <summary>
	/// <para>
	/// Inference endpoint that will be used to generate embeddings at query time.
	/// You can update this parameter by using the Update mapping API. Use the Create inference API to create the endpoint.
	/// If not specified, the inference endpoint defined by inference_id will be used at both index and query time.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.SemanticTextPropertyDescriptor<TDocument> SearchInferenceId(Elastic.Clients.Elasticsearch.Id? value)
	{
		Instance.SearchInferenceId = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Mapping.SemanticTextProperty Build(System.Action<Elastic.Clients.Elasticsearch.Mapping.SemanticTextPropertyDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Mapping.SemanticTextProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Mapping.SemanticTextPropertyDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Mapping.SemanticTextProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct SemanticTextPropertyDescriptor
{
	internal Elastic.Clients.Elasticsearch.Mapping.SemanticTextProperty Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SemanticTextPropertyDescriptor(Elastic.Clients.Elasticsearch.Mapping.SemanticTextProperty instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SemanticTextPropertyDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Mapping.SemanticTextProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Mapping.SemanticTextPropertyDescriptor(Elastic.Clients.Elasticsearch.Mapping.SemanticTextProperty instance) => new Elastic.Clients.Elasticsearch.Mapping.SemanticTextPropertyDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Mapping.SemanticTextProperty(Elastic.Clients.Elasticsearch.Mapping.SemanticTextPropertyDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Inference endpoint that will be used to generate embeddings for the field.
	/// This parameter cannot be updated. Use the Create inference API to create the endpoint.
	/// If <c>search_inference_id</c> is specified, the inference endpoint will only be used at index time.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.SemanticTextPropertyDescriptor InferenceId(Elastic.Clients.Elasticsearch.Id? value)
	{
		Instance.InferenceId = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.SemanticTextPropertyDescriptor Meta(System.Collections.Generic.IDictionary<string, string>? value)
	{
		Instance.Meta = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.SemanticTextPropertyDescriptor Meta()
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringString.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.SemanticTextPropertyDescriptor Meta(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringString>? action)
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.SemanticTextPropertyDescriptor AddMeta(string key, string value)
	{
		Instance.Meta ??= new System.Collections.Generic.Dictionary<string, string>();
		Instance.Meta.Add(key, value);
		return this;
	}

	/// <summary>
	/// <para>
	/// Inference endpoint that will be used to generate embeddings at query time.
	/// You can update this parameter by using the Update mapping API. Use the Create inference API to create the endpoint.
	/// If not specified, the inference endpoint defined by inference_id will be used at both index and query time.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.SemanticTextPropertyDescriptor SearchInferenceId(Elastic.Clients.Elasticsearch.Id? value)
	{
		Instance.SearchInferenceId = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Mapping.SemanticTextProperty Build(System.Action<Elastic.Clients.Elasticsearch.Mapping.SemanticTextPropertyDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Mapping.SemanticTextProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Mapping.SemanticTextPropertyDescriptor(new Elastic.Clients.Elasticsearch.Mapping.SemanticTextProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}