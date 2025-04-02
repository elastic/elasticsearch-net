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

namespace Elastic.Clients.Elasticsearch.MachineLearning;

public sealed partial class GetCalendarEventsRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// Specifies to get events with timestamps earlier than this time.
	/// </para>
	/// </summary>
	public System.DateTime? End { get => Q<System.DateTime?>("end"); set => Q("end", value); }

	/// <summary>
	/// <para>
	/// Skips the specified number of events.
	/// </para>
	/// </summary>
	public int? From { get => Q<int?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>
	/// Specifies to get events for a specific anomaly detection job identifier or job group. It must be used with a calendar identifier of <c>_all</c> or <c>*</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Id? JobId { get => Q<Elastic.Clients.Elasticsearch.Id?>("job_id"); set => Q("job_id", value); }

	/// <summary>
	/// <para>
	/// Specifies the maximum number of events to obtain.
	/// </para>
	/// </summary>
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }

	/// <summary>
	/// <para>
	/// Specifies to get events with timestamps after this time.
	/// </para>
	/// </summary>
	public System.DateTime? Start { get => Q<System.DateTime?>("start"); set => Q("start", value); }
}

internal sealed partial class GetCalendarEventsRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequest>
{
	public override Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get info about events in calendars.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequestConverter))]
public sealed partial class GetCalendarEventsRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetCalendarEventsRequest(Elastic.Clients.Elasticsearch.Id calendarId) : base(r => r.Required("calendar_id", calendarId))
	{
	}
#if NET7_0_OR_GREATER
	public GetCalendarEventsRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetCalendarEventsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.MachineLearningGetCalendarEvents;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ml.get_calendar_events";

	/// <summary>
	/// <para>
	/// A string that uniquely identifies a calendar. You can get information for multiple calendars by using a comma-separated list of ids or a wildcard expression. You can get information for all calendars by using <c>_all</c> or <c>*</c> or by omitting the calendar identifier.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id CalendarId { get => P<Elastic.Clients.Elasticsearch.Id>("calendar_id"); set => PR("calendar_id", value); }

	/// <summary>
	/// <para>
	/// Specifies to get events with timestamps earlier than this time.
	/// </para>
	/// </summary>
	public System.DateTime? End { get => Q<System.DateTime?>("end"); set => Q("end", value); }

	/// <summary>
	/// <para>
	/// Skips the specified number of events.
	/// </para>
	/// </summary>
	public int? From { get => Q<int?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>
	/// Specifies to get events for a specific anomaly detection job identifier or job group. It must be used with a calendar identifier of <c>_all</c> or <c>*</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Id? JobId { get => Q<Elastic.Clients.Elasticsearch.Id?>("job_id"); set => Q("job_id", value); }

	/// <summary>
	/// <para>
	/// Specifies the maximum number of events to obtain.
	/// </para>
	/// </summary>
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }

	/// <summary>
	/// <para>
	/// Specifies to get events with timestamps after this time.
	/// </para>
	/// </summary>
	public System.DateTime? Start { get => Q<System.DateTime?>("start"); set => Q("start", value); }
}

/// <summary>
/// <para>
/// Get info about events in calendars.
/// </para>
/// </summary>
public readonly partial struct GetCalendarEventsRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetCalendarEventsRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequest instance)
	{
		Instance = instance;
	}

	public GetCalendarEventsRequestDescriptor(Elastic.Clients.Elasticsearch.Id calendarId)
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequest(calendarId);
	}

	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public GetCalendarEventsRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequest instance) => new Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequest(Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A string that uniquely identifies a calendar. You can get information for multiple calendars by using a comma-separated list of ids or a wildcard expression. You can get information for all calendars by using <c>_all</c> or <c>*</c> or by omitting the calendar identifier.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequestDescriptor CalendarId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.CalendarId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies to get events with timestamps earlier than this time.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequestDescriptor End(System.DateTime? value)
	{
		Instance.End = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Skips the specified number of events.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequestDescriptor From(int? value)
	{
		Instance.From = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies to get events for a specific anomaly detection job identifier or job group. It must be used with a calendar identifier of <c>_all</c> or <c>*</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequestDescriptor JobId(Elastic.Clients.Elasticsearch.Id? value)
	{
		Instance.JobId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies the maximum number of events to obtain.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequestDescriptor Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies to get events with timestamps after this time.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequestDescriptor Start(System.DateTime? value)
	{
		Instance.Start = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequest Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequestDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarEventsRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}