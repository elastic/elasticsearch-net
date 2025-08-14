// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

#nullable enable

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Aggregations;

/// <summary>
/// Filters which terms to include in the response.
/// </summary>
[JsonConverter(typeof(Json.TermsIncludeConverter))]
public class TermsInclude
{
	/// <summary>
	/// Creates an instance of <see cref="TermsInclude" /> that uses a regular expression pattern
	/// to determine the terms to include in the response.
	/// </summary>
	/// <param name="regexPattern">The regular expression pattern.</param>
	public TermsInclude(string regexPattern)
	{
		if (regexPattern is null)
			throw new ArgumentNullException(nameof(regexPattern));

		RegexPattern = regexPattern;
	}

	/// <summary>
	/// Creates an instance of <see cref="TermsInclude" /> that uses a collection of terms
	/// to include in the response.
	/// </summary>
	/// <param name="values">The exact terms to include.</param>
	public TermsInclude(IEnumerable<string> values)
	{
		if (values is null)
			throw new ArgumentNullException(nameof(values));

		Values = values;
	}

	/// <summary>
	/// Creates an instance of <see cref="TermsInclude" /> that partitions the terms into a number of
	/// partitions to receive in multiple requests. Used to process many unique terms.
	/// </summary>
	/// <param name="partition">The 0-based partition number for this request.</param>
	/// <param name="numberOfPartitions">The total number of partitions.</param>
	public TermsInclude(long partition, long numberOfPartitions)
	{
		Partition = partition;
		NumberOfPartitions = numberOfPartitions;
	}

	/// <summary>
	/// The total number of partitions we are interested in.
	/// </summary>
	public long? NumberOfPartitions { get; }

	/// <summary>
	/// The current partition of terms we are interested in.
	/// </summary>
	public long? Partition { get; }

	/// <summary>
	/// The regular expression pattern to determine terms to include in the response.
	/// </summary>
	public string? RegexPattern { get; }

	/// <summary>
	/// Collection of terms to include in the response.
	/// </summary>
	public IEnumerable<string>? Values { get; }
}
