// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch.Esql;

/// <summary>Per-query options for LINQ-to-ES|QL queries executed via WithOptions.</summary>
public sealed record EsqlQueryOptions
{
	/// <summary>Per-request transport configuration (timeouts, headers, auth).</summary>
	public IRequestConfiguration? RequestConfiguration { get; init; }

	/// <summary>If true, partial results will be returned on shard failures.</summary>
	public bool? AllowPartialResults { get; init; }

	/// <summary>If true, entirely null columns are removed from the response.</summary>
	public bool? DropNullColumns { get; init; }

	/// <summary>A Query DSL filter applied to the document set before the ES|QL query runs.</summary>
	public QueryDsl.Query? Filter { get; init; }

	/// <summary>Locale for result formatting (e.g., "en-US").</summary>
	public string? Locale { get; init; }

	/// <summary>
	/// User-supplied named parameters. Merged with parameters from the translated query.
	/// If a key exists in both, NamedParameters takes precedence.
	/// </summary>
	public Dictionary<string, FieldValue>? NamedParameters { get; init; }
}
