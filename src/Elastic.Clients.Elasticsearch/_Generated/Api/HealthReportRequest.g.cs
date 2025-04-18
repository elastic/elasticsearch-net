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

public sealed partial class HealthReportRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// Limit the number of affected resources the health report API returns.
	/// </para>
	/// </summary>
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }

	/// <summary>
	/// <para>
	/// Explicit operation timeout.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>
	/// Opt-in for more information about the health of the system.
	/// </para>
	/// </summary>
	public bool? Verbose { get => Q<bool?>("verbose"); set => Q("verbose", value); }
}

internal sealed partial class HealthReportRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.HealthReportRequest>
{
	public override Elastic.Clients.Elasticsearch.HealthReportRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.HealthReportRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.HealthReportRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get the cluster health.
/// Get a report with the health status of an Elasticsearch cluster.
/// The report contains a list of indicators that compose Elasticsearch functionality.
/// </para>
/// <para>
/// Each indicator has a health status of: green, unknown, yellow or red.
/// The indicator will provide an explanation and metadata describing the reason for its current health status.
/// </para>
/// <para>
/// The cluster’s status is controlled by the worst indicator status.
/// </para>
/// <para>
/// In the event that an indicator’s status is non-green, a list of impacts may be present in the indicator result which detail the functionalities that are negatively affected by the health issue.
/// Each impact carries with it a severity level, an area of the system that is affected, and a simple description of the impact on the system.
/// </para>
/// <para>
/// Some health indicators can determine the root cause of a health problem and prescribe a set of steps that can be performed in order to improve the health of the system.
/// The root cause and remediation steps are encapsulated in a diagnosis.
/// A diagnosis contains a cause detailing a root cause analysis, an action containing a brief description of the steps to take to fix the problem, the list of affected resources (if applicable), and a detailed step-by-step troubleshooting guide to fix the diagnosed problem.
/// </para>
/// <para>
/// NOTE: The health indicators perform root cause analysis of non-green health statuses. This can be computationally expensive when called frequently.
/// When setting up automated polling of the API for health status, set verbose to false to disable the more expensive analysis logic.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.HealthReportRequestConverter))]
public sealed partial class HealthReportRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.HealthReportRequestParameters>
{
	public HealthReportRequest(System.Collections.Generic.ICollection<string>? feature) : base(r => r.Optional("feature", feature))
	{
	}
#if NET7_0_OR_GREATER
	public HealthReportRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public HealthReportRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal HealthReportRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.NoNamespaceHealthReport;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "health_report";

	/// <summary>
	/// <para>
	/// A feature of the cluster, as returned by the top-level health report API.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<string>? Feature { get => P<System.Collections.Generic.ICollection<string>?>("feature"); set => PO("feature", value); }

	/// <summary>
	/// <para>
	/// Limit the number of affected resources the health report API returns.
	/// </para>
	/// </summary>
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }

	/// <summary>
	/// <para>
	/// Explicit operation timeout.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>
	/// Opt-in for more information about the health of the system.
	/// </para>
	/// </summary>
	public bool? Verbose { get => Q<bool?>("verbose"); set => Q("verbose", value); }
}

/// <summary>
/// <para>
/// Get the cluster health.
/// Get a report with the health status of an Elasticsearch cluster.
/// The report contains a list of indicators that compose Elasticsearch functionality.
/// </para>
/// <para>
/// Each indicator has a health status of: green, unknown, yellow or red.
/// The indicator will provide an explanation and metadata describing the reason for its current health status.
/// </para>
/// <para>
/// The cluster’s status is controlled by the worst indicator status.
/// </para>
/// <para>
/// In the event that an indicator’s status is non-green, a list of impacts may be present in the indicator result which detail the functionalities that are negatively affected by the health issue.
/// Each impact carries with it a severity level, an area of the system that is affected, and a simple description of the impact on the system.
/// </para>
/// <para>
/// Some health indicators can determine the root cause of a health problem and prescribe a set of steps that can be performed in order to improve the health of the system.
/// The root cause and remediation steps are encapsulated in a diagnosis.
/// A diagnosis contains a cause detailing a root cause analysis, an action containing a brief description of the steps to take to fix the problem, the list of affected resources (if applicable), and a detailed step-by-step troubleshooting guide to fix the diagnosed problem.
/// </para>
/// <para>
/// NOTE: The health indicators perform root cause analysis of non-green health statuses. This can be computationally expensive when called frequently.
/// When setting up automated polling of the API for health status, set verbose to false to disable the more expensive analysis logic.
/// </para>
/// </summary>
public readonly partial struct HealthReportRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.HealthReportRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public HealthReportRequestDescriptor(Elastic.Clients.Elasticsearch.HealthReportRequest instance)
	{
		Instance = instance;
	}

	public HealthReportRequestDescriptor(System.Collections.Generic.ICollection<string>? feature)
	{
		Instance = new Elastic.Clients.Elasticsearch.HealthReportRequest(feature);
	}

	public HealthReportRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.HealthReportRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.HealthReportRequestDescriptor(Elastic.Clients.Elasticsearch.HealthReportRequest instance) => new Elastic.Clients.Elasticsearch.HealthReportRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.HealthReportRequest(Elastic.Clients.Elasticsearch.HealthReportRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A feature of the cluster, as returned by the top-level health report API.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.HealthReportRequestDescriptor Feature(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.Feature = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A feature of the cluster, as returned by the top-level health report API.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.HealthReportRequestDescriptor Feature(params string[] values)
	{
		Instance.Feature = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Limit the number of affected resources the health report API returns.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.HealthReportRequestDescriptor Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Explicit operation timeout.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.HealthReportRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Opt-in for more information about the health of the system.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.HealthReportRequestDescriptor Verbose(bool? value = true)
	{
		Instance.Verbose = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.HealthReportRequest Build(System.Action<Elastic.Clients.Elasticsearch.HealthReportRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.HealthReportRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.HealthReportRequestDescriptor(new Elastic.Clients.Elasticsearch.HealthReportRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.HealthReportRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.HealthReportRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.HealthReportRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.HealthReportRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.HealthReportRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.HealthReportRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.HealthReportRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}