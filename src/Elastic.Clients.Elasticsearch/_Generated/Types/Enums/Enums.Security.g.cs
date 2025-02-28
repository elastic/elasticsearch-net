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

using Elastic.Clients.Elasticsearch.Core;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using System;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Security;

[JsonConverter(typeof(AccessTokenGrantTypeConverter))]
public enum AccessTokenGrantType
{
	[EnumMember(Value = "refresh_token")]
	RefreshToken,
	[EnumMember(Value = "password")]
	Password,
	[EnumMember(Value = "_kerberos")]
	Kerberos,
	[EnumMember(Value = "client_credentials")]
	ClientCredentials
}

internal sealed partial class AccessTokenGrantTypeConverter : System.Text.Json.Serialization.JsonConverter<AccessTokenGrantType>
{
	private static readonly System.Text.Json.JsonEncodedText MemberRefreshToken = System.Text.Json.JsonEncodedText.Encode("refresh_token");
	private static readonly System.Text.Json.JsonEncodedText MemberPassword = System.Text.Json.JsonEncodedText.Encode("password");
	private static readonly System.Text.Json.JsonEncodedText MemberKerberos = System.Text.Json.JsonEncodedText.Encode("_kerberos");
	private static readonly System.Text.Json.JsonEncodedText MemberClientCredentials = System.Text.Json.JsonEncodedText.Encode("client_credentials");

	public override AccessTokenGrantType Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		if (reader.ValueTextEquals(MemberRefreshToken))
		{
			return AccessTokenGrantType.RefreshToken;
		}

		if (reader.ValueTextEquals(MemberPassword))
		{
			return AccessTokenGrantType.Password;
		}

		if (reader.ValueTextEquals(MemberKerberos))
		{
			return AccessTokenGrantType.Kerberos;
		}

		if (reader.ValueTextEquals(MemberClientCredentials))
		{
			return AccessTokenGrantType.ClientCredentials;
		}

		var value = reader.GetString()!;
		if (string.Equals(value, MemberRefreshToken.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return AccessTokenGrantType.RefreshToken;
		}

		if (string.Equals(value, MemberPassword.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return AccessTokenGrantType.Password;
		}

		if (string.Equals(value, MemberKerberos.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return AccessTokenGrantType.Kerberos;
		}

		if (string.Equals(value, MemberClientCredentials.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return AccessTokenGrantType.ClientCredentials;
		}

		throw new System.Text.Json.JsonException($"Unknown member '{value}' for enum '{nameof(AccessTokenGrantType)}'.");
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, AccessTokenGrantType value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value)
		{
			case AccessTokenGrantType.RefreshToken:
				writer.WriteStringValue(MemberRefreshToken);
				break;
			case AccessTokenGrantType.Password:
				writer.WriteStringValue(MemberPassword);
				break;
			case AccessTokenGrantType.Kerberos:
				writer.WriteStringValue(MemberKerberos);
				break;
			case AccessTokenGrantType.ClientCredentials:
				writer.WriteStringValue(MemberClientCredentials);
				break;
			default:
				throw new System.Text.Json.JsonException($"Invalid value '{value}' for enum '{nameof(AccessTokenGrantType)}'.");
		}
	}

	public override AccessTokenGrantType ReadAsPropertyName(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return Read(ref reader, typeToConvert, options);
	}

	public override void WriteAsPropertyName(System.Text.Json.Utf8JsonWriter writer, AccessTokenGrantType value, System.Text.Json.JsonSerializerOptions options)
	{
		Write(writer, value, options);
	}
}

[JsonConverter(typeof(ApiKeyGrantTypeConverter))]
public enum ApiKeyGrantType
{
	[EnumMember(Value = "password")]
	Password,
	[EnumMember(Value = "access_token")]
	AccessToken
}

internal sealed partial class ApiKeyGrantTypeConverter : System.Text.Json.Serialization.JsonConverter<ApiKeyGrantType>
{
	private static readonly System.Text.Json.JsonEncodedText MemberPassword = System.Text.Json.JsonEncodedText.Encode("password");
	private static readonly System.Text.Json.JsonEncodedText MemberAccessToken = System.Text.Json.JsonEncodedText.Encode("access_token");

	public override ApiKeyGrantType Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		if (reader.ValueTextEquals(MemberPassword))
		{
			return ApiKeyGrantType.Password;
		}

		if (reader.ValueTextEquals(MemberAccessToken))
		{
			return ApiKeyGrantType.AccessToken;
		}

		var value = reader.GetString()!;
		if (string.Equals(value, MemberPassword.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return ApiKeyGrantType.Password;
		}

		if (string.Equals(value, MemberAccessToken.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return ApiKeyGrantType.AccessToken;
		}

		throw new System.Text.Json.JsonException($"Unknown member '{value}' for enum '{nameof(ApiKeyGrantType)}'.");
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, ApiKeyGrantType value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value)
		{
			case ApiKeyGrantType.Password:
				writer.WriteStringValue(MemberPassword);
				break;
			case ApiKeyGrantType.AccessToken:
				writer.WriteStringValue(MemberAccessToken);
				break;
			default:
				throw new System.Text.Json.JsonException($"Invalid value '{value}' for enum '{nameof(ApiKeyGrantType)}'.");
		}
	}

	public override ApiKeyGrantType ReadAsPropertyName(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return Read(ref reader, typeToConvert, options);
	}

	public override void WriteAsPropertyName(System.Text.Json.Utf8JsonWriter writer, ApiKeyGrantType value, System.Text.Json.JsonSerializerOptions options)
	{
		Write(writer, value, options);
	}
}

[JsonConverter(typeof(ApiKeyTypeConverter))]
public enum ApiKeyType
{
	[EnumMember(Value = "rest")]
	Rest,
	[EnumMember(Value = "cross_cluster")]
	CrossCluster
}

internal sealed partial class ApiKeyTypeConverter : System.Text.Json.Serialization.JsonConverter<ApiKeyType>
{
	private static readonly System.Text.Json.JsonEncodedText MemberRest = System.Text.Json.JsonEncodedText.Encode("rest");
	private static readonly System.Text.Json.JsonEncodedText MemberCrossCluster = System.Text.Json.JsonEncodedText.Encode("cross_cluster");

	public override ApiKeyType Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		if (reader.ValueTextEquals(MemberRest))
		{
			return ApiKeyType.Rest;
		}

		if (reader.ValueTextEquals(MemberCrossCluster))
		{
			return ApiKeyType.CrossCluster;
		}

		var value = reader.GetString()!;
		if (string.Equals(value, MemberRest.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return ApiKeyType.Rest;
		}

		if (string.Equals(value, MemberCrossCluster.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return ApiKeyType.CrossCluster;
		}

		throw new System.Text.Json.JsonException($"Unknown member '{value}' for enum '{nameof(ApiKeyType)}'.");
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, ApiKeyType value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value)
		{
			case ApiKeyType.Rest:
				writer.WriteStringValue(MemberRest);
				break;
			case ApiKeyType.CrossCluster:
				writer.WriteStringValue(MemberCrossCluster);
				break;
			default:
				throw new System.Text.Json.JsonException($"Invalid value '{value}' for enum '{nameof(ApiKeyType)}'.");
		}
	}

	public override ApiKeyType ReadAsPropertyName(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return Read(ref reader, typeToConvert, options);
	}

	public override void WriteAsPropertyName(System.Text.Json.Utf8JsonWriter writer, ApiKeyType value, System.Text.Json.JsonSerializerOptions options)
	{
		Write(writer, value, options);
	}
}

[JsonConverter(typeof(EnumStructConverter<ClusterPrivilege>))]
public readonly partial struct ClusterPrivilege : IEnumStruct<ClusterPrivilege>
{
	public ClusterPrivilege(string value) => Value = value;
#if NET7_0_OR_GREATER
	static ClusterPrivilege IEnumStruct<ClusterPrivilege>.Create(string value) => value;
#else
	ClusterPrivilege IEnumStruct<ClusterPrivilege>.Create(string value) => value;
#endif
	public readonly string Value { get; }
	public static ClusterPrivilege WriteFleetSecrets { get; } = new ClusterPrivilege("write_fleet_secrets");
	public static ClusterPrivilege WriteConnectorSecrets { get; } = new ClusterPrivilege("write_connector_secrets");
	public static ClusterPrivilege TransportClient { get; } = new ClusterPrivilege("transport_client");
	public static ClusterPrivilege ReadSlm { get; } = new ClusterPrivilege("read_slm");
	public static ClusterPrivilege ReadSecurity { get; } = new ClusterPrivilege("read_security");
	public static ClusterPrivilege ReadPipeline { get; } = new ClusterPrivilege("read_pipeline");
	public static ClusterPrivilege ReadIlm { get; } = new ClusterPrivilege("read_ilm");
	public static ClusterPrivilege ReadFleetSecrets { get; } = new ClusterPrivilege("read_fleet_secrets");
	public static ClusterPrivilege ReadCcr { get; } = new ClusterPrivilege("read_ccr");
	public static ClusterPrivilege PostBehavioralAnalyticsEvent { get; } = new ClusterPrivilege("post_behavioral_analytics_event");
	public static ClusterPrivilege None { get; } = new ClusterPrivilege("none");
	public static ClusterPrivilege MonitorWatcher { get; } = new ClusterPrivilege("monitor_watcher");
	public static ClusterPrivilege MonitorTransform { get; } = new ClusterPrivilege("monitor_transform");
	public static ClusterPrivilege MonitorTextStructure { get; } = new ClusterPrivilege("monitor_text_structure");
	public static ClusterPrivilege MonitorStats { get; } = new ClusterPrivilege("monitor_stats");
	public static ClusterPrivilege MonitorSnapshot { get; } = new ClusterPrivilege("monitor_snapshot");
	public static ClusterPrivilege MonitorRollup { get; } = new ClusterPrivilege("monitor_rollup");
	public static ClusterPrivilege MonitorMl { get; } = new ClusterPrivilege("monitor_ml");
	public static ClusterPrivilege MonitorInference { get; } = new ClusterPrivilege("monitor_inference");
	public static ClusterPrivilege MonitorEnrich { get; } = new ClusterPrivilege("monitor_enrich");
	public static ClusterPrivilege MonitorDataStreamGlobalRetention { get; } = new ClusterPrivilege("monitor_data_stream_global_retention");
	public static ClusterPrivilege MonitorDataFrameTransforms { get; } = new ClusterPrivilege("monitor_data_frame_transforms");
	public static ClusterPrivilege Monitor { get; } = new ClusterPrivilege("monitor");
	public static ClusterPrivilege ManageWatcher { get; } = new ClusterPrivilege("manage_watcher");
	public static ClusterPrivilege ManageUserProfile { get; } = new ClusterPrivilege("manage_user_profile");
	public static ClusterPrivilege ManageTransform { get; } = new ClusterPrivilege("manage_transform");
	public static ClusterPrivilege ManageToken { get; } = new ClusterPrivilege("manage_token");
	public static ClusterPrivilege ManageSlm { get; } = new ClusterPrivilege("manage_slm");
	public static ClusterPrivilege ManageServiceAccount { get; } = new ClusterPrivilege("manage_service_account");
	public static ClusterPrivilege ManageSecurity { get; } = new ClusterPrivilege("manage_security");
	public static ClusterPrivilege ManageSearchSynonyms { get; } = new ClusterPrivilege("manage_search_synonyms");
	public static ClusterPrivilege ManageSearchQueryRules { get; } = new ClusterPrivilege("manage_search_query_rules");
	public static ClusterPrivilege ManageSearchApplication { get; } = new ClusterPrivilege("manage_search_application");
	public static ClusterPrivilege ManageSaml { get; } = new ClusterPrivilege("manage_saml");
	public static ClusterPrivilege ManageRollup { get; } = new ClusterPrivilege("manage_rollup");
	public static ClusterPrivilege ManagePipeline { get; } = new ClusterPrivilege("manage_pipeline");
	public static ClusterPrivilege ManageOwnApiKey { get; } = new ClusterPrivilege("manage_own_api_key");
	public static ClusterPrivilege ManageOidc { get; } = new ClusterPrivilege("manage_oidc");
	public static ClusterPrivilege ManageMl { get; } = new ClusterPrivilege("manage_ml");
	public static ClusterPrivilege ManageLogstashPipelines { get; } = new ClusterPrivilege("manage_logstash_pipelines");
	public static ClusterPrivilege ManageIngestPipelines { get; } = new ClusterPrivilege("manage_ingest_pipelines");
	public static ClusterPrivilege ManageInference { get; } = new ClusterPrivilege("manage_inference");
	public static ClusterPrivilege ManageIndexTemplates { get; } = new ClusterPrivilege("manage_index_templates");
	public static ClusterPrivilege ManageIlm { get; } = new ClusterPrivilege("manage_ilm");
	public static ClusterPrivilege ManageEnrich { get; } = new ClusterPrivilege("manage_enrich");
	public static ClusterPrivilege ManageDataStreamGlobalRetention { get; } = new ClusterPrivilege("manage_data_stream_global_retention");
	public static ClusterPrivilege ManageDataFrameTransforms { get; } = new ClusterPrivilege("manage_data_frame_transforms");
	public static ClusterPrivilege ManageCcr { get; } = new ClusterPrivilege("manage_ccr");
	public static ClusterPrivilege ManageBehavioralAnalytics { get; } = new ClusterPrivilege("manage_behavioral_analytics");
	public static ClusterPrivilege ManageAutoscaling { get; } = new ClusterPrivilege("manage_autoscaling");
	public static ClusterPrivilege ManageApiKey { get; } = new ClusterPrivilege("manage_api_key");
	public static ClusterPrivilege Manage { get; } = new ClusterPrivilege("manage");
	public static ClusterPrivilege GrantApiKey { get; } = new ClusterPrivilege("grant_api_key");
	public static ClusterPrivilege DelegatePki { get; } = new ClusterPrivilege("delegate_pki");
	public static ClusterPrivilege CrossClusterSearch { get; } = new ClusterPrivilege("cross_cluster_search");
	public static ClusterPrivilege CrossClusterReplication { get; } = new ClusterPrivilege("cross_cluster_replication");
	public static ClusterPrivilege CreateSnapshot { get; } = new ClusterPrivilege("create_snapshot");
	public static ClusterPrivilege CancelTask { get; } = new ClusterPrivilege("cancel_task");
	public static ClusterPrivilege All { get; } = new ClusterPrivilege("all");

	public override string ToString() => Value ?? string.Empty;

	public static implicit operator string(ClusterPrivilege clusterPrivilege) => clusterPrivilege.Value;
	public static implicit operator ClusterPrivilege(string value) => new(value);

	public override int GetHashCode() => Value.GetHashCode();
	public override bool Equals(object obj) => obj is ClusterPrivilege other && this.Equals(other);
	public bool Equals(ClusterPrivilege other) => Value == other.Value;

	public static bool operator ==(ClusterPrivilege a, ClusterPrivilege b) => a.Equals(b);
	public static bool operator !=(ClusterPrivilege a, ClusterPrivilege b) => !(a == b);
}

[JsonConverter(typeof(GrantTypeConverter))]
public enum GrantType
{
	/// <summary>
	/// <para>
	/// In this type of grant, you must supply the user ID and password for which you want to create the API key.
	/// </para>
	/// </summary>
	[EnumMember(Value = "password")]
	Password,
	/// <summary>
	/// <para>
	/// In this type of grant, you must supply an access token that was created by the Elasticsearch token service.
	/// </para>
	/// </summary>
	[EnumMember(Value = "access_token")]
	AccessToken
}

internal sealed partial class GrantTypeConverter : System.Text.Json.Serialization.JsonConverter<GrantType>
{
	private static readonly System.Text.Json.JsonEncodedText MemberPassword = System.Text.Json.JsonEncodedText.Encode("password");
	private static readonly System.Text.Json.JsonEncodedText MemberAccessToken = System.Text.Json.JsonEncodedText.Encode("access_token");

	public override GrantType Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		if (reader.ValueTextEquals(MemberPassword))
		{
			return GrantType.Password;
		}

		if (reader.ValueTextEquals(MemberAccessToken))
		{
			return GrantType.AccessToken;
		}

		var value = reader.GetString()!;
		if (string.Equals(value, MemberPassword.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return GrantType.Password;
		}

		if (string.Equals(value, MemberAccessToken.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return GrantType.AccessToken;
		}

		throw new System.Text.Json.JsonException($"Unknown member '{value}' for enum '{nameof(GrantType)}'.");
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, GrantType value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value)
		{
			case GrantType.Password:
				writer.WriteStringValue(MemberPassword);
				break;
			case GrantType.AccessToken:
				writer.WriteStringValue(MemberAccessToken);
				break;
			default:
				throw new System.Text.Json.JsonException($"Invalid value '{value}' for enum '{nameof(GrantType)}'.");
		}
	}

	public override GrantType ReadAsPropertyName(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return Read(ref reader, typeToConvert, options);
	}

	public override void WriteAsPropertyName(System.Text.Json.Utf8JsonWriter writer, GrantType value, System.Text.Json.JsonSerializerOptions options)
	{
		Write(writer, value, options);
	}
}

[JsonConverter(typeof(EnumStructConverter<IndexPrivilege>))]
public readonly partial struct IndexPrivilege : IEnumStruct<IndexPrivilege>
{
	public IndexPrivilege(string value) => Value = value;
#if NET7_0_OR_GREATER
	static IndexPrivilege IEnumStruct<IndexPrivilege>.Create(string value) => value;
#else
	IndexPrivilege IEnumStruct<IndexPrivilege>.Create(string value) => value;
#endif
	public readonly string Value { get; }
	public static IndexPrivilege Write { get; } = new IndexPrivilege("write");
	public static IndexPrivilege ViewIndexMetadata { get; } = new IndexPrivilege("view_index_metadata");
	public static IndexPrivilege ReadCrossCluster { get; } = new IndexPrivilege("read_cross_cluster");
	public static IndexPrivilege Read { get; } = new IndexPrivilege("read");
	public static IndexPrivilege None { get; } = new IndexPrivilege("none");
	public static IndexPrivilege Monitor { get; } = new IndexPrivilege("monitor");
	public static IndexPrivilege ManageLeaderIndex { get; } = new IndexPrivilege("manage_leader_index");
	public static IndexPrivilege ManageIlm { get; } = new IndexPrivilege("manage_ilm");
	public static IndexPrivilege ManageFollowIndex { get; } = new IndexPrivilege("manage_follow_index");
	public static IndexPrivilege ManageDataStreamLifecycle { get; } = new IndexPrivilege("manage_data_stream_lifecycle");
	public static IndexPrivilege Manage { get; } = new IndexPrivilege("manage");
	public static IndexPrivilege Maintenance { get; } = new IndexPrivilege("maintenance");
	public static IndexPrivilege Index { get; } = new IndexPrivilege("index");
	public static IndexPrivilege DeleteIndex { get; } = new IndexPrivilege("delete_index");
	public static IndexPrivilege Delete { get; } = new IndexPrivilege("delete");
	public static IndexPrivilege CrossClusterReplicationInternal { get; } = new IndexPrivilege("cross_cluster_replication_internal");
	public static IndexPrivilege CrossClusterReplication { get; } = new IndexPrivilege("cross_cluster_replication");
	public static IndexPrivilege CreateIndex { get; } = new IndexPrivilege("create_index");
	public static IndexPrivilege CreateDoc { get; } = new IndexPrivilege("create_doc");
	public static IndexPrivilege Create { get; } = new IndexPrivilege("create");
	public static IndexPrivilege AutoConfigure { get; } = new IndexPrivilege("auto_configure");
	public static IndexPrivilege All { get; } = new IndexPrivilege("all");

	public override string ToString() => Value ?? string.Empty;

	public static implicit operator string(IndexPrivilege indexPrivilege) => indexPrivilege.Value;
	public static implicit operator IndexPrivilege(string value) => new(value);

	public override int GetHashCode() => Value.GetHashCode();
	public override bool Equals(object obj) => obj is IndexPrivilege other && this.Equals(other);
	public bool Equals(IndexPrivilege other) => Value == other.Value;

	public static bool operator ==(IndexPrivilege a, IndexPrivilege b) => a.Equals(b);
	public static bool operator !=(IndexPrivilege a, IndexPrivilege b) => !(a == b);
}

[JsonConverter(typeof(RemoteClusterPrivilegeConverter))]
public enum RemoteClusterPrivilege
{
	[EnumMember(Value = "monitor_stats")]
	MonitorStats,
	[EnumMember(Value = "monitor_enrich")]
	MonitorEnrich
}

internal sealed partial class RemoteClusterPrivilegeConverter : System.Text.Json.Serialization.JsonConverter<RemoteClusterPrivilege>
{
	private static readonly System.Text.Json.JsonEncodedText MemberMonitorStats = System.Text.Json.JsonEncodedText.Encode("monitor_stats");
	private static readonly System.Text.Json.JsonEncodedText MemberMonitorEnrich = System.Text.Json.JsonEncodedText.Encode("monitor_enrich");

	public override RemoteClusterPrivilege Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		if (reader.ValueTextEquals(MemberMonitorStats))
		{
			return RemoteClusterPrivilege.MonitorStats;
		}

		if (reader.ValueTextEquals(MemberMonitorEnrich))
		{
			return RemoteClusterPrivilege.MonitorEnrich;
		}

		var value = reader.GetString()!;
		if (string.Equals(value, MemberMonitorStats.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return RemoteClusterPrivilege.MonitorStats;
		}

		if (string.Equals(value, MemberMonitorEnrich.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return RemoteClusterPrivilege.MonitorEnrich;
		}

		throw new System.Text.Json.JsonException($"Unknown member '{value}' for enum '{nameof(RemoteClusterPrivilege)}'.");
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, RemoteClusterPrivilege value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value)
		{
			case RemoteClusterPrivilege.MonitorStats:
				writer.WriteStringValue(MemberMonitorStats);
				break;
			case RemoteClusterPrivilege.MonitorEnrich:
				writer.WriteStringValue(MemberMonitorEnrich);
				break;
			default:
				throw new System.Text.Json.JsonException($"Invalid value '{value}' for enum '{nameof(RemoteClusterPrivilege)}'.");
		}
	}

	public override RemoteClusterPrivilege ReadAsPropertyName(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return Read(ref reader, typeToConvert, options);
	}

	public override void WriteAsPropertyName(System.Text.Json.Utf8JsonWriter writer, RemoteClusterPrivilege value, System.Text.Json.JsonSerializerOptions options)
	{
		Write(writer, value, options);
	}
}

[JsonConverter(typeof(EnumStructConverter<RestrictionWorkflow>))]
public readonly partial struct RestrictionWorkflow : IEnumStruct<RestrictionWorkflow>
{
	public RestrictionWorkflow(string value) => Value = value;
#if NET7_0_OR_GREATER
	static RestrictionWorkflow IEnumStruct<RestrictionWorkflow>.Create(string value) => value;
#else
	RestrictionWorkflow IEnumStruct<RestrictionWorkflow>.Create(string value) => value;
#endif
	public readonly string Value { get; }
	public static RestrictionWorkflow SearchApplicationQuery { get; } = new RestrictionWorkflow("search_application_query");

	public override string ToString() => Value ?? string.Empty;

	public static implicit operator string(RestrictionWorkflow restrictionWorkflow) => restrictionWorkflow.Value;
	public static implicit operator RestrictionWorkflow(string value) => new(value);

	public override int GetHashCode() => Value.GetHashCode();
	public override bool Equals(object obj) => obj is RestrictionWorkflow other && this.Equals(other);
	public bool Equals(RestrictionWorkflow other) => Value == other.Value;

	public static bool operator ==(RestrictionWorkflow a, RestrictionWorkflow b) => a.Equals(b);
	public static bool operator !=(RestrictionWorkflow a, RestrictionWorkflow b) => !(a == b);
}

[JsonConverter(typeof(TemplateFormatConverter))]
public enum TemplateFormat
{
	[EnumMember(Value = "string")]
	String,
	[EnumMember(Value = "json")]
	Json
}

internal sealed partial class TemplateFormatConverter : System.Text.Json.Serialization.JsonConverter<TemplateFormat>
{
	private static readonly System.Text.Json.JsonEncodedText MemberString = System.Text.Json.JsonEncodedText.Encode("string");
	private static readonly System.Text.Json.JsonEncodedText MemberJson = System.Text.Json.JsonEncodedText.Encode("json");

	public override TemplateFormat Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		if (reader.ValueTextEquals(MemberString))
		{
			return TemplateFormat.String;
		}

		if (reader.ValueTextEquals(MemberJson))
		{
			return TemplateFormat.Json;
		}

		var value = reader.GetString()!;
		if (string.Equals(value, MemberString.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return TemplateFormat.String;
		}

		if (string.Equals(value, MemberJson.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return TemplateFormat.Json;
		}

		throw new System.Text.Json.JsonException($"Unknown member '{value}' for enum '{nameof(TemplateFormat)}'.");
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, TemplateFormat value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value)
		{
			case TemplateFormat.String:
				writer.WriteStringValue(MemberString);
				break;
			case TemplateFormat.Json:
				writer.WriteStringValue(MemberJson);
				break;
			default:
				throw new System.Text.Json.JsonException($"Invalid value '{value}' for enum '{nameof(TemplateFormat)}'.");
		}
	}

	public override TemplateFormat ReadAsPropertyName(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return Read(ref reader, typeToConvert, options);
	}

	public override void WriteAsPropertyName(System.Text.Json.Utf8JsonWriter writer, TemplateFormat value, System.Text.Json.JsonSerializerOptions options)
	{
		Write(writer, value, options);
	}
}