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

namespace Elastic.Clients.Elasticsearch.Security;

public sealed partial class HasPrivilegesRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class HasPrivilegesRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropApplication = System.Text.Json.JsonEncodedText.Encode("application");
	private static readonly System.Text.Json.JsonEncodedText PropCluster = System.Text.Json.JsonEncodedText.Encode("cluster");
	private static readonly System.Text.Json.JsonEncodedText PropIndex = System.Text.Json.JsonEncodedText.Encode("index");

	public override Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.ApplicationPrivilegesCheck>?> propApplication = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.ClusterPrivilege>?> propCluster = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.IndexPrivilegesCheck>?> propIndex = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propApplication.TryReadProperty(ref reader, options, PropApplication, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.ApplicationPrivilegesCheck>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Security.ApplicationPrivilegesCheck>(o, null)))
			{
				continue;
			}

			if (propCluster.TryReadProperty(ref reader, options, PropCluster, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.ClusterPrivilege>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Security.ClusterPrivilege>(o, null)))
			{
				continue;
			}

			if (propIndex.TryReadProperty(ref reader, options, PropIndex, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.IndexPrivilegesCheck>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Security.IndexPrivilegesCheck>(o, null)))
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
		return new Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Application = propApplication.Value,
			Cluster = propCluster.Value,
			Index = propIndex.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropApplication, value.Application, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.ApplicationPrivilegesCheck>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Security.ApplicationPrivilegesCheck>(o, v, null));
		writer.WriteProperty(options, PropCluster, value.Cluster, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.ClusterPrivilege>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Security.ClusterPrivilege>(o, v, null));
		writer.WriteProperty(options, PropIndex, value.Index, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.IndexPrivilegesCheck>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Security.IndexPrivilegesCheck>(o, v, null));
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Check user privileges.
/// </para>
/// <para>
/// Determine whether the specified user has a specified list of privileges.
/// All users can use this API, but only to determine their own privileges.
/// To check the privileges of other users, you must use the run as feature.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequestConverter))]
public sealed partial class HasPrivilegesRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequestParameters>
{
	public HasPrivilegesRequest(Elastic.Clients.Elasticsearch.Name? user) : base(r => r.Optional("user", user))
	{
	}
#if NET7_0_OR_GREATER
	public HasPrivilegesRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public HasPrivilegesRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal HasPrivilegesRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SecurityHasPrivileges;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.has_privileges";

	/// <summary>
	/// <para>
	/// Username
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Name? User { get => P<Elastic.Clients.Elasticsearch.Name?>("user"); set => PO("user", value); }
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.ApplicationPrivilegesCheck>? Application { get; set; }

	/// <summary>
	/// <para>
	/// A list of the cluster privileges that you want to check.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.ClusterPrivilege>? Cluster { get; set; }
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.IndexPrivilegesCheck>? Index { get; set; }
}

/// <summary>
/// <para>
/// Check user privileges.
/// </para>
/// <para>
/// Determine whether the specified user has a specified list of privileges.
/// All users can use this API, but only to determine their own privileges.
/// To check the privileges of other users, you must use the run as feature.
/// </para>
/// </summary>
public readonly partial struct HasPrivilegesRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public HasPrivilegesRequestDescriptor(Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequest instance)
	{
		Instance = instance;
	}

	public HasPrivilegesRequestDescriptor(Elastic.Clients.Elasticsearch.Name user)
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequest(user);
	}

	public HasPrivilegesRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequestDescriptor(Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequest instance) => new Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequest(Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Username
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequestDescriptor User(Elastic.Clients.Elasticsearch.Name? value)
	{
		Instance.User = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequestDescriptor Application(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.ApplicationPrivilegesCheck>? value)
	{
		Instance.Application = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequestDescriptor Application(params Elastic.Clients.Elasticsearch.Security.ApplicationPrivilegesCheck[] values)
	{
		Instance.Application = [.. values];
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequestDescriptor Application(params System.Action<Elastic.Clients.Elasticsearch.Security.ApplicationPrivilegesCheckDescriptor>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Security.ApplicationPrivilegesCheck>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Security.ApplicationPrivilegesCheckDescriptor.Build(action));
		}

		Instance.Application = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of the cluster privileges that you want to check.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequestDescriptor Cluster(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.ClusterPrivilege>? value)
	{
		Instance.Cluster = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of the cluster privileges that you want to check.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequestDescriptor Cluster(params Elastic.Clients.Elasticsearch.Security.ClusterPrivilege[] values)
	{
		Instance.Cluster = [.. values];
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequestDescriptor Index(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.IndexPrivilegesCheck>? value)
	{
		Instance.Index = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequestDescriptor Index(params Elastic.Clients.Elasticsearch.Security.IndexPrivilegesCheck[] values)
	{
		Instance.Index = [.. values];
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequestDescriptor Index(params System.Action<Elastic.Clients.Elasticsearch.Security.IndexPrivilegesCheckDescriptor>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Security.IndexPrivilegesCheck>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Security.IndexPrivilegesCheckDescriptor.Build(action));
		}

		Instance.Index = items;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequest Build(System.Action<Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequestDescriptor(new Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.HasPrivilegesRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}