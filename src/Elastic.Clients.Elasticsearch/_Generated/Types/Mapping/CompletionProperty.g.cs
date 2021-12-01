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
namespace Elastic.Clients.Elasticsearch.Mapping
{
	public partial class CompletionProperty : Mapping.DocValuesPropertyBase
	{
		[JsonInclude]
		[JsonPropertyName("analyzer")]
		public string? Analyzer { get; init; }

		[JsonInclude]
		[JsonPropertyName("contexts")]
		public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Mapping.SuggestContext>? Contexts { get; init; }

		[JsonInclude]
		[JsonPropertyName("max_input_length")]
		public int? MaxInputLength { get; init; }

		[JsonInclude]
		[JsonPropertyName("preserve_position_increments")]
		public bool? PreservePositionIncrements { get; init; }

		[JsonInclude]
		[JsonPropertyName("preserve_separators")]
		public bool? PreserveSeparators { get; init; }

		[JsonInclude]
		[JsonPropertyName("search_analyzer")]
		public string? SearchAnalyzer { get; init; }

		[JsonInclude]
		[JsonPropertyName("type")]
		public string Type => "completion";
	}
}