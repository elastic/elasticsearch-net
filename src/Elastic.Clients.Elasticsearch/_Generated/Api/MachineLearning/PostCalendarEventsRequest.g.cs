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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.MachineLearning;

public sealed partial class PostCalendarEventsRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Add scheduled events to the calendar.
/// </para>
/// </summary>
public sealed partial class PostCalendarEventsRequest : PlainRequest<PostCalendarEventsRequestParameters>
{
	public PostCalendarEventsRequest(Elastic.Clients.Elasticsearch.Id calendarId) : base(r => r.Required("calendar_id", calendarId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningPostCalendarEvents;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.post_calendar_events";

	/// <summary>
	/// <para>
	/// A list of one of more scheduled events. The event’s start and end times can be specified as integer milliseconds since the epoch or as a string in ISO 8601 format.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("events")]
	public ICollection<Elastic.Clients.Elasticsearch.MachineLearning.CalendarEvent> Events { get; set; }
}

/// <summary>
/// <para>
/// Add scheduled events to the calendar.
/// </para>
/// </summary>
public sealed partial class PostCalendarEventsRequestDescriptor : RequestDescriptor<PostCalendarEventsRequestDescriptor, PostCalendarEventsRequestParameters>
{
	internal PostCalendarEventsRequestDescriptor(Action<PostCalendarEventsRequestDescriptor> configure) => configure.Invoke(this);

	public PostCalendarEventsRequestDescriptor(Elastic.Clients.Elasticsearch.Id calendarId) : base(r => r.Required("calendar_id", calendarId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningPostCalendarEvents;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.post_calendar_events";

	public PostCalendarEventsRequestDescriptor CalendarId(Elastic.Clients.Elasticsearch.Id calendarId)
	{
		RouteValues.Required("calendar_id", calendarId);
		return Self;
	}

	private ICollection<Elastic.Clients.Elasticsearch.MachineLearning.CalendarEvent> EventsValue { get; set; }
	private Elastic.Clients.Elasticsearch.MachineLearning.CalendarEventDescriptor EventsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.MachineLearning.CalendarEventDescriptor> EventsDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.MachineLearning.CalendarEventDescriptor>[] EventsDescriptorActions { get; set; }

	/// <summary>
	/// <para>
	/// A list of one of more scheduled events. The event’s start and end times can be specified as integer milliseconds since the epoch or as a string in ISO 8601 format.
	/// </para>
	/// </summary>
	public PostCalendarEventsRequestDescriptor Events(ICollection<Elastic.Clients.Elasticsearch.MachineLearning.CalendarEvent> events)
	{
		EventsDescriptor = null;
		EventsDescriptorAction = null;
		EventsDescriptorActions = null;
		EventsValue = events;
		return Self;
	}

	public PostCalendarEventsRequestDescriptor Events(Elastic.Clients.Elasticsearch.MachineLearning.CalendarEventDescriptor descriptor)
	{
		EventsValue = null;
		EventsDescriptorAction = null;
		EventsDescriptorActions = null;
		EventsDescriptor = descriptor;
		return Self;
	}

	public PostCalendarEventsRequestDescriptor Events(Action<Elastic.Clients.Elasticsearch.MachineLearning.CalendarEventDescriptor> configure)
	{
		EventsValue = null;
		EventsDescriptor = null;
		EventsDescriptorActions = null;
		EventsDescriptorAction = configure;
		return Self;
	}

	public PostCalendarEventsRequestDescriptor Events(params Action<Elastic.Clients.Elasticsearch.MachineLearning.CalendarEventDescriptor>[] configure)
	{
		EventsValue = null;
		EventsDescriptor = null;
		EventsDescriptorAction = null;
		EventsDescriptorActions = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (EventsDescriptor is not null)
		{
			writer.WritePropertyName("events");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, EventsDescriptor, options);
			writer.WriteEndArray();
		}
		else if (EventsDescriptorAction is not null)
		{
			writer.WritePropertyName("events");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.MachineLearning.CalendarEventDescriptor(EventsDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (EventsDescriptorActions is not null)
		{
			writer.WritePropertyName("events");
			writer.WriteStartArray();
			foreach (var action in EventsDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.MachineLearning.CalendarEventDescriptor(action), options);
			}

			writer.WriteEndArray();
		}
		else
		{
			writer.WritePropertyName("events");
			JsonSerializer.Serialize(writer, EventsValue, options);
		}

		writer.WriteEndObject();
	}
}