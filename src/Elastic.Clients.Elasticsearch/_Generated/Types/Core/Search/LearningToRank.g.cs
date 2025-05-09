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

internal sealed partial class LearningToRankConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.Search.LearningToRank>
{
	private static readonly System.Text.Json.JsonEncodedText PropModelId = System.Text.Json.JsonEncodedText.Encode("model_id");
	private static readonly System.Text.Json.JsonEncodedText PropParams = System.Text.Json.JsonEncodedText.Encode("params");

	public override Elastic.Clients.Elasticsearch.Core.Search.LearningToRank Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propModelId = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<string, object>?> propParams = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propModelId.TryReadProperty(ref reader, options, PropModelId, null))
			{
				continue;
			}

			if (propParams.TryReadProperty(ref reader, options, PropParams, static System.Collections.Generic.IDictionary<string, object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)))
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
		return new Elastic.Clients.Elasticsearch.Core.Search.LearningToRank(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ModelId = propModelId.Value,
			Params = propParams.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.Search.LearningToRank value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropModelId, value.ModelId, null, null);
		writer.WriteProperty(options, PropParams, value.Params, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, object>? v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.Search.LearningToRankConverter))]
public sealed partial class LearningToRank
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public LearningToRank(string modelId)
	{
		ModelId = modelId;
	}
#if NET7_0_OR_GREATER
	public LearningToRank()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public LearningToRank()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal LearningToRank(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The unique identifier of the trained model uploaded to Elasticsearch
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string ModelId { get; set; }

	/// <summary>
	/// <para>
	/// Named parameters to be passed to the query templates used for feature
	/// </para>
	/// </summary>
	public System.Collections.Generic.IDictionary<string, object>? Params { get; set; }
}

public readonly partial struct LearningToRankDescriptor
{
	internal Elastic.Clients.Elasticsearch.Core.Search.LearningToRank Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public LearningToRankDescriptor(Elastic.Clients.Elasticsearch.Core.Search.LearningToRank instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public LearningToRankDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Core.Search.LearningToRank(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Core.Search.LearningToRankDescriptor(Elastic.Clients.Elasticsearch.Core.Search.LearningToRank instance) => new Elastic.Clients.Elasticsearch.Core.Search.LearningToRankDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Core.Search.LearningToRank(Elastic.Clients.Elasticsearch.Core.Search.LearningToRankDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The unique identifier of the trained model uploaded to Elasticsearch
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.LearningToRankDescriptor ModelId(string value)
	{
		Instance.ModelId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Named parameters to be passed to the query templates used for feature
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.LearningToRankDescriptor Params(System.Collections.Generic.IDictionary<string, object>? value)
	{
		Instance.Params = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Named parameters to be passed to the query templates used for feature
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.LearningToRankDescriptor Params()
	{
		Instance.Params = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Named parameters to be passed to the query templates used for feature
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.LearningToRankDescriptor Params(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject>? action)
	{
		Instance.Params = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Search.LearningToRankDescriptor AddParam(string key, object value)
	{
		Instance.Params ??= new System.Collections.Generic.Dictionary<string, object>();
		Instance.Params.Add(key, value);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.Search.LearningToRank Build(System.Action<Elastic.Clients.Elasticsearch.Core.Search.LearningToRankDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Core.Search.LearningToRankDescriptor(new Elastic.Clients.Elasticsearch.Core.Search.LearningToRank(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}