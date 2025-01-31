// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

using Elastic.Clients.Elasticsearch.Fluent;

namespace Elastic.Clients.Elasticsearch.QueryDsl;

// TODO: This should be removed after implementing descriptor generation for union types

public sealed partial class QueryDescriptor<TDocument>
{
	public QueryDescriptor<TDocument> Range(Action<RangeQueryDescriptor<TDocument>> configure) => ProxiedSet(configure, "range");

	private QueryDescriptor<TDocument> ProxiedSet<T>(Action<T> descriptorAction, string variantName) where T : ProxiedDescriptor<T>
	{
		var descriptor = (T)Activator.CreateInstance(typeof(T), true);
		descriptorAction?.Invoke(descriptor);

		return Set(descriptor.Result, variantName);
	}
}

public sealed partial class QueryDescriptor
{
	public QueryDescriptor Range(Action<RangeQueryDescriptor> configure) => ProxiedSet(configure, "range");
	public QueryDescriptor Range<TDocument>(Action<RangeQueryDescriptor<TDocument>> configure) => ProxiedSet(configure, "range");

	private QueryDescriptor ProxiedSet<T>(Action<T> descriptorAction, string variantName) where T : ProxiedDescriptor<T>
	{
		var descriptor = (T)Activator.CreateInstance(typeof(T), true);
		descriptorAction?.Invoke(descriptor);

		return Set(descriptor.Result, variantName);
	}
}

public abstract class ProxiedDescriptor<T> : Descriptor<T>
	where T : Descriptor<T>
{
	internal Descriptor Result { get; set; }

	protected T SetResult<TD>(Action<TD> descriptorAction) where TD : Descriptor
	{
		var descriptor = (TD)Activator.CreateInstance(typeof(TD), true);
		descriptorAction?.Invoke(descriptor);
		Result = descriptor;
		return Self;
	}
}

public sealed class RangeQueryDescriptor<TDocument> : ProxiedDescriptor<RangeQueryDescriptor<TDocument>>
{
	public RangeQueryDescriptor<TDocument> NumberRange(Action<NumberRangeQueryDescriptor<TDocument>> configure) =>
		SetResult(configure);

	public RangeQueryDescriptor<TDocument> DateRange(Action<DateRangeQueryDescriptor<TDocument>> configure) =>
		SetResult(configure);

	public RangeQueryDescriptor<TDocument> TermRange(Action<DateRangeQueryDescriptor<TDocument>> configure) =>
		SetResult(configure);
}

public sealed class RangeQueryDescriptor : ProxiedDescriptor<RangeQueryDescriptor>
{
	public RangeQueryDescriptor NumberRange(Action<NumberRangeQueryDescriptor> configure) => SetResult(configure);

	public RangeQueryDescriptor NumberRange<TDocument>(Action<NumberRangeQueryDescriptor<TDocument>> configure) => SetResult(configure);

	public RangeQueryDescriptor DateRange(Action<DateRangeQueryDescriptor> configure) => SetResult(configure);

	public RangeQueryDescriptor DateRange<TDocument>(Action<DateRangeQueryDescriptor<TDocument>> configure) => SetResult(configure);

	public RangeQueryDescriptor TermRange(Action<TermRangeQueryDescriptor> configure) => SetResult(configure);

	public RangeQueryDescriptor TermRange<TDocument>(Action<TermRangeQueryDescriptor<TDocument>> configure) => SetResult(configure);
}

public sealed partial class NumberRangeQuery
{
	public static implicit operator Query(NumberRangeQuery numberRangeQuery) => Query.Range(numberRangeQuery);
}

public sealed partial class DateRangeQuery
{
	public static implicit operator Query(DateRangeQuery dateRangeQuery) => Query.Range(dateRangeQuery);
}

public sealed partial class TermRangeQuery
{
	public static implicit operator Query(TermRangeQuery termRangeQuery) => Query.Range(termRangeQuery);
}
