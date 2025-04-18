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

namespace Elastic.Clients.Elasticsearch;

public sealed partial class ClearScrollRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class ClearScrollRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.ClearScrollRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropScrollId = System.Text.Json.JsonEncodedText.Encode("scroll_id");

	public override Elastic.Clients.Elasticsearch.ClearScrollRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.ScrollIds?> propScrollId = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propScrollId.TryReadProperty(ref reader, options, PropScrollId, null))
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
		return new Elastic.Clients.Elasticsearch.ClearScrollRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ScrollId = propScrollId.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.ClearScrollRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropScrollId, value.ScrollId, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Clear a scrolling search.
/// Clear the search context and results for a scrolling search.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.ClearScrollRequestConverter))]
public sealed partial class ClearScrollRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.ClearScrollRequestParameters>
{
#if NET7_0_OR_GREATER
	public ClearScrollRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public ClearScrollRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ClearScrollRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.NoNamespaceClearScroll;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.DELETE;

	internal override bool SupportsBody => true;

	internal override string OperationName => "clear_scroll";

	/// <summary>
	/// <para>
	/// The scroll IDs to clear.
	/// To clear all scroll IDs, use <c>_all</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ScrollIds? ScrollId { get; set; }
}

/// <summary>
/// <para>
/// Clear a scrolling search.
/// Clear the search context and results for a scrolling search.
/// </para>
/// </summary>
public readonly partial struct ClearScrollRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.ClearScrollRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ClearScrollRequestDescriptor(Elastic.Clients.Elasticsearch.ClearScrollRequest instance)
	{
		Instance = instance;
	}

	public ClearScrollRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.ClearScrollRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.ClearScrollRequestDescriptor(Elastic.Clients.Elasticsearch.ClearScrollRequest instance) => new Elastic.Clients.Elasticsearch.ClearScrollRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.ClearScrollRequest(Elastic.Clients.Elasticsearch.ClearScrollRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The scroll IDs to clear.
	/// To clear all scroll IDs, use <c>_all</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ClearScrollRequestDescriptor ScrollId(Elastic.Clients.Elasticsearch.ScrollIds? value)
	{
		Instance.ScrollId = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.ClearScrollRequest Build(System.Action<Elastic.Clients.Elasticsearch.ClearScrollRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.ClearScrollRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.ClearScrollRequestDescriptor(new Elastic.Clients.Elasticsearch.ClearScrollRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.ClearScrollRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ClearScrollRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ClearScrollRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ClearScrollRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ClearScrollRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ClearScrollRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ClearScrollRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}