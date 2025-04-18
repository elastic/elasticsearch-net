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

namespace Elastic.Clients.Elasticsearch.CrossClusterReplication;

public sealed partial class ResumeAutoFollowPatternRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// The period to wait for a connection to the master node.
	/// If the master node is not available before the timeout expires, the request fails and returns an error.
	/// It can also be set to <c>-1</c> to indicate that the request should never timeout.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
}

internal sealed partial class ResumeAutoFollowPatternRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeAutoFollowPatternRequest>
{
	public override Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeAutoFollowPatternRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeAutoFollowPatternRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeAutoFollowPatternRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Resume an auto-follow pattern.
/// </para>
/// <para>
/// Resume a cross-cluster replication auto-follow pattern that was paused.
/// The auto-follow pattern will resume configuring following indices for newly created indices that match its patterns on the remote cluster.
/// Remote indices created while the pattern was paused will also be followed unless they have been deleted or closed in the interim.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeAutoFollowPatternRequestConverter))]
public sealed partial class ResumeAutoFollowPatternRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeAutoFollowPatternRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ResumeAutoFollowPatternRequest(Elastic.Clients.Elasticsearch.Name name) : base(r => r.Required("name", name))
	{
	}
#if NET7_0_OR_GREATER
	public ResumeAutoFollowPatternRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ResumeAutoFollowPatternRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.CrossClusterReplicationResumeAutoFollowPattern;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ccr.resume_auto_follow_pattern";

	/// <summary>
	/// <para>
	/// The name of the auto-follow pattern to resume.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Name Name { get => P<Elastic.Clients.Elasticsearch.Name>("name"); set => PR("name", value); }

	/// <summary>
	/// <para>
	/// The period to wait for a connection to the master node.
	/// If the master node is not available before the timeout expires, the request fails and returns an error.
	/// It can also be set to <c>-1</c> to indicate that the request should never timeout.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
}

/// <summary>
/// <para>
/// Resume an auto-follow pattern.
/// </para>
/// <para>
/// Resume a cross-cluster replication auto-follow pattern that was paused.
/// The auto-follow pattern will resume configuring following indices for newly created indices that match its patterns on the remote cluster.
/// Remote indices created while the pattern was paused will also be followed unless they have been deleted or closed in the interim.
/// </para>
/// </summary>
public readonly partial struct ResumeAutoFollowPatternRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeAutoFollowPatternRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ResumeAutoFollowPatternRequestDescriptor(Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeAutoFollowPatternRequest instance)
	{
		Instance = instance;
	}

	public ResumeAutoFollowPatternRequestDescriptor(Elastic.Clients.Elasticsearch.Name name)
	{
		Instance = new Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeAutoFollowPatternRequest(name);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public ResumeAutoFollowPatternRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeAutoFollowPatternRequestDescriptor(Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeAutoFollowPatternRequest instance) => new Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeAutoFollowPatternRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeAutoFollowPatternRequest(Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeAutoFollowPatternRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The name of the auto-follow pattern to resume.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeAutoFollowPatternRequestDescriptor Name(Elastic.Clients.Elasticsearch.Name value)
	{
		Instance.Name = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The period to wait for a connection to the master node.
	/// If the master node is not available before the timeout expires, the request fails and returns an error.
	/// It can also be set to <c>-1</c> to indicate that the request should never timeout.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeAutoFollowPatternRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeAutoFollowPatternRequest Build(System.Action<Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeAutoFollowPatternRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeAutoFollowPatternRequestDescriptor(new Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeAutoFollowPatternRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeAutoFollowPatternRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeAutoFollowPatternRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeAutoFollowPatternRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeAutoFollowPatternRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeAutoFollowPatternRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeAutoFollowPatternRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeAutoFollowPatternRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}