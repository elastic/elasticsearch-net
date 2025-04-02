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

namespace Elastic.Clients.Elasticsearch.Eql;

public sealed partial class GetEqlStatusRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class GetEqlStatusRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Eql.GetEqlStatusRequest>
{
	public override Elastic.Clients.Elasticsearch.Eql.GetEqlStatusRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Eql.GetEqlStatusRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Eql.GetEqlStatusRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get the async EQL status.
/// Get the current status for an async EQL search or a stored synchronous EQL search without returning results.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Eql.GetEqlStatusRequestConverter))]
public sealed partial class GetEqlStatusRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Eql.GetEqlStatusRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetEqlStatusRequest(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
	{
	}
#if NET7_0_OR_GREATER
	public GetEqlStatusRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetEqlStatusRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.EqlGetStatus;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "eql.get_status";

	/// <summary>
	/// <para>
	/// Identifier for the search.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id Id { get => P<Elastic.Clients.Elasticsearch.Id>("id"); set => PR("id", value); }
}

/// <summary>
/// <para>
/// Get the async EQL status.
/// Get the current status for an async EQL search or a stored synchronous EQL search without returning results.
/// </para>
/// </summary>
public readonly partial struct GetEqlStatusRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Eql.GetEqlStatusRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetEqlStatusRequestDescriptor(Elastic.Clients.Elasticsearch.Eql.GetEqlStatusRequest instance)
	{
		Instance = instance;
	}

	public GetEqlStatusRequestDescriptor(Elastic.Clients.Elasticsearch.Id id)
	{
		Instance = new Elastic.Clients.Elasticsearch.Eql.GetEqlStatusRequest(id);
	}

	[System.Obsolete("TODO")]
	public GetEqlStatusRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Eql.GetEqlStatusRequestDescriptor(Elastic.Clients.Elasticsearch.Eql.GetEqlStatusRequest instance) => new Elastic.Clients.Elasticsearch.Eql.GetEqlStatusRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Eql.GetEqlStatusRequest(Elastic.Clients.Elasticsearch.Eql.GetEqlStatusRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Identifier for the search.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Eql.GetEqlStatusRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.Id = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Eql.GetEqlStatusRequest Build(System.Action<Elastic.Clients.Elasticsearch.Eql.GetEqlStatusRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Eql.GetEqlStatusRequestDescriptor(new Elastic.Clients.Elasticsearch.Eql.GetEqlStatusRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Eql.GetEqlStatusRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Eql.GetEqlStatusRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Eql.GetEqlStatusRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Eql.GetEqlStatusRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Eql.GetEqlStatusRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Eql.GetEqlStatusRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Eql.GetEqlStatusRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}