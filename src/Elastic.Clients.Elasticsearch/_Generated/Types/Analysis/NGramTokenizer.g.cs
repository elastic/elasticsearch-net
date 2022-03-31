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
namespace Elastic.Clients.Elasticsearch.Analysis
{
	public partial class NGramTokenizer : TokenizerBase, ITokenizerDefinition
	{
		[JsonInclude]
		[JsonPropertyName("custom_token_chars")]
		public string? CustomTokenChars { get; init; }

		[JsonInclude]
		[JsonPropertyName("max_gram")]
		public int MaxGram { get; init; }

		[JsonInclude]
		[JsonPropertyName("min_gram")]
		public int MinGram { get; init; }

		[JsonInclude]
		[JsonPropertyName("token_chars")]
		public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Analysis.TokenChar> TokenChars { get; init; }

		[JsonInclude]
		[JsonPropertyName("type")]
		public string Type => "ngram";
	}
}