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

namespace Elastic.Clients.Elasticsearch.Nodes;

public sealed partial class ReloadSecureSettingsRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// Period to wait for a response.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

internal sealed partial class ReloadSecureSettingsRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropSecureSettingsPassword = System.Text.Json.JsonEncodedText.Encode("secure_settings_password");

	public override Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propSecureSettingsPassword = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propSecureSettingsPassword.TryReadProperty(ref reader, options, PropSecureSettingsPassword, null))
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
		return new Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			SecureSettingsPassword = propSecureSettingsPassword.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropSecureSettingsPassword, value.SecureSettingsPassword, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Reload the keystore on nodes in the cluster.
/// </para>
/// <para>
/// Secure settings are stored in an on-disk keystore. Certain of these settings are reloadable.
/// That is, you can change them on disk and reload them without restarting any nodes in the cluster.
/// When you have updated reloadable secure settings in your keystore, you can use this API to reload those settings on each node.
/// </para>
/// <para>
/// When the Elasticsearch keystore is password protected and not simply obfuscated, you must provide the password for the keystore when you reload the secure settings.
/// Reloading the settings for the whole cluster assumes that the keystores for all nodes are protected with the same password; this method is allowed only when inter-node communications are encrypted.
/// Alternatively, you can reload the secure settings on each node by locally accessing the API and passing the node-specific Elasticsearch keystore password.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequestConverter))]
public sealed partial class ReloadSecureSettingsRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequestParameters>
{
	public ReloadSecureSettingsRequest(Elastic.Clients.Elasticsearch.NodeIds? nodeId) : base(r => r.Optional("node_id", nodeId))
	{
	}
#if NET7_0_OR_GREATER
	public ReloadSecureSettingsRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public ReloadSecureSettingsRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ReloadSecureSettingsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.NodesReloadSecureSettings;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "nodes.reload_secure_settings";

	/// <summary>
	/// <para>
	/// The names of particular nodes in the cluster to target.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.NodeIds? NodeId { get => P<Elastic.Clients.Elasticsearch.NodeIds?>("node_id"); set => PO("node_id", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a response.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>
	/// The password for the Elasticsearch keystore.
	/// </para>
	/// </summary>
	public string? SecureSettingsPassword { get; set; }
}

/// <summary>
/// <para>
/// Reload the keystore on nodes in the cluster.
/// </para>
/// <para>
/// Secure settings are stored in an on-disk keystore. Certain of these settings are reloadable.
/// That is, you can change them on disk and reload them without restarting any nodes in the cluster.
/// When you have updated reloadable secure settings in your keystore, you can use this API to reload those settings on each node.
/// </para>
/// <para>
/// When the Elasticsearch keystore is password protected and not simply obfuscated, you must provide the password for the keystore when you reload the secure settings.
/// Reloading the settings for the whole cluster assumes that the keystores for all nodes are protected with the same password; this method is allowed only when inter-node communications are encrypted.
/// Alternatively, you can reload the secure settings on each node by locally accessing the API and passing the node-specific Elasticsearch keystore password.
/// </para>
/// </summary>
public readonly partial struct ReloadSecureSettingsRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ReloadSecureSettingsRequestDescriptor(Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequest instance)
	{
		Instance = instance;
	}

	public ReloadSecureSettingsRequestDescriptor(Elastic.Clients.Elasticsearch.NodeIds nodeId)
	{
		Instance = new Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequest(nodeId);
	}

	public ReloadSecureSettingsRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequestDescriptor(Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequest instance) => new Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequest(Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The names of particular nodes in the cluster to target.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequestDescriptor NodeId(Elastic.Clients.Elasticsearch.NodeIds? value)
	{
		Instance.NodeId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a response.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The password for the Elasticsearch keystore.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequestDescriptor SecureSettingsPassword(string? value)
	{
		Instance.SecureSettingsPassword = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequest Build(System.Action<Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequestDescriptor(new Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.ReloadSecureSettingsRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}