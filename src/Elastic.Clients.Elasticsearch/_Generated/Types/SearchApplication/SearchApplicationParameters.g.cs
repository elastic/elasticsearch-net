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

namespace Elastic.Clients.Elasticsearch.SearchApplication;

internal sealed partial class SearchApplicationParametersConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationParameters>
{
	private static readonly System.Text.Json.JsonEncodedText PropAnalyticsCollectionName = System.Text.Json.JsonEncodedText.Encode("analytics_collection_name");
	private static readonly System.Text.Json.JsonEncodedText PropIndices = System.Text.Json.JsonEncodedText.Encode("indices");
	private static readonly System.Text.Json.JsonEncodedText PropTemplate = System.Text.Json.JsonEncodedText.Encode("template");

	public override Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationParameters Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Name?> propAnalyticsCollectionName = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.IndexName>> propIndices = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationTemplate?> propTemplate = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAnalyticsCollectionName.TryReadProperty(ref reader, options, PropAnalyticsCollectionName, null))
			{
				continue;
			}

			if (propIndices.TryReadProperty(ref reader, options, PropIndices, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.IndexName> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.IndexName>(o, null)!))
			{
				continue;
			}

			if (propTemplate.TryReadProperty(ref reader, options, PropTemplate, null))
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
		return new Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationParameters(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AnalyticsCollectionName = propAnalyticsCollectionName.Value,
			Indices = propIndices.Value,
			Template = propTemplate.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationParameters value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAnalyticsCollectionName, value.AnalyticsCollectionName, null, null);
		writer.WriteProperty(options, PropIndices, value.Indices, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.IndexName> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.IndexName>(o, v, null));
		writer.WriteProperty(options, PropTemplate, value.Template, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationParametersConverter))]
public sealed partial class SearchApplicationParameters
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SearchApplicationParameters(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.IndexName> indices)
	{
		Indices = indices;
	}
#if NET7_0_OR_GREATER
	public SearchApplicationParameters()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public SearchApplicationParameters()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal SearchApplicationParameters(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Analytics collection associated to the Search Application.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Name? AnalyticsCollectionName { get; set; }

	/// <summary>
	/// <para>
	/// Indices that are part of the Search Application.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.IndexName> Indices { get; set; }

	/// <summary>
	/// <para>
	/// Search template to use on search operations.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationTemplate? Template { get; set; }
}

public readonly partial struct SearchApplicationParametersDescriptor
{
	internal Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationParameters Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SearchApplicationParametersDescriptor(Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationParameters instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SearchApplicationParametersDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationParameters(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationParametersDescriptor(Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationParameters instance) => new Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationParametersDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationParameters(Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationParametersDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Analytics collection associated to the Search Application.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationParametersDescriptor AnalyticsCollectionName(Elastic.Clients.Elasticsearch.Name? value)
	{
		Instance.AnalyticsCollectionName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Indices that are part of the Search Application.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationParametersDescriptor Indices(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.IndexName> value)
	{
		Instance.Indices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Indices that are part of the Search Application.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationParametersDescriptor Indices()
	{
		Instance.Indices = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfIndexName.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Indices that are part of the Search Application.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationParametersDescriptor Indices(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfIndexName>? action)
	{
		Instance.Indices = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfIndexName.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Indices that are part of the Search Application.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationParametersDescriptor Indices(params Elastic.Clients.Elasticsearch.IndexName[] values)
	{
		Instance.Indices = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Search template to use on search operations.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationParametersDescriptor Template(Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationTemplate? value)
	{
		Instance.Template = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Search template to use on search operations.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationParametersDescriptor Template(System.Action<Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationTemplateDescriptor> action)
	{
		Instance.Template = Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationTemplateDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationParameters Build(System.Action<Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationParametersDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationParametersDescriptor(new Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationParameters(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}