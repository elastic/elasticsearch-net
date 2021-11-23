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
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Analysis
{
	public partial class CustomAnalyzer : IAnalyzersVariant
	{
		[JsonInclude]
		[JsonPropertyName("type")]
		public string Type => "custom";
		[JsonInclude]
		[JsonPropertyName("char_filter")]
		public IReadOnlyCollection<string>? CharFilter { get; init; }

		[JsonInclude]
		[JsonPropertyName("filter")]
		public IReadOnlyCollection<string>? Filter { get; init; }

		[JsonInclude]
		[JsonPropertyName("position_increment_gap")]
		public int? PositionIncrementGap { get; init; }

		[JsonInclude]
		[JsonPropertyName("position_offset_gap")]
		public int? PositionOffsetGap { get; init; }

		[JsonInclude]
		[JsonPropertyName("tokenizer")]
		public string Tokenizer { get; init; }
	}
}