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

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Ml
{
	public partial class Influencer
	{
		[JsonInclude]
		[JsonPropertyName("bucket_span")]
		public long BucketSpan { get; init; }

		[JsonInclude]
		[JsonPropertyName("foo")]
		public string? Foo { get; init; }

		[JsonInclude]
		[JsonPropertyName("influencer_field_name")]
		public string InfluencerFieldName { get; init; }

		[JsonInclude]
		[JsonPropertyName("influencer_field_value")]
		public string InfluencerFieldValue { get; init; }

		[JsonInclude]
		[JsonPropertyName("influencer_score")]
		public double InfluencerScore { get; init; }

		[JsonInclude]
		[JsonPropertyName("initial_influencer_score")]
		public double InitialInfluencerScore { get; init; }

		[JsonInclude]
		[JsonPropertyName("is_interim")]
		public bool IsInterim { get; init; }

		[JsonInclude]
		[JsonPropertyName("job_id")]
		public string JobId { get; init; }

		[JsonInclude]
		[JsonPropertyName("probability")]
		public double Probability { get; init; }

		[JsonInclude]
		[JsonPropertyName("result_type")]
		public string ResultType { get; init; }

		[JsonInclude]
		[JsonPropertyName("timestamp")]
		public Elastic.Clients.Elasticsearch.Time Timestamp { get; init; }
	}
}