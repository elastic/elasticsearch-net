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

namespace Elastic.Clients.Elasticsearch.Cluster;

public sealed partial class DeleteVotingConfigExclusionsRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// Specifies whether to wait for all excluded nodes to be removed from the
	/// cluster before clearing the voting configuration exclusions list.
	/// Defaults to true, meaning that all excluded nodes must be removed from
	/// the cluster before this API takes any action. If set to false then the
	/// voting configuration exclusions list is cleared even if some excluded
	/// nodes are still in the cluster.
	/// </para>
	/// </summary>
	public bool? WaitForRemoval { get => Q<bool?>("wait_for_removal"); set => Q("wait_for_removal", value); }
}

internal sealed partial class DeleteVotingConfigExclusionsRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequest>
{
	public override Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Clear cluster voting config exclusions.
/// Remove master-eligible nodes from the voting configuration exclusion list.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequestConverter))]
public sealed partial class DeleteVotingConfigExclusionsRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequestParameters>
{
#if NET7_0_OR_GREATER
	public DeleteVotingConfigExclusionsRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public DeleteVotingConfigExclusionsRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DeleteVotingConfigExclusionsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.ClusterDeleteVotingConfigExclusions;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "cluster.delete_voting_config_exclusions";

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// Specifies whether to wait for all excluded nodes to be removed from the
	/// cluster before clearing the voting configuration exclusions list.
	/// Defaults to true, meaning that all excluded nodes must be removed from
	/// the cluster before this API takes any action. If set to false then the
	/// voting configuration exclusions list is cleared even if some excluded
	/// nodes are still in the cluster.
	/// </para>
	/// </summary>
	public bool? WaitForRemoval { get => Q<bool?>("wait_for_removal"); set => Q("wait_for_removal", value); }
}

/// <summary>
/// <para>
/// Clear cluster voting config exclusions.
/// Remove master-eligible nodes from the voting configuration exclusion list.
/// </para>
/// </summary>
public readonly partial struct DeleteVotingConfigExclusionsRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DeleteVotingConfigExclusionsRequestDescriptor(Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequest instance)
	{
		Instance = instance;
	}

	public DeleteVotingConfigExclusionsRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequestDescriptor(Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequest instance) => new Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequest(Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies whether to wait for all excluded nodes to be removed from the
	/// cluster before clearing the voting configuration exclusions list.
	/// Defaults to true, meaning that all excluded nodes must be removed from
	/// the cluster before this API takes any action. If set to false then the
	/// voting configuration exclusions list is cleared even if some excluded
	/// nodes are still in the cluster.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequestDescriptor WaitForRemoval(bool? value = true)
	{
		Instance.WaitForRemoval = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequest Build(System.Action<Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequestDescriptor(new Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.DeleteVotingConfigExclusionsRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}