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

internal sealed partial class PutDataFrameAnalyticsResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.PutDataFrameAnalyticsResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropAllowLazyStart = System.Text.Json.JsonEncodedText.Encode("allow_lazy_start");
	private static readonly System.Text.Json.JsonEncodedText PropAnalysis = System.Text.Json.JsonEncodedText.Encode("analysis");
	private static readonly System.Text.Json.JsonEncodedText PropAnalyzedFields = System.Text.Json.JsonEncodedText.Encode("analyzed_fields");
	private static readonly System.Text.Json.JsonEncodedText PropAuthorization = System.Text.Json.JsonEncodedText.Encode("authorization");
	private static readonly System.Text.Json.JsonEncodedText PropCreateTime = System.Text.Json.JsonEncodedText.Encode("create_time");
	private static readonly System.Text.Json.JsonEncodedText PropDescription = System.Text.Json.JsonEncodedText.Encode("description");
	private static readonly System.Text.Json.JsonEncodedText PropDest = System.Text.Json.JsonEncodedText.Encode("dest");
	private static readonly System.Text.Json.JsonEncodedText PropId = System.Text.Json.JsonEncodedText.Encode("id");
	private static readonly System.Text.Json.JsonEncodedText PropMaxNumThreads = System.Text.Json.JsonEncodedText.Encode("max_num_threads");
	private static readonly System.Text.Json.JsonEncodedText PropMeta = System.Text.Json.JsonEncodedText.Encode("_meta");
	private static readonly System.Text.Json.JsonEncodedText PropModelMemoryLimit = System.Text.Json.JsonEncodedText.Encode("model_memory_limit");
	private static readonly System.Text.Json.JsonEncodedText PropSource = System.Text.Json.JsonEncodedText.Encode("source");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.MachineLearning.PutDataFrameAnalyticsResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool> propAllowLazyStart = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysis> propAnalysis = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFields?> propAnalyzedFields = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsAuthorization?> propAuthorization = default;
		LocalJsonValue<System.DateTimeOffset> propCreateTime = default;
		LocalJsonValue<string?> propDescription = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsDestination> propDest = default;
		LocalJsonValue<string> propId = default;
		LocalJsonValue<int> propMaxNumThreads = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, object>?> propMeta = default;
		LocalJsonValue<string> propModelMemoryLimit = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsSource> propSource = default;
		LocalJsonValue<string> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAllowLazyStart.TryReadProperty(ref reader, options, PropAllowLazyStart, null))
			{
				continue;
			}

			if (propAnalysis.TryReadProperty(ref reader, options, PropAnalysis, null))
			{
				continue;
			}

			if (propAnalyzedFields.TryReadProperty(ref reader, options, PropAnalyzedFields, null))
			{
				continue;
			}

			if (propAuthorization.TryReadProperty(ref reader, options, PropAuthorization, null))
			{
				continue;
			}

			if (propCreateTime.TryReadProperty(ref reader, options, PropCreateTime, static System.DateTimeOffset (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTimeOffset>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker))))
			{
				continue;
			}

			if (propDescription.TryReadProperty(ref reader, options, PropDescription, null))
			{
				continue;
			}

			if (propDest.TryReadProperty(ref reader, options, PropDest, null))
			{
				continue;
			}

			if (propId.TryReadProperty(ref reader, options, PropId, null))
			{
				continue;
			}

			if (propMaxNumThreads.TryReadProperty(ref reader, options, PropMaxNumThreads, null))
			{
				continue;
			}

			if (propMeta.TryReadProperty(ref reader, options, PropMeta, static System.Collections.Generic.IReadOnlyDictionary<string, object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)))
			{
				continue;
			}

			if (propModelMemoryLimit.TryReadProperty(ref reader, options, PropModelMemoryLimit, null))
			{
				continue;
			}

			if (propSource.TryReadProperty(ref reader, options, PropSource, null))
			{
				continue;
			}

			if (propVersion.TryReadProperty(ref reader, options, PropVersion, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.PutDataFrameAnalyticsResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AllowLazyStart = propAllowLazyStart.Value,
			Analysis = propAnalysis.Value,
			AnalyzedFields = propAnalyzedFields.Value,
			Authorization = propAuthorization.Value,
			CreateTime = propCreateTime.Value,
			Description = propDescription.Value,
			Dest = propDest.Value,
			Id = propId.Value,
			MaxNumThreads = propMaxNumThreads.Value,
			Meta = propMeta.Value,
			ModelMemoryLimit = propModelMemoryLimit.Value,
			Source = propSource.Value,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.PutDataFrameAnalyticsResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAllowLazyStart, value.AllowLazyStart, null, null);
		writer.WriteProperty(options, PropAnalysis, value.Analysis, null, null);
		writer.WriteProperty(options, PropAnalyzedFields, value.AnalyzedFields, null, null);
		writer.WriteProperty(options, PropAuthorization, value.Authorization, null, null);
		writer.WriteProperty(options, PropCreateTime, value.CreateTime, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTimeOffset v) => w.WriteValueEx<System.DateTimeOffset>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker)));
		writer.WriteProperty(options, PropDescription, value.Description, null, null);
		writer.WriteProperty(options, PropDest, value.Dest, null, null);
		writer.WriteProperty(options, PropId, value.Id, null, null);
		writer.WriteProperty(options, PropMaxNumThreads, value.MaxNumThreads, null, null);
		writer.WriteProperty(options, PropMeta, value.Meta, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, object>? v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteProperty(options, PropModelMemoryLimit, value.ModelMemoryLimit, null, null);
		writer.WriteProperty(options, PropSource, value.Source, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.PutDataFrameAnalyticsResponseConverter))]
public sealed partial class PutDataFrameAnalyticsResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutDataFrameAnalyticsResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PutDataFrameAnalyticsResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
		required
#endif
		bool AllowLazyStart { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysis Analysis { get; set; }
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFields? AnalyzedFields { get; set; }
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsAuthorization? Authorization { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		System.DateTimeOffset CreateTime { get; set; }
	public string? Description { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsDestination Dest { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		string Id { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		int MaxNumThreads { get; set; }
	public System.Collections.Generic.IReadOnlyDictionary<string, object>? Meta { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		string ModelMemoryLimit { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsSource Source { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		string Version { get; set; }
}