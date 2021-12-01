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
namespace Elastic.Clients.Elasticsearch.Aggregations
{
	public partial class StandardDeviationBoundsAsString
	{
		[JsonInclude]
		[JsonPropertyName("upper")]
		public string Upper { get; init; }

		[JsonInclude]
		[JsonPropertyName("lower")]
		public string Lower { get; init; }

		[JsonInclude]
		[JsonPropertyName("upper_population")]
		public string UpperPopulation { get; init; }

		[JsonInclude]
		[JsonPropertyName("lower_population")]
		public string LowerPopulation { get; init; }

		[JsonInclude]
		[JsonPropertyName("upper_sampling")]
		public string UpperSampling { get; init; }

		[JsonInclude]
		[JsonPropertyName("lower_sampling")]
		public string LowerSampling { get; init; }
	}
}