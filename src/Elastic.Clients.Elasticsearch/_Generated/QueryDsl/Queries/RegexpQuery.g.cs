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
using Elastic.Transport;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.QueryDsl
{
	[ConvertAs(typeof(RegexpQuery))]
	public partial interface IRegexpQuery : IFieldNameQuery
	{
		bool? CaseInsensitive { get; set; }

		string? Flags { get; set; }

		int? MaxDeterminizedStates { get; set; }

		Elastic.Clients.Elasticsearch.MultiTermQueryRewrite? Rewrite { get; set; }

		string Value { get; set; }
	}

	public partial class RegexpQuery : FieldNameQueryBase, IRegexpQuery
	{
		[JsonInclude]
		[JsonPropertyName("case_insensitive")]
		public bool? CaseInsensitive { get; set; }

		[JsonInclude]
		[JsonPropertyName("flags")]
		public string? Flags { get; set; }

		[JsonInclude]
		[JsonPropertyName("max_determinized_states")]
		public int? MaxDeterminizedStates { get; set; }

		[JsonInclude]
		[JsonPropertyName("rewrite")]
		public Elastic.Clients.Elasticsearch.MultiTermQueryRewrite? Rewrite { get; set; }

		[JsonInclude]
		[JsonPropertyName("value")]
		public string Value { get; set; }
	}

	public partial class RegexpQueryDescriptor : IRegexpQuery
	{
		bool? IRegexpQuery.CaseInsensitive { get; set; }

		string? IRegexpQuery.Flags { get; set; }

		int? IRegexpQuery.MaxDeterminizedStates { get; set; }

		Elastic.Clients.Elasticsearch.MultiTermQueryRewrite? IRegexpQuery.Rewrite { get; set; }

		string IRegexpQuery.Value { get; set; }
	}
}