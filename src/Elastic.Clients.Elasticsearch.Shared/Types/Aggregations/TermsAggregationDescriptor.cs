// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Elastic.Clients.Elasticsearch.Aggregations;

public partial class TermsAggregationDescriptor<TDocument>
{
	public TermsAggregationDescriptor<TDocument> Include(long partition, long numberOfPartitions)
	{
		IncludeValue = new TermsInclude(partition, numberOfPartitions);
		return Self;
	}

	public TermsAggregationDescriptor<TDocument> Include(string includeRegexPattern)
	{
		IncludeValue = new TermsInclude(includeRegexPattern);
		return Self;
	}

	public TermsAggregationDescriptor<TDocument> Include(IEnumerable<string> values)
	{
		IncludeValue = new TermsInclude(values);
		return Self;
	}

	public TermsAggregationDescriptor<TDocument> Exclude(string excludeRegexPattern)
	{
		ExcludeValue = new TermsExclude(excludeRegexPattern);
		return Self;
	}

	public TermsAggregationDescriptor<TDocument> Exclude(IEnumerable<string> values)
	{
		ExcludeValue = new TermsExclude(values);
		return Self;
	}
}
