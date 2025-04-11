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

public sealed partial class PostCalendarEventsRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class PostCalendarEventsRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropEvents = System.Text.Json.JsonEncodedText.Encode("events");

	public override Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.CalendarEvent>> propEvents = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propEvents.TryReadProperty(ref reader, options, PropEvents, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.CalendarEvent> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.CalendarEvent>(o, null)!))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Events = propEvents.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropEvents, value.Events, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.CalendarEvent> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.CalendarEvent>(o, v, null));
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Add scheduled events to the calendar.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequestConverter))]
public sealed partial class PostCalendarEventsRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequestParameters>
{
	[System.Obsolete("The request contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PostCalendarEventsRequest(Elastic.Clients.Elasticsearch.Id calendarId) : base(r => r.Required("calendar_id", calendarId))
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PostCalendarEventsRequest(Elastic.Clients.Elasticsearch.Id calendarId, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.CalendarEvent> events) : base(r => r.Required("calendar_id", calendarId))
	{
		Events = events;
	}
#if NET7_0_OR_GREATER
	public PostCalendarEventsRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PostCalendarEventsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.MachineLearningPostCalendarEvents;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.post_calendar_events";

	/// <summary>
	/// <para>
	/// A string that uniquely identifies a calendar.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id CalendarId { get => P<Elastic.Clients.Elasticsearch.Id>("calendar_id"); set => PR("calendar_id", value); }

	/// <summary>
	/// <para>
	/// A list of one of more scheduled events. The event’s start and end times can be specified as integer milliseconds since the epoch or as a string in ISO 8601 format.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.CalendarEvent> Events { get; set; }
}

/// <summary>
/// <para>
/// Add scheduled events to the calendar.
/// </para>
/// </summary>
public readonly partial struct PostCalendarEventsRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PostCalendarEventsRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequest instance)
	{
		Instance = instance;
	}

	public PostCalendarEventsRequestDescriptor(Elastic.Clients.Elasticsearch.Id calendarId)
	{
#pragma warning disable CS0618
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequest(calendarId);
#pragma warning restore CS0618
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public PostCalendarEventsRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequest instance) => new Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequest(Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A string that uniquely identifies a calendar.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequestDescriptor CalendarId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.CalendarId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of one of more scheduled events. The event’s start and end times can be specified as integer milliseconds since the epoch or as a string in ISO 8601 format.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequestDescriptor Events(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.CalendarEvent> value)
	{
		Instance.Events = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of one of more scheduled events. The event’s start and end times can be specified as integer milliseconds since the epoch or as a string in ISO 8601 format.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequestDescriptor Events(params Elastic.Clients.Elasticsearch.MachineLearning.CalendarEvent[] values)
	{
		Instance.Events = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of one of more scheduled events. The event’s start and end times can be specified as integer milliseconds since the epoch or as a string in ISO 8601 format.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequestDescriptor Events(params System.Action<Elastic.Clients.Elasticsearch.MachineLearning.CalendarEventDescriptor>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.MachineLearning.CalendarEvent>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.MachineLearning.CalendarEventDescriptor.Build(action));
		}

		Instance.Events = items;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequest Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequestDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.PostCalendarEventsRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}