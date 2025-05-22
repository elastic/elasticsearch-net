// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

namespace Elastic.Clients.Elasticsearch.QueryDsl;

public readonly partial struct IRangeQueryFactory<TDocument>
{
	[Obsolete("Use 'Date()' instead.")]
	public Elastic.Clients.Elasticsearch.QueryDsl.IRangeQuery DateRange(Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQuery value)
	{
		return value;
	}

	[Obsolete("Use 'Date()' instead.")]
	public Elastic.Clients.Elasticsearch.QueryDsl.IRangeQuery DateRange(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor<TDocument>> action)
	{
		return Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor<TDocument>.Build(action);
	}

	[Obsolete("Use 'Number()' instead.")]
	public Elastic.Clients.Elasticsearch.QueryDsl.IRangeQuery NumberRange(Elastic.Clients.Elasticsearch.QueryDsl.NumberRangeQuery value)
	{
		return value;
	}

	[Obsolete("Use 'Number()' instead.")]
	public Elastic.Clients.Elasticsearch.QueryDsl.IRangeQuery NumberRange(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.NumberRangeQueryDescriptor<TDocument>> action)
	{
		return Elastic.Clients.Elasticsearch.QueryDsl.NumberRangeQueryDescriptor<TDocument>.Build(action);
	}

	[Obsolete("Use 'Term()' instead.")]
	public Elastic.Clients.Elasticsearch.QueryDsl.IRangeQuery TermRange(Elastic.Clients.Elasticsearch.QueryDsl.TermRangeQuery value)
	{
		return value;
	}

	[Obsolete("Use 'Term()' instead.")]
	public Elastic.Clients.Elasticsearch.QueryDsl.IRangeQuery TermRange(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.TermRangeQueryDescriptor<TDocument>> action)
	{
		return Elastic.Clients.Elasticsearch.QueryDsl.TermRangeQueryDescriptor<TDocument>.Build(action);
	}
}

public readonly partial struct IRangeQueryFactory
{
	[Obsolete("Use 'Date()' instead.")]
	public Elastic.Clients.Elasticsearch.QueryDsl.IRangeQuery DateRange(Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQuery value)
	{
		return value;
	}

	[Obsolete("Use 'Date()' instead.")]
	public Elastic.Clients.Elasticsearch.QueryDsl.IRangeQuery DateRange(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor> action)
	{
		return Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor.Build(action);
	}

	[Obsolete("Use 'Date()' instead.")]
	public Elastic.Clients.Elasticsearch.QueryDsl.IRangeQuery DateRange<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor<T>> action)
	{
		return Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor<T>.Build(action);
	}

	[Obsolete("Use 'Number()' instead.")]
	public Elastic.Clients.Elasticsearch.QueryDsl.IRangeQuery NumberRange(Elastic.Clients.Elasticsearch.QueryDsl.NumberRangeQuery value)
	{
		return value;
	}

	[Obsolete("Use 'Number()' instead.")]
	public Elastic.Clients.Elasticsearch.QueryDsl.IRangeQuery NumberRange(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.NumberRangeQueryDescriptor> action)
	{
		return Elastic.Clients.Elasticsearch.QueryDsl.NumberRangeQueryDescriptor.Build(action);
	}

	[Obsolete("Use 'Number()' instead.")]
	public Elastic.Clients.Elasticsearch.QueryDsl.IRangeQuery NumberRange<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.NumberRangeQueryDescriptor<T>> action)
	{
		return Elastic.Clients.Elasticsearch.QueryDsl.NumberRangeQueryDescriptor<T>.Build(action);
	}

	[Obsolete("Use 'Term()' instead.")]
	public Elastic.Clients.Elasticsearch.QueryDsl.IRangeQuery TermRange(Elastic.Clients.Elasticsearch.QueryDsl.TermRangeQuery value)
	{
		return value;
	}

	[Obsolete("Use 'Term()' instead.")]
	public Elastic.Clients.Elasticsearch.QueryDsl.IRangeQuery TermRange(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.TermRangeQueryDescriptor> action)
	{
		return Elastic.Clients.Elasticsearch.QueryDsl.TermRangeQueryDescriptor.Build(action);
	}

	[Obsolete("Use 'Term()' instead.")]
	public Elastic.Clients.Elasticsearch.QueryDsl.IRangeQuery TermRange<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.TermRangeQueryDescriptor<T>> action)
	{
		return Elastic.Clients.Elasticsearch.QueryDsl.TermRangeQueryDescriptor<T>.Build(action);
	}
}
