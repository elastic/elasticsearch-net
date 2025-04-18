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

namespace Elastic.Clients.Elasticsearch.SearchApplication;

internal sealed partial class EventTypeConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.SearchApplication.EventType>
{
	private static readonly System.Text.Json.JsonEncodedText MemberPageView = System.Text.Json.JsonEncodedText.Encode("page_view");
	private static readonly System.Text.Json.JsonEncodedText MemberSearch = System.Text.Json.JsonEncodedText.Encode("search");
	private static readonly System.Text.Json.JsonEncodedText MemberSearchClick = System.Text.Json.JsonEncodedText.Encode("search_click");

	public override Elastic.Clients.Elasticsearch.SearchApplication.EventType Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		if (reader.ValueTextEquals(MemberPageView))
		{
			return Elastic.Clients.Elasticsearch.SearchApplication.EventType.PageView;
		}

		if (reader.ValueTextEquals(MemberSearch))
		{
			return Elastic.Clients.Elasticsearch.SearchApplication.EventType.Search;
		}

		if (reader.ValueTextEquals(MemberSearchClick))
		{
			return Elastic.Clients.Elasticsearch.SearchApplication.EventType.SearchClick;
		}

		var value = reader.GetString()!;
		if (string.Equals(value, MemberPageView.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.SearchApplication.EventType.PageView;
		}

		if (string.Equals(value, MemberSearch.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.SearchApplication.EventType.Search;
		}

		if (string.Equals(value, MemberSearchClick.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.SearchApplication.EventType.SearchClick;
		}

		throw new System.Text.Json.JsonException($"Unknown member '{value}' for enum '{nameof(Elastic.Clients.Elasticsearch.SearchApplication.EventType)}'.");
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.SearchApplication.EventType value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value)
		{
			case Elastic.Clients.Elasticsearch.SearchApplication.EventType.PageView:
				writer.WriteStringValue(MemberPageView);
				break;
			case Elastic.Clients.Elasticsearch.SearchApplication.EventType.Search:
				writer.WriteStringValue(MemberSearch);
				break;
			case Elastic.Clients.Elasticsearch.SearchApplication.EventType.SearchClick:
				writer.WriteStringValue(MemberSearchClick);
				break;
			default:
				throw new System.Text.Json.JsonException($"Invalid value '{value}' for enum '{nameof(Elastic.Clients.Elasticsearch.SearchApplication.EventType)}'.");
		}
	}

	public override Elastic.Clients.Elasticsearch.SearchApplication.EventType ReadAsPropertyName(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return Read(ref reader, typeToConvert, options);
	}

	public override void WriteAsPropertyName(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.SearchApplication.EventType value, System.Text.Json.JsonSerializerOptions options)
	{
		Write(writer, value, options);
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.SearchApplication.EventTypeConverter))]
public enum EventType
{
	[System.Runtime.Serialization.EnumMember(Value = "page_view")]
	PageView,
	[System.Runtime.Serialization.EnumMember(Value = "search")]
	Search,
	[System.Runtime.Serialization.EnumMember(Value = "search_click")]
	SearchClick
}