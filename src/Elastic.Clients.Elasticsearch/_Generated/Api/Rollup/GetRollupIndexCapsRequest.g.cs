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

namespace Elastic.Clients.Elasticsearch.Rollup;

public sealed partial class GetRollupIndexCapsRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class GetRollupIndexCapsRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Rollup.GetRollupIndexCapsRequest>
{
	public override Elastic.Clients.Elasticsearch.Rollup.GetRollupIndexCapsRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Rollup.GetRollupIndexCapsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Rollup.GetRollupIndexCapsRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get the rollup index capabilities.
/// Get the rollup capabilities of all jobs inside of a rollup index.
/// A single rollup index may store the data for multiple rollup jobs and may have a variety of capabilities depending on those jobs. This API enables you to determine:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// What jobs are stored in an index (or indices specified via a pattern)?
/// </para>
/// </item>
/// <item>
/// <para>
/// What target indices were rolled up, what fields were used in those rollups, and what aggregations can be performed on each job?
/// </para>
/// </item>
/// </list>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Rollup.GetRollupIndexCapsRequestConverter))]
public sealed partial class GetRollupIndexCapsRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Rollup.GetRollupIndexCapsRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetRollupIndexCapsRequest(Elastic.Clients.Elasticsearch.Ids index) : base(r => r.Required("index", index))
	{
	}
#if NET7_0_OR_GREATER
	public GetRollupIndexCapsRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetRollupIndexCapsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.RollupGetRollupIndexCaps;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "rollup.get_rollup_index_caps";

	/// <summary>
	/// <para>
	/// Data stream or index to check for rollup capabilities.
	/// Wildcard (<c>*</c>) expressions are supported.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Ids Index { get => P<Elastic.Clients.Elasticsearch.Ids>("index"); set => PR("index", value); }
}

/// <summary>
/// <para>
/// Get the rollup index capabilities.
/// Get the rollup capabilities of all jobs inside of a rollup index.
/// A single rollup index may store the data for multiple rollup jobs and may have a variety of capabilities depending on those jobs. This API enables you to determine:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// What jobs are stored in an index (or indices specified via a pattern)?
/// </para>
/// </item>
/// <item>
/// <para>
/// What target indices were rolled up, what fields were used in those rollups, and what aggregations can be performed on each job?
/// </para>
/// </item>
/// </list>
/// </summary>
public readonly partial struct GetRollupIndexCapsRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Rollup.GetRollupIndexCapsRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetRollupIndexCapsRequestDescriptor(Elastic.Clients.Elasticsearch.Rollup.GetRollupIndexCapsRequest instance)
	{
		Instance = instance;
	}

	public GetRollupIndexCapsRequestDescriptor(Elastic.Clients.Elasticsearch.Ids index)
	{
		Instance = new Elastic.Clients.Elasticsearch.Rollup.GetRollupIndexCapsRequest(index);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public GetRollupIndexCapsRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Rollup.GetRollupIndexCapsRequestDescriptor(Elastic.Clients.Elasticsearch.Rollup.GetRollupIndexCapsRequest instance) => new Elastic.Clients.Elasticsearch.Rollup.GetRollupIndexCapsRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Rollup.GetRollupIndexCapsRequest(Elastic.Clients.Elasticsearch.Rollup.GetRollupIndexCapsRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Data stream or index to check for rollup capabilities.
	/// Wildcard (<c>*</c>) expressions are supported.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Rollup.GetRollupIndexCapsRequestDescriptor Index(Elastic.Clients.Elasticsearch.Ids value)
	{
		Instance.Index = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Rollup.GetRollupIndexCapsRequest Build(System.Action<Elastic.Clients.Elasticsearch.Rollup.GetRollupIndexCapsRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Rollup.GetRollupIndexCapsRequestDescriptor(new Elastic.Clients.Elasticsearch.Rollup.GetRollupIndexCapsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Rollup.GetRollupIndexCapsRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.GetRollupIndexCapsRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.GetRollupIndexCapsRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.GetRollupIndexCapsRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.GetRollupIndexCapsRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.GetRollupIndexCapsRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.GetRollupIndexCapsRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}