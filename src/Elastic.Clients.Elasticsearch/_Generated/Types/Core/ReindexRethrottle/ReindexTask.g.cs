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

namespace Elastic.Clients.Elasticsearch.Core.ReindexRethrottle;

internal sealed partial class ReindexTaskConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.ReindexRethrottle.ReindexTask>
{
	private static readonly System.Text.Json.JsonEncodedText PropAction = System.Text.Json.JsonEncodedText.Encode("action");
	private static readonly System.Text.Json.JsonEncodedText PropCancellable = System.Text.Json.JsonEncodedText.Encode("cancellable");
	private static readonly System.Text.Json.JsonEncodedText PropDescription = System.Text.Json.JsonEncodedText.Encode("description");
	private static readonly System.Text.Json.JsonEncodedText PropHeaders = System.Text.Json.JsonEncodedText.Encode("headers");
	private static readonly System.Text.Json.JsonEncodedText PropId = System.Text.Json.JsonEncodedText.Encode("id");
	private static readonly System.Text.Json.JsonEncodedText PropNode = System.Text.Json.JsonEncodedText.Encode("node");
	private static readonly System.Text.Json.JsonEncodedText PropRunningTimeInNanos = System.Text.Json.JsonEncodedText.Encode("running_time_in_nanos");
	private static readonly System.Text.Json.JsonEncodedText PropStartTimeInMillis = System.Text.Json.JsonEncodedText.Encode("start_time_in_millis");
	private static readonly System.Text.Json.JsonEncodedText PropStatus = System.Text.Json.JsonEncodedText.Encode("status");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");

	public override Elastic.Clients.Elasticsearch.Core.ReindexRethrottle.ReindexTask Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propAction = default;
		LocalJsonValue<bool> propCancellable = default;
		LocalJsonValue<string> propDescription = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.ICollection<string>>> propHeaders = default;
		LocalJsonValue<long> propId = default;
		LocalJsonValue<string> propNode = default;
		LocalJsonValue<System.TimeSpan> propRunningTimeInNanos = default;
		LocalJsonValue<System.DateTime> propStartTimeInMillis = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Core.ReindexRethrottle.ReindexStatus> propStatus = default;
		LocalJsonValue<string> propType = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAction.TryReadProperty(ref reader, options, PropAction, null))
			{
				continue;
			}

			if (propCancellable.TryReadProperty(ref reader, options, PropCancellable, null))
			{
				continue;
			}

			if (propDescription.TryReadProperty(ref reader, options, PropDescription, null))
			{
				continue;
			}

			if (propHeaders.TryReadProperty(ref reader, options, PropHeaders, static System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.ICollection<string>> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, System.Collections.Generic.ICollection<string>>(o, null, static System.Collections.Generic.ICollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadSingleOrManyCollectionValue<string>(o, null)!)!))
			{
				continue;
			}

			if (propId.TryReadProperty(ref reader, options, PropId, null))
			{
				continue;
			}

			if (propNode.TryReadProperty(ref reader, options, PropNode, null))
			{
				continue;
			}

			if (propRunningTimeInNanos.TryReadProperty(ref reader, options, PropRunningTimeInNanos, static System.TimeSpan (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanNanosMarker))))
			{
				continue;
			}

			if (propStartTimeInMillis.TryReadProperty(ref reader, options, PropStartTimeInMillis, static System.DateTime (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTime>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker))))
			{
				continue;
			}

			if (propStatus.TryReadProperty(ref reader, options, PropStatus, null))
			{
				continue;
			}

			if (propType.TryReadProperty(ref reader, options, PropType, null))
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
		return new Elastic.Clients.Elasticsearch.Core.ReindexRethrottle.ReindexTask(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Action = propAction.Value,
			Cancellable = propCancellable.Value,
			Description = propDescription.Value,
			Headers = propHeaders.Value,
			Id = propId.Value,
			Node = propNode.Value,
			RunningTimeInNanos = propRunningTimeInNanos.Value,
			StartTimeInMillis = propStartTimeInMillis.Value,
			Status = propStatus.Value,
			Type = propType.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.ReindexRethrottle.ReindexTask value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAction, value.Action, null, null);
		writer.WriteProperty(options, PropCancellable, value.Cancellable, null, null);
		writer.WriteProperty(options, PropDescription, value.Description, null, null);
		writer.WriteProperty(options, PropHeaders, value.Headers, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.ICollection<string>> v) => w.WriteDictionaryValue<string, System.Collections.Generic.ICollection<string>>(o, v, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string> v) => w.WriteSingleOrManyCollectionValue<string>(o, v, null)));
		writer.WriteProperty(options, PropId, value.Id, null, null);
		writer.WriteProperty(options, PropNode, value.Node, null, null);
		writer.WriteProperty(options, PropRunningTimeInNanos, value.RunningTimeInNanos, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan v) => w.WriteValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanNanosMarker)));
		writer.WriteProperty(options, PropStartTimeInMillis, value.StartTimeInMillis, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTime v) => w.WriteValueEx<System.DateTime>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker)));
		writer.WriteProperty(options, PropStatus, value.Status, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.ReindexRethrottle.ReindexTaskConverter))]
public sealed partial class ReindexTask
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ReindexTask(string action, bool cancellable, string description, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.ICollection<string>> headers, long id, string node, System.TimeSpan runningTimeInNanos, System.DateTime startTimeInMillis, Elastic.Clients.Elasticsearch.Core.ReindexRethrottle.ReindexStatus status, string type)
	{
		Action = action;
		Cancellable = cancellable;
		Description = description;
		Headers = headers;
		Id = id;
		Node = node;
		RunningTimeInNanos = runningTimeInNanos;
		StartTimeInMillis = startTimeInMillis;
		Status = status;
		Type = type;
	}
#if NET7_0_OR_GREATER
	public ReindexTask()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ReindexTask()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ReindexTask(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	string Action { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool Cancellable { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Description { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.ICollection<string>> Headers { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long Id { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Node { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.TimeSpan RunningTimeInNanos { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.DateTime StartTimeInMillis { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Core.ReindexRethrottle.ReindexStatus Status { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Type { get; set; }
}