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
namespace Elastic.Clients.Elasticsearch.Xpack
{
	public partial class EqlFeaturesJoin
	{
		[JsonInclude]
		[JsonPropertyName("join_queries_five_or_more")]
		public int JoinQueriesFiveOrMore { get; init; }

		[JsonInclude]
		[JsonPropertyName("join_queries_four")]
		public int JoinQueriesFour { get; init; }

		[JsonInclude]
		[JsonPropertyName("join_queries_three")]
		public int JoinQueriesThree { get; init; }

		[JsonInclude]
		[JsonPropertyName("join_queries_two")]
		public int JoinQueriesTwo { get; init; }

		[JsonInclude]
		[JsonPropertyName("join_until")]
		public int JoinUntil { get; init; }
	}
}