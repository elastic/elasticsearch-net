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

public sealed partial class GetCalendarsRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// Skips the specified number of calendars. This parameter is supported only when you omit the calendar identifier.
	/// </para>
	/// </summary>
	public int? From { get => Q<int?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>
	/// Specifies the maximum number of calendars to obtain. This parameter is supported only when you omit the calendar identifier.
	/// </para>
	/// </summary>
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }
}

internal sealed partial class GetCalendarsRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropPage = System.Text.Json.JsonEncodedText.Encode("page");

	public override Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.Page?> propPage = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propPage.TryReadProperty(ref reader, options, PropPage, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Page = propPage.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropPage, value.Page, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get calendar configuration info.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequestConverter))]
public sealed partial class GetCalendarsRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequestParameters>
{
	public GetCalendarsRequest(Elastic.Clients.Elasticsearch.Id? calendarId) : base(r => r.Optional("calendar_id", calendarId))
	{
	}
#if NET7_0_OR_GREATER
	public GetCalendarsRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public GetCalendarsRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetCalendarsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.MachineLearningGetCalendars;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.get_calendars";

	/// <summary>
	/// <para>
	/// A string that uniquely identifies a calendar. You can get information for multiple calendars by using a comma-separated list of ids or a wildcard expression. You can get information for all calendars by using <c>_all</c> or <c>*</c> or by omitting the calendar identifier.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Id? CalendarId { get => P<Elastic.Clients.Elasticsearch.Id?>("calendar_id"); set => PO("calendar_id", value); }

	/// <summary>
	/// <para>
	/// Skips the specified number of calendars. This parameter is supported only when you omit the calendar identifier.
	/// </para>
	/// </summary>
	public int? From { get => Q<int?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>
	/// Specifies the maximum number of calendars to obtain. This parameter is supported only when you omit the calendar identifier.
	/// </para>
	/// </summary>
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }

	/// <summary>
	/// <para>
	/// This object is supported only when you omit the calendar identifier.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.Page? Page { get; set; }
}

/// <summary>
/// <para>
/// Get calendar configuration info.
/// </para>
/// </summary>
public readonly partial struct GetCalendarsRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetCalendarsRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequest instance)
	{
		Instance = instance;
	}

	public GetCalendarsRequestDescriptor(Elastic.Clients.Elasticsearch.Id calendarId)
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequest(calendarId);
	}

	public GetCalendarsRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequest instance) => new Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequest(Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A string that uniquely identifies a calendar. You can get information for multiple calendars by using a comma-separated list of ids or a wildcard expression. You can get information for all calendars by using <c>_all</c> or <c>*</c> or by omitting the calendar identifier.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequestDescriptor CalendarId(Elastic.Clients.Elasticsearch.Id? value)
	{
		Instance.CalendarId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Skips the specified number of calendars. This parameter is supported only when you omit the calendar identifier.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequestDescriptor From(int? value)
	{
		Instance.From = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies the maximum number of calendars to obtain. This parameter is supported only when you omit the calendar identifier.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequestDescriptor Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// This object is supported only when you omit the calendar identifier.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequestDescriptor Page(Elastic.Clients.Elasticsearch.MachineLearning.Page? value)
	{
		Instance.Page = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// This object is supported only when you omit the calendar identifier.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequestDescriptor Page()
	{
		Instance.Page = Elastic.Clients.Elasticsearch.MachineLearning.PageDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// This object is supported only when you omit the calendar identifier.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequestDescriptor Page(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.PageDescriptor>? action)
	{
		Instance.Page = Elastic.Clients.Elasticsearch.MachineLearning.PageDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequest Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequestDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.GetCalendarsRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}