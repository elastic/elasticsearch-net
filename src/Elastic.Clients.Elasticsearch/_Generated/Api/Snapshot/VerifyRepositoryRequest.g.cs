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

namespace Elastic.Clients.Elasticsearch.Snapshot;

public sealed partial class VerifyRepositoryRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// The period to wait for the master node.
	/// If the master node is not available before the timeout expires, the request fails and returns an error.
	/// To indicate that the request should never timeout, set it to <c>-1</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// The period to wait for a response from all relevant nodes in the cluster after updating the cluster metadata.
	/// If no response is received before the timeout expires, the cluster metadata update still applies but the response will indicate that it was not completely acknowledged.
	/// To indicate that the request should never timeout, set it to <c>-1</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

internal sealed partial class VerifyRepositoryRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequest>
{
	public override Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Verify a snapshot repository.
/// Check for common misconfigurations in a snapshot repository.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequestConverter))]
public sealed partial class VerifyRepositoryRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public VerifyRepositoryRequest(Elastic.Clients.Elasticsearch.Name name) : base(r => r.Required("repository", name))
	{
	}
#if NET7_0_OR_GREATER
	public VerifyRepositoryRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal VerifyRepositoryRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SnapshotVerifyRepository;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "snapshot.verify_repository";

	/// <summary>
	/// <para>
	/// The name of the snapshot repository to verify.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Name Name { get => P<Elastic.Clients.Elasticsearch.Name>("repository"); set => PR("repository", value); }

	/// <summary>
	/// <para>
	/// The period to wait for the master node.
	/// If the master node is not available before the timeout expires, the request fails and returns an error.
	/// To indicate that the request should never timeout, set it to <c>-1</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// The period to wait for a response from all relevant nodes in the cluster after updating the cluster metadata.
	/// If no response is received before the timeout expires, the cluster metadata update still applies but the response will indicate that it was not completely acknowledged.
	/// To indicate that the request should never timeout, set it to <c>-1</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>
/// Verify a snapshot repository.
/// Check for common misconfigurations in a snapshot repository.
/// </para>
/// </summary>
public readonly partial struct VerifyRepositoryRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public VerifyRepositoryRequestDescriptor(Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequest instance)
	{
		Instance = instance;
	}

	public VerifyRepositoryRequestDescriptor(Elastic.Clients.Elasticsearch.Name name)
	{
		Instance = new Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequest(name);
	}

	[System.Obsolete("TODO")]
	public VerifyRepositoryRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequestDescriptor(Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequest instance) => new Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequest(Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The name of the snapshot repository to verify.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequestDescriptor Name(Elastic.Clients.Elasticsearch.Name value)
	{
		Instance.Name = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The period to wait for the master node.
	/// If the master node is not available before the timeout expires, the request fails and returns an error.
	/// To indicate that the request should never timeout, set it to <c>-1</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The period to wait for a response from all relevant nodes in the cluster after updating the cluster metadata.
	/// If no response is received before the timeout expires, the cluster metadata update still applies but the response will indicate that it was not completely acknowledged.
	/// To indicate that the request should never timeout, set it to <c>-1</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequest Build(System.Action<Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequestDescriptor(new Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.VerifyRepositoryRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}