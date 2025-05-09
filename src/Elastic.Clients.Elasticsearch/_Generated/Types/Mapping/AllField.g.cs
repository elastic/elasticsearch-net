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

internal sealed partial class AllFieldConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Mapping.AllField>
{
	private static readonly System.Text.Json.JsonEncodedText PropAnalyzer = System.Text.Json.JsonEncodedText.Encode("analyzer");
	private static readonly System.Text.Json.JsonEncodedText PropEnabled = System.Text.Json.JsonEncodedText.Encode("enabled");
	private static readonly System.Text.Json.JsonEncodedText PropOmitNorms = System.Text.Json.JsonEncodedText.Encode("omit_norms");
	private static readonly System.Text.Json.JsonEncodedText PropSearchAnalyzer = System.Text.Json.JsonEncodedText.Encode("search_analyzer");
	private static readonly System.Text.Json.JsonEncodedText PropSimilarity = System.Text.Json.JsonEncodedText.Encode("similarity");
	private static readonly System.Text.Json.JsonEncodedText PropStore = System.Text.Json.JsonEncodedText.Encode("store");
	private static readonly System.Text.Json.JsonEncodedText PropStoreTermVectorOffsets = System.Text.Json.JsonEncodedText.Encode("store_term_vector_offsets");
	private static readonly System.Text.Json.JsonEncodedText PropStoreTermVectorPayloads = System.Text.Json.JsonEncodedText.Encode("store_term_vector_payloads");
	private static readonly System.Text.Json.JsonEncodedText PropStoreTermVectorPositions = System.Text.Json.JsonEncodedText.Encode("store_term_vector_positions");
	private static readonly System.Text.Json.JsonEncodedText PropStoreTermVectors = System.Text.Json.JsonEncodedText.Encode("store_term_vectors");

	public override Elastic.Clients.Elasticsearch.Mapping.AllField Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propAnalyzer = default;
		LocalJsonValue<bool> propEnabled = default;
		LocalJsonValue<bool> propOmitNorms = default;
		LocalJsonValue<string> propSearchAnalyzer = default;
		LocalJsonValue<string> propSimilarity = default;
		LocalJsonValue<bool> propStore = default;
		LocalJsonValue<bool> propStoreTermVectorOffsets = default;
		LocalJsonValue<bool> propStoreTermVectorPayloads = default;
		LocalJsonValue<bool> propStoreTermVectorPositions = default;
		LocalJsonValue<bool> propStoreTermVectors = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAnalyzer.TryReadProperty(ref reader, options, PropAnalyzer, null))
			{
				continue;
			}

			if (propEnabled.TryReadProperty(ref reader, options, PropEnabled, null))
			{
				continue;
			}

			if (propOmitNorms.TryReadProperty(ref reader, options, PropOmitNorms, null))
			{
				continue;
			}

			if (propSearchAnalyzer.TryReadProperty(ref reader, options, PropSearchAnalyzer, null))
			{
				continue;
			}

			if (propSimilarity.TryReadProperty(ref reader, options, PropSimilarity, null))
			{
				continue;
			}

			if (propStore.TryReadProperty(ref reader, options, PropStore, null))
			{
				continue;
			}

			if (propStoreTermVectorOffsets.TryReadProperty(ref reader, options, PropStoreTermVectorOffsets, null))
			{
				continue;
			}

			if (propStoreTermVectorPayloads.TryReadProperty(ref reader, options, PropStoreTermVectorPayloads, null))
			{
				continue;
			}

			if (propStoreTermVectorPositions.TryReadProperty(ref reader, options, PropStoreTermVectorPositions, null))
			{
				continue;
			}

			if (propStoreTermVectors.TryReadProperty(ref reader, options, PropStoreTermVectors, null))
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
		return new Elastic.Clients.Elasticsearch.Mapping.AllField(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Analyzer = propAnalyzer.Value,
			Enabled = propEnabled.Value,
			OmitNorms = propOmitNorms.Value,
			SearchAnalyzer = propSearchAnalyzer.Value,
			Similarity = propSimilarity.Value,
			Store = propStore.Value,
			StoreTermVectorOffsets = propStoreTermVectorOffsets.Value,
			StoreTermVectorPayloads = propStoreTermVectorPayloads.Value,
			StoreTermVectorPositions = propStoreTermVectorPositions.Value,
			StoreTermVectors = propStoreTermVectors.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Mapping.AllField value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAnalyzer, value.Analyzer, null, null);
		writer.WriteProperty(options, PropEnabled, value.Enabled, null, null);
		writer.WriteProperty(options, PropOmitNorms, value.OmitNorms, null, null);
		writer.WriteProperty(options, PropSearchAnalyzer, value.SearchAnalyzer, null, null);
		writer.WriteProperty(options, PropSimilarity, value.Similarity, null, null);
		writer.WriteProperty(options, PropStore, value.Store, null, null);
		writer.WriteProperty(options, PropStoreTermVectorOffsets, value.StoreTermVectorOffsets, null, null);
		writer.WriteProperty(options, PropStoreTermVectorPayloads, value.StoreTermVectorPayloads, null, null);
		writer.WriteProperty(options, PropStoreTermVectorPositions, value.StoreTermVectorPositions, null, null);
		writer.WriteProperty(options, PropStoreTermVectors, value.StoreTermVectors, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Mapping.AllFieldConverter))]
public sealed partial class AllField
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AllField(string analyzer, bool enabled, bool omitNorms, string searchAnalyzer, string similarity, bool store, bool storeTermVectorOffsets, bool storeTermVectorPayloads, bool storeTermVectorPositions, bool storeTermVectors)
	{
		Analyzer = analyzer;
		Enabled = enabled;
		OmitNorms = omitNorms;
		SearchAnalyzer = searchAnalyzer;
		Similarity = similarity;
		Store = store;
		StoreTermVectorOffsets = storeTermVectorOffsets;
		StoreTermVectorPayloads = storeTermVectorPayloads;
		StoreTermVectorPositions = storeTermVectorPositions;
		StoreTermVectors = storeTermVectors;
	}
#if NET7_0_OR_GREATER
	public AllField()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public AllField()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal AllField(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	string Analyzer { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool Enabled { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool OmitNorms { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string SearchAnalyzer { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Similarity { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool Store { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool StoreTermVectorOffsets { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool StoreTermVectorPayloads { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool StoreTermVectorPositions { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool StoreTermVectors { get; set; }
}

public readonly partial struct AllFieldDescriptor
{
	internal Elastic.Clients.Elasticsearch.Mapping.AllField Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AllFieldDescriptor(Elastic.Clients.Elasticsearch.Mapping.AllField instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AllFieldDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Mapping.AllField(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Mapping.AllFieldDescriptor(Elastic.Clients.Elasticsearch.Mapping.AllField instance) => new Elastic.Clients.Elasticsearch.Mapping.AllFieldDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Mapping.AllField(Elastic.Clients.Elasticsearch.Mapping.AllFieldDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Mapping.AllFieldDescriptor Analyzer(string value)
	{
		Instance.Analyzer = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.AllFieldDescriptor Enabled(bool value = true)
	{
		Instance.Enabled = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.AllFieldDescriptor OmitNorms(bool value = true)
	{
		Instance.OmitNorms = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.AllFieldDescriptor SearchAnalyzer(string value)
	{
		Instance.SearchAnalyzer = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.AllFieldDescriptor Similarity(string value)
	{
		Instance.Similarity = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.AllFieldDescriptor Store(bool value = true)
	{
		Instance.Store = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.AllFieldDescriptor StoreTermVectorOffsets(bool value = true)
	{
		Instance.StoreTermVectorOffsets = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.AllFieldDescriptor StoreTermVectorPayloads(bool value = true)
	{
		Instance.StoreTermVectorPayloads = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.AllFieldDescriptor StoreTermVectorPositions(bool value = true)
	{
		Instance.StoreTermVectorPositions = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.AllFieldDescriptor StoreTermVectors(bool value = true)
	{
		Instance.StoreTermVectors = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Mapping.AllField Build(System.Action<Elastic.Clients.Elasticsearch.Mapping.AllFieldDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Mapping.AllFieldDescriptor(new Elastic.Clients.Elasticsearch.Mapping.AllField(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}