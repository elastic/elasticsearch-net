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

namespace Elastic.Clients.Elasticsearch.Synonyms;

public sealed partial class GetSynonymRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// The starting offset for query rules to retrieve.
	/// </para>
	/// </summary>
	public int? From { get => Q<int?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>
	/// The max number of query rules to retrieve.
	/// </para>
	/// </summary>
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }
}

internal sealed partial class GetSynonymRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequest>
{
	public override Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get a synonym set.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequestConverter))]
public sealed partial class GetSynonymRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetSynonymRequest(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
	{
	}
#if NET7_0_OR_GREATER
	public GetSynonymRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetSynonymRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SynonymsGetSynonym;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "synonyms.get_synonym";

	/// <summary>
	/// <para>
	/// The synonyms set identifier to retrieve.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id Id { get => P<Elastic.Clients.Elasticsearch.Id>("id"); set => PR("id", value); }

	/// <summary>
	/// <para>
	/// The starting offset for query rules to retrieve.
	/// </para>
	/// </summary>
	public int? From { get => Q<int?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>
	/// The max number of query rules to retrieve.
	/// </para>
	/// </summary>
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }
}

/// <summary>
/// <para>
/// Get a synonym set.
/// </para>
/// </summary>
public readonly partial struct GetSynonymRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetSynonymRequestDescriptor(Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequest instance)
	{
		Instance = instance;
	}

	public GetSynonymRequestDescriptor(Elastic.Clients.Elasticsearch.Id id)
	{
		Instance = new Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequest(id);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public GetSynonymRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequestDescriptor(Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequest instance) => new Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequest(Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The synonyms set identifier to retrieve.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.Id = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The starting offset for query rules to retrieve.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequestDescriptor From(int? value)
	{
		Instance.From = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The max number of query rules to retrieve.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequestDescriptor Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequest Build(System.Action<Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequestDescriptor(new Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}